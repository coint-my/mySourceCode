namespace Balance_My
{
    partial class myDialogSwitchDebitCredit
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
            this.radioButtonDebit = new System.Windows.Forms.RadioButton();
            this.radioButtonCredit = new System.Windows.Forms.RadioButton();
            this.labelTittle = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // radioButtonDebit
            // 
            this.radioButtonDebit.AutoSize = true;
            this.radioButtonDebit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioButtonDebit.Location = new System.Drawing.Point(12, 35);
            this.radioButtonDebit.Name = "radioButtonDebit";
            this.radioButtonDebit.Size = new System.Drawing.Size(86, 24);
            this.radioButtonDebit.TabIndex = 0;
            this.radioButtonDebit.Text = "Доходы";
            this.radioButtonDebit.UseVisualStyleBackColor = true;
            // 
            // radioButtonCredit
            // 
            this.radioButtonCredit.AutoSize = true;
            this.radioButtonCredit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioButtonCredit.Location = new System.Drawing.Point(12, 65);
            this.radioButtonCredit.Name = "radioButtonCredit";
            this.radioButtonCredit.Size = new System.Drawing.Size(92, 24);
            this.radioButtonCredit.TabIndex = 2;
            this.radioButtonCredit.Text = "Расходы";
            this.radioButtonCredit.UseVisualStyleBackColor = true;
            // 
            // labelTittle
            // 
            this.labelTittle.AutoSize = true;
            this.labelTittle.Font = new System.Drawing.Font("Arial Black", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelTittle.Location = new System.Drawing.Point(12, 9);
            this.labelTittle.Name = "labelTittle";
            this.labelTittle.Size = new System.Drawing.Size(64, 23);
            this.labelTittle.TabIndex = 2;
            this.labelTittle.Text = "label1";
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonOK.Location = new System.Drawing.Point(12, 104);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(92, 30);
            this.buttonOK.TabIndex = 3;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // myDialogSwitchDebitCredit
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(118, 143);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.labelTittle);
            this.Controls.Add(this.radioButtonCredit);
            this.Controls.Add(this.radioButtonDebit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "myDialogSwitchDebitCredit";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Information";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.myDialogSwitchDebitCredit_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radioButtonDebit;
        private System.Windows.Forms.RadioButton radioButtonCredit;
        private System.Windows.Forms.Label labelTittle;
        private System.Windows.Forms.Button buttonOK;
    }
}