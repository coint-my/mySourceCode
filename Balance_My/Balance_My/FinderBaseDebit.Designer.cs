namespace Balance_My
{
    partial class FinderBaseDebit
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridFindBaseDebit = new System.Windows.Forms.DataGridView();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.labelName = new System.Windows.Forms.Label();
            this.contextMenuInfo = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.data = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.category = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.подсчитатьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridFindBaseDebit)).BeginInit();
            this.contextMenuInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridFindBaseDebit
            // 
            this.dataGridFindBaseDebit.AllowUserToAddRows = false;
            this.dataGridFindBaseDebit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridFindBaseDebit.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridFindBaseDebit.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridFindBaseDebit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridFindBaseDebit.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.data,
            this.category,
            this.cost,
            this.description});
            this.dataGridFindBaseDebit.Location = new System.Drawing.Point(12, 56);
            this.dataGridFindBaseDebit.Name = "dataGridFindBaseDebit";
            this.dataGridFindBaseDebit.Size = new System.Drawing.Size(800, 431);
            this.dataGridFindBaseDebit.TabIndex = 0;
            this.dataGridFindBaseDebit.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridFindBaseDebit_CellBeginEdit);
            this.dataGridFindBaseDebit.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridFindBaseDebit_CellEndEdit);
            this.dataGridFindBaseDebit.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridFindBaseDebit_CellValueChanged);
            this.dataGridFindBaseDebit.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dataGridFindBaseDebit_UserDeletedRow);
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSearch.Location = new System.Drawing.Point(277, 12);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(406, 32);
            this.textBoxSearch.TabIndex = 1;
            this.textBoxSearch.TextChanged += new System.EventHandler(this.textBoxSearch_TextChanged);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Enabled = false;
            this.buttonOK.Location = new System.Drawing.Point(689, 12);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(123, 32);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "Изменить";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(12, 16);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(246, 26);
            this.labelName.TabIndex = 3;
            this.labelName.Text = "Поиск По Базе Дохода";
            // 
            // contextMenuInfo
            // 
            this.contextMenuInfo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.подсчитатьToolStripMenuItem});
            this.contextMenuInfo.Name = "contextMenuInfo";
            this.contextMenuInfo.Size = new System.Drawing.Size(207, 48);
            // 
            // id
            // 
            this.id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.id.FillWeight = 150F;
            this.id.Frozen = true;
            this.id.HeaderText = "ID";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Width = 59;
            // 
            // data
            // 
            this.data.HeaderText = "Дата";
            this.data.Name = "data";
            this.data.Width = 88;
            // 
            // category
            // 
            this.category.HeaderText = "Категория";
            this.category.Name = "category";
            this.category.Width = 142;
            // 
            // cost
            // 
            this.cost.ContextMenuStrip = this.contextMenuInfo;
            dataGridViewCellStyle1.NullValue = null;
            this.cost.DefaultCellStyle = dataGridViewCellStyle1;
            this.cost.HeaderText = "Цена";
            this.cost.Name = "cost";
            this.cost.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.cost.Width = 89;
            // 
            // description
            // 
            this.description.HeaderText = "Описание";
            this.description.Name = "description";
            this.description.Width = 137;
            // 
            // подсчитатьToolStripMenuItem
            // 
            this.подсчитатьToolStripMenuItem.Name = "подсчитатьToolStripMenuItem";
            this.подсчитатьToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.подсчитатьToolStripMenuItem.Text = "Посчитать всю колонку";
            this.подсчитатьToolStripMenuItem.Click += new System.EventHandler(this.подсчитатьToolStripMenuItem_Click);
            // 
            // FinderBaseDebit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 499);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.textBoxSearch);
            this.Controls.Add(this.dataGridFindBaseDebit);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "FinderBaseDebit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FinderBaseDebit";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridFindBaseDebit)).EndInit();
            this.contextMenuInfo.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridFindBaseDebit;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn data;
        private System.Windows.Forms.DataGridViewTextBoxColumn category;
        private System.Windows.Forms.DataGridViewTextBoxColumn cost;
        private System.Windows.Forms.ContextMenuStrip contextMenuInfo;
        private System.Windows.Forms.ToolStripMenuItem подсчитатьToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn description;
    }
}