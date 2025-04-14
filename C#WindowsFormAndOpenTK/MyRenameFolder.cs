using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C_WindowsFormAndOpenTK
{
    public partial class MyRenameFolder : Form
    {
        public string MyTextRename {  get; private set; }

        public MyRenameFolder(string _name)
        {
            InitializeComponent();

            MyTextRename = _name;
            textBoxRename.Text = _name;
            textBoxRename.Focus();
            textBoxRename.SelectAll();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            MyTextRename = textBoxRename.Text;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void buttonOK_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.DialogResult = DialogResult.OK;
                MyTextRename = textBoxRename.Text;
            }
        }
    }
}
