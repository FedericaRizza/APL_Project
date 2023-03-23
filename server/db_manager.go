package main

import (
	"database/sql"
	"fmt"

	_ "github.com/go-sql-driver/mysql"
)

// FUNZIONE PER CONNETTERSI AL DB----------------------------------------------------------
func connectDB() (*sql.DB, error) {

	db, err := sql.Open("mysql", "user:password@tcp(127.0.0.1:3306)/SocialGames")

	if err != nil {
		return nil, err
	} else {
		fmt.Println("Connessione al database avvenuta con successo!")
	}

	return db, nil

}

// FUNZIONE DI REGISTRAZIONE---------------------------------------------------------------
func register(nick string, psw string) bool {

	db, err := connectDB()

	if err != nil {
		return false
	}

	defer db.Close()

	//verifichiamo che l'utente non esiste già (contiamo gli utenti con lo stesso nickname)
	var count int
	err = db.QueryRow("SELECT COUNT(*) FROM utenti WHERE nickname = ?", nick).Scan(&count)

	if err != nil {
		return false
	}

	//controllo che non ci siano altri utenti e procedo con la registrazione
	if count == 0 {
		//uso il blank identifier perchè Exec mi restituisce anche il risultato dell'exec che in questo caso non mi interessa
		_, err = db.Exec("INSERT INTO utenti (nickname, pass) VALUES (?,?)", nick, psw)
		if err != nil {
			return false

		}

		return true
	} else {

		return false
	}

}

// FUNZIONE DI LOGIN-----------------------------------------------------------------------
func login(nick string, psw string, u *user) bool {

	db, err := connectDB()

	if err != nil {
		return false
	}

	defer db.Close()

	//controllo che nickname e password siano presenti nel DB
	var password string
	e := db.QueryRow("SELECT id_utente, nickname, pass FROM utenti WHERE nickname = ?", nick).Scan(&u.UserID, &u.Nick, &password)
	if e != nil {

		return false
	}

	if password != psw {
		return false
	}

	//restituisco la lista dei giochi
	rows, er := db.Query("SELECT nome FROM giochi JOIN utente_giochi ug ON id_gioco = ug.gioco JOIN utenti u ON u.id_utente = ug.utente WHERE u.nickname = ?", nick)
	if er != nil {
		return false
	}

	for rows.Next() {
		var gioco string
		rows.Scan(&gioco)
		u.setGameList(gioco)

	}

	//restituisco la mappa dei seguiti
	rows1, er1 := db.Query("SELECT u2.id_utente, u2.nickname FROM seguiti JOIN utenti u1 ON utente = u1.id_utente JOIN utenti u2 ON seguito = u2.id_utente WHERE u1.nickname = ?", nick)
	if er1 != nil {
		return false
	}

	for rows1.Next() {
		var id int
		var seguito string
		rows1.Scan(&id, &seguito)
		u.setFollowingList(id, seguito)
	}

	//recupero della lista chat di un utente dalla tabella conversazioni
	rows2, er2 := db.Query("SELECT nickname FROM utenti u JOIN conversazioni c ON u.id_utente = c.utente1 OR u.id_utente = c.utente2 WHERE c.id_conversazione IN (SELECT conversazione FROM messaggi WHERE mittente = ? OR destinatario = ? ) AND u.id_utente != ?", u.UserID, u.UserID, u.UserID)
	if er2 != nil {
		return false
	}

	for rows2.Next() {
		var utenteConversazione string
		rows2.Scan(&utenteConversazione)
		u.setChatList(utenteConversazione)
	}

	//query per creare la mappa gioco-following che hanno quel gioco
	rows3, er3 := db.Query("SELECT id_gioco, nome FROM giochi g JOIN utente_giochi ug ON g.id_gioco = ug.gioco JOIN utenti u ON ug.utente = u.id_utente WHERE u.nickname = ?", nick)
	if er3 != nil {
		fmt.Println(er3)
		return false
	}

	var GameList = make(map[int]string)

	for rows3.Next() {
		var idGioco int
		var nameGioco string
		rows3.Scan(&idGioco, &nameGioco)
		fmt.Println("gioco in lista: ", nameGioco)
		GameList[idGioco] = nameGioco
	}

	for id, gioco := range GameList {
		rows4, er4 := db.Query("SELECT u2.nickname FROM utenti u1 JOIN seguiti s ON u1.id_utente = s.utente JOIN utenti u2 ON s.seguito = u2.id_utente WHERE u1.id_utente = ? AND s.seguito IN (SELECT ug.utente FROM utente_giochi ug WHERE ug.gioco = ?)", u.UserID, id)
		if er4 != nil {
			fmt.Println(er4)
			return false
		}

		for rows4.Next() {
			var seguito string
			rows4.Scan(&seguito)
			u.setSharedGames(gioco, seguito)
			fmt.Println("aggiungo ", gioco, seguito)
		}
	}

	return true

}

