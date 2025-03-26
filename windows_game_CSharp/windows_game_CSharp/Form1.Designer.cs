namespace windows_game_CSharp
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.gameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBoxGame = new System.Windows.Forms.GroupBox();
            this.panelMiddle = new System.Windows.Forms.Panel();
            this.panelRight = new System.Windows.Forms.Panel();
            this.pictureBoxRight = new System.Windows.Forms.PictureBox();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.pictureBoxLeft = new System.Windows.Forms.PictureBox();
            this.panelDown = new System.Windows.Forms.Panel();
            this.pictureBoxBottom = new System.Windows.Forms.PictureBox();
            this.panelTop = new System.Windows.Forms.Panel();
            this.pictureBoxTop = new System.Windows.Forms.PictureBox();
            this.buttonFront = new System.Windows.Forms.Button();
            this.buttonBack = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.groupBoxGame.SuspendLayout();
            this.panelMiddle.SuspendLayout();
            this.panelRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRight)).BeginInit();
            this.panelLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLeft)).BeginInit();
            this.panelDown.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBottom)).BeginInit();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTop)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gameToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(671, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // gameToolStripMenuItem
            // 
            this.gameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.gameToolStripMenuItem.Name = "gameToolStripMenuItem";
            this.gameToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.gameToolStripMenuItem.Text = "file";
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(97, 22);
            this.startToolStripMenuItem.Text = "start";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.StartToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(97, 22);
            this.exitToolStripMenuItem.Text = "exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // groupBoxGame
            // 
            this.groupBoxGame.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxGame.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBoxGame.Controls.Add(this.panelMiddle);
            this.groupBoxGame.Controls.Add(this.panelRight);
            this.groupBoxGame.Controls.Add(this.panelLeft);
            this.groupBoxGame.Controls.Add(this.panelDown);
            this.groupBoxGame.Controls.Add(this.panelTop);
            this.groupBoxGame.Location = new System.Drawing.Point(12, 27);
            this.groupBoxGame.Name = "groupBoxGame";
            this.groupBoxGame.Size = new System.Drawing.Size(647, 420);
            this.groupBoxGame.TabIndex = 1;
            this.groupBoxGame.TabStop = false;
            this.groupBoxGame.Text = "Game";
            // 
            // panelMiddle
            // 
            this.panelMiddle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelMiddle.Controls.Add(this.buttonBack);
            this.panelMiddle.Controls.Add(this.buttonFront);
            this.panelMiddle.Location = new System.Drawing.Point(190, 125);
            this.panelMiddle.Name = "panelMiddle";
            this.panelMiddle.Size = new System.Drawing.Size(267, 183);
            this.panelMiddle.TabIndex = 4;
            // 
            // panelRight
            // 
            this.panelRight.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelRight.Controls.Add(this.pictureBoxRight);
            this.panelRight.Location = new System.Drawing.Point(463, 125);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(178, 183);
            this.panelRight.TabIndex = 3;
            // 
            // pictureBoxRight
            // 
            this.pictureBoxRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxRight.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxRight.Name = "pictureBoxRight";
            this.pictureBoxRight.Size = new System.Drawing.Size(178, 183);
            this.pictureBoxRight.TabIndex = 0;
            this.pictureBoxRight.TabStop = false;
            // 
            // panelLeft
            // 
            this.panelLeft.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panelLeft.Controls.Add(this.pictureBoxLeft);
            this.panelLeft.Location = new System.Drawing.Point(6, 125);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(178, 183);
            this.panelLeft.TabIndex = 2;
            // 
            // pictureBoxLeft
            // 
            this.pictureBoxLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxLeft.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxLeft.Name = "pictureBoxLeft";
            this.pictureBoxLeft.Size = new System.Drawing.Size(178, 183);
            this.pictureBoxLeft.TabIndex = 0;
            this.pictureBoxLeft.TabStop = false;
            // 
            // panelDown
            // 
            this.panelDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelDown.Controls.Add(this.pictureBoxBottom);
            this.panelDown.Location = new System.Drawing.Point(6, 314);
            this.panelDown.Name = "panelDown";
            this.panelDown.Size = new System.Drawing.Size(635, 100);
            this.panelDown.TabIndex = 1;
            // 
            // pictureBoxBottom
            // 
            this.pictureBoxBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxBottom.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxBottom.Name = "pictureBoxBottom";
            this.pictureBoxBottom.Size = new System.Drawing.Size(635, 100);
            this.pictureBoxBottom.TabIndex = 0;
            this.pictureBoxBottom.TabStop = false;
            // 
            // panelTop
            // 
            this.panelTop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTop.Controls.Add(this.pictureBoxTop);
            this.panelTop.Location = new System.Drawing.Point(6, 19);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(635, 100);
            this.panelTop.TabIndex = 0;
            // 
            // pictureBoxTop
            // 
            this.pictureBoxTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxTop.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxTop.Name = "pictureBoxTop";
            this.pictureBoxTop.Size = new System.Drawing.Size(635, 100);
            this.pictureBoxTop.TabIndex = 0;
            this.pictureBoxTop.TabStop = false;
            // 
            // buttonFront
            // 
            this.buttonFront.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonFront.Location = new System.Drawing.Point(3, 3);
            this.buttonFront.Name = "buttonFront";
            this.buttonFront.Size = new System.Drawing.Size(261, 47);
            this.buttonFront.TabIndex = 0;
            this.buttonFront.Text = "Вперёд";
            this.buttonFront.UseVisualStyleBackColor = true;
            // 
            // buttonBack
            // 
            this.buttonBack.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBack.Location = new System.Drawing.Point(3, 133);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(261, 47);
            this.buttonBack.TabIndex = 1;
            this.buttonBack.Text = "Назад";
            this.buttonBack.UseVisualStyleBackColor = true;        
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(671, 521);
            this.Controls.Add(this.groupBoxGame);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBoxGame.ResumeLayout(false);
            this.panelMiddle.ResumeLayout(false);
            this.panelRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRight)).EndInit();
            this.panelLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLeft)).EndInit();
            this.panelDown.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBottom)).EndInit();
            this.panelTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTop)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem gameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBoxGame;
        private System.Windows.Forms.Panel panelMiddle;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Panel panelDown;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.PictureBox pictureBoxTop;
        private System.Windows.Forms.PictureBox pictureBoxLeft;
        private System.Windows.Forms.PictureBox pictureBoxRight;
        private System.Windows.Forms.PictureBox pictureBoxBottom;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.Button buttonFront;
    }
}

