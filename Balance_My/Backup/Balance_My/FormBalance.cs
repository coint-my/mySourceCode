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
    public partial class FormBalance : Form
    {
        private bool b = false;
        private bool c = false;

        public event EventHandler butClic;

        internal static List<string> listCategory = new List<string>() { "Сантехника", "Электрика", "Ремонт", "Такси" };

        public FormBalance()
        {
            InitializeComponent();   
        }

        private void comboBoxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxCategory.Text.Length > 1)
                c = true;
            else
                c = false;

            if (b && c)
                buttonOk.Enabled = true;
            else
                buttonOk.Enabled = false;
        }

        private void comboBoxCategory_TextChanged(object sender, EventArgs e)
        {
            if (comboBoxCategory.Text.Length > 1)
                c = true;
            else
                c = false;

            if (b && c)
                buttonOk.Enabled = true;
            else
                buttonOk.Enabled = false;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            PresenterForm.AddBalance(new BalanceBase(Convert.ToInt32(numericBalance.Value),
                dateTimePicker1.Value.Date.ToString("d"), comboBoxCategory.Text, textBoxDescription.Text));

            if (butClic != null)
                butClic(this, EventArgs.Empty);

            this.DialogResult = DialogResult.OK;

            this.Close();
        }

        private void numericBalance_ValueChanged(object sender, EventArgs e)
        {
            if (numericBalance.Value > 0)
                b = true;
            else
                b = false;

            if (c && b)
                buttonOk.Enabled = true;
            else
                buttonOk.Enabled = false;
        }
    }
}
