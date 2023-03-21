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
                if (msg.Sender == Client.utente.UserID)
                    line = "Tu:\t";
                else
                    line = ReceiverName + ":\t";
                line += msg.Text +"\n";
                richTextBoxChat.AppendText(line);
            }

            ChatDel del = Update;
            //del = delHome;
            Thread listener = new Thread(() => Client.ChatListener(del));
            listener.IsBackground = true;
            listener.Start();
        }
        /*
        public void UpdateChat(MsgData newMsg)
        {//sistemare prendendo il nick dalla mappa, receiver è id

            if (chatOpened == null || !chatOpened.ReceiverName.Equals(Client.utente.FollowingList[newMsg.Sender]))
            {
                ChatForm chat = new ChatForm(Client.utente.FollowingList[newMsg.Sender]);
                //var conv = Client.OpenChat(newMsg.Receiver);
                chat.Show();
                chatOpened = chat;
                
            }
            else
            {
                chatOpened.Update(newMsg);
            }


        }*/

        public void Update (MsgData newMsg)
        {
            if(richTextBoxChat.InvokeRequired)
            {
                Action selfdel = delegate { Update(newMsg); };
                richTextBoxChat.Invoke(selfdel);
            }
            else
                richTextBoxChat.AppendText(ReceiverName + ":\t" + newMsg.Text + "\n");
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

            richTextBoxChat.AppendText("Tu:\t" + txt + "\n");
        }
    }
}
