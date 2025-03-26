using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ShoopSallon
{
    public partial class Form1 : Form, IParameterForm1
    {
        private ModelSallon model;
        private bool isCol = false;

        Timer time = new Timer();

        internal static string FolderPath = "source";

        public Form1()
        {
            InitializeComponent();

            CreateFolder(FolderPath);

            model = new ModelSallon();
            model.LoadList(FolderPath + "//model.dat", FolderPath + "//model_store.dat", FolderPath + "//model_credit.dat");

            time.Interval = 1400;
            time.Start();
            time.Tick += new EventHandler(time_Tick);

            toolStripStatusLabel4.Text = Presenter.WinerCost();
        }

        private void time_Tick(object sender, EventArgs e)
        {
            if (!isCol)
            {
                SaveText.ForeColor = Color.Red;
                isCol = true;
            }
            else
            {
                SaveText.ForeColor = Color.Black;
                isCol = false;
            }
        }

        private void CreateFolder(string pathName)
        {
            DirectoryInfo di = new DirectoryInfo(pathName);

            if (!Directory.Exists(pathName))
                di.Create();

            if (!File.Exists(pathName + "//model.dat"))
                File.Create(pathName + "//model.dat");

            if (!File.Exists(pathName + "//model_store.dat"))
                File.Create(pathName + "//model_store.dat");

            if (!File.Exists(pathName + "//model_credit.dat"))
                File.Create(pathName + "//model_credit.dat");
        }

        private void импортБазыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileImport.ShowDialog() == DialogResult.OK)
            {
                Presenter.LoadListSalle(model.ImportBaseColectionSalle(openFileImport.FileName));
            }
        }

        private void таблицаПродажToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WievFormSalle wfs = new WievFormSalle(true);
            wfs.ShowDialog();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            model.SaveListSalle(Presenter.ListSalle, FolderPath + "//model.dat");
            model.SaveListStore(Presenter.ListStorage, FolderPath + "//model_store.dat");
            model.SaveListCredit(Presenter.ListCredit, FolderPath + "//model_credit.dat");

            SaveText.Visible = false;
            time.Stop();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (openFileImport.ShowDialog() == DialogResult.OK)
            {
                Presenter.LoadListStore(model.ImportBaseColectionStorage(openFileImport.FileName));
            }
        }

        private void таблицаЗакупкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WievFormSalle wfs = new WievFormSalle(false);
            wfs.ShowDialog();
        }

        private void внестиПродажуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WievFormShoop wfs = new WievFormShoop();
            if (wfs.ShowDialog() == DialogResult.OK)
            {
                SaveText.Visible = true;
            }
        }

        private void внестиЗакупкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StorageForm sf = new StorageForm();
            if (sf.ShowDialog() == DialogResult.OK)
            {
                SaveText.Visible = true;
            }
        }

        private void таблицаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WievTabbleCredit wtc = new WievTabbleCredit();
            wtc.ShowDialog();
        }

        private void внестиРасходыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreditForm cf = new CreditForm();
            cf.ShowDialog();
        }

        private void поискЗакупокToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SerchForm sf = new SerchForm(true);
            sf.ShowDialog();
            
        }

        private void поискПродажToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SerchForm sf = new SerchForm(false);
            sf.ShowDialog();
        }

        private void поискЗакупокИПродажToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SerchAllForm saf = new SerchAllForm();
            saf.ShowDialog();
        }

        private void балансToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BalanceForm bf = new BalanceForm();
            bf.ShowDialog();
        }

        private void доходыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BalanceDebitForm bdf = new BalanceDebitForm();
            bdf.ShowDialog();
        }

        private void топВсехПродажКоличествоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TopSalleForm tsf = new TopSalleForm();
            tsf.ShowDialog();
        }

        private void балансЗаМесяцToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BalanceMounth bm = new BalanceMounth();
            bm.ShowDialog();
        }

        private void параметрыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ParameterForm pf = new ParameterForm(this);

            pf.ShowDialog();
        }

        public MonthCalendar CalendarForm1(bool isV)
        {
            monthCalendar.Visible = isV;

            return monthCalendar;
        }

        public MonthCalendar CalendarForm1()
        {
            return monthCalendar;
        }

        private void параметрыToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ParameterForm pf = new ParameterForm(this);
            pf.ShowDialog();
        }

        private void продажаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WievFormShoop wfs = new WievFormShoop();
            wfs.ShowDialog();
        }

        private void закупкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StorageForm sf = new StorageForm();
            sf.ShowDialog();
        }
    }

    public interface IParameterForm1
    {
        /// <summary>
        /// календарь месяцев
        /// </summary>
        /// <param name="isVisible">видимость истина или ложь</param>
        MonthCalendar CalendarForm1(bool isVisible);
        /// <summary>
        /// календарь месяцев
        /// </summary>
        /// <returns></returns>
        MonthCalendar CalendarForm1();
    }
}
