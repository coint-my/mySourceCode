using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace windows_game_CSharp
{
    struct LocationAreaGroupe
    {
        public GroupBox groupBoxLocation;
        public PictureBox pictureBoxLocation;
        public Button buttonLocationCancel;
        public GroupBox groupBoxEnemy;
        public PictureBox pictureBoxEnemy;
        public GroupBox groupBoxPlayer;
        public PictureBox pictureBoxPlayer;

        public void Initialize(bool isInitialize)
        {
            if(isInitialize)
            {
                groupBoxLocation = new GroupBox();
                pictureBoxLocation = new PictureBox();
                buttonLocationCancel = new Button();

                groupBoxEnemy = new GroupBox();
                pictureBoxLocation.Controls.Add(groupBoxEnemy);
                pictureBoxEnemy = new PictureBox();
                groupBoxEnemy.Controls.Add(pictureBoxEnemy);

                groupBoxPlayer = new GroupBox();
                pictureBoxLocation.Controls.Add(groupBoxPlayer);
                pictureBoxPlayer = new PictureBox();
                groupBoxPlayer.Controls.Add(pictureBoxPlayer);
            }
        }
        public void free()
        {
            pictureBoxLocation.Dispose();
            buttonLocationCancel.Dispose();
            groupBoxLocation.Dispose();
        }
    }
    public partial class Form1 : Form
    {
        const int maxLevel = 1;

        /// <summary>
        /// группа класов локации местности
        /// </summary>
        LocationAreaGroupe locationArea;
        public Form1()
        {
            InitializeComponent();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void StartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainMenu mainMenu = new MainMenu();
            mainMenu.ShowDialog();
        }
        private void SetCurrLevel(int _level, string _top, string _left, string _right, string _bottom)
        {
            if(_level <= maxLevel && _level >= 0)
            {
                if (pictureBoxTop.Image != null)
                {
                    pictureBoxTop.Image.Dispose();
                    pictureBoxLeft.Image.Dispose();
                    pictureBoxRight.Image.Dispose();
                    pictureBoxBottom.Image.Dispose();
                }

                pictureBoxTop.Load(_top);
                Label lb1 = new Label();
                lb1.Text = pictureBoxTop.Name;
                pictureBoxTop.Controls.Add(lb1);
                pictureBoxTop.MouseDown += PictureBoxTop_MouseDown;
                //---------------------------
                pictureBoxLeft.Load(_left);
                Label lb2 = new Label();
                lb2.Text = pictureBoxLeft.Name;
                pictureBoxLeft.Controls.Add(lb2);
                pictureBoxLeft.MouseDown += PictureBoxLeft_MouseDown;
                //---------------------------
                pictureBoxRight.Load(_right);
                Label lb3 = new Label();
                lb3.Text = pictureBoxRight.Name;
                pictureBoxRight.Controls.Add(lb3);
                pictureBoxRight.MouseDown += PictureBoxRight_MouseDown;
                //---------------------------
                pictureBoxBottom.Load(_bottom);
                Label lb4 = new Label();
                lb4.Text = pictureBoxBottom.Name;
                pictureBoxBottom.Controls.Add(lb4);
                pictureBoxBottom.MouseDown += PictureBoxBottom_MouseDown;
            }
        }

        private void PictureBoxBottom_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                locationArea.Initialize(true);
                createLocation(ref locationArea);

                groupBoxGame.Visible = false;

                var obj = (PictureBox)sender;
                locationArea.pictureBoxLocation.Image = obj.Image;
            }
        }

        private void PictureBoxRight_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                locationArea.Initialize(true);
                createLocation(ref locationArea);

                groupBoxGame.Visible = false;

                var obj = (PictureBox)sender;
                locationArea.pictureBoxLocation.Image = obj.Image;
            }
        }

        private void PictureBoxLeft_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                locationArea.Initialize(true);
                createLocation(ref locationArea);

                groupBoxGame.Visible = false;

                var obj = (PictureBox)sender;
                locationArea.pictureBoxLocation.Image = obj.Image;
            }
        }

        private void PictureBoxTop_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                locationArea.Initialize(true);
                createLocation(ref locationArea);

                groupBoxGame.Visible = false;

                var obj = (PictureBox)sender;
                locationArea.pictureBoxLocation.Image = obj.Image;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetCurrLevel(0, "texture//forest_01.jpg", "texture//forest_02.jpg", "texture//forest_03.jpg", "texture//forest_04.jpg");
        }

        private void createLocation(ref LocationAreaGroupe _loc)
        {
            //форма 1
            Controls.Add(_loc.groupBoxLocation);
            // 
            // groupBoxLocation
            // 
            _loc.groupBoxLocation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            _loc.groupBoxLocation.Controls.Add(_loc.buttonLocationCancel);
            _loc.groupBoxLocation.Controls.Add(_loc.pictureBoxLocation);
            _loc.groupBoxLocation.Location = new System.Drawing.Point(12, 27);
            _loc.groupBoxLocation.Name = "groupBoxLocation";
            _loc.groupBoxLocation.Size = new System.Drawing.Size(Size.Width - 40, Size.Height - 140);
            _loc.groupBoxLocation.Text = "location";
            // 
            // pictureBoxLocation
            // 
            _loc.pictureBoxLocation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            _loc.pictureBoxLocation.Location = new System.Drawing.Point(9, 22);
            _loc.pictureBoxLocation.Name = "pictureBoxLocation";
            _loc.pictureBoxLocation.Size = new System.Drawing.Size(_loc.groupBoxLocation.Width - 18, _loc.groupBoxLocation.Height - 64);
            _loc.pictureBoxLocation.TabStop = false;
            // 
            // buttonCancel
            // 
            _loc.buttonLocationCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            _loc.buttonLocationCancel.Location = new System.Drawing.Point(_loc.groupBoxLocation.Width - 133, _loc.groupBoxLocation.Height - 33);
            _loc.buttonLocationCancel.Name = "buttonCancel";
            _loc.buttonLocationCancel.Size = new System.Drawing.Size(123, 23);
            _loc.buttonLocationCancel.Text = "Выход из локации";
            _loc.buttonLocationCancel.UseVisualStyleBackColor = true;
            _loc.buttonLocationCancel.Click += ButtonLocationCancel_Click;
            //
            //groupBoxEnemy
            //
            _loc.groupBoxEnemy.Anchor = (AnchorStyles.Left | AnchorStyles.Top);
            _loc.groupBoxEnemy.Location = new Point(10, 10);
            _loc.groupBoxEnemy.Text = "Enemy";
            //
            //pictureBoxEnemy
            //
            _loc.pictureBoxEnemy.Location = new Point(15, 15);
            //
            //groupBoxPlayer
            //
            _loc.groupBoxPlayer.Anchor = (AnchorStyles.Right | AnchorStyles.Top);
            _loc.groupBoxPlayer.Location = new Point(_loc.pictureBoxLocation.Width - _loc.groupBoxPlayer.Width - 10, 10);
            _loc.groupBoxPlayer.Text = "Player";
            //
            //pictureBoxPlayer
            //
            _loc.pictureBoxPlayer.Location = new Point(15, 15);
        }

        private void ButtonLocationCancel_Click(object sender, EventArgs e)
        {
            locationArea.free();
            groupBoxGame.Visible = true;
        }
    }
}
