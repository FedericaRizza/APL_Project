create database SocialGames;

use SocialGames;

drop table utenti;
drop table amici;

CREATE TABLE utenti (
	id_utente INT(11) NOT NULL AUTO_INCREMENT,
    nickname VARCHAR(255) NOT NULL,
    pass VARCHAR(255) NOT NULL,
    PRIMARY KEY(id_utente),
    UNIQUE(nickname)
);


CREATE TABLE amici (
	id INT(11) NOT NULL AUTO_INCREMENT,
    utente INT(11) NOT NULL,
    amico INT(11) NOT NULL,
    UNIQUE (utente,amico),
    PRIMARY KEY (id),
    FOREIGN KEY (utente) REFERENCES utenti(id_utente) ON DELETE CASCADE,
    FOREIGN KEY (amico) REFERENCES utenti(id_utente) ON DELETE CASCADE
    
);


CREATE TABLE giochi (
	id_gioco INT(11) NOT NULL AUTO_INCREMENT,
    nome VARCHAR(255) NOT NULL,
    UNIQUE (nome),
    PRIMARY KEY (id_gioco)
);

CREATE TABLE utente_giochi (
	utente INT(11) NOT NULL,
    gioco INT(11) NOT NULL, 
	PRIMARY KEY(utente, gioco),
    FOREIGN KEY (utente) REFERENCES utenti(id_utente) ON DELETE CASCADE,
    FOREIGN KEY (gioco) REFERENCES giochi(id_gioco) ON DELETE CASCADE
);


CREATE TABLE conversazioni (
	id_conversazione INT(11) NOT NULL AUTO_INCREMENT,
    PRIMARY KEY(id_conversazione)
);

CREATE TABLE messaggi (
	id_messaggio TIMESTAMP NOT NULL,
    conversazione INT(11) NOT NULL,
    mittente INT(11) NOT NULL,
    destinatario INT(11) NOT NULL,
    testo TEXT NOT NULL,
    PRIMARY KEY(id_messaggio),
    FOREIGN KEY (conversazione) REFERENCES conversazioni(id_conversazione) ON DELETE CASCADE,
	FOREIGN KEY (mittente) REFERENCES utenti(id_utente) ON DELETE CASCADE,
    FOREIGN KEY (destinatario) REFERENCES utenti(id_utente) ON DELETE CASCADE
);