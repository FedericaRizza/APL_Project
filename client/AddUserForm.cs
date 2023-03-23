namespace client
{
    public partial class AddUserForm : Form
    {
        public AddUserForm()
        {
            InitializeComponent();           
            listBoxGames.Items.AddRange(Client.utente.GameList.ToArray<String>());
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
                if (users.Length > 0)
                {
                    listBoxUser.Items.Clear();
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
            listBoxUser.ClearSelected();
            listBoxGames.ClearSelected(); 
            panel1.BringToFront();
            Client.AbortAddOp();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (listBoxUser.SelectedItem != null)
            {
                if (Client.FollowUser(listBoxUser.SelectedItem.ToString(), listBoxGames.SelectedItems.ToString()))
                {
                    MessageBox.Show("Utente seguito");                
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

        private void AddUserForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Client.AbortAddOp();
        }
    }
}
