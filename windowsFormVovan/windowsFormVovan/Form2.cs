using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace windowsFormVovan
{
    public partial class Form2 : Form
    {
        Form1 form;

        public Form2(Form1 _form)
        {
            form = _form;

            InitializeComponent();

            labelNameLogin.Text = "Если это ваше имя (" + form.strLogin.ToUpper() + ") то нажми на кнопку 'да'";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Добро пожаловать (" + form.strLogin.ToUpper() + ") в программу подсчет баланса");
            form.isActive = true;
            Close();
        }
    }
}
