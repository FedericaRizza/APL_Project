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

	for i := 0; i < 1; i++ {
		//accept viene usata per accettare nuove connessioni in entrata sulla porta del server.
		//se la connessione è accettata con successo viene restituita un'istanza della connessione (net.Conn) che mi rappresenta la connessione appena stabilita tra il server e il vlient
		//in questo caso viene fatto dentro ad un loop che viene eseguito solo una volta, per accettare quindi una singola connessione
		connection, err := server.Accept()
		if err != nil {
			fmt.Println("Errore di connessione")
			return
		}

		handleClient(connection) //gestisce la connessione appena aperta

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
		case "LOGIN ":
			/*usr,_:=bufio.NewReader(conn).ReadString(' ')
			psw,_:=bufio.NewReader(conn).ReadString('\n')
			resp:=login(usr,psw)
			buffer = byte(resp[:])
			conn.Write(buffer)*/
		default:
			fmt.Println(request)
		}

	}

}
