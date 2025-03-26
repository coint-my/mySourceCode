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
    public partial class FormDebitMount : Form
    {
        private bool isOk;

        public FormDebitMount(bool ok)
        {
            InitializeComponent();

            isOk = ok;

            if (ok)
            {
                PresenterForm.ListBalance.Sort();
                PresenterForm.ListBalance.Reverse();

                for (int i = 0; i < PresenterForm.LoadSumMounth(PresenterForm.ListBalance).Count; i++)
                {
                    listBoxMoundth.Items.Add(PresenterForm.LoadSumMounth(PresenterForm.ListBalance)[i]);
                }

                this.Text = "Доходы";
            }
            else
            {
                PresenterForm.ListCredit.Sort();
                PresenterForm.ListCredit.Reverse();

                for (int i = 0; i < PresenterForm.LoadSumMounth(PresenterForm.ListCredit).Count; i++)
                {
                    string s = PresenterForm.LoadSumMounth(PresenterForm.ListCredit)[i];               

                    listBoxMoundth.Items.Add(s);
                }

                this.Text = "Расходы";
            }
        }

        private void listBoxMoundth_DoubleClick(object sender, EventArgs e)
        {
            string str = listBoxMoundth.SelectedItem.ToString();

            string str_mounth = str.Split(' ')[0];
            string str_year = str.Split(' ')[1];

            string str_calculate = "01." + str_mounth + "." + str_year;

            PresenterForm.BufferDateTime = DateTime.Parse(str_calculate);
            PresenterForm.BufferMounth.Clear();
            PresenterForm.BufferMounthCredit.Clear();

            if (isOk)
            {
                for (int deb = 0; deb < PresenterForm.ListBalance.Count; deb++)
                {
                    BalanceBase _bb = PresenterForm.ListBalance[deb];
                    DateTime _dt = DateTime.Parse(_bb.DateBalance);

                    if ((_dt.Month == PresenterForm.BufferDateTime.Month) && (_dt.Year == PresenterForm.BufferDateTime.Year))
                    {
                        PresenterForm.BufferMounth.Add(_bb);
                    }
                }

                FormDebitMounthAnaliz fdma = new FormDebitMounthAnaliz();
                fdma.ShowDialog();
            }
            else
            {
                for (int cre = 0; cre < PresenterForm.ListCredit.Count; cre++)
                {
                    BalanceBase _bb = PresenterForm.ListCredit[cre];
                    DateTime _dt = DateTime.Parse(_bb.DateBalance);

                    if ((_dt.Month == PresenterForm.BufferDateTime.Month) && (_dt.Year == PresenterForm.BufferDateTime.Year))
                    {
                        PresenterForm.BufferMounthCredit.Add(_bb);
                    }
                }

                FormCreditMounthAnalize fcma = new FormCreditMounthAnalize();
                fcma.ShowDialog();
            }
        }
    }
}
