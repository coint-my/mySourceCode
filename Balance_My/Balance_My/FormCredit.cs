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
    public partial class FormCredit : Form
    {
        public FormCredit()
        {
            InitializeComponent();

            textBoxDecrioption.Items.AddRange(Settings.tamplate_credit.ToArray());
        }

        private void numericCredit_ValueChanged(object sender, EventArgs e)
        {
            if (numericCredit.Value > 1)
                buttonOK.Enabled = true;
            else
                buttonOK.Enabled = false;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            PresenterForm.AddCredit(new BalanceBase(Convert.ToInt32(numericCredit.Value),
                dateTimePicker1.Value.Date.ToString("d"), "", textBoxDecrioption.Text));

            this.DialogResult = DialogResult.OK;

            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
