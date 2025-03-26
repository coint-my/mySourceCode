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
    public partial class TopSalleForm : Form
    {
        List<int> c = new List<int>();

        public TopSalleForm()
        {
            InitializeComponent();

            string[] s = GetNameSuperSalle();

            for (int i = 0; i < GetNameSuperSalle().Length; i++)
            {
                dataGridView1.Rows.Add(s[i], c[i]);
            }
        }

        private string[] GetNameSuperSalle()
        {
            string[] str = new string[Presenter.ListSalle.Count];

            for (int i = 0; i < Presenter.ListSalle.Count; i++)
            {
                str[i] = Presenter.ListSalle[i].Name;
            }
            string[] strReal = InitializeNames(str);

            int[] count = new int[strReal.Length];

            for (int i = 0; i < strReal.Length; i++)
            {
                for (int j = 0; j < str.Length; j++)
                {
                    if (strReal[i].CompareTo(str[j]) == 0)
                    {
                        count[i]++;
                    }
                }
            }

            for (int i = 0; i < strReal.Length; i++)
            {
                c.Add(count[i]);
            }

            return strReal;
        }

        private string[] InitializeNames(string[] _list)
        {
            List<string> list = new List<string>();

            list.Add(_list[0]);

            for (int i = 0; i < _list.Length; i++)
            {
                string s = _list[i];
                bool isFinde = false;

                for (int j = 0; j < list.Count; j++)
                {
                    if (s == list[j])
                    {
                        isFinde = true;
                    }
                }

                if (!isFinde)
                {
                    list.Add(s);
                }
            }

            list.Sort();

            string[] str = new string[list.Count];

            for (int i = 0; i < list.Count; i++)
            {
                str[i] = list[i];
            }

            return str;
        }
    }
}
