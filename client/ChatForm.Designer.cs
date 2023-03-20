namespace client
{
    partial class ChatForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labelNickname = new System.Windows.Forms.Label();
            this.richTextBoxChat = new System.Windows.Forms.RichTextBox();
            this.buttonSend = new System.Windows.Forms.Button();
            this.richTextBoxMsg = new System.Windows.Forms.RichTextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.SteelBlue;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Controls.Add(this.labelNickname, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.richTextBoxChat, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.buttonSend, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.richTextBoxMsg, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 17.88963F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 82.11037F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 170F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1363, 996);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // labelNickname
            // 
            this.labelNickname.AutoSize = true;
            this.labelNickname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelNickname.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelNickname.Font = new System.Drawing.Font("Segoe UI Semibold", 13.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelNickname.ForeColor = System.Drawing.Color.LightCyan;
            this.labelNickname.Location = new System.Drawing.Point(3, 0);
            this.labelNickname.Name = "labelNickname";
            this.labelNickname.Size = new System.Drawing.Size(1084, 147);
            this.labelNickname.TabIndex = 2;
            this.labelNickname.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelNickname.UseMnemonic = false;
            // 
            // richTextBoxChat
            // 
            this.richTextBoxChat.BackColor = System.Drawing.Color.AliceBlue;
            this.richTextBoxChat.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tableLayoutPanel1.SetColumnSpan(this.richTextBoxChat, 2);
            this.richTextBoxChat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxChat.Location = new System.Drawing.Point(3, 150);
            this.richTextBoxChat.Name = "richTextBoxChat";
            this.richTextBoxChat.ReadOnly = true;
            this.richTextBoxChat.Size = new System.Drawing.Size(1357, 672);
            this.richTextBoxChat.TabIndex = 3;
            this.richTextBoxChat.Text = "";
            // 
            // buttonSend
            // 
            this.buttonSend.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonSend.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.buttonSend.ForeColor = System.Drawing.Color.SteelBlue;
            this.buttonSend.Location = new System.Drawing.Point(1120, 864);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(212, 93);
            this.buttonSend.TabIndex = 4;
            this.buttonSend.Text = "Invia";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // richTextBoxMsg
            // 
            this.richTextBoxMsg.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.richTextBoxMsg.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBoxMsg.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.richTextBoxMsg.Location = new System.Drawing.Point(36, 837);
            this.richTextBoxMsg.Name = "richTextBoxMsg";
            this.richTextBoxMsg.Size = new System.Drawing.Size(1018, 147);
            this.richTextBoxMsg.TabIndex = 5;
            this.richTextBoxMsg.Text = "";
            // 
            // ChatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1366, 996);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ChatForm";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Label labelNickname;
        private RichTextBox richTextBoxChat;
        private Button buttonSend;
        private RichTextBox richTextBoxMsg;
    }
}