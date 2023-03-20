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
    public partial class ChatForm : Form
    {
        public String ReceiverName { get; }
        public ChatForm(String ToUser)
        {
            InitializeComponent();
            ReceiverName = ToUser;
            labelNickname.Text = ReceiverName;
            //spostare in homeform?
            var chat = Client.OpenChat(ToUser);

            foreach (var msg in chat)
            {
                String line;
                if (msg.Sender == Client.utente.Nick)
                    line = "Tu: ";
                else
                    line = msg.Sender + ": ";
                line += msg.Text +"\n";
                richTextBoxChat.AppendText(line);
            }
        }

        public void UpdateChat (MsgData newMsg)
        {
            richTextBoxChat.AppendText(newMsg.Text+"\n");
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            var txt = richTextBoxMsg.Text;
            if (txt.Length <= 0)
                return;

            richTextBoxMsg.Clear();            

            if (!Client.SendMessage(ReceiverName, txt))
            { 
                MessageBox.Show("Errore invio messaggio");
                return; 
            }

            richTextBoxChat.AppendText("Tu: " + txt + "\n");
        }
    }
}
