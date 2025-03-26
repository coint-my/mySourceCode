using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace MyBalanceProject
{
    public partial class FormStart : Form
    {
        private string defName = "1010";
        private string dirFileName = "sourse";

        public FormStart()
        {
            InitializeComponent();

            if (!File.Exists(defName))
            {
                File.Create(defName);
            }

            DirectoryInfo di;

            if (!Directory.Exists(Path.GetFullPath(dirFileName)))
            {
                di = new DirectoryInfo(dirFileName);
                di.Create();
            }

        }

        private void LogFileCreate(string name)
        {
            DirectoryInfo dI = new DirectoryInfo(dirFileName);

            FileInfo[] fI = dI.GetFiles();

            bool flag = false;

            if (fI.Length == 0)
            {
                BinaryWriter bw = new BinaryWriter(File.Create(name + ".dat"));
                bw.Close();
                File.Move(Directory.GetCurrentDirectory() + "\\" + name + ".dat", dirFileName + "\\" + name + ".dat");

            }

            for (int i = 0; i < fI.Length; i++)
            {
                flag = true;

                if (fI[i].Name.CompareTo(name + ".dat") == 0)
                {
                    flag = false;
                    break;
                }
            }
            if (flag)
            {
                BinaryWriter bw = new BinaryWriter(File.Create(name + ".dat"));
                bw.Close();
                File.Move(Directory.GetCurrentDirectory() + "\\" + name + ".dat", dirFileName + "\\" + name + ".dat");
            }
        }

        private bool LoadLogin(string nameLog)
        {
            bool flag = true;

            BinaryReader br = new BinaryReader(File.Open(defName, FileMode.Open));

            while (flag)
            {
                try
                {
                    string tempName = br.ReadString().ToLower();

                    if (tempName.CompareTo(nameLog.ToLower()) == 0)
                    {
                        return true;
                        flag = false;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    break;
                }
            }

            br.Close();

            return false;
        }

        private void buttonLogOk_Click(object sender, EventArgs e)
        {
            if (LoadLogin(textBoxLogin.Text.ToLower()))
            {
                LogFileCreate(textBoxLogin.Text.ToLower());

                MessageBox.Show("приветствую вас ''" + textBoxLogin.Text.ToLower() + "''");

                Balance_My.Form1 formBalance = new Balance_My.Form1(dirFileName, textBoxLogin.Text.ToLower() + ".dat");

                formBalance.ShowDialog();

                this.Close();
            }
            else
            {
                MessageBox.Show("Неверный логин или несуществует!\n" + "\n" + sender.ToString(), "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            textBoxLogin.Text = "";
            buttonLogOk.Enabled = false;
        }

        private void buttonSaveLog_Click(object sender, EventArgs e)
        {
            bool flag = true;

            BinaryReader br = new BinaryReader(File.Open(defName, FileMode.Open));

            while (flag)
            {
                try
                {
                    string tempName = br.ReadString().ToLower();

                    if (tempName.CompareTo(textBoxSaveLog.Text.ToLower()) == 0)
                    {
                        flag = false;
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    break;
                }
            }

            br.Close();

            if (flag)
            {
                BinaryWriter bw = new BinaryWriter(File.Open(defName, FileMode.Append));

                bw.Write(textBoxSaveLog.Text.ToLower());

                bw.Close();

                MessageBox.Show("Это имя (" + textBoxSaveLog.Text.ToLower() + ") сохраню");
            }
            else
            {
                MessageBox.Show("Это имя уже существует");
            }

            textBoxSaveLog.Text = "";

            buttonSaveLog.Enabled = false;
        }

        private void textBoxLogin_TextChanged(object sender, EventArgs e)
        {
            buttonLogOk.Enabled = true;
        }

        private void textBoxSaveLog_TextChanged(object sender, EventArgs e)
        {
            if (textBoxSaveLog.Text.Length > 3)
            {
                buttonSaveLog.Enabled = true;
            }
            else
                buttonSaveLog.Enabled = false;
        }

        private void textBoxLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter && buttonLogOk.Enabled == true)
                buttonLogOk_Click(sender, e);
        }
    }
}
