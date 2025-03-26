namespace MyBalanceProject
{
    partial class FormStart
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
            this.buttonLogOk = new System.Windows.Forms.Button();
            this.textBoxLogin = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxSaveLog = new System.Windows.Forms.TextBox();
            this.buttonSaveLog = new System.Windows.Forms.Button();
            this.textBoxServerInfo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownSoket = new System.Windows.Forms.NumericUpDown();
            this.textBoxIpAdress = new System.Windows.Forms.TextBox();
            this.buttonSend = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSoket)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Введите свой логин";
            // 
            // buttonLogOk
            // 
            this.buttonLogOk.Enabled = false;
            this.buttonLogOk.Location = new System.Drawing.Point(389, 69);
            this.buttonLogOk.Name = "buttonLogOk";
            this.buttonLogOk.Size = new System.Drawing.Size(75, 23);
            this.buttonLogOk.TabIndex = 1;
            this.buttonLogOk.Text = "ОК";
            this.buttonLogOk.UseVisualStyleBackColor = true;
            this.buttonLogOk.Click += new System.EventHandler(this.buttonLogOk_Click);
            // 
            // textBoxLogin
            // 
            this.textBoxLogin.Location = new System.Drawing.Point(126, 71);
            this.textBoxLogin.Name = "textBoxLogin";
            this.textBoxLogin.Size = new System.Drawing.Size(257, 20);
            this.textBoxLogin.TabIndex = 2;
            this.textBoxLogin.TextChanged += new System.EventHandler(this.textBoxLogin_TextChanged);
            this.textBoxLogin.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxLogin_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Сохранить логин";
            // 
            // textBoxSaveLog
            // 
            this.textBoxSaveLog.Location = new System.Drawing.Point(126, 108);
            this.textBoxSaveLog.Name = "textBoxSaveLog";
            this.textBoxSaveLog.Size = new System.Drawing.Size(257, 20);
            this.textBoxSaveLog.TabIndex = 4;
            this.textBoxSaveLog.TextChanged += new System.EventHandler(this.textBoxSaveLog_TextChanged);
            // 
            // buttonSaveLog
            // 
            this.buttonSaveLog.Enabled = false;
            this.buttonSaveLog.Location = new System.Drawing.Point(389, 106);
            this.buttonSaveLog.Name = "buttonSaveLog";
            this.buttonSaveLog.Size = new System.Drawing.Size(75, 23);
            this.buttonSaveLog.TabIndex = 5;
            this.buttonSaveLog.Text = "Save";
            this.buttonSaveLog.UseVisualStyleBackColor = true;
            this.buttonSaveLog.Click += new System.EventHandler(this.buttonSaveLog_Click);
            // 
            // textBoxServerInfo
            // 
            this.textBoxServerInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxServerInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxServerInfo.Enabled = false;
            this.textBoxServerInfo.Location = new System.Drawing.Point(12, 12);
            this.textBoxServerInfo.Multiline = true;
            this.textBoxServerInfo.Name = "textBoxServerInfo";
            this.textBoxServerInfo.Size = new System.Drawing.Size(452, 49);
            this.textBoxServerInfo.TabIndex = 6;
            this.textBoxServerInfo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 144);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "IP Adress Server";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(279, 144);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Soket";
            // 
            // numericUpDownSoket
            // 
            this.numericUpDownSoket.Location = new System.Drawing.Point(320, 142);
            this.numericUpDownSoket.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownSoket.Name = "numericUpDownSoket";
            this.numericUpDownSoket.Size = new System.Drawing.Size(63, 20);
            this.numericUpDownSoket.TabIndex = 9;
            this.numericUpDownSoket.KeyUp += new System.Windows.Forms.KeyEventHandler(this.numericUpDownSoket_KeyUp);
            // 
            // textBoxIpAdress
            // 
            this.textBoxIpAdress.Location = new System.Drawing.Point(126, 141);
            this.textBoxIpAdress.Name = "textBoxIpAdress";
            this.textBoxIpAdress.Size = new System.Drawing.Size(147, 20);
            this.textBoxIpAdress.TabIndex = 10;
            this.textBoxIpAdress.TextChanged += new System.EventHandler(this.textBoxIpAdress_TextChanged);
            // 
            // buttonSend
            // 
            this.buttonSend.Location = new System.Drawing.Point(389, 140);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(75, 23);
            this.buttonSend.TabIndex = 11;
            this.buttonSend.Text = "Отправить";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // FormStart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 175);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.textBoxIpAdress);
            this.Controls.Add(this.numericUpDownSoket);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxServerInfo);
            this.Controls.Add(this.buttonSaveLog);
            this.Controls.Add(this.textBoxSaveLog);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxLogin);
            this.Controls.Add(this.buttonLogOk);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormStart";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Авторизация";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormStart_FormClosing);
            this.Shown += new System.EventHandler(this.FormStart_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSoket)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonLogOk;
        private System.Windows.Forms.TextBox textBoxLogin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxSaveLog;
        private System.Windows.Forms.Button buttonSaveLog;
        private System.Windows.Forms.TextBox textBoxServerInfo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDownSoket;
        private System.Windows.Forms.TextBox textBoxIpAdress;
        private System.Windows.Forms.Button buttonSend;
    }
}

