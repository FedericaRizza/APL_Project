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
        public HomeForm()
        {
            InitializeComponent();
        }

        private void labelUser_MouseHover(object sender, EventArgs e)
        {
            panelUser.BringToFront();
            buttonAddGame.BringToFront();
            buttonFindFriend.BringToFront();
            buttonLogout.BringToFront();
            panelUser.Size = panelUser.MaximumSize;
        }

        private void buttonFindFriend_Click(object sender, EventArgs e)
        {
            panelUser.Size = panelUser.MinimumSize;
            AddFriendForm addFriend = new AddFriendForm(this);
            addFriend.Show();
        }

        private void buttonAddGame_Click(object sender, EventArgs e)
        {
            panelUser.Size = panelUser.MinimumSize;
            AddGameForm addGame = new AddGameForm(this);
            addGame.Show();
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            Client.Logout();
            this.Close(); //fare exit o tornare al login?
        }
    }
}
