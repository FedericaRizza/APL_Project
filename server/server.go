package main

import (
	"bufio"
	"fmt"
	"net"
	"os"
)

const (
	ServerType = "tcp"
	ServerPort = ":8000"
)
//serve una map dove salvare client-connessione
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

<<<<<<< HEAD
	for i := 0; i < 1; i++ {
		//accept viene usata per accettare nuove connessioni in entrata sulla porta del server.
		//se la connessione è accettata con successo viene restituita un'istanza della connessione (net.Conn) che mi rappresenta la connessione appena stabilita tra il server e il vlient
		//in questo caso viene fatto dentro ad un loop che viene eseguito solo una volta, per accettare quindi una singola connessione
=======
	for {
>>>>>>> 92a4d427309b2a2289c8fb6dd6ca0208e2e47ddb
		connection, err := server.Accept()
		if err != nil {
			fmt.Println("Errore di connessione")
			return
		}

<<<<<<< HEAD
		handleClient(connection) //gestisce la connessione appena aperta
=======
		go handleClient(connection)
>>>>>>> 92a4d427309b2a2289c8fb6dd6ca0208e2e47ddb

	}
}

// chiamata ogni volta che un client si connette al server
func handleClient(conn net.Conn) {
	defer conn.Close() //appena il client termina la comunicazione, la connesione viene chiusa
	//const userid int

	//buffer:= make([]byte,1024)

	for {
		//cicla continuamente finchè il clienti rimane connesso
		//fmt.Println("Attesa del client...")

		//legge il messaggio del client
		request, _ := bufio.NewReader(conn).ReadString(' ') //provare readline

		//switch per gestire le varie richieste del client
		switch request {

		case "REGISTER ":
			usr := bufio.NewReader(conn).ReadString(' ')
			psw := bufio.NewReader(conn).ReadString('\n')
			done := register(usr, psw) //TODO func register(usr,psw string) bool

		case "LOGIN ":
			usr, _ := bufio.NewReader(conn).ReadString(' ')
			psw, _ := bufio.NewReader(conn).ReadString('\n')
			resp := login(usr, psw) //TODO func login(usr,psw string) bool
			buffer = byte(resp[:])
			conn.Write(buffer)

		case "ADDGAME ":
			//cercare su igdb
			done := addGame(gamename) //TODO func addgame(name string) bool

		case "ADDFRIEND ":
			userlist, done := findUser(gamename) //TODO func finduser(gamename string) []string
			//decidere se fare mutua amicizia con richiesta o solo il segui
		default:
			fmt.Println(request)

		}

	}

}
