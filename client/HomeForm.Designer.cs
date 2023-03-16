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
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listBoxGames = new System.Windows.Forms.ListBox();
            this.listBoxFollowing = new System.Windows.Forms.ListBox();
            this.panelUser = new System.Windows.Forms.Panel();
            this.buttonLogout = new System.Windows.Forms.Button();
            this.buttonFindUser = new System.Windows.Forms.Button();
            this.buttonAddGame = new System.Windows.Forms.Button();
            this.labelUser = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelUser.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 90);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1343, 823);
            this.panel1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.listBoxGames, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.listBoxFollowing, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Font = new System.Drawing.Font("Segoe UI", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1343, 823);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 37);
            this.label1.TabIndex = 0;
            this.label1.Text = "Giochi";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(674, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 37);
            this.label2.TabIndex = 1;
            this.label2.Text = "Following";
            // 
            // listBoxGames
            // 
            this.listBoxGames.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxGames.FormattingEnabled = true;
            this.listBoxGames.ItemHeight = 37;
            this.listBoxGames.Location = new System.Drawing.Point(3, 40);
            this.listBoxGames.Name = "listBoxGames";
            this.listBoxGames.Size = new System.Drawing.Size(665, 780);
            this.listBoxGames.TabIndex = 2;
            // 
            // listBoxFollowing
            // 
            this.listBoxFollowing.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxFollowing.FormattingEnabled = true;
            this.listBoxFollowing.ItemHeight = 37;
            this.listBoxFollowing.Location = new System.Drawing.Point(674, 40);
            this.listBoxFollowing.Name = "listBoxFollowing";
            this.listBoxFollowing.Size = new System.Drawing.Size(666, 780);
            this.listBoxFollowing.TabIndex = 3;
            // 
            // panelUser
            // 
            this.panelUser.BackColor = System.Drawing.SystemColors.HotTrack;
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
            // HomeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HotTrack;
            this.ClientSize = new System.Drawing.Size(1343, 913);
            this.Controls.Add(this.panelUser);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "HomeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Game Project";
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panelUser.ResumeLayout(false);
            this.panelUser.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private Label labelUser;
        private Panel panelUser;
        private Button buttonLogout;
        private Button buttonFindUser;
        private Button buttonAddGame;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label1;
        private Label label2;
        private ListBox listBoxGames;
        private ListBox listBoxFollowing;
    }
}