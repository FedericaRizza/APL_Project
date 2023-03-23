package main

import (
	"net"
)

type userData struct {
	id   int
	conn net.Conn
}

type reqData struct {
	id      int
	replyCh chan userData
}

type msgData struct {
	Sender   int    `json:"Sender"`
	Receiver int    `json:"Receiver"`
	Text     string `json:"Text"`
}

type Relation struct {
	IDutente  int `json:"IDutente"`
	IDseguito int `json:"IDseguito"`
}

type Conversazione struct {
	Nmessaggi      int `json:"Nmessaggi"`
	IDmittente     int `json:"IDmittente"`
	IDdestinatario int `json:"IDdestinatario"`
}
