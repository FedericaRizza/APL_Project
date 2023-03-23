using System.Text;
using System.Text.Json;
using System.Net.Sockets;
using System.Net;
using System.Security.Cryptography;

namespace client
{
    internal static class Client
    {
        private static Socket client;
        public static User utente;
        private static byte[] buffer;
        
        public static void StartConnection()
        {            
            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPAddress ipAddress = host.AddressList[0];
            IPEndPoint localEndPoint = new (ipAddress, 8000);
            try
            {
                Socket c = new (ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                c.Connect(localEndPoint);
                client = c;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.ToString());
            }
            buffer = new byte[512];

        }

        public static bool SendRegister(String nick, String psw) {
            String hash;
            using (SHA256 mySHA=SHA256.Create())
            {
                byte[] data = mySHA.ComputeHash(Encoding.UTF8.GetBytes(psw));
                var builder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    builder.Append(data[i].ToString("x2"));
                }
                hash = builder.ToString();
            }
            byte[] msg = Encoding.ASCII.GetBytes("REGISTER\n"+nick+" "+hash+"\n");
            client.Send(msg);
           
            int len = client.Receive(buffer);

            if (len == 0)
                return false;

            byte[] reply = buffer.Take(len).ToArray();
            Array.Clear(buffer);
            if (Encoding.ASCII.GetString(reply).Equals("1"))           
                return true;     
            
            else return false;
        }

        public static bool SendLogin (String nick, String psw)
        {
            String hash;
            using (SHA256 mySHA = SHA256.Create())
            {
                byte[] data = mySHA.ComputeHash(Encoding.UTF8.GetBytes(psw));
                var builder = new StringBuilder();
                for (int i=0; i<data.Length; i++){
                    builder.Append(data[i].ToString("x2"));
                }
                hash = builder.ToString();
            }
            byte[] msg = Encoding.ASCII.GetBytes("LOGIN\n" + nick + " " + hash + "\n");
            client.Send(msg);
            int len = client.Receive(buffer);

            if (len == 0)
                return false;

            byte[] reply = buffer.Take(len).ToArray();
            Array.Clear(buffer);
            if (Encoding.ASCII.GetString(reply).Equals("1"))
            { 
                //salvataggio dati utente in locale
                
                len= client.Receive(buffer);
                byte[] jsonObj = buffer.Take(len).ToArray();
                Array.Clear(buffer);
                utente = JsonSerializer.Deserialize<User>(jsonObj);
                if (utente.GameList == null)
                    utente.GameList = new List<String>();
                if (utente.FollowingList == null)
                    utente.FollowingList = new Dictionary<int, String>();
                return true;
            }
                

            else return false;
        }

        public static String[] SearchUser(String GameName)
        {
            IList<String> userList = new List<String>();
            byte[] msg = Encoding.ASCII.GetBytes("FOLLOW\n" + GameName + "\n");
            client.Send(msg);
            int len = client.Receive(buffer);

            if (len == 0)
            {
                Array.Clear(msg);
                msg = Encoding.ASCII.GetBytes("ABORT\n");
                client.Send(msg);
                return userList.ToArray<String>();
            }

            byte[] reply = buffer.Take(len).ToArray();
            Array.Clear(buffer);
            while (!Encoding.ASCII.GetString(reply).Equals("*"))
            {
                client.Send(Encoding.ASCII.GetBytes("-"));
                userList.Add(Encoding.ASCII.GetString(reply));
                len = client.Receive(buffer);
                reply = buffer.Take(len).ToArray();
                Array.Clear(buffer);                
            }
            if (!userList.Any())
                client.Send(Encoding.ASCII.GetBytes("ABORT\n"));
            return userList.ToArray<String>();

        }

        public static bool FollowUser(String UserName, String GameName)
        {
            byte[] msg = Encoding.ASCII.GetBytes(UserName + "\n");
            client.Send(msg);

            int len = client.Receive(buffer);

            if (len == 0)
                return false;

            byte[] reply = buffer.Take(len).ToArray();
            Array.Clear(buffer);
            if (!Encoding.ASCII.GetString(reply).Equals("error"))
            {
                var id = Encoding.ASCII.GetString(reply);
                int idUser;
                if (int.TryParse(id, out idUser))
                {
                    utente.FollowingList.Add(idUser, UserName);
                    if (!utente.SharedGames.ContainsKey(GameName))
                        utente.SharedGames.Add(GameName, new List<String>());
                    utente.SharedGames[GameName].Add(UserName);
                    return true;
                }
                else
                    return false;
                
            }

            else return false;
        }

        public static void AbortAddOp()
        {
            byte[] msg = Encoding.ASCII.GetBytes("ABORT\n");
            client.Send(msg);
        }

