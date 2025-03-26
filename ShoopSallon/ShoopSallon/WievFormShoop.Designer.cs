namespace ShoopSallon
{
    partial class WievFormShoop
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
            this.dataTemp = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBoxID = new System.Windows.Forms.TextBox();
            this.numericCount = new System.Windows.Forms.NumericUpDown();
            this.numericMoney = new System.Windows.Forms.NumericUpDown();
            this.dateTimeDate = new System.Windows.Forms.DateTimePicker();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxTitleName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.checkMoney = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataTemp)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMoney)).BeginInit();
            this.SuspendLayout();
            // 
            // dataTemp
            // 
            this.dataTemp.AllowUserToAddRows = false;
            this.dataTemp.AllowUserToDeleteRows = false;
            this.dataTemp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataTemp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataTemp.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column4,
            this.Column2,
            this.Column3,
            this.Column5});
            this.dataTemp.Location = new System.Drawing.Point(6, 19);
            this.dataTemp.Name = "dataTemp";
            this.dataTemp.Size = new System.Drawing.Size(485, 312);
            this.dataTemp.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "id";
            this.Column1.Name = "Column1";
            this.Column1.Width = 50;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Дата";
            this.Column4.Name = "Column4";
            this.Column4.Width = 80;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Имя товара";
            this.Column2.Name = "Column2";
            this.Column2.Width = 120;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Описание";
            this.Column3.Name = "Column3";
            this.Column3.Width = 120;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Количество";
            this.Column5.Name = "Column5";
            this.Column5.Width = 70;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Название товара :";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.dataTemp);
            this.groupBox1.Location = new System.Drawing.Point(293, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(497, 337);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Товар в наличии";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkMoney);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.textBoxID);
            this.groupBox2.Controls.Add(this.numericCount);
            this.groupBox2.Controls.Add(this.numericMoney);
            this.groupBox2.Controls.Add(this.dateTimeDate);
            this.groupBox2.Controls.Add(this.textBoxDescription);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.textBoxTitleName);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.textBoxName);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(275, 337);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Ввод Информатции";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(9, 308);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(260, 23);
            this.button1.TabIndex = 16;
            this.button1.Text = "Добавить в список продаж";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBoxID
            // 
            this.textBoxID.Location = new System.Drawing.Point(113, 59);
            this.textBoxID.Name = "textBoxID";
            this.textBoxID.Size = new System.Drawing.Size(156, 20);
            this.textBoxID.TabIndex = 15;
            this.textBoxID.TextChanged += new System.EventHandler(this.textBoxID_TextChanged);
            // 
            // numericCount
            // 
            this.numericCount.Location = new System.Drawing.Point(113, 248);
            this.numericCount.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericCount.Name = "numericCount";
            this.numericCount.Size = new System.Drawing.Size(156, 20);
            this.numericCount.TabIndex = 14;
            this.numericCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericMoney
            // 
            this.numericMoney.Location = new System.Drawing.Point(113, 211);
            this.numericMoney.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numericMoney.Name = "numericMoney";
            this.numericMoney.Size = new System.Drawing.Size(156, 20);
            this.numericMoney.TabIndex = 13;
            this.numericMoney.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numericMoney_KeyPress);
            // 
            // dateTimeDate
            // 
            this.dateTimeDate.Location = new System.Drawing.Point(113, 176);
            this.dateTimeDate.Name = "dateTimeDate";
            this.dateTimeDate.Size = new System.Drawing.Size(156, 20);
            this.dateTimeDate.TabIndex = 12;
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Location = new System.Drawing.Point(113, 139);
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(156, 20);
            this.textBoxDescription.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 255);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Количество :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 218);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Цена :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 182);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Дата :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 142);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Описание :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "(ID) Товара :";
            // 
            // textBoxTitleName
            // 
            this.textBoxTitleName.Enabled = false;
            this.textBoxTitleName.Location = new System.Drawing.Point(113, 98);
            this.textBoxTitleName.Name = "textBoxTitleName";
            this.textBoxTitleName.Size = new System.Drawing.Size(156, 20);
            this.textBoxTitleName.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Наименование :";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(113, 26);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(156, 20);
            this.textBoxName.TabIndex = 2;
            this.textBoxName.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
            // 
            // checkMoney
            // 
            this.checkMoney.AutoSize = true;
            this.checkMoney.Checked = true;
            this.checkMoney.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkMoney.Location = new System.Drawing.Point(113, 281);
            this.checkMoney.Name = "checkMoney";
            this.checkMoney.Size = new System.Drawing.Size(159, 17);
            this.checkMoney.TabIndex = 17;
            this.checkMoney.Text = "Контроль суммы на товар";
            this.checkMoney.UseVisualStyleBackColor = true;
            // 
            // WievFormShoop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(802, 360);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "WievFormShoop";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Меню Продажи";
            ((System.ComponentModel.ISupportInitialize)(this.dataTemp)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMoney)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataTemp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxTitleName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericCount;
        private System.Windows.Forms.NumericUpDown numericMoney;
        private System.Windows.Forms.DateTimePicker dateTimeDate;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.TextBox textBoxID;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox checkMoney;
    }
}