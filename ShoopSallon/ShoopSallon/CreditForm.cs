using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ShoopSallon
{
    public partial class CreditForm : Form
    {
        public CreditForm()
        {
            InitializeComponent();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (textBoxDescr.Text != "" && numericSumma.Value > 0)
            {
                Presenter.ListCredit.Add(new SallonObject("", textBoxDescr.Text, dateTimePicker1.Value.Date,
                    0, (int)numericSumma.Value, 0));

                textBoxDescr.Text = "";
                numericSumma.Value = 0;
            }
            else
                MessageBox.Show("Надо внести все поля");
        }
    }
}
