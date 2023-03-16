package main

import (
	"database/sql" //per connettersi al DB
	"fmt"

	_ "github.com/go-sql-driver/mysql" //per importare il driver MySQL, senza questo il terminale mi da errore, il driver viene registrano nel metodo init()
)

/*type user struct {
	userID                  int
	nick                    string
	gameList, followingList []string
}*/

// FUNZIONE PER CONNETTERSI AL DB----------------------------------------------------------
// connessione al DB MySQL e mi restituisce l'oggetto di connessione *sql.DB e mi servirà per fare le query
// usando il puntatore passo la connessione per riferimento anzichè per valore evitando quindi di doverla copiare ogni volta che viene passata a una funzione
// in questo modo tutt le varie funzioni che utilizzano la connessione faranno riferimento alla stessa istanza invece di creare istanze separate e cosi si evitano problemi di coerenza
func connectDB() (*sql.DB, error) {

	//apriamo la connesione al DB, mi restituisce un oggetto *sql.DB
	//se la connessione non riesce il puntatore al DB è nullo
	db, err := sql.Open("mysql", "user:password@tcp(127.0.0.1:3306)/SocialGames")

	if err != nil {
		return nil, err //nil è lo zero value del puntatore RICORDA!
	} else {
		fmt.Println("Connessione al database avvenuta con successo!")
	}

	//per verificare che la connessione al DB sia ancoa aperta e valida
	/*err = db.Ping()
	if err != nil {
		return nil, err
	}*/

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
	//con QueryRow conto il numero di utenti che hanno lo stesso nick e la uso perchè in questo caso mi torna solo una riga con la colonna COUNT(*) ovviamente
	//con Scan leggo i valori della riga e lo assegno alla variabile count
	var count int
	err = db.QueryRow("SELECT COUNT(*) FROM utenti WHERE nickname = ?", nick).Scan(&count)

	if err != nil {
		return false
	}

	//controllo che non ci siano altri utenti e procedo con la registrazione
	if count == 0 {
		//uso il blank identifier perchè Exec mi restituisce anche il risultato dell'exec che in questo caso non mi interessa
		_, err = db.Exec("INSERT INTO utenti (nickname, pass) VALUES (?,?)", nick, psw) //passo psw_hash come array di byte

		if err != nil {
			return false

		}

		return true //nil perchè zero value per err (in questo caso puntatore?)
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
	e := db.QueryRow("SELECT id_utente, nickname FROM utenti WHERE nickname = ? AND pass = ?", nick, psw).Scan(&u.UserID, &u.Nick)
	if e != nil {
		//ovviamente se entra qui basta per non poter fare il login
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
		u.GameList = append(u.GameList, gioco) //append restituisce un nuovo slice contenente gli elementi aggiunti
	}

	//restituisco la lista dei seguiti
	rows1, er1 := db.Query("SELECT u2.nickname FROM seguiti JOIN utenti u1 ON utente = u1.id_utente JOIN utenti u2 ON seguito = u2.id_utente WHERE u1.nickname = ?", nick)
	if er1 != nil {
		return false
	}

	for rows1.Next() {
		var amico string
		rows1.Scan(&amico)
		u.FollowingList = append(u.FollowingList, amico) //append restituisce un nuovo slice contenente gli elementi aggiunti
	}

	//prove stampe
	/*for _, gioco := range u.gameList {
		fmt.Println("giochi:", gioco)
	}

	for _, amico := range u.followingList {
		fmt.Println("amico:", amico)
	}

	fmt.Println(u.nick)
	fmt.Println(u.userID)*/

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
		_, err = db.Exec("INSERT INTO giochi (nome) VALUES (?)", gameName) //passo psw_hash come array di byte
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

	_, err = db.Exec("INSERT INTO utente_giochi (utente, gioco) VALUES (?,?)", userID, id_gioco) //passo psw_hash come array di byte
	if err != nil {
		return false
	}

	//TODO: aggiungere il gioco anche nella struttura

	return true
}

// FUNZIONE TROVA UTENTI CHE POSSIEDONO UN DETERMINATO GIOCO -------------------------------
func findUser(gameName string) (map[int]string, bool) {
	db, err := connectDB()

	if err != nil {
		return nil, false
	}

	defer db.Close()

	//join con utente_giochi per verificare che effetivamente un utente possiede il gioco
	//join con giochi per recuperare i giochi
	rows, err := db.Query("SELECT id_utente, nickname FROM utenti JOIN utente_giochi ug ON id_utente = ug.utente JOIN giochi g ON ug.gioco = g.id_gioco WHERE g.nome = ?", gameName)
	if err != nil {
		return nil, false
	}

	utenti := make(map[int]string)
	//accedo al risultato della query con Next
	for rows.Next() {
		var id int
		var nick string
		e := rows.Scan(&id, &nick)
		if e != nil {
			return nil, false
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
		return false
	}

	//TODO: aggiungere il seguito anche alla struttura

	return true

}

/*func allFollowing(userID int) (map[int]string, bool) {
	db, err := connectDB()

	if err != nil {
		return nil, false
	}

	defer db.Close()

	rows, err := db.Query("SELECT id_utente, nickname FROM seguiti JOIN utenti ON seguiti.seguito = utenti.id_utente WHERE seguiti.utente = ?", userID)
	if err != nil {
		return nil, false
	}

	seguiti := make(map[int]string)
	//accedo al risultato della query con Next
	for rows.Next() {
		var id int
		var nick string
		e := rows.Scan(&id, &nick)
		if e != nil {
			return nil, false
		}
		seguiti[id] = nick
	}

	return seguiti, true
}
*/

/*
	func allFollowing(userID int) (map[int]string, bool) {
		db, err := connectDB()

		if err != nil {
			return nil, false
		}

		defer db.Close()

		rows, err := db.Query("SELECT id_utente, nickname FROM seguiti WHERE id_utente = ?", userID)
		if err != nil {
			return nil, false
		}

		seguiti := make(map[int]string)
		//accedo al risultato della query con Next
		for rows.Next() {
			var id int
			var nick string
			e := rows.Scan(&id, &nick)
			if e != nil {
				return nil, false
			}
			seguiti[id] = nick
		}

		return seguiti, true
	}
*/

type Relation struct {
	IDutente  int `json:"IDutente"`
	IDseguito int `json:"IDseguito"`
}

func allRelation(userID int) ([]Relation, bool) {
	db, err := connectDB()

	if err != nil {
		return nil, false
	}

	defer db.Close()

	/*
		rows, err := db.Query("SELECT utente, seguito FROM seguiti")
		if err != nil {
			return nil, false
		}*/

	//Questa query selezionerà i seguiti dei seguiti dell'utente loggatoa che sono anche seguiti dall'utente con id 1, insieme all'id dell'utente che segue quel seguito e al nickname dell'utente con id 1.
	rows, err := db.Query("SELECT DISTINCT s2.utente, s2.seguito FROM seguiti s1 JOIN seguiti s2 ON s1.seguito = s2.utente JOIN utenti u2 ON s2.seguito = u2.id_utente WHERE s1.utente = ? AND s2.seguito IN (SELECT seguito FROM seguiti WHERE utente = ?)", userID, userID)

	if err != nil {
		return nil, false
	}

	seguiti := []Relation{} //creo uno slice vuoto

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

/*func main() {

	//done := register("giorgia", "prova")

	//var utente user
	//done := login("giorgia", "prova", &utente)

	//done := aggGame("Zelda", 2)

	//ut, done := findUser("Zelda")
	//fmt.Println("utenti:", ut)

	done := addFollowing(2, 1)

	if done {
		return
	}

}*/
