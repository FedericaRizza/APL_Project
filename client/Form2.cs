namespace client
{
    public partial class LogForm : Form
    {
        private RegForm register;
        
        public LogForm()
        {
            InitializeComponent();
            register= new RegForm(this);
            
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            this.Hide();
            register.FormClosing += Register_FormClosing;
            register.Show();
        }

        private void Register_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Close();
        }

        private void buttonLog_Click(object sender, EventArgs e)
        {
            if (textBoxNick.Text.Length == 0 )
            {
                MessageBox.Show("Inserire nickname");
                return;
            }

            if (textBoxPsw.Text.Length == 0)
            {
                MessageBox.Show("Inserire password");
                return;
            }

            if (Client.SendLogin(textBoxNick.Text, textBoxPsw.Text))
            {
                HomeForm home = new HomeForm(this);
                home.Show();
                textBoxNick.Clear();
                textBoxPsw.Clear();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Nickname o password errati, riprovare!");
                textBoxNick.Clear();
                textBoxPsw.Clear();
                return;
            }
            
        }

        private void LogForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Client.Exit();
        }
    }
}
