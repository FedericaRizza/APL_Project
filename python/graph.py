import plotly.graph_objects as go
import socket
import json
from buildgraph import Grafo
import random
import sys

grafo = Grafo()


#connessione al server 
SERVER_IP = "localhost"
SERVER_PORT = 8000

#creo il socket tcp 
client_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
client_socket.connect((SERVER_IP, SERVER_PORT))


# FUNZIONE: costruzione logica del grafo
def grafo_utenti(id_utente): 

    
    message_utenti = "PYUTENTI\n" + str(id_utente) + "\n"

    #AGGIUNGO IL NODO DELL'UTENTE LOGGATO------------------------------------------------------------
    client_socket.send(message_utenti.encode())
    response_utente = client_socket.recv(1024).decode()

    utente = json.loads(response_utente) 
    grafo.aggiungi_nodo(str(utente['UserID']), nickname=utente['Nick']) #casting necessario per trasformare la chiave (che è una stringa) in un intero come si aspetta aggiungi_nodo
    for id_seguito, nick_seguito in utente['FollowingList'].items():
        grafo.aggiungi_nodo(id_seguito, nickname=nick_seguito)
        grafo.aggiungi_connessione(str(utente['UserID']), id_seguito)

    client_socket.send('-'.encode()) 
    
    

    #AGGIUNGO NODI SEGUITI E CONNESSIONI---------------------
    response_seguiti = client_socket.recv(1024).decode('utf-8') 
    if response_seguiti != "*":
        relazioni = json.loads(response_seguiti)
        for relazione in relazioni:   
            seguito1 = relazione['IDutente']
            seguito2 = relazione['IDseguito']
            grafo.aggiungi_connessione(seguito1, seguito2) 


def mostra_grafo(id_utente, nickname):
    message = "PYPESO\n"
    client_socket.send(message.encode())
    response = client_socket.recv(1024).decode()
    listConversazioni = json.loads(response)
  
    
    
    fig = go.Figure()

    #mi serve conservare le coordinate dei nodi che disegno per poi indicare l'inizio e la fine della freccia che mi rappresenta la relazione
    dictCoordinate = {}
    
    
    #AGGIUNGO TUTTI I NODI
    for nodo in grafo.dictNodi.values(): 
        #genero x, y casuale in modo da mettere i nodi sparsi nel disegno 
        x_coordinata = random.uniform(0,30)
        y_coordinata = random.uniform(0,30)
        dictCoordinate[nodo.id] = (x_coordinata, y_coordinata) 


        fig.add_trace(go.Scatter(x =[x_coordinata], y= [y_coordinata], mode='markers', marker=dict(symbol='circle', size=50), name=nodo.nickname))
    

    #AGGIUNGO TUTTI GLI ARCHI
    for nodo in grafo.dictNodi.values():
        for connessione in nodo.listConnessioni:
            nodoPartenza_x, nodoPartenza_y = dictCoordinate[nodo.id] 
            nodoArrivo_x, nodoArrivo_y = dictCoordinate[connessione.arrivo] 
            
            if nodo.id == str(id_utente):
                fig.add_annotation(
                    x=nodoArrivo_x, y=nodoArrivo_y, ax=nodoPartenza_x, ay=nodoPartenza_y,
                    xref='x', yref='y', axref='x', ayref='y',
                    showarrow=True, arrowhead=5, arrowsize=1, arrowwidth=2, arrowcolor = 'black' 
                    )
                for conversazione in listConversazioni:   
                    if int(nodo.id) == conversazione['IDmittente'] and int(connessione.arrivo) == conversazione['IDdestinatario'] and conversazione['Nmessaggi'] >=5 and conversazione['Nmessaggi'] <10:
                        fig.add_annotation(
                        x=nodoArrivo_x, y=nodoArrivo_y, ax=nodoPartenza_x, ay=nodoPartenza_y,
                        xref='x', yref='y', axref='x', ayref='y',
                        showarrow=True, arrowhead=5, arrowsize=1, arrowwidth=3, arrowcolor = 'black',
                        )
                    elif int(nodo.id) == conversazione['IDmittente'] and int(connessione.arrivo) == conversazione['IDdestinatario'] and conversazione['Nmessaggi'] >=10:
                        fig.add_annotation(
                        x=nodoArrivo_x, y=nodoArrivo_y, ax=nodoPartenza_x, ay=nodoPartenza_y,
                        xref='x', yref='y', axref='x', ayref='y',
                        showarrow=True, arrowhead=5, arrowsize=1, arrowwidth=5, arrowcolor = 'black',
                        )
                    elif int(nodo.id) == conversazione['IDmittente'] and int(connessione.arrivo) == conversazione['IDdestinatario'] and conversazione['Nmessaggi'] < 5:
                        fig.add_annotation(
                        x=nodoArrivo_x, y=nodoArrivo_y, ax=nodoPartenza_x, ay=nodoPartenza_y,
                        xref='x', yref='y', axref='x', ayref='y',
                        showarrow=True, arrowhead=5, arrowsize=1, arrowwidth=2, arrowcolor = 'black' 
                        )
            else:
                fig.add_annotation(
                x=nodoArrivo_x, y=nodoArrivo_y, ax=nodoPartenza_x, ay=nodoPartenza_y,
                xref='x', yref='y', axref='x', ayref='y',
                showarrow=True, arrowhead=5, arrowsize=1, arrowwidth=2, arrowcolor = 'grey' 
                )

            
    
    fig.update_layout(title='Grafo dei seguiti di ' + nickname + ' pesato in base alle conversazioni')

    # Visualizzazione del grafo
    fig.show()
    
def logout():
    message = "CLOSE\n"
    client_socket.send(message.encode())
    



if __name__ == "__main__":
    grafo_utenti(int(sys.argv[1]))
    mostra_grafo(int(sys.argv[1]), sys.argv[2])
    logout()
    






