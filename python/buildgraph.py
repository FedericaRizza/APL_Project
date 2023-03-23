#implementazione della classe Nodo
class Nodo:
    def __init__(self, id, nickname): #costruttore
        self.id = id #id univoco del nodo
        self.nickname = nickname #nickname che mi rappresenta il nome dell'utente
        self.listConnessioni = [] #inizializzo la lista delle listConnessioni tra i dictNodi (vuota)


class Arco:
    def __init__(self, arrivo):
        self.arrivo = arrivo
        #self.direzione = direzione #a noi serve un grafo diretto perchè abbiamo a che fare con "seguiti" dunque non vale la relazione reciproca


#implementazione della classe Grafo
class Grafo:
    def __init__(self):
        self.dictNodi = {} #con questo dizionario il grafo tiene traccia di tutti i suoi dictNodi
    

    #ogni grafo deve poter aggiungere dei dictNodi e delle listConnessioni tra questi dictNodi
    #metodo per aggiungere un nuovo nodo
    def aggiungi_nodo(self, id, nickname): #self ci vuole ovunque se non ricordo male
        #controllo che il nodo non esista già nel grafo (quindi controllo se esiste nel dizionario)
        id = str(id)

        if id not in self.dictNodi:
            self.dictNodi[id] = Nodo(id,nickname) #quindi se l'id non è presente creo un nuovo oggetto della classe Nodo aggiungendolo al dizionario dei dictNodi utilizzo quindi id come chiave e il nodo creato come valore
            print("sto aggiungengo il nodo...")
    #metodo per aggiungere una nuova connessione
    def aggiungi_connessione(self, nodo_sorgente, nodo_destinazione):
        #controlliamo che i dictNodi sorgente e destinazione siano ovviamente presenti all'interno del grafo (devono essere presenti come chiave del dizionario)
        #TODO: potrei definire delle eccezioni specifiche io
        nodo_sorgente = str(nodo_sorgente)
        nodo_destinazione = str(nodo_destinazione)
    
        if nodo_sorgente not in [id for id in self.dictNodi]: #TODO 
            raise ValueError ("il nodo sorgente non esiste all'interno del grafo!")
        if nodo_destinazione not in [nodo.id for nodo in self.dictNodi.values()]:
            raise ValueError ("il nodo di destinazione non esiste all'interno del grafo!")
        #controllo che la connessione non esista già
        if Arco(nodo_destinazione) not in self.dictNodi[nodo_sorgente].listConnessioni:
            #aggiungo l'arco solo in una direzione crendo quindi un grafo diretto
            print("sto aggiungendo la relazione...")
            self.dictNodi[nodo_sorgente].listConnessioni.append(Arco(nodo_destinazione))
        