func getInfoUtente(userID int, u *user) bool {

	db, err := connectDB()

	if err != nil {
		return false
	}

	defer db.Close()

	e := db.QueryRow("SELECT id_utente, nickname FROM utenti WHERE id_utente = ?", userID).Scan(&u.UserID, &u.Nick)
	if e != nil {
		return false
	}

	//restituisco la lista dei giochi
	rows, er := db.Query("SELECT nome FROM giochi JOIN utente_giochi ug ON id_gioco = ug.gioco JOIN utenti u ON u.id_utente = ug.utente WHERE u.id_utente = ?", u.UserID)
	if er != nil {
		return false
	}

	for rows.Next() {
		var gioco string
		rows.Scan(&gioco)
		u.GameList = append(u.GameList, gioco)
	}

	//restituisco la mappa dei seguiti
	rows1, er1 := db.Query("SELECT u2.id_utente, u2.nickname FROM seguiti JOIN utenti u1 ON utente = u1.id_utente JOIN utenti u2 ON seguito = u2.id_utente WHERE u1.nickname = ?", u.Nick)
	if er1 != nil {
		return false
	}

	for rows1.Next() {
		var id int
		var seguito string
		rows1.Scan(&id, &seguito)
		u.setFollowingList(id, seguito)
	}

	return true

}

// FUNZIONE AGGIUNGI GIOCO --------------------------------------------------------------
func addGame(gameName string, userID int) bool {

	db, err := connectDB()

	if err != nil {
		return false
	}

	defer db.Close()

	var count int
	err = db.QueryRow("SELECT COUNT(*) FROM giochi WHERE nome = ?", gameName).Scan(&count)
	if count == 0 {
		//inserisco il gioco nella tabella giochi
		_, err = db.Exec("INSERT INTO giochi (nome) VALUES (?)", gameName)
		if err != nil {
			return false
		}
	}

	//inserisco anche nella tabella utente_giochi selezionando prima l'id del gioco
	var id_gioco int
	e := db.QueryRow("SELECT id_gioco FROM giochi WHERE nome = ?", gameName).Scan(&id_gioco)
	if e != nil {
		return false
	}

	_, err = db.Exec("INSERT INTO utente_giochi (utente, gioco) VALUES (?,?)", userID, id_gioco)
	if err != nil {
		return false
	}

	return true
}

// FUNZIONE TROVA UTENTI CHE POSSIEDONO UN DETERMINATO GIOCO -------------------------------
func findUser(gameName string) (map[int]string, bool) {

	utenti := make(map[int]string)
	db, err := connectDB()

	if err != nil {
		return utenti, false
	}

	defer db.Close()

	rows, err := db.Query("SELECT id_utente, nickname FROM utenti JOIN utente_giochi ug ON id_utente = ug.utente JOIN giochi g ON ug.gioco = g.id_gioco WHERE g.nome = ?", gameName)
	if err != nil {
		return utenti, false
	}

	for rows.Next() {
		var id int
		var nick string
		e := rows.Scan(&id, &nick)
		if e != nil {
			return utenti, false
		}
		utenti[id] = nick
	}

	return utenti, true

}

func followUser(userID int, follwingID int) bool {

	db, err := connectDB()

	if err != nil {
		return false
	}

	defer db.Close()

	_, err = db.Exec("INSERT INTO seguiti (utente, seguito) VALUES (?,?)", userID, follwingID) //passo psw_hash come array di byte
	if err != nil {
		fmt.Println("dentro insert following")
		return false
	}
	fmt.Println("la query torna true")

	return true

}

