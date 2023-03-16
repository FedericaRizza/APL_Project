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

		fmt.Println("Routine principale in attesa di nuovi client")
		connection, err := server.Accept()
		if err != nil {
			fmt.Println("Errore di connessione")
			return
		}

		go handleClient(connection)

	}
	fmt.Println("Processo principale in chiusura, oh no!")
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
		//request,_:=bufio.NewReader(conn).ReadString(' ')//provare readline
		request, _ := reader.ReadString('\n')
		fmt.Println(request)
		request = strings.ReplaceAll(request, "\n", "")
		//switch per gestire le varie richieste del client
		switch request {
		case "REGISTER":
			usr, _ := reader.ReadString(' ')
			usr = strings.ReplaceAll(usr, " ", "") //per togliere lo spazio dopo il nome
			psw, _ := reader.ReadString('\n')
			psw = strings.ReplaceAll(psw, "\n", "")
			fmt.Println("sono in register, password: ", psw)
			done := register(usr, psw)
			if done {
				buffer = []byte("1")
			} else {
				buffer = []byte("0")
			}
			conn.Write(buffer) //gestire err?

		case "LOGIN":
			usr, _ := reader.ReadString(' ')
			usr = strings.ReplaceAll(usr, " ", "")

			psw, _ := reader.ReadString('\n')
			psw = strings.ReplaceAll(psw, "\n", "")

			//svuoto utente per ogni login utente
			utente = newUser()
			//qui si deve salvare nella mappa utenti-connessioni i dati relativi al client che si collega
			done := login(usr, psw, &utente)
			if done {
				buffer = []byte("1")
				conn.Write(buffer)
				//invio i dati utente al client sotto forma di json
				jsonUtente, _ := json.Marshal(utente)
				conn.Write(jsonUtente)
			} else {
				buffer = []byte("0")
				conn.Write(buffer)
			}

		case "ADDGAME":
			fmt.Println("Sono in addgame")
			name, _ := reader.ReadString('\n')
			fmt.Println(name)
			name = strings.ReplaceAll(name, "\n", "")
			fmt.Println("cerco il gioco con: ", name)
			//ricerca gioco tramite igdb API
			igdbClient := igdb.NewClient(id, token, nil)
			gameList, _ := igdbClient.Search(name, igdb.SetLimit(30), igdb.SetFields("name")) //rimettere err
			for _, v := range gameList {
				fmt.Println("trovato: ", v.Name)
				buffer = []byte(v.Name)
				conn.Write(buffer)
				reader.ReadByte()

			}
			conn.Write([]byte("*")) //indica la fine della lista giochi

			gameName, _ := reader.ReadString('\n')
			fmt.Println("leggo il nome gioco: ", gameName)
			gameName = strings.ReplaceAll(gameName, "\n", "")
			//se anzichè inserire il gioco faccio annulla, arriva il comando di ABORT
			if gameName == "ABORT" {
				break
			}
			fmt.Println("dopo il break abort ", gameName)
			//se anzichè inserire un gioco faccio una nuova ricerca??? ---> modificare il form con due panel
			if gameName == "ADDGAME" {
				//goto label?
			}
			fmt.Println("dopo Addname ", gameName)
			//se salva correttamente nel db lo aggiunge in gameList di utente
			done := addGame(gameName, utente.UserID)
			if done {
				utente.setGameList(gameName)
				buffer = []byte("1")
			} else {
				buffer = []byte("0")
			}
			conn.Write(buffer)

		case "FOLLOW":
			fmt.Println("sono in follow")
			gameName, _ := reader.ReadString('\n')
			gameName = strings.ReplaceAll(gameName, "\n", "")
			fmt.Println("gioco ", gameName)
			userMap, _ := findUser(gameName) //rimettere err
			//tolgo dalla mappa utenti che hanno il gioco l'utente relativo al client e i suoi seguiti
			delete(userMap, utente.UserID)
			for userId := range utente.FollowingList {
				delete(userMap, userId)
			}
			//invia lista utenti
			for _, uName := range userMap {
				fmt.Println("dentro map ", uName)
				buffer = []byte(uName)
				conn.Write(buffer)
				reader.ReadByte()
			}
			conn.Write([]byte("*"))

			//riceve nick dell'utente da aggiungere
			followingName, _ := reader.ReadString('\n')
			fmt.Println("--", followingName, "--")
			followingName = strings.ReplaceAll(followingName, "\n", "")
			fmt.Println("nick dell utente--", followingName, "--")
			if followingName == "ABORT" {
				fmt.Println("dentro abort")
				break
			}
			fmt.Println("fuori abort")
			var done bool
			var followingID int
			//for sbagliato, in userName c è il nome del gioco, controllare
			for uID := range userMap {
				fmt.Println("dentro ricerca mappa")
				fmt.Println(uID, userMap[uID])
				if userMap[uID] == followingName {
					fmt.Println("utente trovato, lo aggiungo")
					done = followUser(utente.UserID, uID)
					fmt.Println(done)
					followingID = uID
					break
				}
			}
			fmt.Println("prima di done")

			//se l'inserimento è andato a buon fine torno l'id del following
			if done {
				utente.setFollowingList(followingID, followingName)
				fmt.Println("done si")
				buffer = []byte(strconv.Itoa(followingID))
			} else {
				fmt.Println("done no")
				buffer = []byte("error")
			}
			fmt.Println("prima di write")
			conn.Write(buffer)
			fmt.Println("dopo write")

		case "ABORT":
			fmt.Println("operazione abort")

		case "CLOSE":
			//cancellare la connessione dalla mappa
			fmt.Println("Dissconnessione del client in corso...")
			conn.Close()
			return

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
			continue
			//fmt.Println(request)

		}

	}

}
