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
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Введите логин";
            // 
            // buttonLogOk
            // 
            this.buttonLogOk.Enabled = false;
            this.buttonLogOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonLogOk.Location = new System.Drawing.Point(454, 70);
            this.buttonLogOk.Name = "buttonLogOk";
            this.buttonLogOk.Size = new System.Drawing.Size(88, 34);
            this.buttonLogOk.TabIndex = 1;
            this.buttonLogOk.Text = "ОК";
            this.buttonLogOk.UseVisualStyleBackColor = true;
            this.buttonLogOk.Click += new System.EventHandler(this.buttonLogOk_Click);
            // 
            // textBoxLogin
            // 
            this.textBoxLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxLogin.Location = new System.Drawing.Point(191, 73);
            this.textBoxLogin.Name = "textBoxLogin";
            this.textBoxLogin.Size = new System.Drawing.Size(257, 29);
            this.textBoxLogin.TabIndex = 2;
            this.textBoxLogin.TextChanged += new System.EventHandler(this.textBoxLogin_TextChanged);
            this.textBoxLogin.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxLogin_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(12, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(165, 24);
            this.label2.TabIndex = 3;
            this.label2.Text = "Сохранить логин";
            // 
            // textBoxSaveLog
            // 
            this.textBoxSaveLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxSaveLog.Location = new System.Drawing.Point(191, 110);
            this.textBoxSaveLog.Name = "textBoxSaveLog";
            this.textBoxSaveLog.Size = new System.Drawing.Size(257, 29);
            this.textBoxSaveLog.TabIndex = 4;
            this.textBoxSaveLog.TextChanged += new System.EventHandler(this.textBoxSaveLog_TextChanged);
            // 
            // buttonSaveLog
            // 
            this.buttonSaveLog.Enabled = false;
            this.buttonSaveLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonSaveLog.Location = new System.Drawing.Point(454, 107);
            this.buttonSaveLog.Name = "buttonSaveLog";
            this.buttonSaveLog.Size = new System.Drawing.Size(88, 34);
            this.buttonSaveLog.TabIndex = 5;
            this.buttonSaveLog.Text = "Save";
            this.buttonSaveLog.UseVisualStyleBackColor = true;
            this.buttonSaveLog.Click += new System.EventHandler(this.buttonSaveLog_Click);
            // 
            // textBoxServerInfo
            // 
            this.textBoxServerInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxServerInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxServerInfo.Enabled = false;
            this.textBoxServerInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxServerInfo.Location = new System.Drawing.Point(12, 12);
            this.textBoxServerInfo.Multiline = true;
            this.textBoxServerInfo.Name = "textBoxServerInfo";
            this.textBoxServerInfo.Size = new System.Drawing.Size(528, 52);
            this.textBoxServerInfo.TabIndex = 6;
            this.textBoxServerInfo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // FormStart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 147);
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
    }
}

