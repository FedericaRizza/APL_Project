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
    public partial class AddGameForm : Form
    {
        private HomeForm home;
        public AddGameForm(HomeForm home)
        {
            InitializeComponent();
            this.home = home;
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            if (textBoxGame.Text.Length == 0)
            {
                MessageBox.Show("Inserire il nome del gioco");
                return;
            }

            var games = Client.SearchGame(textBoxGame.Text);
            if (games.Length > 0)
            {
                listBoxGames.Items.AddRange(games);
                textBoxGame.Clear();
                this.Size = MaximumSize;
                panel2.BringToFront();

            }
            else
                MessageBox.Show("Non è stato trovato nessun gioco");
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Size = MinimumSize;
            panel1.BringToFront();
            listBoxGames.Items.Clear();
        }


        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (listBoxGames.SelectedItem != null)
            {
                if (Client.AddGame(listBoxGames.SelectedItem.ToString()))
                {
                    MessageBox.Show("Gioco aggiunto");
                    
                    //home.listBoxGames.Items.Add(listBoxGames.SelectedItem.ToString());
                    //home.listBoxGames.Refresh();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Errore");
                    this.Close();
                }

            }
            else
                MessageBox.Show("Seleziona un gioco dalla lista");
        }

        private void AddGameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Client.AbortAddOp();
        }
    }
}
