#implementazione della classe Nodo
class Nodo:
    def __init__(self, id, nickname): 
        self.id = id 
        self.nickname = nickname 
        self.listConnessioni = [] 


#implementazione della classe Arco
class Arco:
    def __init__(self, arrivo):
        self.arrivo = arrivo


#implementazione della classe Grafo
class Grafo:
    def __init__(self):
        self.dictNodi = {} #con questo dizionario il grafo tiene traccia di tutti i suoi Nodi
    

    #ogni grafo deve poter aggiungere dei dictNodi e delle listConnessioni tra questi Nodi
    def aggiungi_nodo(self, id, nickname): 
        #controllo che il nodo non esista già nel grafo (quindi controllo se esiste nel dizionario)
        id = str(id)
        if id not in self.dictNodi:
            self.dictNodi[id] = Nodo(id,nickname) #quindi se l'id non è presente creo un nuovo oggetto della classe Nodo aggiungendolo al dizionario dei Nodi utilizzo quindi id come chiave e il nodo creato come valore
    def aggiungi_connessione(self, nodo_sorgente, nodo_destinazione):
        #controlliamo che i nodi sorgente e destinazione siano ovviamente presenti all'interno del grafo
        nodo_sorgente = str(nodo_sorgente)
        nodo_destinazione = str(nodo_destinazione)
        if nodo_sorgente not in [nodo.id for nodo in self.dictNodi.values()]: 
            raise ValueError ("il nodo sorgente non esiste all'interno del grafo!")
        if nodo_destinazione not in [nodo.id for nodo in self.dictNodi.values()]:
            raise ValueError ("il nodo di destinazione non esiste all'interno del grafo!")
        #controllo che la connessione non esista già
        if Arco(nodo_destinazione) not in self.dictNodi[nodo_sorgente].listConnessioni:
            self.dictNodi[nodo_sorgente].listConnessioni.append(Arco(nodo_destinazione))
        