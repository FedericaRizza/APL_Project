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

<<<<<<< HEAD
var (
	logChan chan userData
	reqChan chan reqData
	bye chan int
)

type userData struct {
	id int
	conn net.Conn
}

type reqData struct {
	id int
	replyCh chan userData
}

type msgData struct {
	Sender int `json:"Sender"`
	Receiver int `json:"Receiver"`
	Text string `json:"Text"`
}
=======
// serve una map dove salvare client-connessione, (lista giochi e utenti?)
var clients = make(map[int]net.Conn)
>>>>>>> 8de70c3177ea47504279c201cfb0cac5a989e8c5

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

	//avvio una goroutine connectionHandler per la gestione della mappa connessioni di tutti i client connessi, solo questa potrà modificarla
	//mentre le altre goroutine faranno richieste get/set sulla mappa tramite canali appositi
	
	logChan = make(chan userData)
	reqChan = make(chan reqData)
	bye = make(chan int)

	go func() {
		//mappa che contiene per ogni utente connesso l'id e la sua connessione col client
		var clients = make(map[int]net.Conn)
		
		for {
			select {
			//quando un client logga invia al login channel il suo id e il suo chatHandler channel
			case user:= <-logChan:
				clients[user.id] = user.conn
			// in reqChan un client richiede la connessione per comunicare con un altro client: invia l'id dell utente che vuole
			//contattare e il canale dove ricevere i dati richiesti (se presenti)
			case req:= <-reqChan:
				req.replyCh<- userData{req.id, clients[req.id]} //se l'utente non è connesso il canale è nil
			//quando un client fa logout lo comunica al connectionHandler, che toglie i suoi dati dalla mappa utenti connessi
			case id:= <-bye:
				delete(clients, id)
			}
		}
	}()

	//accept viene usata per accettare nuove connessioni in entrata sulla porta del server.
	//se la connessione è accettata con successo viene restituita un'istanza della connessione (net.Conn) che mi rappresenta la connessione appena stabilita tra il server e il vlient
	//in questo caso viene fatto dentro ad un loop che viene eseguito solo una volta, per accettare quindi una singola connessione

	for {

		fmt.Println("Server in attesa di nuovi client")
		connection, err := server.Accept()
		if err != nil {
			return
		}

		go handleClient(connection)
<<<<<<< HEAD
		
	}	
=======

	}
	fmt.Println("Processo principale in chiusura, oh no!")
>>>>>>> 8de70c3177ea47504279c201cfb0cac5a989e8c5
}

