namespace client
{
    public partial class AddGameForm : Form
    {
        public AddGameForm()
        {
            InitializeComponent();          
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
            Client.AbortAddOp();
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
