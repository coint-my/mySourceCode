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
        public FormDebitMount(bool ok)
        {
            InitializeComponent();

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
    }
}
