using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace client
{
    //visibilità namespace, così tutte le classi possono creare delegati ChatDel
    public delegate void ChatDel(MsgData newMsg);
    public partial class HomeForm : Form
    {
        
        private LogForm login;
        //string e non list perchè può essere aperta solo una chat per volta?
        private ChatForm chatOpened;
        public HomeForm(LogForm log)
        {
            InitializeComponent();
            login = log;
            labelUser.Text = "Ciao, " + Client.utente.Nick;
            listBoxGames.Items.AddRange(Client.utente.GameList.ToArray<String>());
            //listBoxFollowing.Items.AddRange(Client.utente.FollowingList.Values.ToArray());
            listBoxChat.Items.AddRange(Client.utente.ChatList.ToArray<String>());
            //fare dei metodi anzichè chiamare le proprietà?          

        }

        //TOGLIERE, NON FUNZIONA
        public void Alert(MsgData newMsg)
        {//sistemare prendendo il nick dalla mappa, receiver è id
            if (listBoxChat.InvokeRequired)
            {
                Action selfdel = delegate { Alert(newMsg); };
                listBoxChat.Invoke(selfdel);
            }
            else
            {
                var name = Client.utente.FollowingList[newMsg.Sender];
                var index = listBoxChat.FindStringExact(name);
                listBoxChat.Items[index] += "\t NUOVO MESSAGGIO!";
            }
        }
        
        private void labelUser_MouseHover(object sender, EventArgs e)
        {
            panelUser.BringToFront();
            buttonAddGame.BringToFront();
            buttonFindUser.BringToFront();
            buttonLogout.BringToFront();
            panelUser.Size = panelUser.MaximumSize;
        }

        private void buttonFindUser_Click(object sender, EventArgs e)
        {
            panelUser.Size = panelUser.MinimumSize;
            AddUserForm addUser = new AddUserForm(this);
            addUser.Show();
            addUser.FormClosing += AddUser_FormClosing; //delegato della funzione sotto
        }

        private void AddUser_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.listBoxFollowing.Items.Clear();
            //this.listBoxFollowing.Items.AddRange(Client.utente.FollowingList.Values.ToArray()); //togliere

        }

        private void buttonAddGame_Click(object sender, EventArgs e)
        {
            panelUser.Size = panelUser.MinimumSize;
            AddGameForm addGame = new AddGameForm(this);
            addGame.Show();
            addGame.FormClosing += AddGame_FormClosing; //delegato della funzione sotto
        }

        private void AddGame_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.listBoxGames.Items.Clear();
            this.listBoxGames.Items.AddRange(Client.utente.GameList.ToArray<String>());
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            Client.Logout();
            this.Close(); //fare exit o tornare al login?
            login.Show();
        }

        private void listBoxFollowing_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBoxFollowing.SelectedItem!= null)
            {
                var user = listBoxFollowing.SelectedItem.ToString();
                listBoxFollowing.ClearSelected();
                //controlla se la chat è già aperta, aggiungere listener close
                if (chatOpened==null || !chatOpened.ReceiverName.Equals(user))
                {
                    ChatForm chat = new ChatForm(user);
                    chatOpened = chat;
                    chat.Show();

                    /*
                    ChatDel del = chatOpened.Update;
                    //del = delHome;
                    Thread listener = new Thread(() => Client.ChatListener(del));
                    listener.IsBackground = true;
                    listener.Start();
                    */ 

                    chat.FormClosing += Chat_FormClosing;
                    
                }
            }
           
        }

        private void Chat_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (!Client.utente.ChatList.Contains(chatOpened.ReceiverName))
            {
                listBoxChat.Items.Add(chatOpened.ReceiverName);
                
            }
            chatOpened=null;
            
        }

        private void listBoxGames_MouseClick(object sender, MouseEventArgs e)
        {
            if (listBoxGames.SelectedItem != null)
            {
                var game = listBoxGames.SelectedItem.ToString();
                listBoxFollowing.Items.Clear();
                if (Client.utente.SharedGames.ContainsKey(game))
                    listBoxFollowing.Items.AddRange(Client.utente.SharedGames[game].ToArray());
            }
        }

        private void buttonGraph_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process cmd = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            //startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = @"/c python graph.py" +" "+ Client.utente.UserID +" "+ Client.utente.Nick;
            //startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;
            startInfo.WorkingDirectory = "C:\\Users\\feder\\Documents\\UProjects\\APL\\GameProject\\python";
            cmd.StartInfo = startInfo;
            cmd.Start();
            cmd.WaitForExit();
        }

        private void listBoxChat_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBoxChat.SelectedItem != null)
            {
                var user = listBoxChat.SelectedItem.ToString();
                listBoxChat.ClearSelected();
                //controlla se la chat è già aperta, aggiungere listener close
                if (chatOpened == null || !chatOpened.ReceiverName.Equals(user))
                {
                    ChatForm chat = new ChatForm(user);
                    chatOpened = chat;
                    chat.Show();

                    chat.FormClosing += Chat_FormClosing;

                }
            }
        }

        private void HomeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Client.Logout();            
            login.Show();
        }
    }
}