func allRelation(userID int) ([]Relation, bool) {
	db, err := connectDB()

	if err != nil {
		return nil, false
	}

	defer db.Close()

	//Questa query selezionerà i seguiti dei seguiti dell'utente loggato
	rows, err := db.Query("SELECT DISTINCT s2.utente, s2.seguito FROM seguiti s1 JOIN seguiti s2 ON s1.seguito = s2.utente JOIN utenti u2 ON s2.seguito = u2.id_utente WHERE s1.utente = ? AND s2.seguito IN (SELECT seguito FROM seguiti WHERE utente = ?)", userID, userID)

	if err != nil {
		return nil, false
	}

	seguiti := []Relation{}

	for rows.Next() {
		var relation Relation
		e := rows.Scan(&relation.IDutente, &relation.IDseguito)
		if e != nil {
			return nil, false
		}
		seguiti = append(seguiti, relation)
	}

	return seguiti, true

}

func getConversazione() ([]Conversazione, bool) {
	db, err := connectDB()

	if err != nil {
		return nil, false
	}

	defer db.Close()

	rows, err := db.Query("SELECT COUNT(*) AS num_messaggi, m.mittente, m.destinatario FROM conversazioni c JOIN messaggi m ON c.id_conversazione = m.conversazione GROUP BY  m.mittente, m.destinatario, c.id_conversazione")

	if err != nil {
		return nil, false
	}

	conversazioni := []Conversazione{}

	for rows.Next() {
		var conversazione Conversazione
		e := rows.Scan(&conversazione.Nmessaggi, &conversazione.IDmittente, &conversazione.IDdestinatario)
		if e != nil {
			return nil, false
		}
		conversazioni = append(conversazioni, conversazione)
	}

	return conversazioni, true

}

// trova l'id conversazione tra id1 e id2, dopodichè ritorna in una lista tutti i messaggi di quella conversazione.
func getChat(id1, id2 int) []msgData {

	db, err := connectDB()

	if err != nil {
		return nil
	}

	defer db.Close()

	fmt.Println("id:", id1, id2)
	rows, err := db.Query("SELECT mittente, destinatario, testo FROM messaggi WHERE conversazione IN (SELECT id_conversazione FROM conversazioni WHERE (utente1 = ? AND utente2 = ?) OR (utente1 = ? AND utente2 = ?))", id1, id2, id2, id1)
	if err != nil {
		fmt.Println(err)
		return nil
	}

	mData := make([]msgData, 0, 10)

	for rows.Next() {
		var msg msgData
		e := rows.Scan(&msg.Sender, &msg.Receiver, &msg.Text)
		fmt.Println(msg.Sender, msg.Receiver, msg.Text)
		if e != nil {
			return nil
		}
		mData = append(mData, msg)
	}
	return mData

}

// salva il messaggio in tabella messaggi: dai due id recupera l'id conversazione da salvare nell'entry del messaggio
// Se non esiste la conversazione tra id1 e id2 o tra id2 e id1 la crea.
func saveMsg(fromID int, toID int, msg string) bool {

	db, err := connectDB()

	if err != nil {
		return false
	}

	defer db.Close()

	var IDconversazione int

	rows, err := db.Query("SELECT id_conversazione FROM conversazioni WHERE (utente1 = ? AND utente2 = ?) OR (utente1 = ? AND utente2 = ?)", fromID, toID, toID, fromID)
	if err != nil {
		return false
	}

	if !rows.Next() {
		_, e := db.Exec("INSERT INTO conversazioni (utente1, utente2) VALUES (?,?)", fromID, toID)
		if e != nil {
			return false
		}

		e2 := db.QueryRow("SELECT id_conversazione FROM conversazioni WHERE (utente1 = ? AND utente2 = ?)", fromID, toID).Scan(&IDconversazione)
		if e2 != nil {
			return false
		}
		fmt.Println("id: ", IDconversazione)

	} else {
		e := rows.Scan(&IDconversazione)
		if e != nil {
			return false
		}

	}
	_, err2 := db.Exec("INSERT INTO messaggi (conversazione, mittente, destinatario, testo) VALUES (?,?,?,?)", IDconversazione, fromID, toID, msg)
	if err2 != nil {
		return false
	}
	return true

}
