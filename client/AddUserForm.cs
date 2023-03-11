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
    public partial class AddUserForm : Form
    {
        private HomeForm home;
        public AddUserForm(HomeForm home)
        {
            InitializeComponent();
            this.home = home;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            if (listBoxGames.SelectedItem != null)
            {
                var users = Client.SearchUser(listBoxGames.SelectedItem.ToString());
                if (users != null)
                {
                    listBoxUser.Items.AddRange(users);
                    panel2.BringToFront();

                }
                else
                    MessageBox.Show("Non è stato trovato nessun utente");
            }
            else
                MessageBox.Show("Seleziona un gioco dalla lista");
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            panel1.BringToFront();
            Client.AbortAddOp();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (listBoxGames.SelectedItem != null)
            {
                if (Client.FollowUser(listBoxGames.SelectedItem.ToString()))
                {
                    MessageBox.Show("Utente seguito");
                    
                    home.listBoxFollowing.Items.Add(listBoxGames.SelectedItem.ToString());
                    home.listBoxFollowing.Refresh();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Errore");
                    this.Close();
                }
                
            }
            else
                MessageBox.Show("Seleziona un utente dalla lista");
        }
    }
}
