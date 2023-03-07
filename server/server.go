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
	//const userid int
	
	//buffer:= make([]byte,1024)
	
	for {
		//fmt.Println("Attesa del client...")
		request,_:=bufio.NewReader(conn).ReadString(' ')//provare readline
		switch request { 
		case "REGISTER ":
			usr:=bufio.NewReader(conn).ReadString(' ')
			psw:=bufio.NewReader(conn).ReadString('\n')
			done:= register(usr,psw) //TODO func register(usr,psw string) bool
		case "LOGIN ":
			usr,_:=bufio.NewReader(conn).ReadString(' ')
			psw,_:=bufio.NewReader(conn).ReadString('\n')
			resp:=login(usr,psw) //TODO func login(usr,psw string) bool
			buffer = byte(resp[:])
			conn.Write(buffer)
		case "ADDGAME ":
			//cercare su igdb
			done:= addgame(gamename) //TODO func addgame(name string) bool
		case "ADDFRIEND ":
			userlist:= finduser(gamename) //TODO func finduser(gamename string) []string
			//decidere se fare mutua amicizia con richiesta o solo il segui
		default:
			fmt.Println(request)
		}
	

	}

}
