import mysql.connector
import networkx as nx
import plotly.graph_objects as go


#connessione al db
while True:
    try:
        db = mysql.connector.connect( 
        host = "127.0.0.1",
        user = "user",
        password = "password",
        database="SocialGames"
        )
        break
    except Exception as sqlerr:
        print("Errore: ", sqlerr)

cursor = db.cursor()

print("")

#creo un nuovo oggetto grafo 
graph = nx.DiGraph()


#aggiungo i nodi utenti al grafo
try:
    cursor.execute("SELECT id_utente, nickname FROM utenti")
except Exception as sql_error:
    print("E:", sql_error)
for (id_utente, nickname) in cursor:
    #add_node permette di aggiungere un nodo al grafo
    #add_node si aspetta almeno un parametro ovvero l'identificativo del nome e in questo caso passiamo l'id_utente
    #ed inoltre è possibile passare altri parametri sotto forma di dizionario, in questo caso nickname diventa un attributo del nodo a cui assegno il nick dell'utente
    print("prova")
    graph.add_node(id_utente, nickname=nickname)


#a questo punto ho bisogno di aggiungere gli archi al grafo
#e per fare questo mi selezioni i seguiti e unisco i nodi sulla base di questo
cursor.execute("SELECT utente, seguito FROM seguiti")
for (utente, seguito) in cursor:
    print("prova2")
    graph.add_edge(utente,seguito)

cursor.close()
db.close()


layout = go.Layout(title="Grafo delle relazioni tra gli utenti")

pos = nx.kamada_kawai_layout(graph)

node_trace = go.Scatter(
    x=[],
    y=[],
    text=[],
    mode="markers",
    hoverinfo="text",
    marker=dict(
        color="lightblue",
        size=10,
        line=dict(width=2, color="black")
    )
)

edge_trace = go.Scatter(
    x=[],
    y=[],
    line=dict(width=1, color="grey"),
    hoverinfo="none",
    mode="lines"
)

# Aggiunta dei nodi al grafico
for node in graph.nodes():
    x, y = pos[node]
    node_trace["x"] += tuple([x])
    node_trace["y"] += tuple([y])
    node_trace["text"] += tuple([graph.nodes[node]["nickname"]])

# Aggiunta degli archi al grafico
for edge in graph.edges():
    x0, y0 = pos[edge[0]]
    x1, y1 = pos[edge[1]]
    edge_trace["x"] += tuple([x0, x1, None])
    edge_trace["y"] += tuple([y0, y1, None])

# Creazione del grafo
fig = go.Figure(data=[edge_trace, node_trace], layout=layout)

# Visualizzazione del grafo
fig.show()

'''# Esempio di visualizzazione del grafo
import matplotlib.pyplot as plt
#nx.draw(graph, with_labels=True)



# Visualizzazione del grafo
nx.draw(graph, pos=pos, with_labels=True)
nx.draw_networkx_edges(graph, pos)

plt.show()'''






