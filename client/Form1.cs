namespace client
{
    public partial class RegForm : Form
    {
        private LogForm login;
        public RegForm(LogForm log)
        {
            InitializeComponent();
            login = log;
        }

        private void buttonLog_Click(object sender, EventArgs e)
        {
            this.Hide();
            login.Show();
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            if (textBoxNick.Text.Length == 0)
            {
                MessageBox.Show("Inserire nickname");
                return;
            }

            if (textBoxPsw.Text.Length == 0)
            {
                MessageBox.Show("Inserire password");
                return;
            }

            if (textBoxPsw2.Text.Length == 0)
            {
                MessageBox.Show("Ripetere password");
                return;
            }

            if (textBoxPsw.Text.Equals(textBoxPsw2.Text))
            {
                if (Client.SendRegister(textBoxNick.Text, textBoxPsw.Text))
                { 
                    MessageBox.Show("Account creato correttamente.");
                    textBoxNick.Clear();
                    textBoxPsw2.Clear();
                    textBoxPsw.Clear();
                    this.Hide();                    
                    login.Show();                    
                }
                    
                    
                   
                else
                    MessageBox.Show("Nickname già esistente");
                
            }
            else
            {
                MessageBox.Show("Le password non combaciano! Riprova");
                textBoxPsw.Clear();
                textBoxPsw2.Clear();
            }
        }
    }
}