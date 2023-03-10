package main

//i campi della struttura devono essere public per poter essere esportati in json, la struttura può restare privata
type user struct {
	UserID int `json:"UserID"`
	Nick string `json:"Nick"`
	GameList []string `json:"GameList"`
	FollowingList []string `json:"FollowingList"`
	//requestList?
}

func (u *user) setGameList(game string) { 
	u.GameList = append(u.GameList, game)
}

func (u *user) setFollowingList(fID string) {
	u.FollowingList = append(u.FollowingList, fID)
}

/*
func (u *user) setID (id int) {
	u.UserID=id
}

func (u *user) setNick (nick string) {
	u.Nick = nick
}




*/