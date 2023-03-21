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

    #if response_utente != "*":
    utente = json.loads(response_utente) #mi converta la stringa json in una lista di dizionari
    #quando viene effettuata una decodifica di una stringa JSON in Python le chiavi vengono rappresentate come stringhe
    print(utente)
    grafo.aggiungi_nodo(str(utente['UserID']), nickname=utente['Nick'])#casting necessario per trasformare la chiave (che è una stringa) in un intero come si aspetta aggiungi_nodo
    for id_seguito, nick_seguito in utente['FollowingList'].items():
        grafo.aggiungi_nodo(id_seguito, nickname=nick_seguito)
        grafo.aggiungi_connessione(str(utente['UserID']), id_seguito)

    client_socket.send('-'.encode()) #per la sincronizzazione (perchè il server manda tutto, connessione a stream, cerco di bloccarmi sincronizzando lettura con scrittura)
    
    #message_seguiti = "PYSEGUITI "
    #client_socket.send(message_seguiti.encode())

    #AGGIUNGO NODI SEGUITI E CONNESSIONI---------------------
    

    response_seguiti = client_socket.recv(1024).decode('utf-8') #utf-8 per convertire una sequenza di byte in stringa
    if response_seguiti != "*":
        relazioni = json.loads(response_seguiti)
        for relazione in relazioni:   #accedo ad ogni dizionario di relazioni
            seguito1 = relazione['IDutente']
            print(seguito1)
            seguito2 = relazione['IDseguito']
            print(seguito2)
            print("sto aggiugengo gli archi rimanenti")
            grafo.aggiungi_connessione(seguito1, seguito2) #e aggiungo l'arco


def mostra_grafo(id_utente, nickname):
    
    message = "PYPESO\n"
    client_socket.send(message.encode())
    response = client_socket.recv(1024).decode()
    print(response)
    print("ei")
    dictConversazioni = json.loads(response)
    #TODO controllo *   
    
    
    #costruzione di un oggetto di tipo go.Figure() per creare il grafico con plotly
    fig = go.Figure()
    dictCoordinate = {}
    
    
    #AGGIUNGO TUTTI I NODI
    for nodo in grafo.dictNodi.values(): 
        #genero x, y casuale in modo da mettere i nodi sparsi nel disegno (altrimenti l'unico modo sarebbe na retta) 
        x_coordinata = random.uniform(0,30)
        y_coordinata = random.uniform(0,30)
        dictCoordinate[nodo.id] = (x_coordinata, y_coordinata) #dizionario id-coppie(tupla)


        fig.add_trace(go.Scatter(x =[x_coordinata], y= [y_coordinata], mode='markers', marker=dict(symbol='circle', size=50), name=nodo.nickname))
    

    #AGGIUNGO TUTTI GLI ARCHI
    for nodo in grafo.dictNodi.values():
        for connessione in nodo.listConnessioni:
        
            #x mi rappresenta gli estremi dell'arco (id nodo partenza e id nodo arrivo) (VALORI NEGLI ASSI DELLE ORDINATE)
            #y mi rappresenta nickname di nodo di partenza e destinazione (VALORI NEGLI ASSI DELLE ASCISSE)
            #mode: modo con cui i punti del grafico vengono connessi, in questo modo con lines da linee rette
            #nome: indiciato dalla direzione(usato come etichetta de)
            #hoverinfo: 
        #per tracciare la linea che unisce i nodi devo recuperare le coordinate e quindi 


            nodoPartenza_x, nodoPartenza_y = dictCoordinate[nodo.id]#prendo le coordinate del nodo di arrvivo
            nodoArrivo_x, nodoArrivo_y = dictCoordinate[connessione.arrivo] 
            
        
        

        #disegno la linea con add_trace in x e y metto i due estremi della connessione
            
            '''
            fig.add_trace(go.Scatter(x=[nodoPartenza_x, nodoArrivo_x], y=[nodoPartenza_y, nodoArrivo_y], mode = 'lines',
                        line=dict(width=2), marker=dict(symbol='arrow', size=10, color = 'grey'), 
                        name="relazione da " + nodo.nickname + ' a ' + grafo.dictNodi[connessione.arrivo].nickname, 
                        hoverinfo='skip'))
            
            #fig.add_annotation(go.Scatter(x=[nodoArrivo_x], y = [nodoArrivo_y], showarrow = True, arrowhead = 2, arrowsize=1, arrowwidth=2 ))
            '''

            if nodo.id == str(id_utente):
                for conversazione in dictConversazioni: 
                    print(conversazione)
                    print(type(conversazione['IDmittente']))
                    print(type(nodo.id))
                    print(type(connessione.arrivo))
                    if int(nodo.id) == conversazione['IDmittente'] and int(connessione.arrivo) == conversazione['IDdestinatario'] and conversazione['Nmessaggi'] >=5 and conversazione['Nmessaggi'] <10:
                #controllo per il PESO delle connessioni
                #è come se stessi mettendo un testo con la freccia, ma non metto il testo. Con l'altro metodo non riuscivo a mettere la freccia
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
    print(type(sys.argv[1]), type(sys.argv[2]))
    grafo_utenti(int(sys.argv[1]))
    mostra_grafo(int(sys.argv[1]), sys.argv[2])
    print("prima di logout")
    logout()
    



'''# Esempio di visualizzazione del grafo
import matplotlib.pyplot as plt
#nx.draw(graph, with_labels=True)



# Visualizzazione del grafo
nx.draw(graph, pos=pos, with_labels=True)
nx.draw_networkx_edges(graph, pos)

plt.show()'''






