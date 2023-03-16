using System.Net.Sockets;

namespace client
{
    internal static class GameProject
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        private static Socket client;
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Client.StartConnection();
            Application.Run(new LogForm());
        }
    }
}