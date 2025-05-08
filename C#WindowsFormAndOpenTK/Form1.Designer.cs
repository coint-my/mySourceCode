namespace C_WindowsFormAndOpenTK
{
    partial class FormMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.panelOpenTK = new System.Windows.Forms.Panel();
            this.listView1 = new System.Windows.Forms.ListView();
            this.Path = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Info = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStripExplorer = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.createFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rename = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStrip = new System.Windows.Forms.ToolStripMenuItem();
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.flowLayoutPanelMyParameters = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBoxExplorer = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.treeViewGameObjects = new System.Windows.Forms.TreeView();
            this.contextMenuStripHierarhy = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.delete = new System.Windows.Forms.ToolStripMenuItem();
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.CreateGameObjectEmpty = new System.Windows.Forms.ToolStripMenuItem();
            this.cubeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sphereToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.planeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cameraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSceneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadSceneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripExplorer.SuspendLayout();
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
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBoxExplorer.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.contextMenuStripHierarhy.SuspendLayout();
            this.menuStrip1.SuspendLayout();
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
            this.panelOpenTK.Size = new System.Drawing.Size(565, 560);
            this.panelOpenTK.TabIndex = 2;
            // 
            // listView1
            // 
            this.listView1.Alignment = System.Windows.Forms.ListViewAlignment.Default;
            this.listView1.AllowColumnReorder = true;
            this.listView1.AllowDrop = true;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Path,
            this.Info});
            this.listView1.ContextMenuStrip = this.contextMenuStripExplorer;
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listView1.HideSelection = false;
            this.listView1.LabelEdit = true;
            this.listView1.Location = new System.Drawing.Point(0, 13);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(255, 110);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // Path
            // 
            this.Path.Text = "Name";
            // 
            // Info
            // 
            this.Info.Text = "Info";
            // 
            // contextMenuStripExplorer
            // 
            this.contextMenuStripExplorer.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createFolderToolStripMenuItem,
            this.rename,
            this.deleteToolStrip});
            this.contextMenuStripExplorer.Name = "contextMenuStripExplorer";
            this.contextMenuStripExplorer.Size = new System.Drawing.Size(145, 70);
            // 
            // createFolderToolStripMenuItem
            // 
            this.createFolderToolStripMenuItem.Image = global::C_WindowsFormAndOpenTK.Properties.Resources.folder;
            this.createFolderToolStripMenuItem.Name = "createFolderToolStripMenuItem";
            this.createFolderToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.createFolderToolStripMenuItem.Text = "Create Folder";
            // 
            // rename
            // 
            this.rename.Image = global::C_WindowsFormAndOpenTK.Properties.Resources.folder;
            this.rename.Name = "rename";
            this.rename.Size = new System.Drawing.Size(144, 22);
            this.rename.Text = "Rename";
            // 
            // deleteToolStrip
            // 
            this.deleteToolStrip.Name = "deleteToolStrip";
            this.deleteToolStrip.Size = new System.Drawing.Size(144, 22);
            this.deleteToolStrip.Text = "Delete";
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
            this.groupBox4.Location = new System.Drawing.Point(169, 189);
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
            this.numericScaleX.DecimalPlaces = 2;
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
            this.numericScaleX.Size = new System.Drawing.Size(56, 22);
            this.numericScaleX.TabIndex = 1;
            // 
            // numericScaleZ
            // 
            this.numericScaleZ.DecimalPlaces = 2;
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
            this.numericScaleZ.Size = new System.Drawing.Size(56, 22);
            this.numericScaleZ.TabIndex = 5;
            // 
            // numericScaleY
            // 
            this.numericScaleY.DecimalPlaces = 2;
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
            this.numericScaleY.Size = new System.Drawing.Size(56, 22);
            this.numericScaleY.TabIndex = 3;
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
            this.groupBox3.Location = new System.Drawing.Point(86, 189);
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
            this.numericRotationX.DecimalPlaces = 2;
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
            this.numericRotationX.Size = new System.Drawing.Size(56, 22);
            this.numericRotationX.TabIndex = 1;
            // 
            // numericRotationZ
            // 
            this.numericRotationZ.DecimalPlaces = 2;
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
            this.numericRotationZ.Size = new System.Drawing.Size(56, 22);
            this.numericRotationZ.TabIndex = 5;
            // 
            // numericRotationY
            // 
            this.numericRotationY.DecimalPlaces = 2;
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
            this.numericRotationY.Size = new System.Drawing.Size(56, 22);
            this.numericRotationY.TabIndex = 3;
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
            this.groupBox2.Location = new System.Drawing.Point(4, 189);
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
            this.numericPositionX.DecimalPlaces = 2;
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
            this.numericPositionX.Size = new System.Drawing.Size(56, 22);
            this.numericPositionX.TabIndex = 1;
            // 
            // numericPositionZ
            // 
            this.numericPositionZ.DecimalPlaces = 2;
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
            this.numericPositionZ.Size = new System.Drawing.Size(56, 22);
            this.numericPositionZ.TabIndex = 5;
            // 
            // numericPositionY
            // 
            this.numericPositionY.DecimalPlaces = 2;
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
            this.numericPositionY.Size = new System.Drawing.Size(56, 22);
            this.numericPositionY.TabIndex = 3;
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
            this.splitContainer1.Panel1.Controls.Add(this.flowLayoutPanelMyParameters);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBoxExplorer);
            this.splitContainer1.Size = new System.Drawing.Size(259, 257);
            this.splitContainer1.SplitterDistance = 126;
            this.splitContainer1.TabIndex = 4;
            // 
            // flowLayoutPanelMyParameters
            // 
            this.flowLayoutPanelMyParameters.AutoScroll = true;
            this.flowLayoutPanelMyParameters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelMyParameters.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.flowLayoutPanelMyParameters.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanelMyParameters.Name = "flowLayoutPanelMyParameters";
            this.flowLayoutPanelMyParameters.Size = new System.Drawing.Size(255, 122);
            this.flowLayoutPanelMyParameters.TabIndex = 0;
            // 
            // groupBoxExplorer
            // 
            this.groupBoxExplorer.Controls.Add(this.listView1);
            this.groupBoxExplorer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxExplorer.Location = new System.Drawing.Point(0, 0);
            this.groupBoxExplorer.Margin = new System.Windows.Forms.Padding(0);
            this.groupBoxExplorer.Name = "groupBoxExplorer";
            this.groupBoxExplorer.Padding = new System.Windows.Forms.Padding(0);
            this.groupBoxExplorer.Size = new System.Drawing.Size(255, 123);
            this.groupBoxExplorer.TabIndex = 0;
            this.groupBoxExplorer.TabStop = false;
            this.groupBoxExplorer.Text = "Explorer";
            this.groupBoxExplorer.UseCompatibleTextRendering = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.treeViewGameObjects);
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
            // treeViewGameObjects
            // 
            this.treeViewGameObjects.AllowDrop = true;
            this.treeViewGameObjects.ContextMenuStrip = this.contextMenuStripHierarhy;
            this.treeViewGameObjects.Dock = System.Windows.Forms.DockStyle.Top;
            this.treeViewGameObjects.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.treeViewGameObjects.LabelEdit = true;
            this.treeViewGameObjects.Location = new System.Drawing.Point(3, 18);
            this.treeViewGameObjects.Name = "treeViewGameObjects";
            this.treeViewGameObjects.Size = new System.Drawing.Size(257, 165);
            this.treeViewGameObjects.TabIndex = 10;
            // 
            // contextMenuStripHierarhy
            // 
            this.contextMenuStripHierarhy.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.delete,
            this.renameToolStripMenuItem,
            this.toolStripMenuItem1,
            this.cameraToolStripMenuItem});
            this.contextMenuStripHierarhy.Name = "contextMenuStripHierarhy";
            this.contextMenuStripHierarhy.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.contextMenuStripHierarhy.Size = new System.Drawing.Size(178, 92);
            this.contextMenuStripHierarhy.Text = "test";
            // 
            // delete
            // 
            this.delete.Name = "delete";
            this.delete.Size = new System.Drawing.Size(177, 22);
            this.delete.Text = "Delete";
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.renameToolStripMenuItem.Text = "Rename";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CreateGameObjectEmpty,
            this.cubeToolStripMenuItem,
            this.sphereToolStripMenuItem,
            this.planeToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(177, 22);
            this.toolStripMenuItem1.Text = "Create GameObject";
            // 
            // CreateGameObjectEmpty
            // 
            this.CreateGameObjectEmpty.Name = "CreateGameObjectEmpty";
            this.CreateGameObjectEmpty.Size = new System.Drawing.Size(110, 22);
            this.CreateGameObjectEmpty.Text = "Empty";
            // 
            // cubeToolStripMenuItem
            // 
            this.cubeToolStripMenuItem.Name = "cubeToolStripMenuItem";
            this.cubeToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.cubeToolStripMenuItem.Text = "Cube";
            // 
            // sphereToolStripMenuItem
            // 
            this.sphereToolStripMenuItem.Name = "sphereToolStripMenuItem";
            this.sphereToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.sphereToolStripMenuItem.Text = "Sphere";
            // 
            // planeToolStripMenuItem
            // 
            this.planeToolStripMenuItem.Name = "planeToolStripMenuItem";
            this.planeToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.planeToolStripMenuItem.Text = "Plane";
            // 
            // cameraToolStripMenuItem
            // 
            this.cameraToolStripMenuItem.Name = "cameraToolStripMenuItem";
            this.cameraToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.cameraToolStripMenuItem.Text = "Create Camera";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(861, 29);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveSceneToolStripMenuItem,
            this.loadSceneToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 25);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveSceneToolStripMenuItem
            // 
            this.saveSceneToolStripMenuItem.Name = "saveSceneToolStripMenuItem";
            this.saveSceneToolStripMenuItem.Size = new System.Drawing.Size(180, 26);
            this.saveSceneToolStripMenuItem.Text = "Save Scene";
            this.saveSceneToolStripMenuItem.Click += new System.EventHandler(this.saveSceneToolStripMenuItem_Click);
            // 
            // loadSceneToolStripMenuItem
            // 
            this.loadSceneToolStripMenuItem.Name = "loadSceneToolStripMenuItem";
            this.loadSceneToolStripMenuItem.Size = new System.Drawing.Size(180, 26);
            this.loadSceneToolStripMenuItem.Text = "Load Scene";
            this.loadSceneToolStripMenuItem.Click += new System.EventHandler(this.loadSceneToolStripMenuItem_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(861, 610);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panelOpenTK);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.Text = "VBA Project Editor";
            this.contextMenuStripExplorer.ResumeLayout(false);
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
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBoxExplorer.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.contextMenuStripHierarhy.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panelOpenTK;
        public System.Windows.Forms.NumericUpDown numericPositionX;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.NumericUpDown numericPositionZ;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.NumericUpDown numericPositionY;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.NumericUpDown numericRotationX;
        public System.Windows.Forms.NumericUpDown numericRotationZ;
        public System.Windows.Forms.NumericUpDown numericRotationY;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.NumericUpDown numericScaleX;
        public System.Windows.Forms.NumericUpDown numericScaleZ;
        public System.Windows.Forms.NumericUpDown numericScaleY;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.FlowLayoutPanel flowLayoutPanelMyParameters;
        public System.Windows.Forms.TreeView treeViewGameObjects;
        public System.Windows.Forms.ContextMenuStrip contextMenuStripHierarhy;
        private System.Windows.Forms.ToolStripMenuItem delete;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem CreateGameObjectEmpty;
        private System.Windows.Forms.ToolStripMenuItem cubeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sphereToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem planeToolStripMenuItem;
        public System.Windows.Forms.GroupBox groupBoxExplorer;
        public System.Windows.Forms.ContextMenuStrip contextMenuStripExplorer;
        private System.Windows.Forms.ToolStripMenuItem createFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rename;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStrip;
        private System.Windows.Forms.ToolStripMenuItem cameraToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
        public System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader Path;
        private System.Windows.Forms.ColumnHeader Info;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveSceneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadSceneToolStripMenuItem;
    }
}

