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

var (
	logChan chan userData
	reqChan chan reqData
	bye     chan int
)

func main() {
	fmt.Println("Avvio del server...")

	server, err := net.Listen(ServerType, ServerPort) //crea un socket sulla porta specificata e si mette in ascolto per eventuali connessioni in ingresso
	if err != nil {
		fmt.Println("Errore creazione server")
		os.Exit(1)
	}

	defer server.Close()

	logChan = make(chan userData)
	reqChan = make(chan reqData)
	bye = make(chan int)

	go func() {
		//mappa che contiene per ogni utente connesso l'id e la sua connessione col client
		var clients = make(map[int]net.Conn)

		for {
			select {
			//quando un client logga invia al login channel il suo id e la sua connessione con il client
			case user := <-logChan:
				clients[user.id] = user.conn
			// in reqChan un client richiede la connessione per comunicare con un altro client: invia l'id dell utente che vuole
			//contattare e il canale dove ricevere i dati richiesti (se presenti)
			case req := <-reqChan:
				req.replyCh <- userData{req.id, clients[req.id]} //se l'utente non è connesso il canale è nil
			//quando un client fa logout lo comunica al connectionHandler, che toglie i suoi dati dalla mappa utenti connessi
			case id := <-bye:
				delete(clients, id)
			}
		}
	}()

	for {

		fmt.Println("Server in attesa di nuovi client")
		connection, err := server.Accept()
		if err != nil {
			return
		}

		go handleClient(connection)

	}
}

