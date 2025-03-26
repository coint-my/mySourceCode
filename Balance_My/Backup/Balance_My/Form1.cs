using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Drawing2D;

namespace Balance_My
{
    public partial class Form1 : Form
    {
        public static string DirSorce;
        public static string FileAccount;

        private ParametersForm parametrs;

        public Form1(string dirSorce, string fileName)
        {
            DirSorce = dirSorce;
            FileAccount = fileName;

            InitializeComponent();

            PresenterForm pF = new PresenterForm();

            if (PresenterForm.ListBalance.Count == 0)
            {
                доходЗаВсToolStripMenuItem.Enabled = false;
                доходЗаТекущийМесяцToolStripMenuItem.Enabled = false;
                лучшийДоходЗаМесяцToolStripMenuItem.Enabled = false;
            }
            if(PresenterForm.ListCredit.Count == 0)
                расходЗаВсеМесяцаToolStripMenuItem.Enabled = false;

            toolStripCategory.Text = StatusCategory();
            toolStripCount.Text = PresenterForm.ListBalance.Count.ToString();
            toolStripCredit.Text = PresenterForm.ListCredit.Count.ToString();

            treeView1.Nodes.Add(Path.GetFullPath(FileAccount));
            LinesList(PresenterForm.ListBalance, "Доходы");
            LinesList(PresenterForm.ListCredit, "Расходы");

            treeView1.Visible = false;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormView formView = new FormView();
            formView.ShowDialog();
        }

        private void внестиЗарплатуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormBalance formBalance = new FormBalance();

            if (formBalance.ShowDialog() == DialogResult.OK)
            {
                labelInfoSave.Text = "Вы забыли \"сохранить\" !!!";
                доходЗаТекущийМесяцToolStripMenuItem.Enabled = true;
                доходЗаВсToolStripMenuItem.Enabled = true;
                лучшийДоходЗаМесяцToolStripMenuItem.Enabled = true;
            }
            
        }

        private void расходыИзЗарплатыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCredit fC = new FormCredit();
            if (fC.ShowDialog() == DialogResult.OK)
            {
                labelInfoSave.Text = "Вы забыли \"сохранить\" !!!";
                расходЗаВсеМесяцаToolStripMenuItem.Enabled = true;
            }
        }

        private void таблицаРасходаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormViewCredit fVC = new FormViewCredit();
            fVC.ShowDialog();
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PresenterForm.SaveListBalance();
            labelInfoSave.Text = "\"OK\"";
        }

        private void доходЗаТекущийМесяцToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(PresenterForm.LoadSumMounth(), "Инфо",MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void доходЗаВсToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormDebitMount fDM = new FormDebitMount(true);
            fDM.ShowDialog();
        }

        private void лучшийДоходЗаМесяцToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int summ = PresenterForm.LoadSumMounthInt(PresenterForm.ListBalance)[0];
            int cout = 0;

            for (int i = 0; i < PresenterForm.LoadSumMounthInt(PresenterForm.ListBalance).Count; i++)
            {
                if (PresenterForm.LoadSumMounthInt(PresenterForm.ListBalance)[i] > summ)
                {
                    summ = PresenterForm.LoadSumMounthInt(PresenterForm.ListBalance)[i];
                    cout = i;
                }
            }

            MessageBox.Show(PresenterForm.LoadSumMounth(PresenterForm.ListBalance)[cout], "доход",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void расходЗаВсеМесяцаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormDebitMount fDM = new FormDebitMount(false);
            fDM.ShowDialog();
        }

        private string StatusCategory()
        {
            int[] count = new int[FormBalance.listCategory.Count];

            string[] name = new string[FormBalance.listCategory.Count];

            for (int i = 0; i < name.Length; i++)
            {
                count[i] = 0;
                name[i] = FormBalance.listCategory[i];
            }

            for (int i = 0; i < FormBalance.listCategory.Count; i++)
            {
                for (int j = 0; j < PresenterForm.ListBalance.Count; j++)
                {
                    if (PresenterForm.ListBalance[j].Category == name[i])
                        count[i]++;
                }
            }

            int idx = 0;
            int temp = 0;

            for (int i = 0; i < count.Length; i++)
            {
                if (temp < count[i])
                {
                    temp = count[i];
                    idx = i;
                }
            }

            if (PresenterForm.ListBalance.Count == 0)
                name[idx] = "?";

            return "\"" + name[idx] + "\"";
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void настройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (parametrs == null)
            {
                ParametersForm pf = new ParametersForm();

                if (pf.ShowDialog() == DialogResult.OK)
                {
                    monthCalendar.Visible = ParametersForm.isMounthCalendar;

                    treeView1.Visible = ParametersForm.isMyBrause;

                    parametrs = pf;
                }
            }

            else if (parametrs != null)
            {
                ParametersForm pf = parametrs;

                if (pf.ShowDialog() == DialogResult.OK)
                {
                    monthCalendar.Visible = ParametersForm.isMounthCalendar;

                    treeView1.Visible = ParametersForm.isMyBrause;

                    parametrs = pf;
                }
            }
        }

        private void LinesList(List<BalanceBase> _list, string _child)
        {
            TreeNode[] treeList = new TreeNode[_list.Count];

            for (int i = 0; i < treeList.Length; i++)
            {
                TreeNode tn1;

                treeList[i] = new TreeNode(_list[i].ToString(), new TreeNode[]{
                tn1 = new TreeNode(_list[i].Money.ToString() + " грн."),
                new TreeNode(_list[i].DateBalance.ToString())
                });

                tn1.BackColor = Color.GreenYellow;
            }

            treeView1.Nodes.Add(new TreeNode(_child, treeList));
        }
    }
}
