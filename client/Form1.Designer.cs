namespace client
{
    partial class RegForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxNick = new System.Windows.Forms.TextBox();
            this.textBoxPsw = new System.Windows.Forms.TextBox();
            this.textBoxPsw2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonRegister = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonLog = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBoxNick
            // 
            this.textBoxNick.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxNick.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxNick.Location = new System.Drawing.Point(125, 269);
            this.textBoxNick.Name = "textBoxNick";
            this.textBoxNick.Size = new System.Drawing.Size(325, 32);
            this.textBoxNick.TabIndex = 0;
            this.textBoxNick.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxPsw
            // 
            this.textBoxPsw.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPsw.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxPsw.Location = new System.Drawing.Point(125, 369);
            this.textBoxPsw.Name = "textBoxPsw";
            this.textBoxPsw.PasswordChar = '*';
            this.textBoxPsw.Size = new System.Drawing.Size(325, 32);
            this.textBoxPsw.TabIndex = 1;
            this.textBoxPsw.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxPsw2
            // 
            this.textBoxPsw2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPsw2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxPsw2.Location = new System.Drawing.Point(125, 471);
            this.textBoxPsw2.Name = "textBoxPsw2";
            this.textBoxPsw2.PasswordChar = '*';
            this.textBoxPsw2.Size = new System.Drawing.Size(325, 32);
            this.textBoxPsw2.TabIndex = 2;
            this.textBoxPsw2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 13.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(147, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(281, 100);
            this.label1.TabIndex = 3;
            this.label1.Text = "Nuovo utente? \r\nRegistrati!";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonRegister
            // 
            this.buttonRegister.Location = new System.Drawing.Point(192, 541);
            this.buttonRegister.Name = "buttonRegister";
            this.buttonRegister.Size = new System.Drawing.Size(207, 60);
            this.buttonRegister.TabIndex = 4;
            this.buttonRegister.Text = "Registrati";
            this.buttonRegister.UseVisualStyleBackColor = true;
            this.buttonRegister.Click += new System.EventHandler(this.buttonRegister_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(125, 234);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(274, 32);
            this.label2.TabIndex = 5;
            this.label2.Text = "Inserisci il tuo Nickname";
            // 
            // buttonLog
            // 
            this.buttonLog.Location = new System.Drawing.Point(125, 629);
            this.buttonLog.Name = "buttonLog";
            this.buttonLog.Size = new System.Drawing.Size(340, 60);
            this.buttonLog.TabIndex = 6;
            this.buttonLog.Text = "Hai già un account? Log In";
            this.buttonLog.UseVisualStyleBackColor = true;
            this.buttonLog.Click += new System.EventHandler(this.buttonLog_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(125, 334);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(251, 32);
            this.label3.TabIndex = 7;
            this.label3.Text = "Inserisci una password";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(125, 436);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(206, 32);
            this.label4.TabIndex = 8;
            this.label4.Text = "Ripeti la password";
            // 
            // RegForm
            // 
            this.AcceptButton = this.buttonRegister;
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 729);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonLog);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonRegister);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxPsw2);
            this.Controls.Add(this.textBoxPsw);
            this.Controls.Add(this.textBoxNick);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "RegForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Registrazione";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox textBoxNick;
        private TextBox textBoxPsw;
        private TextBox textBoxPsw2;
        private Label label1;
        private Button buttonRegister;
        private Label label2;
        private Button buttonLog;
        private Label label3;
        private Label label4;
    }
}