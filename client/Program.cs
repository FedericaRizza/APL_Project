using System.Net.Sockets;

namespace client
{
    internal static class GameProject
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        
        [STAThread]
        static void Main()
        {            
            ApplicationConfiguration.Initialize();
            Client.StartConnection();
            Application.Run(new LogForm());
        }
    }
}