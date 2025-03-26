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
    struct AnalyzePair
    {
        public ForAnalys credit;
        public ForAnalys debit;
        public string data;
        public DateTime year;
    }
    public partial class AnalyzeBalance : Form
    {
        private List<AnalyzePair> pairs;
        private List<AnalyzePair> pairsYear;

        private int rowIndex;
        private bool is_down = false;

        public AnalyzeBalance()
        {
            InitializeComponent();
        }

        private void AnalizeBalance_Shown(object sender, EventArgs e)
        {
            List<ForAnalys> list_debit = PresenterForm.CounterSumMounth(PresenterForm.ListBalance);
            List<ForAnalys> list_credit = PresenterForm.CounterSumMounth(PresenterForm.ListCredit);
            
            int count = list_debit.Count > list_credit.Count ? list_debit.Count : list_credit.Count;
            bool isDebit = list_debit.Count > list_credit.Count ? true : false;

            pairs = new List<AnalyzePair>();

            for (int i = 0; i < count; i++)
            {
                AnalyzePair pair = new AnalyzePair();

                if (isDebit)
                {
                    pair.debit = list_debit[i];
                    pair.data = list_debit[i].date.ToString("y");
                    pair.year = list_debit[i].date;

                    for (int j = 0; j < list_credit.Count; j++)
                    {
                        ForAnalys fa = list_credit[j];
                        if ((pair.debit.date.Year == fa.date.Year) && (pair.debit.date.Month == fa.date.Month))
                        {
                            pair.credit = fa;                            
                            break;
                        }
                        else pair.credit = new ForAnalys();
                    }
                }
                else
                {
                    pair.credit = list_credit[i];
                    pair.data = list_credit[i].date.ToString("y");
                    pair.year = list_credit[i].date;

                    for (int j = 0; j < list_debit.Count; j++)
                    {
                        ForAnalys fa = list_debit[j];
                        if ((pair.credit.date.Year == fa.date.Year) && (pair.credit.date.Month == fa.date.Month))
                        {
                            pair.debit = fa;
                            break;
                        }
                        else pair.debit = new ForAnalys();
                    }
                }

                pairs.Add(pair);
            }

            for (int i = 0; i < pairs.Count; i++)
            {
                dataGridView1.Rows.Add(pairs[i].data, pairs[i].debit.balance, pairs[i].credit.balance, 
                                        pairs[i].debit.balance - pairs[i].credit.balance);
            }

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if ((int)dataGridView1.Rows[i].Cells[3].Value < 0)
                {
                    dataGridView1.Rows[i].Cells[3].Style.BackColor = Color.Red;
                }
            }

            TestAnalyzeYear();
        }

        private void TestAnalyzeYear()
        {
            try
            {
                pairsYear = new List<AnalyzePair>();
                int resultDebit = 0;
                int resultCredit = 0;
                DateTime dtNew;
                DateTime dtOld = pairs[0].year;
                AnalyzePair analyzePair = new AnalyzePair();

                for (int i = 0; i < pairs.Count; i++)
                {
                    dtNew = pairs[i].year;
                    if (dtOld.Year == dtNew.Year)
                    {
                        resultDebit += pairs[i].debit.balance;
                        resultCredit += pairs[i].credit.balance;
                    }
                    else
                    {                        
                        analyzePair.debit.date = analyzePair.credit.date = dtOld;
                        analyzePair.debit.balance = resultDebit;
                        analyzePair.credit.balance = resultCredit;

                        pairsYear.Add(analyzePair);

                        resultDebit = pairs[i].debit.balance;
                        resultCredit = pairs[i].credit.balance;
                        dtOld = pairs[i].year;
                    }
                }

                analyzePair.debit.date = analyzePair.credit.date = dtOld;
                analyzePair.debit.balance = resultDebit;
                analyzePair.credit.balance = resultCredit;

                pairsYear.Add(analyzePair);
            }
            catch(Exception ex) { }

            ShowAnalyzeYear();
        }

        private void ShowAnalyzeYear()
        {
            DataGridView dgv = dataGridView1;
            for (int i = 0; i < pairs.Count; i++)
            {
                for (int j = 0; j < pairsYear.Count; j++)
                {
                    if(pairs[i].debit.date.Year == pairsYear[j].debit.date.Year)
                    {
                        dgv.Rows[i].Cells[4].Value = pairsYear[j].debit.balance;
                        dgv.Rows[i].Cells[5].Value = pairsYear[j].credit.balance;
                        break;
                    }
                }
            }
        }
        
        private void dataGridView1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            rowIndex = e.RowIndex;
        }

        private void myLookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rowIndex >= 0)
            {
                myDialogSwitchDebitCredit mdsdc = new myDialogSwitchDebitCredit();
                mdsdc.AddTittle(pairs[rowIndex].data);

                if (mdsdc.ShowDialog(this) == DialogResult.OK)
                {
                    bool is_debit = mdsdc.myGetTypeRadioButton.Checked;
                    string mouth = pairs[rowIndex].year.Month.ToString();
                    string year = pairs[rowIndex].year.Year.ToString();

                    PresenterForm.my_show_DebitCreditMountAnalize(mouth, year, is_debit);
                }
                else if (mdsdc.DialogResult == DialogResult.Cancel)
                    mdsdc = null;
            }
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            myLookToolStripMenuItem_Click(sender, e);
        }

        private void AnalyzeBalance_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }

        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && is_down)
            {
                myLookToolStripMenuItem_Click(sender, e);
            }
        }

        private void dataGridView1_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            rowIndex = e.RowIndex;            
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            is_down = true;
        }
    }
}
