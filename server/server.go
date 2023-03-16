package main

import (
	"bufio"
	"encoding/json"
	"fmt"
	"net"
	"os"
	"strconv"
	"strings"

	"github.com/Henry-Sarabia/igdb/v2"
)

const (
	ServerType = "tcp"
	ServerPort = ":8000"
	id         = "79qp0roupexv53hnpss3x1wwdytgq5"
	token      = "b1sz8clgm6864g2d0kl5kc4uoz0l0r"
)

// serve una map dove salvare client-connessione, (lista giochi e utenti?)
var clients = make(map[int]net.Conn)

func main() {
	fmt.Println("Avvio del server...")

	server, err := net.Listen(ServerType, ServerPort) //crea un socket sulla porta specificata e si mette in ascolto per eventuali connessioni in ingresso
	//gestione degli errori in Go: se si verifica un errore (ad esempio la porta è già in uso) la funzione restituisce un errore (non nullo)
	//accetta due parametri: il primo è il tipo protocollo in questo caso è TCP e l'indirizzo su cui il servere deve mettersi in ascolto
	if err != nil {
		fmt.Println("Errore creazione server")
		os.Exit(1)
	}

	defer server.Close()

	//accept viene usata per accettare nuove connessioni in entrata sulla porta del server.
	//se la connessione è accettata con successo viene restituita un'istanza della connessione (net.Conn) che mi rappresenta la connessione appena stabilita tra il server e il vlient
	//in questo caso viene fatto dentro ad un loop che viene eseguito solo una volta, per accettare quindi una singola connessione

	for {

		connection, err := server.Accept()
		if err != nil {
			fmt.Println("Errore di connessione")
			return
		}

		go handleClient(connection)

	}
}

