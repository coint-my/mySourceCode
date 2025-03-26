using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Balance_My
{
    public partial class myDialogSwitchDebitCredit : Form
    {
        public RadioButton myGetTypeRadioButton { get { return radioButtonDebit; } }

        public myDialogSwitchDebitCredit()
        {
            InitializeComponent();

            radioButtonDebit.Checked = true;
        }

        public void AddTittle(string _name)
        {
            labelTittle.Text = _name;
        }

        private void myDialogSwitchDebitCredit_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                buttonOK_Click(sender, EventArgs.Empty);
            else if (e.KeyCode == Keys.Escape)
                buttonCancel_Click(sender, EventArgs.Empty);
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