func handleClient(conn net.Conn) {
	defer conn.Close()

	var utente user

	buffer := make([]byte, 512)
	var reader = bufio.NewReader(conn)

	for {
		request, _ := reader.ReadString('\n')

		request = strings.ReplaceAll(request, "\n", "")

		switch request {
		case "REGISTER":
			usr, _ := reader.ReadString(' ')
			usr = strings.ReplaceAll(usr, " ", "") //per togliere lo spazio dopo il nome
			psw, _ := reader.ReadString('\n')
			psw = strings.ReplaceAll(psw, "\n", "")
			done := register(usr, psw)
			if done {
				buffer = []byte("1")
			} else {
				buffer = []byte("0")
			}
			conn.Write(buffer)

		case "LOGIN":
			usr, _ := reader.ReadString(' ')
			usr = strings.ReplaceAll(usr, " ", "")

			psw, _ := reader.ReadString('\n')
			psw = strings.ReplaceAll(psw, "\n", "")

			utente = newUser()
			done := login(usr, psw, &utente)
			if done {
				buffer = []byte("1")
				conn.Write(buffer)
				//avvenuta la connessione aggiungo id e conn alla mappa utenti connessi
				logChan <- userData{utente.UserID, conn}

				jsonUtente, _ := json.Marshal(utente)
				conn.Write(jsonUtente)
			} else {
				buffer = []byte("0")
				conn.Write(buffer)
			}

		case "ADDGAME":
			name, _ := reader.ReadString('\n')
			name = strings.ReplaceAll(name, "\n", "")
			igdbClient := igdb.NewClient(id, token, nil)
			gameList, _ := igdbClient.Search(name, igdb.SetLimit(30), igdb.SetFields("name"))
			for _, v := range gameList {
				buffer = []byte(v.Name)
				conn.Write(buffer)
				reader.ReadByte()

			}
			conn.Write([]byte("*"))

			gameName, _ := reader.ReadString('\n')
			gameName = strings.ReplaceAll(gameName, "\n", "")
			//se anzichè inserire il gioco faccio annulla, arriva il comando di ABORT
			if gameName == "ABORT" {
				break
			}

			done := addGame(gameName, utente.UserID)
			if done {
				utente.setGameList(gameName)
				buffer = []byte("1")
			} else {
				buffer = []byte("0")
			}
			conn.Write(buffer)

		case "FOLLOW":

			gameName, _ := reader.ReadString('\n')
			gameName = strings.ReplaceAll(gameName, "\n", "")
			userMap, empty := findUser(gameName)

			//tolgo dalla mappa utenti che hanno il gioco l'utente relativo al client e i suoi seguiti
			if !empty {
				delete(userMap, utente.UserID)
				for userId := range utente.FollowingList {
					delete(userMap, userId)
				}
			}

			//invia lista utenti
			for _, uName := range userMap {
				buffer = []byte(uName)
				conn.Write(buffer)
				reader.ReadByte()
			}
			conn.Write([]byte("*"))

			//riceve nick dell'utente da aggiungere
			followingName, _ := reader.ReadString('\n')
			followingName = strings.ReplaceAll(followingName, "\n", "")
			if followingName == "ABORT" {
				break
			}
			var done bool
			var followingID int
			for uID := range userMap {
				if userMap[uID] == followingName {
					done = followUser(utente.UserID, uID)
					followingID = uID
					break
				}
			}

			//se l'inserimento è andato a buon fine torno l'id del following
			if done {
				utente.setFollowingList(followingID, followingName)
				buffer = []byte(strconv.Itoa(followingID))
			} else {
				buffer = []byte("error")
			}
			conn.Write(buffer)

		case "CHAT":
			//ricevo il nome dell'utente con cui si vuole chattare, e si recupera la conversazione
			receiver, _ := reader.ReadString('\n')
			receiver = strings.ReplaceAll(receiver, "\n", "")
			var recID int
			for k, v := range utente.FollowingList {
				if v == receiver {
					recID = k
					break
				}
			}
			conversation := getChat(utente.UserID, recID)
			for _, msg := range conversation {
				jsonMsg, _ := json.Marshal(msg)
				conn.Write(jsonMsg)
				reader.ReadByte()
			}
			conn.Write([]byte("*")) //end of msg list

		case "SEND":
			//ricevo il nome dell'utente con cui si sta chattando e il messaggio da inviargli
			receiver, _ := reader.ReadString(' ')
			receiver = strings.ReplaceAll(receiver, " ", "")
			textMsg, _ := reader.ReadString('\n')
			textMsg = strings.ReplaceAll(textMsg, "\n", "")

			var receiverID int
			for k, v := range utente.FollowingList {
				if v == receiver {
					receiverID = k
					break
				}
			}

			done := saveMsg(utente.UserID, receiverID, textMsg)

			if done {

				buffer = []byte("1")
			} else {
				buffer = []byte("0")
			}
			conn.Write(buffer)

			//se l utente è in mappa connessioni inviare il messaggio anche al suo client
			reply := make(chan userData)
			reqChan <- reqData{receiverID, reply}
			receiverData := <-reply

			if receiverData.conn != nil {
				buffer = []byte("!")
				msg := msgData{utente.UserID, receiverID, textMsg}
				jsonMsg, _ := json.Marshal(msg)
				buffer = append(buffer, jsonMsg...)
				receiverData.conn.Write(buffer)
			}

		case "ABORT":
			fmt.Println("operazione abort")

		case "LOGOUT":
			bye <- utente.UserID

		case "CLOSE":
			fmt.Println("Dissconnessione del client in corso...")
			bye <- utente.UserID
			conn.Close()
			return

		case "PYUTENTI":
			utente = newUser()
			id_utente, _ := reader.ReadString('\n')
			id_utente = strings.ReplaceAll(id_utente, "\n", "")

			id, _ := strconv.Atoi(id_utente)
			done := getInfoUtente(id, &utente)

			if done {
				infoUtente, _ := json.Marshal(utente)
				conn.Write(infoUtente)
			} else {
				conn.Write([]byte("*"))
			}

			reader.ReadByte()

			//QUI MANDO LA LISTA DI TUTTI I SEGUITI DELL'UTENTE LOGGATO
			relazioni, done := allRelation(id)
			if done {
				jsonSeguiti2Py, _ := json.Marshal(relazioni)
				conn.Write(jsonSeguiti2Py)
			} else {
				conn.Write([]byte("*"))
			}

		case "PYPESO":
			listConversazione, done := getConversazione()
			if done {
				jsonConversazione, _ := json.Marshal(listConversazione)
				conn.Write(jsonConversazione)
			} else {
				conn.Write([]byte("*"))
			}

		default:
			continue

		}
	}

}