// chiamata ogni volta che un client si connette al server
func handleClient(conn net.Conn){
	defer conn.Close()
	//appena il client termina la comunicazione, la connesione viene chiusa

	var utente user
<<<<<<< HEAD
	//var chatHandlerChan chan userData
	//var quit chan bool
	
	buffer:= make([]byte,512)
=======

	buffer := make([]byte, 1024)
>>>>>>> 8de70c3177ea47504279c201cfb0cac5a989e8c5
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
			
			//inizializzo un utente vuoto al login
			utente = newUser()
			//qui si deve salvare nella mappa utenti-connessioni i dati relativi al client che si collega
			done := login(usr, psw, &utente)
			if done {
				buffer = []byte("1")
				conn.Write(buffer)
				//avvenuta la connessione aggiungo id e conn alla mappa utenti connessi
				logChan <- userData{utente.UserID, conn}
				/*
				chatHandlerChan= make(chan userData)
				go handleChatRequest(chatHandlerChan, quit) //devo passargli il canale?*/
				//invio i dati utente al client sotto forma di json
				jsonUtente, _ := json.Marshal(utente)
				conn.Write(jsonUtente)
			} else {
				buffer = []byte("0")
				conn.Write(buffer)
			}

		case "ADDGAME":
<<<<<<< HEAD
			//fmt.Println("Sono in addgame")
			name,_:=reader.ReadString('\n')
			//fmt.Println(name)
			name = strings.ReplaceAll(name, "\n", "")
			//fmt.Println("cerco il gioco con: ",name)
			//ricerca gioco tramite igdb API
			igdbClient:= igdb.NewClient(id, token, nil)
			gameList,_:= igdbClient.Search(name, igdb.SetLimit(30), igdb.SetFields("name")) //rimettere err
			for _,v:= range gameList {
				//fmt.Println("trovato: ",v.Name)
=======
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
>>>>>>> 8de70c3177ea47504279c201cfb0cac5a989e8c5
				buffer = []byte(v.Name)
				conn.Write(buffer)
				reader.ReadByte()

			}
			conn.Write([]byte("*")) //indica la fine della lista giochi

<<<<<<< HEAD
			gameName,_:=reader.ReadString('\n')
			//fmt.Println("leggo il nome gioco: ", gameName)
=======
			gameName, _ := reader.ReadString('\n')
			fmt.Println("leggo il nome gioco: ", gameName)
>>>>>>> 8de70c3177ea47504279c201cfb0cac5a989e8c5
			gameName = strings.ReplaceAll(gameName, "\n", "")
			//se anzichè inserire il gioco faccio annulla, arriva il comando di ABORT
			if gameName == "ABORT" {
				break				
			}
<<<<<<< HEAD
			//fmt.Println("dopo il break abort ",gameName)
			
			//fmt.Println("dopo Addname ", gameName)
=======
			fmt.Println("dopo il break abort ", gameName)
			//se anzichè inserire un gioco faccio una nuova ricerca??? ---> modificare il form con due panel
			if gameName == "ADDGAME" {
				//goto label?
			}
			fmt.Println("dopo Addname ", gameName)
>>>>>>> 8de70c3177ea47504279c201cfb0cac5a989e8c5
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
<<<<<<< HEAD
			//fmt.Println("sono in follow")
			gameName,_:=reader.ReadString('\n')
			gameName = strings.ReplaceAll(gameName, "\n", "")
			//fmt.Println("gioco ", gameName)
			userMap, empty:= findUser(gameName) //rimettere err
			//tolgo dalla mappa utenti che hanno il gioco l'utente relativo al client e i suoi seguiti
			if(!empty) {				
				delete(userMap, utente.UserID)	
				for userId:= range utente.FollowingList {
					delete(userMap, userId)
				}		
			}
			//invia lista utenti
			for _,uName:= range userMap {
				//fmt.Println("dentro map ", uName)
=======
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
>>>>>>> 8de70c3177ea47504279c201cfb0cac5a989e8c5
				buffer = []byte(uName)
				conn.Write(buffer)
				reader.ReadByte()
			}
			conn.Write([]byte("*"))

			//riceve nick dell'utente da aggiungere
<<<<<<< HEAD
			followingName,_:= reader.ReadString('\n')
			//fmt.Println("--",followingName,"--")
			followingName = strings.ReplaceAll(followingName, "\n", "")
			//fmt.Println("nick dell utente--",followingName,"--")
			if followingName=="ABORT" {
=======
			followingName, _ := reader.ReadString('\n')
			fmt.Println("--", followingName, "--")
			followingName = strings.ReplaceAll(followingName, "\n", "")
			fmt.Println("nick dell utente--", followingName, "--")
			if followingName == "ABORT" {
>>>>>>> 8de70c3177ea47504279c201cfb0cac5a989e8c5
				fmt.Println("dentro abort")
				break
			}
			//fmt.Println("fuori abort")
			var done bool
			var followingID int
			//for sbagliato, in userName c è il nome del gioco, controllare
<<<<<<< HEAD
			for uID:= range userMap {
				//fmt.Println("dentro ricerca mappa")
				//fmt.Println(uID, userMap[uID])
				if userMap[uID]==followingName {
					//fmt.Println("utente trovato, lo aggiungo")
=======
			for uID := range userMap {
				fmt.Println("dentro ricerca mappa")
				fmt.Println(uID, userMap[uID])
				if userMap[uID] == followingName {
					fmt.Println("utente trovato, lo aggiungo")
>>>>>>> 8de70c3177ea47504279c201cfb0cac5a989e8c5
					done = followUser(utente.UserID, uID)
					fmt.Println(done)
					followingID = uID
					break
				}
			}
<<<<<<< HEAD
			//fmt.Println("prima di done")
			
			//se l'inserimento è andato a buon fine torno l'id del following
			if done {
				utente.setFollowingList(followingID,followingName)
				//fmt.Println("done si")
				buffer=[]byte(strconv.Itoa(followingID))
			} else {
				//fmt.Println("done no")
				buffer=[]byte("error")
=======
			fmt.Println("prima di done")

			//se l'inserimento è andato a buon fine torno l'id del following
			if done {
				utente.setFollowingList(followingID, followingName)
				fmt.Println("done si")
				buffer = []byte(strconv.Itoa(followingID))
			} else {
				fmt.Println("done no")
				buffer = []byte("error")
>>>>>>> 8de70c3177ea47504279c201cfb0cac5a989e8c5
			}
			//fmt.Println("prima di write")
			conn.Write(buffer)
<<<<<<< HEAD
			//fmt.Println("dopo write")

		case "CHAT":
			//ricevo il nome dell'utente con cui si vuole chattare, e si recupera la conversazione
			receiver,_ := reader.ReadString('\n')
			receiver = strings.ReplaceAll(receiver, "\n", "")
			var recID int
			for k,v:= range utente.FollowingList {
				if v == receiver {
					//fmt.Println("utente trovato: ", k,v)
					recID = k
					break
				}
			}
			conversation := getChat(utente.UserID, recID)
			//torna la lista di messaggi, la invio al client
			//fmt.Println("conv: ", conversation)

			for _,msg:= range conversation {
				//fmt.Println("messaggio: ", msg.Text)
				jsonMsg,_ := json.Marshal(msg)
				conn.Write(jsonMsg)
				reader.ReadByte() //per sync con client
			}
			conn.Write([]byte("*")) //end of msg list

		case "SEND":
			//ricevo il nome dell'utente con cui si sta chattando e il messaggio da inviargli
			receiver,_ := reader.ReadString(' ')
			receiver = strings.ReplaceAll(receiver, " ", "")
			textMsg,_ := reader.ReadString('\n')
			textMsg = strings.ReplaceAll(textMsg, "\n", "")

			//fmt.Println("ricevuto: ", receiver, textMsg)
			var receiverID int
			for k,v:= range utente.FollowingList {
				if v == receiver {
					receiverID = k
					break
				}
			}
			
			done:= saveMsg (utente.UserID, receiverID, textMsg)

			if done {
				
				buffer=[]byte("1")
				} else {
					buffer=[]byte("0")
				}
			conn.Write(buffer)
			
			//se l utente è in mappa connessioni inviare il messaggio anche al suo client
			reply:= make(chan userData)
			reqChan<- reqData {receiverID, reply}
			receiverData:= <-reply
			//fmt.Println(receiverData.id, receiverData.conn)

			if (receiverData.conn != nil) {
				buffer= []byte("!")
				msg:= msgData{utente.UserID, receiverID, textMsg}				
				jsonMsg,_:= json.Marshal(msg)
				buffer = append(buffer,jsonMsg...)
				receiverData.conn.Write(buffer)
			}
			//questo messaggio viene letto dal chatListener del client


			/*uID := reader.ReadString('\n')
			uID = strings.ReplaceAll(chatID, "\n", "")
			respCh := make(chan userData)
			reqChan<- userData{id: uID, replyCh:respCh}
			data:=<-respCh
			go chat(data)
			/*go func() {
				chatID := reader.ReadString('\n')
				chatID = strings.ReplaceAll(chatID, "\n", "")
				reqChan <- chatID
				destData:= <-dataChan
				toChat := destData.conn
				toChat.Write([]byte("CHAT\n"))
				*/
		
=======
			fmt.Println("dopo write")

>>>>>>> 8de70c3177ea47504279c201cfb0cac5a989e8c5
		case "ABORT":
			fmt.Println("operazione abort")

		case "LOGOUT":
			bye<- utente.UserID
			

		case "CLOSE":
			//cancellare la connessione dalla mappa
			fmt.Println("Dissconnessione del client in corso...")
			bye <- utente.UserID
			//quit <- true
			//chiudere i canali?
			conn.Close()
			//esce dal for e la goroutine termina
			return

<<<<<<< HEAD
		default:
			continue			
=======
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
>>>>>>> 8de70c3177ea47504279c201cfb0cac5a989e8c5

		}
	}

}
/*
func handleChatRequest(chatHandlerChan chan userData, quit chan bool) {
	for {
		select {
		//quando arriva una richiesta di chat da un utente, viene avviata una goroutine per quella conversazione
		case data:= <-chatHandlerChan:
			go chat(data)
		//gestire quit di tutte le chat?	

		case esc:= <-quit:
			if esc {
				return
			}

		}
	}
}

func chat(data userData) {
	readCh:= make(chan chatMsg)
	data.replyCh<- userData{chat:readCh}
	writeCh:= data.chat
	select {
	case a:=<-readCh:
		utente.id
	default:
	}

}
*/
