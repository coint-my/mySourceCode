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
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Введите свой логин";
            // 
            // buttonLogOk
            // 
            this.buttonLogOk.Enabled = false;
            this.buttonLogOk.Location = new System.Drawing.Point(386, 24);
            this.buttonLogOk.Name = "buttonLogOk";
            this.buttonLogOk.Size = new System.Drawing.Size(75, 23);
            this.buttonLogOk.TabIndex = 1;
            this.buttonLogOk.Text = "ОК";
            this.buttonLogOk.UseVisualStyleBackColor = true;
            this.buttonLogOk.Click += new System.EventHandler(this.buttonLogOk_Click);
            // 
            // textBoxLogin
            // 
            this.textBoxLogin.Location = new System.Drawing.Point(123, 26);
            this.textBoxLogin.Name = "textBoxLogin";
            this.textBoxLogin.Size = new System.Drawing.Size(257, 20);
            this.textBoxLogin.TabIndex = 2;
            this.textBoxLogin.TextChanged += new System.EventHandler(this.textBoxLogin_TextChanged);
            this.textBoxLogin.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxLogin_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Сохранить логин";
            // 
            // textBoxSaveLog
            // 
            this.textBoxSaveLog.Location = new System.Drawing.Point(123, 63);
            this.textBoxSaveLog.Name = "textBoxSaveLog";
            this.textBoxSaveLog.Size = new System.Drawing.Size(257, 20);
            this.textBoxSaveLog.TabIndex = 4;
            this.textBoxSaveLog.TextChanged += new System.EventHandler(this.textBoxSaveLog_TextChanged);
            // 
            // buttonSaveLog
            // 
            this.buttonSaveLog.Enabled = false;
            this.buttonSaveLog.Location = new System.Drawing.Point(386, 61);
            this.buttonSaveLog.Name = "buttonSaveLog";
            this.buttonSaveLog.Size = new System.Drawing.Size(75, 23);
            this.buttonSaveLog.TabIndex = 5;
            this.buttonSaveLog.Text = "Save";
            this.buttonSaveLog.UseVisualStyleBackColor = true;
            this.buttonSaveLog.Click += new System.EventHandler(this.buttonSaveLog_Click);
            // 
            // FormStart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 98);
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
            this.Text = "Form1";
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
    }
}

