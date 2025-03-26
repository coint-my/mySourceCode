using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace windowsFormVovan
{
    public partial class Form1 : Form
    {
        List<string> lstDebit;
        List<string> lstCredit;
        List<string> lstLogins;

        DateTime dt = DateTime.Now;

        string[] strStart = new string[3];
        string[] strSS = new string[2];
        string empty = "||||";
        string timeDt = "";
        string strChar = "Приветствие!!!| | |0|";
        string strYear = "мДоход";
        string ssL = "";
        public string strLogin = "";

        char[] ch1;
        Random rand = new Random();

        int info = 0;
        int infoDebitCreditCount = 0;
        public int countString = 0;
        private int produkty;
        private int razvlechenia;
        private int raznoe;
        private int arenda;
        private int oldCredit;
        private int oldDebit;
        private int oldStateBalanse;
        private int year;
        int cout = 2;
        int coutInfoLogin = 0;
        int resultDebit = 0;
        int z = 0;
        int a = 0;

        public bool isActive = false;

        private bool cout_1, cout_2, cout_3;

        public Form1()
        {
            InitializeComponent();
            lstDebit = new List<string>();
            lstCredit = new List<string>();
            cout_1 = cout_2 = cout_3 = false;
            labelData.Text = " " + dt.Day.ToString() + " . " + mountString(dt.Month) + "я . " + dt.Year.ToString();
            label14.Text = "";
            label15.Text = mountString(dt.Month);
            label16.Text = mountString(dt.Month);
            label17.Text = "";
            tabControl1.Visible = false;
            labelD.Visible = false;
            labelData.Visible = false;
            checkBox1.CheckState = CheckState.Checked;
            textBoxDataCredit.Text = dt.ToString("d");
            button4.Enabled = false;

            ch1 = strChar.ToCharArray();

            BinaryLogin();
        }

        private void BinaryLogin()
        {
            StreamReader streamReader = new StreamReader("binary.dat");

            lstLogins = new List<string>();

            while (!streamReader.EndOfStream)
            {
                string stringBinaryLogin = streamReader.ReadLine();

                lstLogins.Add(stringBinaryLogin);
            }

            streamReader.Close();
        }

        //кнопка зарплаты
        private void button2_Click_1(object sender, EventArgs e)
        {
            if (textBoxData.Text != "" && textBoxDescription.Text != "" && textBoxSumma.Text != "")
            {
                strStart[0] = textBoxData.Text;
                strStart[1] = textBoxDescription.Text;
                strStart[2] = textBoxSumma.Text;
                int x = 0;
                int.TryParse(textBoxSumma.Text, out x);
                oldDebit += x;

                string strEnd = " |" + "Прибыль" + "|" + strStart[1] + "|" + strStart[2] + "|" + strStart[0];

                lstDebit.Add(strEnd);

                textBoxSumma.Text = "";
                textBoxDescription.Text = "";
                textBoxData.Text = "";

                dataGridDebit.Rows.Add("Прибыль", strStart[1], strStart[2] + " грн.", strStart[0]);
            }
        }

        private void textBoxDescription_TextChanged(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;

            textBoxData.Text = dt.ToString("d");
        }
        //Сохранение
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (File.Exists(strLogin + "Debit.txt") && tabControl1.Enabled == true)
            {
                StreamWriter sw = File.CreateText(strLogin + "Debit.txt");

                foreach (var x in lstDebit)
                {
                    sw.WriteLine(x);
                }
                sw.Close();
                if (File.Exists(strLogin + "Credit.txt"))
                {
                    StreamWriter _sw = File.CreateText(strLogin + "Credit.txt");
                    SaveCredit(_sw);
                    _sw.Close();
                }

                StreamWriter swMount = File.CreateText(dt.Month - 1 + strLogin + "info.txt");

                string s = produkty + "|" + razvlechenia + "|" + raznoe + "|" + oldDebit + "|" + oldStateBalanse + "|" + arenda + "|" + dt.Year;
                swMount.WriteLine(s);
                swMount.Close();

                MessageBox.Show("Выход [ " + strSS[0] + " ] ");
            }
        }

        public void SaveCredit(StreamWriter sw)
        {
            foreach (var v in lstCredit)
            {
                sw.WriteLine(v);
            }
        }
        //загрузка
        public void LoadBase()
        {
            if (File.Exists(strLogin + "Debit.txt"))
            {
                StreamReader sr = File.OpenText(strLogin + "Debit.txt");

                if (File.Exists(strLogin + "Credit.txt"))
                {
                    StreamReader sr01 = File.OpenText(strLogin + "Credit.txt");
                    LoadCredit(sr01);
                    sr01.Close();
                }

                while (!sr.EndOfStream)
                {
                    string str = sr.ReadLine();
                    string[] str2 = str.Split('|');

                    dataGridDebit.Rows.Add(str2[1], str2[2], str2[3] + " грн.", str2[4]);

                    lstDebit.Add(str);
                }
                if (!File.Exists(dt.Month - 1 + strLogin + "info.txt"))
                {
                    StreamWriter swMounth = File.CreateText(dt.Month - 1 + strLogin + "info.txt");
                    swMounth.WriteLine("0|0|0|0|0|0|0");
                    swMounth.Close();
                }

                StreamReader srMounth = File.OpenText(dt.Month - 1 + strLogin + "info.txt");

                string[] strInfoLogin = new string[12];

                string s = srMounth.ReadLine();
                string[] s1 = s.Split('|');

                int.TryParse(s1[6], out year);

                for (int strInfo = 0; strInfo < 11; strInfo++)
                {
                    StreamReader streamReaderInfo = File.OpenText(strInfo + strLogin + "info.txt");
                    strInfoLogin[strInfo] = streamReaderInfo.ReadLine();
                    string[] stringItemsInfo = strInfoLogin[strInfo].Split('|');

                    if (stringItemsInfo[0] == "0" && stringItemsInfo[1] == "0" && stringItemsInfo[2] == "0" && stringItemsInfo[3] == "0" && stringItemsInfo[4] == "0")
                    {

                    }
                    else
                    {
                        listBox1.Items.Add(year + " " + mountString(strInfo + 1) + " (" + stringItemsInfo[3] + "грн.) Остаток (" + stringItemsInfo[4] + "грн.)");
                    }
                    streamReaderInfo.Close();
                }

                int.TryParse(s1[0], out produkty);
                int.TryParse(s1[1], out razvlechenia);
                int.TryParse(s1[2], out raznoe);
                int.TryParse(s1[5], out arenda);
                int.TryParse(s1[3], out oldDebit);
                int.TryParse(s1[4], out oldStateBalanse);

                srMounth.Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        //загрузка годового отсчета за месяц

        private void checkedListBoxCredit_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            cout_1 = true;
            AddItemCredit();
        }

        public void LoadCredit(StreamReader _sr)
        {
            while (!_sr.EndOfStream)
            {
                string line = _sr.ReadLine();
                string[] addLine = line.Split('|');

                dataGridCredit.Rows.Add(addLine[1], addLine[2], addLine[3] + " грн.", addLine[4]);

                lstCredit.Add(line);
            }
        }

        #region Button3 ON & OF
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            cout_2 = true;
            AddItemCredit();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            cout_3 = true;
            AddItemCredit();
        }

        private void AddItemCredit()
        {
            if (cout_1 == true && cout_2 == true && cout_3 == true)
            {

                button3.Enabled = true;
            }
        }
        #endregion
        //Кнопка расходов
        private void button3_Click(object sender, EventArgs e)
        {
            string[] s = new string[4] { "", "", "", "" };
            string[] str = new string[3] { "", "", "" };

            s[0] = "Продукты";
            s[1] = "Развлечения";
            s[2] = "Разное";
            s[3] = "Аренда";

            int i = checkedListBoxCredit.SelectedIndex;
            str[0] = textBox2.Text;
            str[1] = textBox1.Text;
            int x = 0;
            int.TryParse(textBox1.Text, out x);

            if (i == 0)
                produkty += x;
            else if (i == 1)
                razvlechenia += x;
            else if (i == 2)
                raznoe += x;
            else if (i == 3)
                arenda += x;

            oldCredit = produkty + razvlechenia + raznoe + arenda;

            dataGridCredit.Rows.Add(s[i], str[0], str[1] + " грн.", textBoxDataCredit.Text);

            textBox1.Text = "";
            textBox2.Text = "";
            checkedListBoxCredit.SetItemChecked(i, false);

            lstCredit.Add("Расход" + "|" + s[i] + "|" + str[0] + "|" + str[1] + "|" + textBoxDataCredit.Text);

            cout_1 = cout_2 = cout_3 = false;
            button3.Enabled = false;
        }
        //Таймер
        private void timerLine_Tick(object sender, EventArgs e)
        {
            UpdateMounth();

            if (isActive == true)
            {
                tabControl1.Enabled = true;
                button4.Enabled = false;
            }

            if (checkBox3.CheckState == CheckState.Checked)
            {
                label17.Text = bestCredit();
                label17.Visible = true;
            }
            else
            {
                label17.Text = "";
            }
            if (checkBox2.CheckState == CheckState.Unchecked)
            {
                labelD.Visible = true;
                labelData.Visible = true;
            }
            else
            {
                labelD.Visible = false;
                labelData.Visible = false;
            }
            if (checkBox1.CheckState == CheckState.Unchecked)
            {
                if (info != ch1.Length + 20)
                {

                }
                else
                {
                    infoDebitCreditCount++;
                    info = 0;
                    textBoxLine.Text = "";
                    if (lstCredit.Count != 0)
                    {
                        if (infoDebitCreditCount < 2)
                        {
                            strChar = lstCredit[rand.Next(0, lstCredit.Count)].ToString();
                        }
                        else
                        {
                            strChar = lstDebit[rand.Next(0, lstDebit.Count)].ToString();
                            infoDebitCreditCount = -1;
                        }
                    }
                }
                ChangeString(strChar);
            }
            else
                ChangeString();

            if (tabControl1.Enabled == true)
            {
                label3.Visible = false;
            }
            else
                label3.Visible = true;
        }
        //строка лучшего дохода
        private string bestCredit()
        {
            //контр точка
            if (lstDebit.Count != 0)
            {
                for (int j = 0; j < lstDebit.Count; j++)
                {
                    string[] strMass = lstDebit[j].Split('|');

                    for (int i = 3; i < strMass.Length; i += 3)
                    {
                        int.TryParse(strMass[i], out z);

                        if (a < z)
                        {
                            a = z;
                            string slr = "";
                            string[] sss = lstDebit[j].Split('|');
                            slr = " " + sss[0] + " " + sss[1] + " " + sss[2] + " " + sss[3] + "гр. " + sss[4];
                            ssL = slr;
                        }
                    }
                }

            }
            else if (z < 100 && a < 100)
                ssL = "У вас недостаточный доход!!!";

            return ssL;
        }
        //Изменяющяя строка
        private void ChangeString(string str)
        {
            string[] ss = str.Split('|');
            string strLens = ss[0] + " " + ss[1] + " " + ss[2] + " " + ss[3] + " грн. " + ss[4];
            ch1 = strLens.ToCharArray();

            if (info < ch1.Length)
                textBoxLine.Text += ch1[info].ToString();

            else
            {
                textBoxLine.Text = strLens;
            }

            if (info == ch1.Length + 20)
                textBoxLine.Text = "";

            this.info++;
        }
        //Изменяющяя строка var stop
        private void ChangeString()
        {
            info = 0;
            textBoxLine.Text = "";
        }

        private void UpdateMounth()
        {
            textBoxProdukty.Text = produkty.ToString() + " грн.";
            textBoxRazvlechenia.Text = razvlechenia.ToString() + " грн.";
            textBoxRaznoe.Text = raznoe.ToString() + " грн.";
            oldCredit = produkty + razvlechenia + raznoe + arenda;
            textBoxOldCredit.Text = oldCredit.ToString() + " грн.";
            textBoxOldDebit.Text = oldDebit.ToString() + " грн.";
            textBoxArenda.Text = arenda.ToString() + " грн.";

            oldStateBalanse = oldDebit - oldCredit;
            textBoxOldStateBalanse.Text = oldStateBalanse.ToString() + " грн.";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            strSS[0] = textBoxLogin.Text;
            strSS[1] = textBoxPassword.Text;

            bool flagLogin = false;

            foreach (string item in lstLogins)
            {
                if (item.CompareTo(strSS[0]) == 0)
                {
                    strLogin = item;
                    flagLogin = true;
                }
            }

            if (textBoxPassword.Text == "00" && flagLogin)
            {
                //tabControl1.Enabled = true;
                button4.Enabled = true;
                textBoxPassword.Enabled = false;
                textBoxPassword.Text = "";

                textBoxLogin.Enabled = false;
                textBoxLogin.Text = "";
                button1.Enabled = false;

                LoadBase();

                //Form1.ActiveForm.Text += " (" + strLogin + ")";
            }
            else
            {
                cout--;
                MessageBox.Show("У вас осталось " + cout + " раз.");
                if (cout == 0)
                {
                    MessageBox.Show("Увы но выход...");
                    Form1.ActiveForm.Close();
                }
                textBoxPassword.Text = "";
                textBoxLogin.Text = "";
            }
        }

        private void timerInfo_Tick(object sender, EventArgs e)
        {
            if (coutInfoLogin == 0)
            {
                label1.Text = "Введите свое имя";
                coutInfoLogin = 1;
            }
            else
            {
                label1.Text = "Например (Вова) ";
                coutInfoLogin = 0;
            }
            if (tabControl1.Enabled == true)
            {
                tabControl1.Visible = true;
                groupBox7.Visible = false;
                coutInfoLogin = 2;
                label14.Text = "Я приветствую тебя < [[ " + strSS[0].ToString() + " ]] > ";
            }
        }

        private string mountString(int i)
        {
            string strMount = "";

            switch (i)
            {
                case 1:
                    strMount = "Январь";
                    break;
                case 2:
                    strMount = "Февраль";
                    break;
                case 3:
                    strMount = "Март";
                    break;
                case 4:
                    strMount = "Апрель";
                    break;
                case 5:
                    strMount = "Май";
                    break;
                case 6:
                    strMount = "Июнь";
                    break;
                case 7:
                    strMount = "Июль";
                    break;
                case 8:
                    strMount = "Август";
                    break;
                case 9:
                    strMount = "Сентябрь";
                    break;
                case 10:
                    strMount = "Октябрь";
                    break;
                case 11:
                    strMount = "Ноябрь";
                    break;
                case 12:
                    strMount = "Декабрь";
                    break;
            }
            return strMount.ToString();
        }

        private void создатьНовыйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lstDebit = new List<string>();

            lstCredit = new List<string>();

            if (textBoxLogin.Text != "" && textBoxPassword.Text == "00")
            {
                strLogin = textBoxLogin.Text;

                for (int i = 0; i < 12; i++)
                {
                    StreamWriter sWriter = new StreamWriter(i.ToString() + strLogin + "info.txt");

                    string stringNull = "0|0|0|0|0";

                    sWriter.WriteLine(stringNull);

                    sWriter.Close();
                }

                StreamWriter sWriterCredit = new StreamWriter(strLogin + "Credit.txt");
                sWriterCredit.Write("");
                sWriterCredit.Close();

                StreamWriter sWriterDebit = new StreamWriter(strLogin + "Debit.txt");
                sWriterDebit.Write("");
                sWriterDebit.Close();

                StreamWriter streamWriterLogin = new StreamWriter(new FileStream("binary.dat",FileMode.Append));

                streamWriterLogin.WriteLine(strLogin);

                streamWriterLogin.Close();

                BinaryLogin();
            }
            else
                MessageBox.Show("Ваш логин пуст\nэтот логин создаёт папку с вашими записями\nпароль должен быть верным.");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(this);
            form2.ShowDialog();
        }
    }
}
