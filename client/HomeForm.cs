namespace client
{
    public delegate void ChatDel(MsgData newMsg);
    public partial class HomeForm : Form
    {
        
        private LogForm login;
        private ChatForm chatOpened;
        public HomeForm(LogForm log)
        {
            InitializeComponent();
            login = log;
            labelUser.Text = "Ciao, " + Client.utente.Nick;
            listBoxGames.Items.AddRange(Client.utente.GameList.ToArray<String>());
            listBoxChat.Items.AddRange(Client.utente.ChatList.ToArray<String>());

        }

        
        private void labelUser_MouseHover(object sender, EventArgs e)
        {
            panelUser.BringToFront();
            buttonAddGame.BringToFront();
            buttonFindUser.BringToFront();
            buttonLogout.BringToFront();
            panelUser.Size = panelUser.MaximumSize;
        }

        private void buttonFindUser_Click(object sender, EventArgs e)
        {
            panelUser.Size = panelUser.MinimumSize;
            AddUserForm addUser = new AddUserForm();
            addUser.Show();
            addUser.FormClosing += AddUser_FormClosing; 
        }

        private void AddUser_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.listBoxFollowing.Items.Clear();

        }

        private void buttonAddGame_Click(object sender, EventArgs e)
        {
            panelUser.Size = panelUser.MinimumSize;
            AddGameForm addGame = new AddGameForm();
            addGame.Show();
            addGame.FormClosing += AddGame_FormClosing; 
        }

        private void AddGame_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.listBoxGames.Items.Clear();
            this.listBoxGames.Items.AddRange(Client.utente.GameList.ToArray<String>());
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            Client.Logout();
            this.Close(); 
            login.Show();
        }

        private void listBoxFollowing_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBoxFollowing.SelectedItem!= null)
            {
                var user = listBoxFollowing.SelectedItem.ToString();
                listBoxFollowing.ClearSelected();
                //controlla se la chat è già aperta
                if (chatOpened==null || !chatOpened.ReceiverName.Equals(user))
                {
                    ChatForm chat = new ChatForm(user);
                    chatOpened = chat;
                    chat.Show();

                    chat.FormClosing += Chat_FormClosing;
                    
                }
            }
           
        }

        private void Chat_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (!Client.utente.ChatList.Contains(chatOpened.ReceiverName))
            {
                listBoxChat.Items.Add(chatOpened.ReceiverName);
                
            }
            chatOpened=null;
            
        }

        private void listBoxGames_MouseClick(object sender, MouseEventArgs e)
        {
            if (listBoxGames.SelectedItem != null)
            {
                var game = listBoxGames.SelectedItem.ToString();
                listBoxFollowing.Items.Clear();
                if (Client.utente.SharedGames.ContainsKey(game))
                    listBoxFollowing.Items.AddRange(Client.utente.SharedGames[game].ToArray());
            }
        }

        private void buttonGraph_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process cmd = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = @"/c python graph.py" +" "+ Client.utente.UserID +" "+ Client.utente.Nick;
            startInfo.CreateNoWindow = true;
            startInfo.WorkingDirectory = "C:\\Users\\feder\\Documents\\UProjects\\APL\\GameProject\\python";
            cmd.StartInfo = startInfo;
            cmd.Start();
            cmd.WaitForExit();
        }

        private void listBoxChat_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBoxChat.SelectedItem != null)
            {
                var user = listBoxChat.SelectedItem.ToString();
                listBoxChat.ClearSelected();
                //controlla se la chat è già aperta
                if (chatOpened == null || !chatOpened.ReceiverName.Equals(user))
                {
                    ChatForm chat = new ChatForm(user);
                    chatOpened = chat;
                    chat.Show();

                    chat.FormClosing += Chat_FormClosing;

                }
            }
        }

        private void HomeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Client.Logout();            
            login.Show();
        }
    }
}
