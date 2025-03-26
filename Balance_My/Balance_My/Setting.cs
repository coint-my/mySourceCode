using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Balance_My
{
    public partial class Setting : Form
    {
        public Setting()
        {
            InitializeComponent();

            button1.Enabled = false;
            button_add_credit_description.Enabled = false;
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text.Length > 2)
                button1.Enabled = true;
            else
                button1.Enabled = false;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            List<string> cat_curr = Settings.category_list;

            string temp_cat = textBox1.Text;
            bool flag = false;

            for (int i = 0; i < cat_curr.Count; i++)
            {
                if (String.Equals(temp_cat.ToLower(), cat_curr[i].ToLower()))
                {
                    flag = true;

                    if (MessageBox.Show("Такая категория уже есть!", "Error", MessageBoxButtons.OKCancel) == DialogResult.OK)
                        textBox1.Text = "?";
                    else
                        textBox1.Text = "!";

                    break;
                }          
            }

            if (!flag)
            {
                cat_curr.Add(temp_cat);
                textBox1.Text = "";               
            }
        }

        private void TextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (button1.Enabled && (e.KeyCode == Keys.Enter))
            {
                Button1_Click(sender, EventArgs.Empty);
            }
        }

        private void button_add_credit_description_Click(object sender, EventArgs e)
        {
            List<string> tamp_curr = Settings.tamplate_credit;

            string temp_tamp = textBox_add_credit_decription.Text;
            bool flag = false;

            for (int i = 0; i < tamp_curr.Count; i++)
            {
                if (String.Equals(temp_tamp.ToLower(), tamp_curr[i].ToLower()))
                {
                    flag = true;

                    if (MessageBox.Show("Такая категория уже есть!", "Error", MessageBoxButtons.OKCancel) == DialogResult.OK)
                        textBox_add_credit_decription.Text = "?";
                    else
                        textBox_add_credit_decription.Text = "!";

                    break;
                }
            }

            if (!flag)
            {
                tamp_curr.Add(temp_tamp);
                textBox_add_credit_decription.Text = "";
            }
        }

        private void textBox_add_credit_decription_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text.Length > 2)
                button_add_credit_description.Enabled = true;
            else
                button_add_credit_description.Enabled = false;
        }
    }
}
