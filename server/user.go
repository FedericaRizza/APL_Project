package main

type user struct {
	userID               int
	nick                 string
	gameList, friendList []string
	//requestList?
}

func (u *user) setGameList(game string) { //variadic cosi può inserire string o list?
	u.gameList = append(u.gameList, game)
}

/*
func (u *user) setID (id int) {
	u.userID=id
}

func (u *user) setNick (nick string) {
	u.nick = nick
}



func (u *user) addFriend (friendID string) {
	u.friendList = append(u.friendList, friendID)
}
*/