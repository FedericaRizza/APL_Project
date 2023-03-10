using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace client
{
    internal static class Client
    {
        private static Socket client;
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
            byte[] msg = Encoding.ASCII.GetBytes("REGISTER "+nick+" "+psw+"\n");
            client.Send(msg);
            client.Receive(msg);
            if (msg.ToString().Equals("0"))
            {
                return true;
            }
            else return false;
        }

    }
}
