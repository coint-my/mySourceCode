namespace Balance_My
{
    partial class FormDebitMount
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
            this.listBoxMoundth = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // listBoxMoundth
            // 
            this.listBoxMoundth.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxMoundth.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listBoxMoundth.FormattingEnabled = true;
            this.listBoxMoundth.ItemHeight = 25;
            this.listBoxMoundth.Location = new System.Drawing.Point(24, 23);
            this.listBoxMoundth.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.listBoxMoundth.Name = "listBoxMoundth";
            this.listBoxMoundth.Size = new System.Drawing.Size(556, 329);
            this.listBoxMoundth.TabIndex = 0;
            this.listBoxMoundth.DoubleClick += new System.EventHandler(this.listBoxMoundth_DoubleClick);
            // 
            // FormDebitMount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(608, 413);
            this.Controls.Add(this.listBoxMoundth);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDebitMount";
            this.Text = "FormDebitMount";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxMoundth;
    }
}