package main

import (
	"bufio"
	"fmt"
	"net"
	"os"

	"github.com/Henry-Sarabia/igdb"
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
			psw,_:=bufio.NewReader(conn).ReadString('\n')
			done:= register(usr,psw) 
			if done {
				buffer=[]byte("1")
				} else {
					buffer=[]byte("0")
				}
				conn.Write(buffer) //gestire err?
		case "LOGIN ":
			usr,_:=bufio.NewReader(conn).ReadString(' ')
			psw,_:=bufio.NewReader(conn).ReadString('\n')
			done=login(usr,psw,&user) 
			if done {
				buffer=[]byte("1")
				} else {
					buffer=[]byte("0")
				}
				conn.Write(buffer) //devo mandare i dati user anche al client?
		case "ADDGAME ":
			name:=bufio.NewReader(conn).ReadString('\n')
			//ricerca gioco tramite igdb API
			igdbClient:= igdb.NewClient(id, token, nil)
			gameList,err:=igdbClient.Search(name, igdb.SetLimit(30), igdb.SetFields("name"))
			for _,v:= range gameList {
				buffer = []byte(v.Name)
				conn.Write(buffer)
			}
			conn.Write([]byte("*")) //indica la fine della lista giochi
			gameName,_:=bufio.NewReader(conn).ReadString('\n')
			//aggiungere in gameList di user
			utente.setGameList(gameName)
			done:= addGame(gameName, utente.userID) 
			if done {
				buffer=[]byte("1")
			} else {
				buffer=[]byte("0")
			}
		case "ADDFRIEND ":
			gameName,_:=bufio.NewReader(conn).ReadString('\n')
			userMap:= findUser(gameName)
			//decidere se fare mutua amicizia con richiesta o solo il segui
			delete(userMap, utente.userID)			
			//invia lista utenti
			for _,uName:= range userMap {
				buffer = []byte(uName)
				conn.Write(buffer)
			}
			conn.Write([]byte("*"))
			//riceve nick friend
			friend,_:= bufio.NewReader(conn).ReadString('\n')
			var done bool
			for uID:= range userMap {
				if userMap[uID]==friend {
					done = addFriend(utente.userID, uID)
					return
				}
			}
			if done {
				buffer=[]byte("1")
			} else {
				buffer=[]byte("0")
			}
		default:
			fmt.Println(request)
		}
	

	}

}
