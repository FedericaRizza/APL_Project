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

namespace client
{
    internal static class Client
    {
        private static Socket client;
        public static User utente;
        public static void StartConnection()
        {
            //byte[] buffer = new byte[1024];
            
            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPAddress ipAddress = host.AddressList[0];
            IPEndPoint localEndPoint = new (ipAddress, 8000);
            client = new (ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            client.Connect(localEndPoint); //gestire eccezione

            //byte[] msg = Encoding.ASCII.GetBytes("stringa di prova\n");

            //conn.Send(msg);
            //conn.Close();
        }

        public static bool SendRegister(String nick, String psw) {
            byte[] buffer = new byte[1024];
            String hash;
            using (SHA256 mySHA=SHA256.Create())
            {
                buffer= mySHA.ComputeHash(Encoding.ASCII.GetBytes(psw));
                hash = Encoding.ASCII.GetString(buffer);
            }
            byte[] msg = Encoding.ASCII.GetBytes("REGISTER "+nick+" "+hash+"\n");
            client.Send(msg);
            //Array.Clear(msg);
            Array.Clear(buffer);
            int len = client.Receive(buffer);

            if (len == 0)
                return false;

            if (Encoding.ASCII.GetString(buffer).Equals("1"))             
                return true;     
            
            else return false;
        }

        public static bool SendLogin (String nick, String psw)
        {
            byte[] buffer = new byte[1024];
            String hash;
            using (SHA256 mySHA = SHA256.Create())
            {
                buffer = mySHA.ComputeHash(Encoding.ASCII.GetBytes(psw));
                hash = Encoding.ASCII.GetString(buffer);
            }
            byte[] msg = Encoding.ASCII.GetBytes("LOGIN " + nick + " " + hash + "\n");
            client.Send(msg);
            Array.Clear(buffer);
            int len = client.Receive(buffer);

            if (len == 0) 
                return false;

            if (Encoding.ASCII.GetString(buffer).Equals("1"))
            { 
                //salvataggio dati utente in locale
                Array.Clear(buffer);
                //da sistemare
                client.Receive(buffer);
                utente = JsonSerializer.Deserialize<User>(buffer);
                return true;
            }
                

            else return false;
        }

        public static string[] SearchFriend(String GameName)
        {
            String[] userList = null;
            byte[] buffer = new byte[1024];
            byte[] msg = Encoding.ASCII.GetBytes("ADDFRIEND " + GameName + "\n");
            client.Send(msg);
            int len = client.Receive(buffer);

            if (len == 0)
                return userList;
            while (!Encoding.ASCII.GetString(buffer).Equals("*"))
            {
                userList.Append(Encoding.ASCII.GetString(buffer));
                Array.Clear(buffer);
                client.Receive(buffer);
            }

            return userList;

        }

        public static bool AddFriend(String FriendName)
        {
            byte[] buffer = new byte[1024];
            byte[] msg = Encoding.ASCII.GetBytes(FriendName + "\n");
            client.Send(msg);

            int len = client.Receive(buffer);

            if (len == 0)
                return false;

            if (Encoding.ASCII.GetString(buffer).Equals("1"))
                return true;

            else return false;
        }

        public static void AbortAddOp()
        {
            byte[] msg = Encoding.ASCII.GetBytes("ABORT\n");
            client.Send(msg);
        }

    }
}
