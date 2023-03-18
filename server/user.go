package main

//i campi della struttura devono essere public per poter essere esportati in json, la struttura può restare privata
type user struct {
	UserID int `json:"UserID"`
	Nick string `json:"Nick"`
	GameList []string `json:"GameList"`
	FollowingList map[int]string `json:"FollowingList"`
	ChatList []string `json:"ChatList"`
	SharedGames map[string][]string `json:"SharedGames"`
}

func newUser() user {
	m:= make(map[string][]string)
	u := user{GameList : make([]string,0,5), FollowingList : make(map[int]string), ChatList : make([]string,0,5), SharedGames : m}
	return u
}

func (u *user) setGameList(game string) { 
	u.GameList = append(u.GameList, game)
}

func (u *user) setFollowingList(fID int, name string) {
	u.FollowingList[fID] = name
}

func (u *user) setChatList(name string) {
	u.ChatList = append(u.ChatList, name)
}

func (u *user) setSharedGames (game string, name string) {
	u.SharedGames[game] = append(u.SharedGames[game], name)
}

/*
func (u *user) setID (id int) {
	u.UserID=id
}

func (u *user) setNick (nick string) {
	u.Nick = nick
}




*/