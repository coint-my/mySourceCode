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
    public partial class FormDebitMounthAnaliz : Form
    {
        public FormDebitMounthAnaliz()
        {
            InitializeComponent();

            labelInfoMounth.Text = PresenterForm.BufferDateTime.ToString("y");

            for (int i = 0; i < PresenterForm.BufferMounth.Count; i++)
            {
                dataGridView1.Rows.Add(PresenterForm.BufferMounth[i].DateBalance, PresenterForm.BufferMounth[i].Category,
                    PresenterForm.BufferMounth[i].Money, PresenterForm.BufferMounth[i].Description);
            }
        }
    }
}
