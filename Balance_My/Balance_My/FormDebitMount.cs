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

            PresenterForm.my_show_DebitCreditMountAnalize(str_mounth, str_year,
                isOk);
        }
    }
}
