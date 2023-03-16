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
    public partial class HomeForm : Form
    {
        private LogForm login;
        public HomeForm(LogForm log)
        {
            InitializeComponent();
            login = log;
            labelUser.Text = "Ciao, " + Client.utente.Nick;
            listBoxGames.Items.AddRange(Client.utente.GameList.ToArray<String>());
            listBoxFollowing.Items.AddRange(Client.utente.FollowingList.Values.ToArray());
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
    }
}
