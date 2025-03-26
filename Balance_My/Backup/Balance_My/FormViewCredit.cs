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
    public partial class FormViewCredit : Form
    {
        public FormViewCredit()
        {
            InitializeComponent();

            for (int i = 0; i < PresenterForm.ListCredit.Count; i++)
            {
                dataGridCredit.Rows.Add(PresenterForm.ListCredit[i].Money, PresenterForm.ListCredit[i].DateBalance,
                    PresenterForm.ListCredit[i].Description);
            }
        }
    }
}