// chiamata ogni volta che un client si connette al server
func handleClient(conn net.Conn) {
	defer conn.Close()
	//appena il client termina la comunicazione, la connesione viene chiusa

	var utente user

	buffer := make([]byte, 1024)
	var reader = bufio.NewReader(conn)

	for {
		//cicla continuamente finchè il clienti rimane connesso
		//fmt.Println("Attesa del client...")

		//legge il messaggio del client
		request, _ := reader.ReadString(' ') //provare readline
		//switch per gestire le varie richieste del client
		switch request {
		case "REGISTER ":
			usr, _ := bufio.NewReader(conn).ReadString(' ')
			usr = strings.ReplaceAll(usr, " ", "") //per togliere lo spazio dopo il nome
			psw, _ := bufio.NewReader(conn).ReadString('\n')
			psw = strings.ReplaceAll(psw, "\n", "")
			done := register(usr, psw)
			if done {
				buffer = []byte("1")
			} else {
				buffer = []byte("0")
			}
			conn.Write(buffer) //gestire err?

		case "LOGIN ":
			usr, _ := bufio.NewReader(conn).ReadString(' ')
			usr = strings.ReplaceAll(usr, " ", "")
			psw, _ := bufio.NewReader(conn).ReadString('\n')
			psw = strings.ReplaceAll(psw, "\n", "")

			done := login(usr, psw, &utente)
			if done {
				buffer = []byte("1")
				conn.Write(buffer) //devo mandare i dati utente anche al client? SI
				jsonUtente, _ := json.Marshal(utente)
				conn.Write(jsonUtente)
			} else {
				buffer = []byte("0")
				conn.Write(buffer)
			}
			//encoder:= json.NewEncoder(conn)
			//encoder.Encode(utente)

		case "ADDGAME ":
			name, _ := bufio.NewReader(conn).ReadString('\n')
			name = strings.ReplaceAll(name, "\n", "")
			//ricerca gioco tramite igdb API
			igdbClient := igdb.NewClient(id, token, nil)
			gameList, _ := igdbClient.Search(name, igdb.SetLimit(30), igdb.SetFields("name"))
			for _, v := range gameList {
				buffer = []byte(v.Name)
				conn.Write(buffer)
			}
			conn.Write([]byte("*")) //indica la fine della lista giochi

			gameName, _ := bufio.NewReader(conn).ReadString('\n')
			gameName = strings.ReplaceAll(gameName, "\n", "")
			if gameName == "ABORT" {
				break
			}
			//se salva correttamente nel db lo aggiunge in gameList di utente
			done := addGame(gameName, utente.UserID)
			if done {
				utente.setGameList(gameName)
				buffer = []byte("1")
			} else {
				buffer = []byte("0")
			}
			conn.Write(buffer)

		case "FOLLOW ":
			gameName, _ := bufio.NewReader(conn).ReadString('\n')
			gameName = strings.ReplaceAll(gameName, "\n", "")
			userMap, _ := findUser(gameName)
			//decidere se fare mutua amicizia con richiesta o solo il segui
			delete(userMap, utente.UserID)
			//invia lista utenti
			for _, uName := range userMap {
				buffer = []byte(uName)
				conn.Write(buffer)
			}
			conn.Write([]byte("*"))

			//riceve nick dell'utente da aggiungere
			userName, _ := bufio.NewReader(conn).ReadString('\n')
			userName = strings.ReplaceAll(userName, "\n", "")
			if userName == "ABORT" {
				break
			}
			var done bool
			for uID := range userMap {
				if userMap[uID] == userName {
					done = followUser(utente.UserID, uID)
					return
				}
			}
			if done {
				utente.setFollowingList(userName)
				buffer = []byte("1")
			} else {
				buffer = []byte("0")
			}
			conn.Write(buffer)

		//prima lettura avviene prima dello switch
		case "PYUTENTI":

			/*type user1 struct {
				UserID        int            `json:"UserID"`
				Nick          string         `json:"Nick"`
				GameList      []string       `json:"GameList"`
				FollowingList map[int]string `json:"FollowingList"`
			}

			u := user1{
				UserID:        1,
				Nick:          "fede",
				GameList:      []string{},
				FollowingList: make(map[int]string),
			}

			u.FollowingList[2] = "gio"
			u.FollowingList[3] = "mattia"

			fmt.Println(u)*/

			id_utente, _ := reader.ReadString('\n')

			id, _ := strconv.Atoi(id_utente)
			done := getInfoUtente(id, &utente)

			if done {
				infoUtente, _ := json.Marshal(utente)
				conn.Write(infoUtente)
			} else {
				conn.Write([]byte("*"))
			}

			//conn.Write([]byte(utente.Nick))

			//PROVE
			/*
				if done {
					jsonUtentiPy, _ := json.Marshal(utentiMap)
					conn.Write(jsonUtentiPy)
				} else {
					conn.Write([]byte("*")) //----> cliente: response controllo se ricevo l'asterisco
				}*/

			//perchè la write non è bloccante e quindi non aspetta prima di riscrevere questa cosa

			reader.ReadByte()

			/*seguitiMap, done := allFollowing(utente.UserID)
			if done {
				jsonSeguitiPy, _ := json.Marshal(seguitiMap)
				conn.Write([]byte(jsonSeguitiPy))
			} else {
				conn.Write([]byte("*"))
			}

			reader.ReadByte()*/

			//QUI DOVREI MANDARE LA LISTA DI TUTTI I SEGUITI DELL'UTENTE LOGGATO

			relazioni, done := allRelation(1)
			fmt.Println(relazioni) //funziona

			if done {
				jsonSeguiti2Py, _ := json.Marshal(relazioni)
				conn.Write(jsonSeguiti2Py)
			} else {
				conn.Write([]byte("*"))
			}

		case "PYPESO":
			fmt.Println("sono qui")
			structConversazione, done := getConversazione()
			fmt.Println(structConversazione)
			fmt.Println("sono qui2")

			if done {
				jsonConversazione, _ := json.Marshal(structConversazione)
				conn.Write(jsonConversazione)
			} else {
				conn.Write([]byte("*"))
			}

		default:
			//fmt.Println(request)

		}

	}

}
