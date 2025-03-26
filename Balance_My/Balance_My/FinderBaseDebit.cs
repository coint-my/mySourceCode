using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Balance_My
{
    struct InfoGridView : IComparer<InfoGridView>
    {
        //структура базы данных
        public BalanceBase bBase;
        //позиция индекс в базе данных
        public int posIndexBase;
        //нужно ли переписывать эту базу
        public bool isflag;

        public int Compare(InfoGridView x, InfoGridView y)
        {
            if (x.posIndexBase < y.posIndexBase)
                return -1;
            else if (x.posIndexBase > y.posIndexBase)
                return 1;
            else return 0;
        }
    }

    public enum Balance { DEBIT, CREDIT }
    public partial class FinderBaseDebit : Form
    {
        List<InfoGridView> infoGridViews;
        List<InfoGridView> infoGridDelete;
        
        string tempBeginValueCell;
        bool isTempValueCell;

        Balance balanceGet { get; }

        public FinderBaseDebit(Balance balance)
        {
            InitializeComponent();

            infoGridViews = new List<InfoGridView>();
            infoGridDelete = new List<InfoGridView>();

            balanceGet = balance;

            if (balanceGet == Balance.DEBIT)
                labelName.Text = "Поиск По Базе Дохода";
            else if (balanceGet == Balance.CREDIT)
                labelName.Text = "Поиск По Базе Расхода";
        }

        private void initializeGrid(string _text, List<BalanceBase> _bases)
        {
            if (_text.Length >= 2)
            {
                infoGridViews.Clear();
                infoGridDelete.Clear();

                for (int ind = 0; ind < _bases.Count; ind++)
                {
                    string s1 = _text.ToLower();
                    string s2 = _bases[ind].Description.ToLower();
                    string s3 = _bases[ind].DateBalance.ToLower();
                    string s4 = _bases[ind].Category.ToLower();


                    if ((s2.Contains(s1)) || (s3.Contains(s1)) || (s4.Contains(s1)))
                    {
                        InfoGridView info;
                        info.bBase = _bases[ind];
                        info.posIndexBase = ind;
                        info.isflag = false;
                        infoGridViews.Add(info);
                    }
                }
            }
        }

        private void addGrid(List<InfoGridView> _infos)
        {
            dataGridFindBaseDebit.Rows.Clear();

            for (int ind = 0; ind < infoGridViews.Count; ind++)
            {
                InfoGridView temp = infoGridViews[ind];
                
                dataGridFindBaseDebit.Rows.Add(temp.posIndexBase, temp.bBase.DateBalance, temp.bBase.Category,
                    temp.bBase.Money, temp.bBase.Description);

            }            
        }
        //надо переделать не надежно!!!
        private void saveGrid()
        {
            for (int ind = 0; ind < infoGridViews.Count; ind++)
            {
                InfoGridView info = infoGridViews[ind];

                if (info.isflag)
                {
                    try
                    {
                        for (int i = 0; i < dataGridFindBaseDebit.Rows.Count; i++)
                        {
                            int id;
                            int.TryParse(dataGridFindBaseDebit.Rows[i].Cells[0].Value.ToString(), out id);

                            if (id == info.posIndexBase)
                            {
                                string data = dataGridFindBaseDebit.Rows[i].Cells[1].Value.ToString();
                                string category = dataGridFindBaseDebit.Rows[i].Cells[2].Value.ToString();
                                int money;
                                int.TryParse(dataGridFindBaseDebit.Rows[i].Cells[3].Value.ToString(), out money);
                                string descr = dataGridFindBaseDebit.Rows[i].Cells[4].Value.ToString();
                                BalanceBase balance = new BalanceBase(money, data, category, descr);

                                if (MessageBox.Show("Изменить:\n" + info.bBase.ToString() + "\nHа:\n" + balance.ToString(),
                                    "Внесены изменения", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                                {
                                    info.bBase = balance;
                                    //внести в базу
                                    if (balanceGet == Balance.DEBIT)
                                        PresenterForm.setBalance(info.bBase, info.posIndexBase);
                                    else if (balanceGet == Balance.CREDIT)
                                        PresenterForm.setCredit(info.bBase, info.posIndexBase);

                                    DialogResult = DialogResult.OK;
                                }
                                break;
                            }
                        }
                    }
                    catch (Exception ex) { MessageBox.Show("ошибка сохранения (" + ex.Message + ")"); }
                }
            }

            //проверяем провильно удаляем
            //сортируем список по индексу в массиве

            if (infoGridDelete.Count > 0)
            {
                infoGridDelete.Sort(infoGridDelete[0]);

                for (int i = (infoGridDelete.Count -1); i >= 0; i--)
                {
                    InfoGridView info = infoGridDelete[i];

                    if (MessageBox.Show("Удалить?\n" + info.bBase.ToString(), "Внесены изменения", 
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        //удалить из базы
                        if (balanceGet == Balance.DEBIT)
                            PresenterForm.removeBalance(info.posIndexBase);
                        else if (balanceGet == Balance.CREDIT)
                            PresenterForm.removeCredit(info.posIndexBase);

                        DialogResult = DialogResult.OK;
                    }
                }
            }
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            string text = ((TextBox)sender).Text;

            if (balanceGet == Balance.DEBIT)
                initializeGrid(text, PresenterForm.ListBalance);
            else if(balanceGet == Balance.CREDIT)
                initializeGrid(text, PresenterForm.ListCredit);

            addGrid(infoGridViews);

            buttonOK.Enabled = false;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            saveGrid();
        }

        private void dataGridFindBaseDebit_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                string tempStr = dataGridFindBaseDebit.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

                if(e.ColumnIndex == 3)
                {                   
                    try
                    {
                        int t = Convert.ToInt32(tempStr);                        
                    }
                    catch(Exception ex) 
                    {
                        isTempValueCell = true;
                        return; 
                    }
                }
                if(e.ColumnIndex == 1)
                {
                    try
                    {
                        DateTime d = DateTime.Parse(tempStr);
                    }
                    catch (Exception ex)
                    {
                        isTempValueCell = true;
                        return;
                    }
                }

                isTempValueCell = false;
            }
        }

        private void dataGridFindBaseDebit_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {            
            try
            {
                int temp;
                int.TryParse(e.Row.Cells[0].Value.ToString(), out temp);

                for (int ind = 0; ind < infoGridViews.Count; ind++)
                {
                    if (infoGridViews[ind].posIndexBase == temp)
                    {
                        InfoGridView info = infoGridViews[ind];                        
                        infoGridViews[ind] = info;

                        buttonOK.Enabled = true;

                        infoGridDelete.Add(info);
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("ошибка ячейки (" + ex.Message + ")"); }
        }

        private void dataGridFindBaseDebit_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            tempBeginValueCell = dataGridFindBaseDebit.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
        }

        private void dataGridFindBaseDebit_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (!isTempValueCell)
            {
                if (e.RowIndex != -1)
                {
                    try
                    {
                        int temp;
                        int.TryParse(dataGridFindBaseDebit.Rows[e.RowIndex].Cells[0].Value.ToString(), out temp);

                        for (int ind = 0; ind < infoGridViews.Count; ind++)
                        {
                            if (infoGridViews[ind].posIndexBase == temp)
                            {
                                InfoGridView info = infoGridViews[ind];
                                info.isflag = true;
                                infoGridViews[ind] = info;

                                buttonOK.Enabled = true;
                            }
                        }
                    }
                    catch (Exception ex) { MessageBox.Show("ошибка ячейки (" + ex.Message + ")"); }
                }
            }
            else
                dataGridFindBaseDebit.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = tempBeginValueCell;
        }

        private void подсчитатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int result = 0;
            int count = 0;
            try
            {
                for (int i = 0; i < dataGridFindBaseDebit.Rows.Count; i++)
                {
                    result += int.Parse(dataGridFindBaseDebit.Rows[i].Cells[3].Value.ToString());
                    ++count;
                }
            }
            catch(Exception ex) { result = -1; }

            MessageBox.Show("Посчитано (" + count + ") колонок\n" + "Сумма = " + result, "Итог",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
