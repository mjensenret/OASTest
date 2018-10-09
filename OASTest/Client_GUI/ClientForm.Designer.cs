namespace Client_GUI
{
    partial class ClientForm
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
            this.txtIPAddress = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.lstServerMessage = new System.Windows.Forms.ListBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtSendText = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtIPAddress
            // 
            this.txtIPAddress.Location = new System.Drawing.Point(13, 13);
            this.txtIPAddress.Name = "txtIPAddress";
            this.txtIPAddress.Size = new System.Drawing.Size(178, 20);
            this.txtIPAddress.TabIndex = 0;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(197, 13);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 1;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // lstServerMessage
            // 
            this.lstServerMessage.FormattingEnabled = true;
            this.lstServerMessage.Location = new System.Drawing.Point(13, 40);
            this.lstServerMessage.Name = "lstServerMessage";
            this.lstServerMessage.Size = new System.Drawing.Size(259, 134);
            this.lstServerMessage.TabIndex = 2;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(197, 180);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 4;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtSendText
            // 
            this.txtSendText.Location = new System.Drawing.Point(13, 180);
            this.txtSendText.Name = "txtSendText";
            this.txtSendText.Size = new System.Drawing.Size(178, 20);
            this.txtSendText.TabIndex = 3;
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 211);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtSendText);
            this.Controls.Add(this.lstServerMessage);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.txtIPAddress);
            this.Name = "ClientForm";
            this.Text = "Client Form";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtIPAddress;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.ListBox lstServerMessage;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtSendText;
    }
}

