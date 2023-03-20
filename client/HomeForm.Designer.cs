namespace client
{
    partial class HomeForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listBoxGames = new System.Windows.Forms.ListBox();
            this.listBoxFollowing = new System.Windows.Forms.ListBox();
            this.panelUser = new System.Windows.Forms.Panel();
            this.buttonLogout = new System.Windows.Forms.Button();
            this.buttonFindUser = new System.Windows.Forms.Button();
            this.buttonAddGame = new System.Windows.Forms.Button();
            this.labelUser = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelGiochiFollowing = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.listBoxChat = new System.Windows.Forms.ListBox();
            this.buttonChat = new System.Windows.Forms.Button();
            this.panelUser.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanelGiochiFollowing.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(659, 56);
            this.label1.TabIndex = 0;
            this.label1.Text = "Giochi";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(3, 405);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(659, 56);
            this.label2.TabIndex = 1;
            this.label2.Text = "Following";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // listBoxGames
            // 
            this.listBoxGames.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxGames.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.listBoxGames.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBoxGames.FormattingEnabled = true;
            this.listBoxGames.ItemHeight = 37;
            this.listBoxGames.Location = new System.Drawing.Point(3, 59);
            this.listBoxGames.Name = "listBoxGames";
            this.listBoxGames.Size = new System.Drawing.Size(659, 333);
            this.listBoxGames.TabIndex = 2;
            this.listBoxGames.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listBoxGames_MouseClick);
            // 
            // listBoxFollowing
            // 
            this.listBoxFollowing.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxFollowing.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.listBoxFollowing.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBoxFollowing.FormattingEnabled = true;
            this.listBoxFollowing.ItemHeight = 37;
            this.listBoxFollowing.Location = new System.Drawing.Point(3, 464);
            this.listBoxFollowing.Name = "listBoxFollowing";
            this.listBoxFollowing.Size = new System.Drawing.Size(659, 333);
            this.listBoxFollowing.TabIndex = 3;
            this.listBoxFollowing.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxFollowing_MouseDoubleClick);
            // 
            // panelUser
            // 
            this.panelUser.BackColor = System.Drawing.Color.SteelBlue;
            this.panelUser.Controls.Add(this.buttonLogout);
            this.panelUser.Controls.Add(this.buttonFindUser);
            this.panelUser.Controls.Add(this.buttonAddGame);
            this.panelUser.Controls.Add(this.labelUser);
            this.panelUser.Location = new System.Drawing.Point(1016, 32);
            this.panelUser.MaximumSize = new System.Drawing.Size(301, 324);
            this.panelUser.MinimumSize = new System.Drawing.Size(301, 56);
            this.panelUser.Name = "panelUser";
            this.panelUser.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.panelUser.Size = new System.Drawing.Size(301, 56);
            this.panelUser.TabIndex = 2;
            // 
            // buttonLogout
            // 
            this.buttonLogout.BackColor = System.Drawing.SystemColors.HotTrack;
            this.buttonLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonLogout.Font = new System.Drawing.Font("Segoe UI", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonLogout.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonLogout.Location = new System.Drawing.Point(0, 234);
            this.buttonLogout.Name = "buttonLogout";
            this.buttonLogout.Size = new System.Drawing.Size(301, 89);
            this.buttonLogout.TabIndex = 3;
            this.buttonLogout.Text = "Log Out";
            this.buttonLogout.UseVisualStyleBackColor = false;
            this.buttonLogout.Click += new System.EventHandler(this.buttonLogout_Click);
            // 
            // buttonFindUser
            // 
            this.buttonFindUser.BackColor = System.Drawing.SystemColors.HotTrack;
            this.buttonFindUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonFindUser.Font = new System.Drawing.Font("Segoe UI", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonFindUser.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonFindUser.Location = new System.Drawing.Point(0, 146);
            this.buttonFindUser.Name = "buttonFindUser";
            this.buttonFindUser.Size = new System.Drawing.Size(301, 89);
            this.buttonFindUser.TabIndex = 2;
            this.buttonFindUser.Text = "Cerca utenti";
            this.buttonFindUser.UseVisualStyleBackColor = false;
            this.buttonFindUser.Click += new System.EventHandler(this.buttonFindUser_Click);
            // 
            // buttonAddGame
            // 
            this.buttonAddGame.BackColor = System.Drawing.SystemColors.HotTrack;
            this.buttonAddGame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAddGame.Font = new System.Drawing.Font("Segoe UI", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonAddGame.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonAddGame.Location = new System.Drawing.Point(0, 58);
            this.buttonAddGame.Name = "buttonAddGame";
            this.buttonAddGame.Size = new System.Drawing.Size(301, 89);
            this.buttonAddGame.TabIndex = 1;
            this.buttonAddGame.Text = "Aggiungi gioco";
            this.buttonAddGame.UseVisualStyleBackColor = false;
            this.buttonAddGame.Click += new System.EventHandler(this.buttonAddGame_Click);
            // 
            // labelUser
            // 
            this.labelUser.AutoSize = true;
            this.labelUser.Dock = System.Windows.Forms.DockStyle.Right;
            this.labelUser.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelUser.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labelUser.Location = new System.Drawing.Point(301, 5);
            this.labelUser.Name = "labelUser";
            this.labelUser.Size = new System.Drawing.Size(0, 45);
            this.labelUser.TabIndex = 0;
            this.labelUser.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelUser.MouseHover += new System.EventHandler(this.labelUser_MouseHover);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanelGiochiFollowing, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 94);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1343, 819);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // tableLayoutPanelGiochiFollowing
            // 
            this.tableLayoutPanelGiochiFollowing.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.tableLayoutPanelGiochiFollowing.ColumnCount = 1;
            this.tableLayoutPanelGiochiFollowing.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelGiochiFollowing.Controls.Add(this.listBoxFollowing, 0, 3);
            this.tableLayoutPanelGiochiFollowing.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanelGiochiFollowing.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanelGiochiFollowing.Controls.Add(this.listBoxGames, 0, 1);
            this.tableLayoutPanelGiochiFollowing.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelGiochiFollowing.Font = new System.Drawing.Font("Segoe UI", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tableLayoutPanelGiochiFollowing.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanelGiochiFollowing.Name = "tableLayoutPanelGiochiFollowing";
            this.tableLayoutPanelGiochiFollowing.RowCount = 4;
            this.tableLayoutPanelGiochiFollowing.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tableLayoutPanelGiochiFollowing.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 43F));
            this.tableLayoutPanelGiochiFollowing.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tableLayoutPanelGiochiFollowing.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 43F));
            this.tableLayoutPanelGiochiFollowing.Size = new System.Drawing.Size(665, 813);
            this.tableLayoutPanelGiochiFollowing.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.listBoxChat, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.buttonChat, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Font = new System.Drawing.Font("Segoe UI", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(674, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.134071F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 92.86593F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(666, 813);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(660, 54);
            this.label3.TabIndex = 0;
            this.label3.Text = "Chat";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // listBoxChat
            // 
            this.listBoxChat.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxChat.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBoxChat.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.listBoxChat.FormattingEnabled = true;
            this.listBoxChat.ItemHeight = 80;
            this.listBoxChat.Location = new System.Drawing.Point(3, 57);
            this.listBoxChat.Name = "listBoxChat";
            this.listBoxChat.Size = new System.Drawing.Size(660, 640);
            this.listBoxChat.TabIndex = 1;
            // 
            // buttonChat
            // 
            this.buttonChat.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonChat.BackColor = System.Drawing.Color.AliceBlue;
            this.buttonChat.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonChat.Location = new System.Drawing.Point(3, 760);
            this.buttonChat.Name = "buttonChat";
            this.buttonChat.Size = new System.Drawing.Size(660, 50);
            this.buttonChat.TabIndex = 2;
            this.buttonChat.Text = "Apri chat";
            this.buttonChat.UseVisualStyleBackColor = false;
            // 
            // HomeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.ClientSize = new System.Drawing.Size(1343, 913);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.panelUser);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "HomeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Game Project";
            this.panelUser.ResumeLayout(false);
            this.panelUser.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanelGiochiFollowing.ResumeLayout(false);
            this.tableLayoutPanelGiochiFollowing.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private Label labelUser;
        private Panel panelUser;
        private Button buttonLogout;
        private Button buttonFindUser;
        private Button buttonAddGame;
        private Label label1;
        private Label label2;
        private ListBox listBoxGames;
        private ListBox listBoxFollowing;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tableLayoutPanelGiochiFollowing;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label3;
        private ListBox listBoxChat;
        private Button buttonChat;
    }
}