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

	server, err := net.Listen(ServerType,ServerPort)
	if err!=nil {
		fmt.Println("Errore creazione server")
		os.Exit(1)
	}

	defer server.Close()

	for i:=0;i<1;i++{
		connection, err := server.Accept()
		if err!=nil {
			fmt.Println("Errore di connessione")
			return
		}

		 handleClient(connection)

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
