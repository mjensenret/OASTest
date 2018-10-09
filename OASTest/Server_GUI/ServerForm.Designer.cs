namespace Server_GUI
{
    partial class ServerForm
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
            this.btnListen = new System.Windows.Forms.Button();
            this.lstClientMessage = new System.Windows.Forms.ListBox();
            this.txtSendText = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnListen
            // 
            this.btnListen.Location = new System.Drawing.Point(13, 13);
            this.btnListen.Name = "btnListen";
            this.btnListen.Size = new System.Drawing.Size(122, 23);
            this.btnListen.TabIndex = 0;
            this.btnListen.Text = "Listen";
            this.btnListen.UseVisualStyleBackColor = true;
            this.btnListen.Click += new System.EventHandler(this.btnListen_Click);
            // 
            // lstClientMessage
            // 
            this.lstClientMessage.FormattingEnabled = true;
            this.lstClientMessage.Location = new System.Drawing.Point(13, 43);
            this.lstClientMessage.Name = "lstClientMessage";
            this.lstClientMessage.Size = new System.Drawing.Size(259, 95);
            this.lstClientMessage.TabIndex = 1;
            // 
            // txtSendText
            // 
            this.txtSendText.Location = new System.Drawing.Point(13, 145);
            this.txtSendText.Name = "txtSendText";
            this.txtSendText.Size = new System.Drawing.Size(178, 20);
            this.txtSendText.TabIndex = 2;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(197, 145);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 3;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // ServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 181);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtSendText);
            this.Controls.Add(this.lstClientMessage);
            this.Controls.Add(this.btnListen);
            this.Name = "ServerForm";
            this.Text = "Server Form";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnListen;
        private System.Windows.Forms.ListBox lstClientMessage;
        private System.Windows.Forms.TextBox txtSendText;
        private System.Windows.Forms.Button btnSend;
    }
}

