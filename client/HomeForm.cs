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
            buttonFindUser.BringToFront();
            buttonLogout.BringToFront();
            panelUser.Size = panelUser.MaximumSize;
        }

        private void buttonFindUser_Click(object sender, EventArgs e)
        {
            panelUser.Size = panelUser.MinimumSize;
            AddUserForm addUser = new AddUserForm(this);
            addUser.Show();
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
