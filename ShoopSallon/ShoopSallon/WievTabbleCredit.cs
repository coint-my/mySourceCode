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
    public partial class WievTabbleCredit : Form
    {
        public WievTabbleCredit()
        {
            InitializeComponent();

            LoadTabble(Presenter.ListCredit);
        }

        private void LoadTabble(List<SallonObject> _list)
        {
            for (int i = 0; i < _list.Count; i++)
            {
                dataGridView1.Rows.Add(_list[i].Date.ToString("d"), _list[i].Description, _list[i].Money);
            }
        }
    }
}
