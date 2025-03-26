using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net.Sockets;

namespace MyBalanceProject
{   
    public partial class FormStart : Form
    {
        private string dirFileName = "sourse";

        private const string ip_my_server = "37.229.162.131";
        private string ip_user;
        private int port_user;
        private const string ip_local_server = "127.0.0.1";

        private TcpClient client = null;
        private System.Net.IPHostEntry curr_ip;
        NetworkStream stream = null;

        public FormStart()
        {
            curr_ip = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());

            InitializeComponent();
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

        private void buttonLogOk_Click(object sender, EventArgs e)
        {
            if (textBoxLogin.Text.ToLower() == "open" || textBoxLogin.Text.ToLower() == "открыть")
            {
                textBoxSaveLog.Enabled = true;
                textBoxLogin.Text = "";
                return;
            }

            BinaryWriter bw = new BinaryWriter(stream);
            BinaryReader br = new BinaryReader(stream);
            bw.Write(textBoxLogin.Text);
            if(br.ReadBoolean())
            {
                textBoxServerInfo.Text = "мы есть на сервере!";
                
                Balance_My.Form1 formBalance = new Balance_My.Form1(br, bw, stream);
                formBalance.ShowDialog();
                Close();
            }
            else
            {
                textBoxServerInfo.Text = textBoxLogin.Text + " нет такого на сервере!";
            }            

            textBoxLogin.Text = "";
            buttonLogOk.Enabled = false;
        }

        private void buttonSaveLog_Click(object sender, EventArgs e)
        {            
            BinaryWriter bw = new BinaryWriter(stream);
            BinaryReader br = new BinaryReader(stream);

            bw.Write("create");
            bw.Write(textBoxSaveLog.Text);

            if (br.ReadBoolean())
                textBoxServerInfo.Text = "Был создан пользователь (" + textBoxSaveLog.Text + ") успешно.";
            else
                textBoxServerInfo.Text = "Простите но такой пользователь (" + textBoxSaveLog.Text + ") уже есть!";

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

        private void FormStart_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (stream != null)
            {
                BinaryWriter bw = new BinaryWriter(stream);
                bw.Write(true);
            }
        }

        private void FormStart_Shown(object sender, EventArgs e)
        {
            string ip = curr_ip.HostName + " [" + curr_ip.AddressList[1] + "]";

            //-------------user IpAdress and Port--------------------
            textBoxIpAdress.Enabled = false;
            numericUpDownSoket.Enabled = false;
            buttonSend.Enabled = false;

            textBoxServerInfo.Text = "Попытка соединения с сервером.(" + ip + ")";

            if (MessageBox.Show("Пробуем соединится с сервером", "Information caption",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                try
                {
                    textBoxSaveLog.Enabled = false;
                    client = new TcpClient(ip_my_server, 8080);
                    stream = client.GetStream();
                    textBoxServerInfo.Text = "соединение установлено = Ваш ip[" + curr_ip.AddressList[1] + "]";
                }
                catch (Exception ex)
                {
                    textBoxServerInfo.Text = ex.Message;
                    textBoxLogin.Enabled = false;
                    textBoxSaveLog.Enabled = false;

                    //-------------user IpAdress and Port--------------------
                    textBoxIpAdress.Enabled = true;
                    numericUpDownSoket.Enabled = true;
                }
            }
            else
                Environment.Exit(0);
        }

        private void textBoxIpAdress_TextChanged(object sender, EventArgs e)
        {
            ip_user = ((TextBox)sender).Text;

            if (port_user > 10 && ip_user.Length > 8)
                buttonSend.Enabled = true;
            else
                buttonSend.Enabled = false;
        }

        private void numericUpDownSoket_KeyUp(object sender, KeyEventArgs e)
        {
            port_user = (int)((NumericUpDown)sender).Value;

            if (port_user > 10 && ip_user.Length > 8 && ((NumericUpDown)sender).Value > 0)
                buttonSend.Enabled = true;
            else
                buttonSend.Enabled = false;
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            string ip = curr_ip.HostName + " [" + curr_ip.AddressList[1] + "]";

            //-------------user IpAdress and Port--------------------
            textBoxIpAdress.Enabled = false;
            numericUpDownSoket.Enabled = false;
            buttonSend.Enabled = false;

            textBoxServerInfo.Text = "Попытка соединения с сервером.(" + ip + ")";

            if (MessageBox.Show("Пробуем соединится с сервером\n" + ip_user, "Information caption",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                try
                {
                    textBoxSaveLog.Enabled = false;
                    client = new TcpClient(ip_user, port_user);
                    stream = client.GetStream();
                    textBoxServerInfo.Text = "соединение установлено = Ваш ip[" + curr_ip.AddressList[1] + "]";
                    textBoxLogin.Enabled = true;
                    buttonLogOk.Enabled = true;
                }
                catch (Exception ex)
                {
                    textBoxServerInfo.Text = ex.Message;
                    textBoxLogin.Enabled = false;
                    textBoxSaveLog.Enabled = false;

                    //-------------user IpAdress and Port--------------------
                    textBoxIpAdress.Enabled = true;
                    numericUpDownSoket.Enabled = true;
                }
            }
            else
                Environment.Exit(0);
        }
    }
}