        public static String[] SearchGame(String GameName)
        {
            IList<String> gameList= new List<String>();
            byte[] msg = Encoding.ASCII.GetBytes("ADDGAME\n" + GameName + "\n");
            client.Send(msg);
            int len = client.Receive(buffer);

            if (len == 0)
            {
                Array.Clear(msg);
                msg = Encoding.ASCII.GetBytes("ABORT\n");
                client.Send(msg);
                return gameList.ToArray<String>();

            }

            byte[] reply = buffer.Take(len).ToArray();
            Array.Clear(buffer);
            while (!Encoding.ASCII.GetString(reply).Equals("*"))
            {
                client.Send(Encoding.ASCII.GetBytes("-"));
                gameList.Add(Encoding.ASCII.GetString(reply));
                len = client.Receive(buffer);
                reply = buffer.Take(len).ToArray();
                Array.Clear(buffer);
            }
            if (!gameList.Any())
                client.Send(Encoding.ASCII.GetBytes("ABORT\n"));
            return gameList.ToArray<String>();
        }

        public static bool AddGame(String GameName)
        {
            byte[] msg = Encoding.ASCII.GetBytes(GameName + "\n");
            client.Send(msg);

            int len = client.Receive(buffer);

            if (len == 0)
                return false;

            byte[] reply = buffer.Take(len).ToArray();
            Array.Clear(buffer);
            if (Encoding.ASCII.GetString(reply).Equals("1"))
            {
                utente.GameList.Add(GameName);
                return true;
            }
            else
                return false;
        }

        public static MsgData[] OpenChat (String ReceiverName)
        {
            IList<MsgData> conversation = new List<MsgData>();
            byte[] msg = Encoding.ASCII.GetBytes("CHAT\n" + ReceiverName + "\n");
            client.Send(msg);

            int len = client.Receive(buffer);

            if (len == 0)
            {
                Array.Clear(msg);
                msg = Encoding.ASCII.GetBytes("ABORT\n");
                client.Send(msg);
                return conversation.ToArray<MsgData>();
            }
            byte[] reply = buffer.Take(len).ToArray();
            Array.Clear(buffer);
            while (!Encoding.ASCII.GetString(reply).Equals("*"))
            {
                client.Send(Encoding.ASCII.GetBytes("-"));
                var message = JsonSerializer.Deserialize<MsgData>(reply);
                conversation.Add(message);
                len = client.Receive(buffer);
                reply = buffer.Take(len).ToArray();
                Array.Clear(buffer);
            }
            
            return conversation.ToArray<MsgData>();
        }

        public static bool SendMessage(string ReceiverName, string Message)
        {
            byte[] msg = Encoding.ASCII.GetBytes("SEND\n"+ ReceiverName + " " + Message + "\n");
            client.Send(msg);

            int len = client.Receive(buffer);

            if (len == 0)
                return false;

            byte[] reply = buffer.Take(len).ToArray();
            Array.Clear(buffer);
            if (Encoding.ASCII.GetString(reply).Equals("1"))
            {
                //se la chat non esisteva, la crea al primo messaggio inviato
                if (!utente.ChatList.Contains(ReceiverName))
                    utente.ChatList.Add(ReceiverName);
                return true;
            }

            else 
                return false;
        }

        public static void Logout()
        {
            byte[] msg = Encoding.ASCII.GetBytes("LOGOUT\n");
            client.Send(msg);
            utente = new User();
        }

        public static void Exit()
        {
            //comunicare al client che si sta chiudendo la connessione
            byte[] msg = Encoding.ASCII.GetBytes("CLOSE\n");
            client.Send(msg);
            client.Close();
        }

        //esegue in un thread in background, per individuare messaggi in arrivo
        public static void ChatListener(ChatDel del)
        {
            var buf = new byte[256];
            while(true){
                //guarda se il primo byte corrisponde a !, simbolo che indica l'arrivo di un messaggio dal server
                client.Receive(buf, 1, SocketFlags.Peek);
                byte[] reply = buf.Take(1).ToArray();
                if (Encoding.ASCII.GetString(reply) == "!")
                {
                    var len = client.Receive(buf);
                    reply = buf.Take(len).ToArray();
                    byte[] jsonMsg = reply.Skip(1).ToArray<byte>();
                    var msg = JsonSerializer.Deserialize<MsgData>(jsonMsg);
                    del(msg);
                }

            }
        }
        
    }

   

    //cambiare in struct??? eredito da eventargs per poter passare il dato con l'evento
    public struct MsgData 
    {
        public int Sender { get; set; }
        public int Receiver { get; set; }
        public String Text { get; set; }
    }
}
