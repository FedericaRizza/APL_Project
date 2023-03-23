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
            
            Thread listener = new Thread(() => Client.ChatListener(del));
            listener.IsBackground = true;
            listener.Start();
        }
    
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
