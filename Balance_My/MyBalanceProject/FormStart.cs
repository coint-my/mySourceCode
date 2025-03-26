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
    struct AllComparerUsers
    {
        public List<string> users;
    }
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
        /// <summary>
        /// Создание файла нового пользователя
        /// </summary>
        /// <param name="_user">Имя пользователя</param>
        public void CreateUserNew(string _user)
        {
            try
            {
                BinaryWriter bw = new BinaryWriter(File.Create(dirFileName + "\\" + _user + ".dat"));
                bw.Close();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK);
            }
        }

        /// <summary>
        /// Проверка на существования файла пользователя
        /// </summary>
        /// <param name="_user">Имя пользователя</param>
        /// <returns>Если файл пользователя существует то вернет true</returns>
        private bool CheckUser(string _user)
        {
            //проверяю всех возможных пользователей в данной папке (sourse)
            DirectoryInfo dir = new DirectoryInfo(dirFileName);
            FileInfo[] files = dir.GetFiles();

            //читаю и записываю в строку всех пользователей
            List<string> temps = new List<string>();
            for (int i = 0; i < files.Length; i++)
            {
                string tt = "";
                tt = files[i].Name.Substring(0, files[i].Name.Length - 4);
                temps.Add(tt);
            }

            //смотрю на совпадения есть ли такой
            for (int i = 0; i < temps.Count; i++)
            {
                if (_user.ToLower().CompareTo(temps[i].ToLower()) == 0)
                    return true;
            }

            return false;
        }
        /// <summary>
        /// проверка на существования пользователя подсказка
        /// </summary>
        /// <param name="_user">совподения пользователя</param>
        /// <param name="b">пока просто null</param>
        /// <returns>структура лист пользователей</returns>
        private AllComparerUsers CheckUser(string _user, char b)
        {
            AllComparerUsers users = new AllComparerUsers();
            users.users = new List<string>();

            //проверяю всех возможных пользователей в данной папке (sourse)
            DirectoryInfo dir = new DirectoryInfo(dirFileName);
            FileInfo[] files = dir.GetFiles();

            //читаю и записываю в строку всех пользователей
            List<string> temps = new List<string>();
            for (int i = 0; i < files.Length; i++)
            {
                string tt = "";
                tt = files[i].Name.Substring(0, files[i].Name.Length - 4);
                temps.Add(tt);
            }
            
            //смотрю на совпадения есть ли такой
            for (int i = 0; i < temps.Count; i++)
            {
                string s1 = _user.ToLower();
                string s2 = temps[i].ToLower();
                string s3 = "";
                try { s3 = s2.Substring(0, s1.Length); } 
                catch(Exception ex) { continue; }
                if (s1.StartsWith(s3))
                    users.users.Add(temps[i]);
            }

            return users;
        }

        private void buttonLogOk_Click(object sender, EventArgs e)
        {
            if (CheckUser(textBoxLogin.Text.ToLower()))
            {
                Balance_My.Form1 formBalance = new Balance_My.Form1(dirFileName, textBoxLogin.Text.ToLower());

                formBalance.ShowDialog();

                Close();
            }
            else
            {
                MessageBox.Show("Неверный логин!\n" + "\n" + "Name : " + textBoxLogin.Text, "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            textBoxLogin.Text = "";
            buttonLogOk.Enabled = false;
        }

        private void buttonSaveLog_Click(object sender, EventArgs e)
        {
            //проверяю всех возможных пользователей в данной папке (sourse)
            DirectoryInfo dir = new DirectoryInfo(dirFileName);
            FileInfo[] files = dir.GetFiles();
            //читаю и записываю в строку всех пользователей
            List<string> temps = new List<string>();
            for (int i = 0; i < files.Length; i++)
            {
                string tt = "";
                tt = files[i].Name.Substring(0, files[i].Name.Length - 4);
                temps.Add(tt);
            }
            //смотрю на совпадения есть ли такой
            for (int i = 0; i < temps.Count; i++)
            {
                if (textBoxSaveLog.Text.ToLower().CompareTo(temps[i].ToLower()) == 0)
                {
                    MessageBox.Show("Такой пользователь уже есть! (" + temps[i] + ")", "Error", MessageBoxButtons.OK);
                    textBoxSaveLog.Text = "";
                    buttonSaveLog.Enabled = false;
                    return;
                }
            }
            //тут надо создать пользователя нового, потому-что такого нет!!!
            CreateUserNew(textBoxSaveLog.Text);

            textBoxSaveLog.Text = "";
            buttonSaveLog.Enabled = false;
        }

        private void textBoxLogin_TextChanged(object sender, EventArgs e)
        {          
            string str = ((TextBox)sender).Text;
            AllComparerUsers test = CheckUser(str, ' ');
            string sub_str = "";
          
            if (str.Length >= 2)
            {
                buttonLogOk.Enabled = true;

                for (int i = 0; i < test.users.Count; i++)
                {
                    sub_str += (test.users[i] + ".");
                }

                if (test.users.Count == 1)
                {
                    ((TextBox)sender).Text = test.users[0];
                    Select(true, true);
                }
            }
            else
                buttonLogOk.Enabled = false;

            textBoxServerInfo.Text = sub_str;
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
