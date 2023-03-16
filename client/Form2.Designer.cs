namespace client
{
    partial class LogForm
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
            this.label3 = new System.Windows.Forms.Label();
            this.buttonLog = new System.Windows.Forms.Button();
            this.buttonRegister = new System.Windows.Forms.Button();
            this.textBoxNick = new System.Windows.Forms.TextBox();
            this.textBoxPsw = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 13.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(169, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(225, 100);
            this.label1.TabIndex = 0;
            this.label1.Text = "Benvenuto! \r\nLog In";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(126, 270);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(274, 32);
            this.label2.TabIndex = 1;
            this.label2.Text = "Inserisci il tuo Nickname";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(120, 378);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(229, 32);
            this.label3.TabIndex = 2;
            this.label3.Text = "Inserisci la password";
            // 
            // buttonLog
            // 
            this.buttonLog.Location = new System.Drawing.Point(210, 541);
            this.buttonLog.Name = "buttonLog";
            this.buttonLog.Size = new System.Drawing.Size(150, 60);
            this.buttonLog.TabIndex = 4;
            this.buttonLog.Text = "Log In";
            this.buttonLog.UseVisualStyleBackColor = true;
            this.buttonLog.Click += new System.EventHandler(this.buttonLog_Click);
            // 
            // buttonRegister
            // 
            this.buttonRegister.Location = new System.Drawing.Point(134, 621);
            this.buttonRegister.Name = "buttonRegister";
            this.buttonRegister.Size = new System.Drawing.Size(300, 60);
            this.buttonRegister.TabIndex = 5;
            this.buttonRegister.Text = "Nuovo Utente? Registrati";
            this.buttonRegister.UseVisualStyleBackColor = true;
            this.buttonRegister.Click += new System.EventHandler(this.buttonRegister_Click);
            // 
            // textBoxNick
            // 
            this.textBoxNick.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxNick.Location = new System.Drawing.Point(126, 305);
            this.textBoxNick.Name = "textBoxNick";
            this.textBoxNick.Size = new System.Drawing.Size(325, 32);
            this.textBoxNick.TabIndex = 0;
            this.textBoxNick.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxPsw
            // 
            this.textBoxPsw.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxPsw.Location = new System.Drawing.Point(126, 413);
            this.textBoxPsw.Name = "textBoxPsw";
            this.textBoxPsw.PasswordChar = '*';
            this.textBoxPsw.Size = new System.Drawing.Size(325, 32);
            this.textBoxPsw.TabIndex = 3;
            this.textBoxPsw.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // LogForm
            // 
            this.AcceptButton = this.buttonLog;
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 729);
            this.Controls.Add(this.textBoxPsw);
            this.Controls.Add(this.textBoxNick);
            this.Controls.Add(this.buttonRegister);
            this.Controls.Add(this.buttonLog);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "LogForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Log In";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LogForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Button buttonLog;
        private Button buttonRegister;
        private TextBox textBoxNick;
        private TextBox textBoxPsw;
    }
}