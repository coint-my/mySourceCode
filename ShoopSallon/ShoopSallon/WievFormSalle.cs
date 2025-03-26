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
    public partial class WievFormSalle : Form
    {
        private string tempText;

        private bool isSalle;

        public WievFormSalle(bool _salleStor)
        {
            InitializeComponent();

            isSalle = _salleStor;

            Presenter.ListSalle.Sort();
            Presenter.ListSalle.Reverse();

            Presenter.ListStorage.Sort();
            Presenter.ListStorage.Reverse();

            if (_salleStor)
            {
                LoadDataGrid(Presenter.ListSalle);
                this.Text = "Таблица Продажи";
            }
            else
            {
                LoadDataGrid(Presenter.ListStorage);
                this.Text = "Таблица Закупок";
            }
        }

        private void LoadDataGrid(List<SallonObject> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                dataGridView1.Rows.Add(list[i].Id, list[i].Date.ToString("d"),
                    list[i].Name, list[i].Description, list[i].Money,
                    list[i].Count);
            }
        }

        private void LoadDataGrid(List<SallonObject> list, bool _isRemove)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (_isRemove && list[i].Count == 0)
                {
                    Presenter.RemoveObject(list[i]);
                }
                else
                    dataGridView1.Rows.Add(list[i].Id, list[i].Date.ToString("d"),
                            list[i].Name, list[i].Description, list[i].Money,
                            list[i].Count);
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (MessageBox.Show("Из \n" + tempText + " \nна \n" + dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value,
                "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                
            }
            else
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = tempText;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                tempText = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
        }

        private void WievFormSalle_FormClosing(object sender, FormClosingEventArgs e)
        {
            List<SallonObject> listSalle = new List<SallonObject>();

            for (int x = 0; x < dataGridView1.RowCount; x++)
            {
                string[] str = new string[dataGridView1.ColumnCount];

                for (int y = 0; y < dataGridView1.ColumnCount; y++)
                {
                    str[y] = dataGridView1.Rows[x].Cells[y].Value.ToString();
                }

                listSalle.Add(new SallonObject(str[2], str[3], DateTime.Parse(str[1]), int.Parse(str[0]), int.Parse(str[4]),
                    int.Parse(str[5])));
            }

            if (isSalle)
                Presenter.LoadListSalle(listSalle);
            else
                Presenter.LoadListStore(listSalle);
        }
    }
}
