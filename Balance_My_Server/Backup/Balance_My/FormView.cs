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
    public partial class FormView : Form
    {
        public FormView()
        {
            InitializeComponent();

            for (int i = 0; i < PresenterForm.ListBalance.Count; i++)
            {
                dataGridBalance.Rows.Add(PresenterForm.ListBalance[i].DateBalance, PresenterForm.ListBalance[i].Category,
                    PresenterForm.ListBalance[i].Money, PresenterForm.ListBalance[i].Description);
            }
        }
    }
}
