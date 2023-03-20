using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client
{
    //visibilità namespace, così tutte le classi possono creare delegati ChatDel
    public delegate void ChatDel(MsgData newMsg);
    public partial class HomeForm : Form
    {
        private ChatDel del;
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

            ChatDel delHome = UpdateChat;
            del = delHome;
            Thread listener = new Thread(() => Client.ChatListener(del));

        }
        public void UpdateChat(MsgData newMsg)
        {
            if (!chatOpened.ReceiverName.Equals(newMsg.Receiver))
            {
                ChatForm chat = new ChatForm(newMsg.Receiver);
                //var conv = Client.OpenChat(newMsg.Receiver);
                chat.Show();
                chatOpened = chat;
            }
            else
            {
                chatOpened.UpdateChat(newMsg);
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
            this.listBoxFollowing.Items.AddRange(Client.utente.FollowingList.Values.ToArray());
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
                if (!chatOpened.ReceiverName.Equals(user))
                {
                    ChatForm chat = new ChatForm(user);
                    chatOpened = chat;
                    chat.Show();
                    chat.FormClosing += Chat_FormClosing;
                    listBoxChat.Refresh();
                }
            }
           
        }

        private void Chat_FormClosing(object? sender, FormClosingEventArgs e)
        {
            chatOpened=null;
            
        }

        private void listBoxGames_MouseClick(object sender, MouseEventArgs e)
        {
            if (listBoxGames.SelectedItem != null)
            {
                var game = listBoxGames.SelectedItem.ToString();
                listBoxFollowing.Items.Clear();
                listBoxFollowing.Items.AddRange(Client.utente.SharedGames[game]);
            }
        }
    }
}
