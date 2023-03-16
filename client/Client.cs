using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Runtime.CompilerServices;

namespace client
{
    internal static class Client
    {
        private static Socket client;
        public static User utente;
        
        public static void StartConnection()
        {            
            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPAddress ipAddress = host.AddressList[0];
            IPEndPoint localEndPoint = new (ipAddress, 8000);
            Socket c = new (ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            c.Connect(localEndPoint); //gestire eccezione
            client = c;
            //inizializzo le liste altrimenti non posso aggiungere i dati se sono null
            
        }

        public static bool SendRegister(String nick, String psw) {
            byte[] buffer = new byte[1024];
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
            //Array.Clear(msg);
            Array.Clear(buffer);
            int len = client.Receive(buffer);

            if (len == 0)
                return false;

            byte[] reply = buffer.Take(len).ToArray();
            if (Encoding.ASCII.GetString(reply).Equals("1"))           
                return true;     
            
            else return false;
        }

        public static bool SendLogin (String nick, String psw)
        {
            byte[] buffer = new byte[1024];
            String hash;
            using (SHA256 mySHA = SHA256.Create())
            {
                //algoritmo per portare hash da array di byte a string, dal sito microsoft
                byte[] data = mySHA.ComputeHash(Encoding.UTF8.GetBytes(psw));
                var builder = new StringBuilder();
                for (int i=0; i<data.Length; i++){
                    builder.Append(data[i].ToString("x2"));
                }
                hash = builder.ToString();
            }
            byte[] msg = Encoding.ASCII.GetBytes("LOGIN\n" + nick + " " + hash + "\n");
            client.Send(msg);
            Array.Clear(buffer);
            int len = client.Receive(buffer);

            if (len == 0) 
                return false;
            byte[] reply = buffer.Take(len).ToArray();
            if (Encoding.ASCII.GetString(reply).Equals("1"))
            { 
                //salvataggio dati utente in locale
                Array.Clear(buffer);
                
                len= client.Receive(buffer);
                byte[] jsonObj = buffer.Take(len).ToArray();
                utente = JsonSerializer.Deserialize<User>(jsonObj);
                if (utente.GameList == null)
                    utente.GameList = new List<String>();
                if (utente.FollowingList == null)
                    utente.FollowingList = new Dictionary<int, String>();
                return true;
            }
                

            else return false;
        }

        public static string[] SearchUser(String GameName)
        {
            IList<String> userList = new List<String>();
            byte[] buffer = new byte[1024];
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
            var prova = Encoding.ASCII.GetString(reply);
            while (!Encoding.ASCII.GetString(reply).Equals("*"))
            {
                client.Send(Encoding.ASCII.GetBytes("-"));
                userList.Add(Encoding.ASCII.GetString(reply));
                Array.Clear(buffer);                
                len = client.Receive(buffer);
                reply = buffer.Take(len).ToArray();
            }
            if (!userList.Any())
                client.Send(Encoding.ASCII.GetBytes("ABORT\n"));
            return userList.ToArray<String>();

        }

        public static bool FollowUser(String UserName)
        {
            byte[] buffer = new byte[1024];
            byte[] msg = Encoding.ASCII.GetBytes(UserName + "\n");
            client.Send(msg);

            int len = client.Receive(buffer);

            if (len == 0)
                return false;

            byte[] reply = buffer.Take(len).ToArray();
            if (!Encoding.ASCII.GetString(reply).Equals("error"))
            {
                var id = Encoding.ASCII.GetString(reply);
                int idUser;
                if (int.TryParse(id, out idUser))
                {
                    utente.FollowingList.Add(idUser, UserName);
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
            byte[] buffer = new byte[1024];
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
            while (!Encoding.ASCII.GetString(reply).Equals("*"))
            {
                client.Send(Encoding.ASCII.GetBytes("-"));
                gameList.Add(Encoding.ASCII.GetString(reply));
                Array.Clear(buffer);
                len = client.Receive(buffer);
                reply = buffer.Take(len).ToArray();
            }

            return gameList.ToArray<String>();
        }

        public static bool AddGame(String GameName)
        {
            byte[] buffer = new byte[1024];
            byte[] msg = Encoding.ASCII.GetBytes(GameName + "\n");
            client.Send(msg);

            int len = client.Receive(buffer);

            if (len == 0) 
                return false;

            byte[] reply = buffer.Take(len).ToArray();
            if (Encoding.ASCII.GetString(reply).Equals("1"))
            {
                utente.GameList.Add(GameName);
                return true;
            }
            else
                return false;
        }

        public static void Logout()
        {
            utente = new User();
        }

        public static void Exit()
        {
            //comunicare al client che si sta chiudendo la connessione
            byte[] msg = Encoding.ASCII.GetBytes("CLOSE\n");
            client.Send(msg);
            client.Close();
        }

    }
}
