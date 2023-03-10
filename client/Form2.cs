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
    public partial class LogForm : Form
    {
        private RegForm register;
        public LogForm(RegForm reg)
        {
            InitializeComponent();
            register = reg;

        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            this.Close();
            //RegForm register= new RegForm();
            register.Show();
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
                HomeForm home = new HomeForm();
                home.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Nickname o password errati, riprovare!");
                return;
            }
            
        }
    }
}
