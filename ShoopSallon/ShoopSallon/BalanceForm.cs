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
    public partial class BalanceForm : Form
    {
        public BalanceForm()
        {
            InitializeComponent();

            textBoxOldBallance.Text = OldBalance(Presenter.ListStorage).ToString("#,###") + ".грн";
        }

        private int OldBalance(List<SallonObject> _list)
        {
            int result = 0;

            for (int i = 0; i < _list.Count; i++)
            {
                int summa = _list[i].Money;
                int count = _list[i].Count;

                result += (summa * count);
            }

            return result;
        }

        private List<string> Mounth(List<SallonObject> _l)
        {
            DateTime d = _l[0].Date;

            _l.Sort();
            _l.Reverse();

            List<string> l = new List<string>();

            int result = 0;
            
            for (int i = 0; i < _l.Count; i++)
            {
                if (d.Month == _l[i].Date.Month && d.Year == _l[i].Date.Year)
                {
                    result += (_l[i].Money * _l[i].Count);
                }
                else
                {
                    l.Add(d.ToString("y") + " = " + result.ToString());

                    result = 0;
                    d = _l[i].Date;
                    result += (_l[i].Money * _l[i].Count);
                }
            }

            l.Add(d.ToString("y") + " = " + result.ToString());

            return l;
        }

        private List<string> Mounth(List<SallonObject> _l, List<SallonObject> _l2)
        {
            DateTime d = _l[0].Date;

            _l.Sort();
            _l.Reverse();

            List<string> l = new List<string>();

            int result = 0;

            for (int i = 0; i < _l.Count; i++)
            {
                int idStore = 0;

                string id_1 = _l[i].Description.Split('(')[0];

                if (d.Month == _l[i].Date.Month && d.Year == _l[i].Date.Year)
                {
                    for (int j = 0; j < _l2.Count; j++)
                    {
                        string id_2 = _l2[j].Description.Split('(')[0];

                        if (_l[i].Name.CompareTo(_l2[j].Name) == 0 && id_1.CompareTo(id_2) == 0)
                        {
                            idStore = _l2[j].Money;

                            result += ((_l[i].Money - idStore) * _l[i].Count);

                            break;
                        }
                    }
                }
                else
                {
                    l.Add(d.ToString("y") + " = " + result.ToString());

                    result = 0;
                    d = _l[i].Date;

                    for (int j = 0; j < _l2.Count; j++)
                    {
                        string id_2 = _l2[j].Description.Split('(')[0];

                        if (_l[i].Name.CompareTo(_l2[j].Name) == 0 && id_1.CompareTo(id_2) == 0)
                        {
                            idStore = _l2[j].Money;

                            result += ((_l[i].Money - idStore) * _l[i].Count);

                            break;
                        }
                    }
                }
            }

            l.Add(d.ToString("y") + " = " + result.ToString());

            return l;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;

            progressBar1.Maximum = (Mounth(Presenter.ListSalle).Count * 2);

            int index = 0;

            for (int i = 0; i < Mounth(Presenter.ListSalle).Count; i++)
            {
                progressBar1.Value = i + 1;

                index = i + 1;

                listBoxDate.Items.Add(Mounth(Presenter.ListSalle)[i]);
            }

            for (int i = 0; i < Mounth(Presenter.ListSalle).Count; i++)
            {
                progressBar1.Value = i + index;

                listBox1.Items.Add(Mounth(Presenter.ListSalle, Presenter.ListStorage)[i]);
            }

            progressBar1.Visible = false;
        }
    }
}
