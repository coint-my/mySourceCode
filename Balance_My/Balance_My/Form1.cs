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
        private static bool isSave = true;
        public static string DirSorce;
        public static string FileAccount;
        static string fileNameFullPath;

        public Form1(string dirSorce, string fileName)
        {
            DirSorce = dirSorce;
            FileAccount = fileName;
            fileNameFullPath = DirSorce + "\\" + FileAccount;

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

            //toolStripCategory.Text = StatusCategory();
            toolStripCount.Text = PresenterForm.ListBalance.Count.ToString();
            toolStripCredit.Text = PresenterForm.ListCredit.Count.ToString();

            //treeView1.Nodes.Add(Path.GetFullPath(FileAccount));
            //LinesList(PresenterForm.ListBalance, "Доходы");
            //LinesList(PresenterForm.ListCredit, "Расходы");
            //treeView1.Visible = false;
            string str = "";
            System.Diagnostics.Process[] pr = System.Diagnostics.Process.GetProcesses();
            for (int i = 0; i < pr.Length; i++)
            {
                str += pr[i].ProcessName + "\r\n";
            }
            textBoxProcess.Text = str;
        }

        public void InfoSave(bool isflag)
        {
            if (!isflag)
            {
                labelInfoSave.Text = "Вы забыли \"сохранить\" !!!";
                isSave = false;
            }
            else
            {
                labelInfoSave.Text = "\"OK\"";
                isSave = true;
            }
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
                доходЗаТекущийМесяцToolStripMenuItem.Enabled = true;
                доходЗаВсToolStripMenuItem.Enabled = true;
                лучшийДоходЗаМесяцToolStripMenuItem.Enabled = true;
                InfoSave(false);
            }
            
        }

        private void расходыИзЗарплатыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCredit fC = new FormCredit();
            if (fC.ShowDialog() == DialogResult.OK)
            {
                расходЗаВсеМесяцаToolStripMenuItem.Enabled = true;
                InfoSave(false);
            }
        }

        private void таблицаРасходаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormViewCredit fVC = new FormViewCredit();
            fVC.ShowDialog();
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PresenterForm.SaveListBalance(fileNameFullPath + ".dat");
            InfoSave(true);
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

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void настройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Setting setting = new Setting();
            setting.ShowDialog();
        }

        private void всеПроцессыНаМашинеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem t = (ToolStripMenuItem)sender;

            if (t.Checked)
            {
                t.CheckState = CheckState.Unchecked;
                textBoxProcess.Visible = false;
            }
            else
            {
                t.CheckState = CheckState.Checked;
                textBoxProcess.Visible = true;
            }
        }

        private void анализБюджетаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AnalyzeBalance analyze = new AnalyzeBalance();
            analyze.ShowDialog(this);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isSave)
            {
                if (MessageBox.Show("Сохранить (Базу Данных) перед выходом?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
                {
                    сохранитьToolStripMenuItem_Click(sender, null);
                }
            }
        }

        private void редактироватьБазуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FinderBaseDebit fBD = new FinderBaseDebit(Balance.DEBIT);
            if(fBD.ShowDialog(this) == DialogResult.OK)
            {
                InfoSave(false);
            }
        }

        private void редактироватьБазуРасходаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FinderBaseDebit fBD = new FinderBaseDebit(Balance.CREDIT);
            if (fBD.ShowDialog(this) == DialogResult.OK)
            {
                InfoSave(false);
            }
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = " text file (*.txt)|*.txt|data file (*.dat)|*.dat";
            saveFile.FilterIndex = 1;
            saveFile.RestoreDirectory = true;
            
            if(saveFile.ShowDialog(this) == DialogResult.OK)
            {
                string tempName = saveFile.FileName;
                if (saveFile.FilterIndex == 1)
                    PresenterForm.SaveListBalanceTXT(tempName);
                else if(saveFile.FilterIndex == 2)
                    PresenterForm.SaveListBalance(tempName);
            }
        }

        private void анализаторToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Analizator analizator = new Analizator();
            analizator.ShowDialog(this);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
                return;

            menuStrip1.Focus();
            toolStripMenuItem2.Select();
            toolStripMenuItem2.ShowDropDown();

            if (e.KeyCode == Keys.Escape)
                Close();
        }

        private void openBaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog myDialog = new OpenFileDialog())
            {
                myDialog.InitialDirectory = Directory.GetCurrentDirectory() + "\\sourse";
                myDialog.Filter = "file dataBase (*.dat) | *.dat";
                myDialog.RestoreDirectory = true;

                if(myDialog.ShowDialog() == DialogResult.OK)
                {
                    string myFileNameAccount = myDialog.SafeFileName.Split('.')[0];

                    Dispose(true);
                    Close();

                    Balance_My.Form1 formBalance = new Balance_My.Form1(DirSorce, myFileNameAccount.ToLower());

                    formBalance.ShowDialog();
                }
            }
        }
    }
}
