using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Drawing2D;
using System.Net.Sockets;

namespace Balance_My
{
    public partial class Form1 : Form
    {
        public static string DirSorce;
        private NetworkStream clientStream;
        private BinaryWriter binaryW;
        private BinaryReader binaryR;

        public Form1(BinaryReader _br, BinaryWriter _bw, NetworkStream _clientStream)
        {
            binaryW = _bw;
            binaryR = _br;
            clientStream = _clientStream;
            
            InitializeComponent();

            PresenterForm pF = new PresenterForm(_br, _bw, _clientStream);

            if (PresenterForm.ListBalance.Count == 0)
            {
                доходЗаВсToolStripMenuItem.Enabled = false;
                доходЗаТекущийМесяцToolStripMenuItem.Enabled = false;
                лучшийДоходЗаМесяцToolStripMenuItem.Enabled = false;
                if(PresenterForm.ListCredit.Count == 0)
                    анализБюджетаToolStripMenuItem.Enabled = false;
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
            PresenterForm.SaveListBalance(clientStream, binaryW, binaryR);
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

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //BinaryWriter writer = new BinaryWriter(clientStream);
            //writer.Write(true);
            this.Close();
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
            AnalizeBalance aBalance = new AnalizeBalance();
            aBalance.ShowDialog();
        }
    }
}
