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
    public partial class AnalizeBalance : Form
    {
        public AnalizeBalance()
        {
            InitializeComponent();
        }

        private void AnalizeBalance_Shown(object sender, EventArgs e)
        {
            if (PresenterForm.ListBalance.Count > 0 && PresenterForm.ListCredit.Count > 0)
            {
                List<ForAnalys> list_debit = PresenterForm.LoadSumMounth(PresenterForm.ListBalance, true);
                List<ForAnalys> list_credit = PresenterForm.LoadSumMounth(PresenterForm.ListCredit, true);
           
                for (int i = 0; i < list_debit.Count; i++)
                {
                    ForAnalys temp_d = list_debit[i];
                    string data = temp_d.date.ToString("y");
                    ForAnalys temp_c = new ForAnalys();
                    for (int j = 0; j < list_credit.Count; j++)
                    {
                        temp_c = list_credit[j];
                        if ((temp_d.date.Month == temp_c.date.Month) && (temp_d.date.Year == temp_c.date.Year))
                            break;
                    }
                    dataGridView1.Rows.Add(data, temp_d.money, temp_c.money, temp_d.money - temp_c.money);
                }

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if ((int)dataGridView1.Rows[i].Cells[3].Value <= 0)
                    {
                        dataGridView1.Rows[i].Cells[3].Style.BackColor = Color.Red;
                    }
                }
            }
        }
    }
}
