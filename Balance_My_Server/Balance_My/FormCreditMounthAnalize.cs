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
    struct Par : IComparer<Par>, IComparable<Par>
    {
        public int ind_row;
        public int money;

        public int Compare(Par _par1, Par _par2)
        {
            if (_par1.money > _par2.money)
                return 1;
            else if (_par1.money < _par2.money)
                return -1;
            else
                return 0;
        }

        public int CompareTo(Par other)
        {
            return other.money.CompareTo(this.money);
        }
    }
    public partial class FormCreditMounthAnalize : Form
    {
        public FormCreditMounthAnalize()
        {
            InitializeComponent();

            int max_mount_money = 0;
            List<Par> temp_list = new List<Par>();

            for (int i = 0; i < PresenterForm.BufferMounthCredit.Count; i++)
            {
                if (max_mount_money < PresenterForm.BufferMounthCredit[i].Money)
                    max_mount_money = PresenterForm.BufferMounthCredit[i].Money;

                dataGridView1.Rows.Add(false, PresenterForm.BufferMounthCredit[i].DateBalance,
                    PresenterForm.BufferMounthCredit[i].Money, PresenterForm.BufferMounthCredit[i].Description);
            }

            int low_mount_money = max_mount_money / 2;
            Par p = new Par();
            //выбираю среднее значение цены из списка месячных расходов, и записываю в список для дальнейшей обработки
            for (int i = 0; i < PresenterForm.BufferMounthCredit.Count; i++)
            {
                if(low_mount_money < PresenterForm.BufferMounthCredit[i].Money)
                {
                    p.ind_row = i;
                    p.money = PresenterForm.BufferMounthCredit[i].Money;
                    temp_list.Add(p);           
                }
            }
            //сортирую временный список записей расхода которые должны поменять цвет
            temp_list.Sort();
            //сортировка в обратную сторону
            temp_list.Reverse();

            int temp_color_seg = (90 / temp_list.Count);
            int curr_color_add = 255;
            //вычисляю какой ячейки надо поменять цвет
            for (int i = 0; i < temp_list.Count; i++)
            {
                Par par = temp_list[i];

                dataGridView1.Rows[par.ind_row].Cells[0].Style.BackColor = Color.FromArgb(curr_color_add, 0, 0);

                curr_color_add -= temp_color_seg;
            }

            textBoxInfo.Text = PresenterForm.BufferDateTime.ToString("y");
            temp_list.Clear();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                int summa = 0;

                if((bool)dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == false)
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = true;
                else
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = false;

                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    if ((bool)dataGridView1.Rows[i].Cells[0].Value == true)
                    {
                        try
                        {
                            int temp_summa = 0;
                            int.TryParse(dataGridView1.Rows[i].Cells[2].Value.ToString(), out temp_summa);
                            summa += temp_summa;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Ошибка складывания общей суммы (" + ex.Message + ")", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }

                textBoxInfo.Text = summa.ToString() + " UAH.";
            }
        }
    }
}
