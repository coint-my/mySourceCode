namespace C_WindowsFormAndOpenTK
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
            this.panelOpenTK = new System.Windows.Forms.Panel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.numericScaleX = new System.Windows.Forms.NumericUpDown();
            this.numericScaleZ = new System.Windows.Forms.NumericUpDown();
            this.numericScaleY = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.numericRotationX = new System.Windows.Forms.NumericUpDown();
            this.numericRotationZ = new System.Windows.Forms.NumericUpDown();
            this.numericRotationY = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numericPositionX = new System.Windows.Forms.NumericUpDown();
            this.numericPositionZ = new System.Windows.Forms.NumericUpDown();
            this.numericPositionY = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.listBoxGameObjects = new System.Windows.Forms.ListBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericScaleX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericScaleZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericScaleY)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericRotationX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericRotationZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericRotationY)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericPositionX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericPositionZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericPositionY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelOpenTK
            // 
            this.panelOpenTK.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelOpenTK.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panelOpenTK.Location = new System.Drawing.Point(12, 38);
            this.panelOpenTK.Name = "panelOpenTK";
            this.panelOpenTK.Size = new System.Drawing.Size(565, 503);
            this.panelOpenTK.TabIndex = 2;
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.numericScaleX);
            this.groupBox4.Controls.Add(this.numericScaleZ);
            this.groupBox4.Controls.Add(this.numericScaleY);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox4.Location = new System.Drawing.Point(173, 189);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(82, 108);
            this.groupBox4.TabIndex = 9;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Scale";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 24);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(16, 16);
            this.label7.TabIndex = 2;
            this.label7.Text = "X";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 76);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(16, 16);
            this.label8.TabIndex = 6;
            this.label8.Text = "Z";
            // 
            // numericScaleX
            // 
            this.numericScaleX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numericScaleX.DecimalPlaces = 1;
            this.numericScaleX.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericScaleX.Location = new System.Drawing.Point(26, 22);
            this.numericScaleX.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericScaleX.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.numericScaleX.Name = "numericScaleX";
            this.numericScaleX.Size = new System.Drawing.Size(46, 22);
            this.numericScaleX.TabIndex = 1;
            this.numericScaleX.ValueChanged += new System.EventHandler(this.numericScaleX_ValueChanged);
            // 
            // numericScaleZ
            // 
            this.numericScaleZ.DecimalPlaces = 1;
            this.numericScaleZ.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericScaleZ.Location = new System.Drawing.Point(26, 74);
            this.numericScaleZ.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericScaleZ.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.numericScaleZ.Name = "numericScaleZ";
            this.numericScaleZ.Size = new System.Drawing.Size(46, 22);
            this.numericScaleZ.TabIndex = 5;
            this.numericScaleZ.ValueChanged += new System.EventHandler(this.numericScaleZ_ValueChanged);
            // 
            // numericScaleY
            // 
            this.numericScaleY.DecimalPlaces = 1;
            this.numericScaleY.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericScaleY.Location = new System.Drawing.Point(26, 48);
            this.numericScaleY.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericScaleY.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.numericScaleY.Name = "numericScaleY";
            this.numericScaleY.Size = new System.Drawing.Size(46, 22);
            this.numericScaleY.TabIndex = 3;
            this.numericScaleY.ValueChanged += new System.EventHandler(this.numericScaleY_ValueChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 50);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(17, 16);
            this.label9.TabIndex = 4;
            this.label9.Text = "Y";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.numericRotationX);
            this.groupBox3.Controls.Add(this.numericRotationZ);
            this.groupBox3.Controls.Add(this.numericRotationY);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox3.Location = new System.Drawing.Point(90, 189);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(82, 108);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Rotation";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(16, 16);
            this.label4.TabIndex = 2;
            this.label4.Text = "X";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 76);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(16, 16);
            this.label5.TabIndex = 6;
            this.label5.Text = "Z";
            // 
            // numericRotationX
            // 
            this.numericRotationX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numericRotationX.DecimalPlaces = 1;
            this.numericRotationX.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericRotationX.Location = new System.Drawing.Point(26, 22);
            this.numericRotationX.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericRotationX.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.numericRotationX.Name = "numericRotationX";
            this.numericRotationX.Size = new System.Drawing.Size(46, 22);
            this.numericRotationX.TabIndex = 1;
            this.numericRotationX.ValueChanged += new System.EventHandler(this.numericRotationX_ValueChanged);
            // 
            // numericRotationZ
            // 
            this.numericRotationZ.DecimalPlaces = 1;
            this.numericRotationZ.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericRotationZ.Location = new System.Drawing.Point(26, 74);
            this.numericRotationZ.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericRotationZ.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.numericRotationZ.Name = "numericRotationZ";
            this.numericRotationZ.Size = new System.Drawing.Size(46, 22);
            this.numericRotationZ.TabIndex = 5;
            this.numericRotationZ.ValueChanged += new System.EventHandler(this.numericRotationZ_ValueChanged);
            // 
            // numericRotationY
            // 
            this.numericRotationY.DecimalPlaces = 1;
            this.numericRotationY.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericRotationY.Location = new System.Drawing.Point(26, 48);
            this.numericRotationY.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericRotationY.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.numericRotationY.Name = "numericRotationY";
            this.numericRotationY.Size = new System.Drawing.Size(46, 22);
            this.numericRotationY.TabIndex = 3;
            this.numericRotationY.ValueChanged += new System.EventHandler(this.numericRotationY_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 16);
            this.label6.TabIndex = 4;
            this.label6.Text = "Y";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.numericPositionX);
            this.groupBox2.Controls.Add(this.numericPositionZ);
            this.groupBox2.Controls.Add(this.numericPositionY);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox2.Location = new System.Drawing.Point(8, 189);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(82, 108);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Position";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "X";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(16, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "Z";
            // 
            // numericPositionX
            // 
            this.numericPositionX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numericPositionX.DecimalPlaces = 1;
            this.numericPositionX.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericPositionX.Location = new System.Drawing.Point(26, 22);
            this.numericPositionX.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericPositionX.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.numericPositionX.Name = "numericPositionX";
            this.numericPositionX.Size = new System.Drawing.Size(46, 22);
            this.numericPositionX.TabIndex = 1;
            this.numericPositionX.ValueChanged += new System.EventHandler(this.numericUpDownX_ValueChanged);
            // 
            // numericPositionZ
            // 
            this.numericPositionZ.DecimalPlaces = 1;
            this.numericPositionZ.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericPositionZ.Location = new System.Drawing.Point(26, 74);
            this.numericPositionZ.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericPositionZ.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.numericPositionZ.Name = "numericPositionZ";
            this.numericPositionZ.Size = new System.Drawing.Size(46, 22);
            this.numericPositionZ.TabIndex = 5;
            this.numericPositionZ.ValueChanged += new System.EventHandler(this.numericUpDownZ_ValueChanged);
            // 
            // numericPositionY
            // 
            this.numericPositionY.DecimalPlaces = 1;
            this.numericPositionY.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericPositionY.Location = new System.Drawing.Point(26, 48);
            this.numericPositionY.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericPositionY.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.numericPositionY.Name = "numericPositionY";
            this.numericPositionY.Size = new System.Drawing.Size(46, 22);
            this.numericPositionY.TabIndex = 3;
            this.numericPositionY.ValueChanged += new System.EventHandler(this.numericUpDownY_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Y";
            // 
            // listBoxGameObjects
            // 
            this.listBoxGameObjects.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxGameObjects.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listBoxGameObjects.FormattingEnabled = true;
            this.listBoxGameObjects.ItemHeight = 16;
            this.listBoxGameObjects.Location = new System.Drawing.Point(6, 19);
            this.listBoxGameObjects.Name = "listBoxGameObjects";
            this.listBoxGameObjects.Size = new System.Drawing.Size(251, 164);
            this.listBoxGameObjects.TabIndex = 0;
            this.listBoxGameObjects.SelectedValueChanged += new System.EventHandler(this.listBoxGameObjects_SelectedValueChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Location = new System.Drawing.Point(584, 341);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(1);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.flowLayoutPanel1);
            this.splitContainer1.Size = new System.Drawing.Size(259, 200);
            this.splitContainer1.SplitterDistance = 120;
            this.splitContainer1.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.listBoxGameObjects);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.Location = new System.Drawing.Point(586, 38);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(263, 304);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "List GameObject";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Controls.Add(this.button1);
            this.flowLayoutPanel1.Controls.Add(this.button2);
            this.flowLayoutPanel1.Controls.Add(this.groupBox5);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(255, 116);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(220, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(3, 32);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(220, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Location = new System.Drawing.Point(3, 61);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(200, 100);
            this.groupBox5.TabIndex = 2;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "groupBox5";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(861, 553);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panelOpenTK);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericScaleX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericScaleZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericScaleY)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericRotationX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericRotationZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericRotationY)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericPositionX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericPositionZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericPositionY)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelOpenTK;
        private System.Windows.Forms.ListBox listBoxGameObjects;
        private System.Windows.Forms.NumericUpDown numericPositionX;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericPositionZ;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericPositionY;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numericRotationX;
        private System.Windows.Forms.NumericUpDown numericRotationZ;
        private System.Windows.Forms.NumericUpDown numericRotationY;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numericScaleX;
        private System.Windows.Forms.NumericUpDown numericScaleZ;
        private System.Windows.Forms.NumericUpDown numericScaleY;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox5;
    }
}

