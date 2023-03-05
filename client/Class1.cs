using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace client
{
    internal class Client
    {
        public static void Main(String[] args)
        {
            byte[] buffer = new byte[1024];
            
            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPAddress ipAddress = host.AddressList[0];
            IPEndPoint localEndPoint = new (ipAddress, 8000);
            Socket conn = new (ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            conn.Connect(localEndPoint);

            byte[] msg = Encoding.ASCII.GetBytes("stringa di prova\n");

            conn.Send(msg);
            conn.Close();
        }
        
    }
}
