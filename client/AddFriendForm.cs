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
    public partial class AddFriendForm : Form
    {
        private HomeForm home;
        public AddFriendForm(HomeForm home)
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
                var friends = Client.SearchFriend(listBoxGames.SelectedItem.ToString());
                if (friends != null)
                {
                    listBoxUser.Items.AddRange(friends);
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
                if (Client.AddFriend(listBoxGames.SelectedItem.ToString()))
                {
                    MessageBox.Show("Amico aggiunto");
                    //AGGIORNARE DATI UTENTE
                    home.listBoxFriends.Items.Add(listBoxGames.SelectedItem.ToString());
                    home.listBoxFriends.Refresh();
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
