package main

import (
	"bufio"
	"encoding/json"
	"fmt"
	"net"
	"os"
	"strings"

	"github.com/Henry-Sarabia/igdb/v2"
)

const (
	ServerType = "tcp"
	ServerPort = ":8000"
	id = "79qp0roupexv53hnpss3x1wwdytgq5"
	token = "b1sz8clgm6864g2d0kl5kc4uoz0l0r"
)
//serve una map dove salvare client-connessione, (lista giochi e utenti?)
var clients = make(map[int]net.Conn)

func main() {
	fmt.Println("Avvio del server...")

	server, err := net.Listen(ServerType,ServerPort)
	if err!=nil {
		fmt.Println("Errore creazione server")
		os.Exit(1)
	}

	defer server.Close()

	
	for {
		connection, err := server.Accept()
		if err!=nil {
			fmt.Println("Errore di connessione")
			return
		}
		
		go handleClient(connection)
		
	}
}

func handleClient(conn net.Conn) {
	defer conn.Close()
	
	var utente user
	
	buffer:= make([]byte,1024)
	
	for {
		request,_:=bufio.NewReader(conn).ReadString(' ')//provare readline
		switch request { 
		case "REGISTER ":
			usr,_:=bufio.NewReader(conn).ReadString(' ')
			usr = strings.ReplaceAll(usr, " ", "") //per togliere lo spazio dopo il nome
			psw,_:=bufio.NewReader(conn).ReadString('\n')
			psw = strings.ReplaceAll(psw, "\n", "")
			done:= register(usr,psw) 
			if done {
				buffer=[]byte("1")
				} else {
					buffer=[]byte("0")
				}
			conn.Write(buffer) //gestire err?

		case "LOGIN ":
			usr,_:=bufio.NewReader(conn).ReadString(' ')
			usr = strings.ReplaceAll(usr, " ", "")
			psw,_:=bufio.NewReader(conn).ReadString('\n')
			psw = strings.ReplaceAll(psw, "\n", "")

			done:=login(usr,psw,&utente)
			if done {
				buffer=[]byte("1")
				conn.Write(buffer) //devo mandare i dati utente anche al client? SI
				jsonUtente,_ := json.Marshal(utente)
				conn.Write(jsonUtente)
				} else {
					buffer=[]byte("0")
					conn.Write(buffer)
				}
			//encoder:= json.NewEncoder(conn)
			//encoder.Encode(utente)

		case "ADDGAME ":
			name,_:=bufio.NewReader(conn).ReadString('\n')
			name = strings.ReplaceAll(name, "\n", "")
			//ricerca gioco tramite igdb API
			igdbClient:= igdb.NewClient(id, token, nil)
			gameList,err:= igdbClient.Search(name, igdb.SetLimit(30), igdb.SetFields("name"))
			for _,v:= range gameList {
				buffer = []byte(v.Name)
				conn.Write(buffer)
			}
			conn.Write([]byte("*")) //indica la fine della lista giochi
			gameName,_:=bufio.NewReader(conn).ReadString('\n')
			gameName = strings.ReplaceAll(gameName, "\n", "")
			//se salva correttamente nel db lo aggiunge in gameList di utente
			done:= addGame(gameName, utente.UserID) 
			if done {
				utente.setGameList(gameName)
				buffer=[]byte("1")
			} else {
				buffer=[]byte("0")
			}
			conn.Write(buffer)

		case "ADDFRIEND ":
			gameName,_:=bufio.NewReader(conn).ReadString('\n')
			gameName = strings.ReplaceAll(gameName, "\n", "")
			userMap:= findUser(gameName)
			//decidere se fare mutua amicizia con richiesta o solo il segui
			delete(userMap, utente.UserID)			
			//invia lista utenti
			for _,uName:= range userMap {
				buffer = []byte(uName)
				conn.Write(buffer)
			}
			conn.Write([]byte("*"))
			
			//riceve nick friend
			friend,_:= bufio.NewReader(conn).ReadString('\n')
			friend = strings.ReplaceAll(friend, "\n", "")
			if friend=="ABORT" {
				break
			}
			var done bool
			for uID:= range userMap {
				if userMap[uID]==friend {
					done = followUser(utente.UserID, uID)
					return
				}
			}
			if done {
				utente.setFollowingList(friend)
				buffer=[]byte("1")
			} else {
				buffer=[]byte("0")
			}
			conn.Write(buffer)

		default:
			fmt.Println(request)
		}
	

	}

}
