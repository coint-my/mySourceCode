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
    public partial class BalanceMounth : Form
    {
        public BalanceMounth()
        {
            InitializeComponent();

            Presenter.ListSalle.Sort();
            Presenter.ListSalle.Reverse();

            for (int i = 0; i < MountSalles(Presenter.ListSalle).Length; i++)
            {
                comboBox1.Items.Add(MountSalles(Presenter.ListSalle)[i].ToString("y"));
            }
        }

        private DateTime[] MountSalles(List<SallonObject> _list)
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

            DateTime[] str = new DateTime[xCount + 1];
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

                    str[xCount] = dTime[xCount];

                    xCount++;

                    xDate = _list[i].Date;

                    result = 0;

                    result += (_list[i].Money * _list[i].Count);
                }
            }

            dTime[xCount] = _list[_list.Count - 1].Date;

            str[xCount] = dTime[xCount];

            Array.Sort(str);
            Array.Reverse(str);

            return str;
        }

        private void Mounth(DateTime _d)
        {
            DateTime dt = _d;

            int count = 0;

            List<SallonObject> list = new List<SallonObject>();

            for (int i = 0; i < Presenter.ListSalle.Count; i++)
            {
                if (dt.Month == Presenter.ListSalle[i].Date.Month &&
                    dt.Year == Presenter.ListSalle[i].Date.Year)
                {
                    count++;
                    list.Add(Presenter.ListSalle[i]);
                }
            }

            int summaDay = 0;
            List<string> ls = new List<string>();
            int day = list[0].Date.Day;
            int oldSumma = 0;

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Date.Day == day)
                {
                    summaDay += (list[i].Money * list[i].Count);
                }
                else
                {
                    ls.Add(list[i - 1].Date.Day.ToString() + " день = " + summaDay.ToString("###,###") + " грн.");

                    oldSumma += summaDay;

                    summaDay = 0;

                    day = list[i].Date.Day;

                    summaDay += (list[i].Money * list[i].Count);
                }

                if (list.Count - 1 == i)
                {
                    ls.Add(list[i].Date.Day.ToString() + " день = " + summaDay.ToString("###,###") + " грн.");
                    oldSumma += summaDay;
                }
            }

            for (int i = 0; i < ls.Count; i++)
            {
                listBox1.Items.Add(ls[i]);
            }

            textBox1.Text = oldSumma.ToString("###,###") + " грн.";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            Mounth(DateTime.Parse(comboBox1.Items[comboBox1.SelectedIndex].ToString()));
        }
    }
}
