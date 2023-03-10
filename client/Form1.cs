namespace client
{
    public partial class RegForm : Form
    {
        public RegForm()
        {
            InitializeComponent();
        }

        private void buttonLog_Click(object sender, EventArgs e)
        {
            this.Hide();
            LogForm login = new LogForm(this);
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
                    this.Hide();
                    LogForm log = new LogForm(this);
                    log.Show();                    
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