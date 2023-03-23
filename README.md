# APL_Project
Applicativo composto da un server in Go, due client in C# e Python e un database MySQL.

Il database viene deployato in un container Docker, per avviarlo da terminale spostarsi nella directory del progetto e digitare:
```
docker-compose up -d
```
Il server utilizza un package esterno per effettuare query al database online IGDB, installarlo con:

```
go get github.com/Henry-Sarabia/igdb/v2
```
Dopo aver avviato il database, avviare il server da terminale, spostandosi nella directory <i>./server</i> e digitare:
```
go run .
```
Successivamente aprire il client in C#, contenuto nella directory <i>./client</i>, tramite Visual Studio e avviarlo.
