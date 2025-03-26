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
    public partial class BalanceDebitForm : Form
    {
        public BalanceDebitForm()
        {
            InitializeComponent();

            for (int i = 0; i < DebitMounth(Presenter.ListStorage).Length; i++)
            {
                listBoxDebit.Items.Add(DebitMounth(Presenter.ListStorage)[i]);
            }
        }

        private string[] DebitMounth(List<SallonObject> _list)
        {
            int xCount = 0;

            DateTime xDate = _list[0].Date;

            for (int i = 0; i < _list.Count; i++)
            {
                if (xDate.Month == _list[i].Date.Month && xDate.Year == _list[i].Date.Year)
                {

                }
                else
                {
                    xCount++;

                    xDate = _list[i].Date;
                }
            }

            string[] str = new string[xCount + 1];
            DateTime[] dTime = new DateTime[xCount + 1];
            xCount = 0;
            xDate = _list[0].Date;

            int result = 0;

            for (int i = 0; i < _list.Count; i++)
            {
                if (xDate.Month == _list[i].Date.Month && xDate.Year == _list[i].Date.Year)
                {
                    int summa = _list[i].Money;
                    int count = _list[i].Count;

                    result += (summa * count);
                }
                else
                {
                    dTime[xCount] = _list[i - 1].Date;

                    str[xCount] = dTime[xCount].Date.ToString("y") + "  = " + result.ToString("###,###") + " грн.";

                    xCount++;

                    xDate = _list[i].Date;

                    result = 0;
                }
            }

            dTime[xCount] = _list[_list.Count - 1].Date;

            str[xCount] = dTime[xCount].Date.ToString("y") + "  = " + result.ToString("###,###") + " грн.";

            return str;
        }
    }
}
