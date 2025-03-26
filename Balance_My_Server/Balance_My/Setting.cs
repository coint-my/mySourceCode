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
    }
}
