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
        RegForm register;
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
    }
}
