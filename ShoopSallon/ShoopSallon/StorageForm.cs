using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ShoopSallon
{
    public partial class StorageForm : Form
    {
        private static int ID;

        public StorageForm()
        {
            InitializeComponent();

            InitializeNames(Presenter.ListStorage);

            checkBox1.Enabled = false;
            comboTempDesr.Enabled = false;

            string[] str = InitializeNames(Presenter.ListStorage);

            for (int i = 0; i < str.Length; i++)
            {
                comboBoxName.Items.Add(str[i]);
            }
        }

        private string[] InitializeNames(List<SallonObject> _list)
        {
            List<string> list = new List<string>();

            list.Add(_list[0].Name);

            for (int i = 0; i < _list.Count; i++)
            {
                string s = _list[i].Name;
                bool isFinde = false;

                for (int j = 0; j < list.Count; j++)
                {
                    if (s == list[j])
                    {
                        isFinde = true;
                    }
                }

                if (!isFinde)
                {
                    list.Add(s);
                }
            }

            list.Sort();

            string[] str = new string[list.Count];

            for (int i = 0; i < list.Count; i++)
			{
			    str[i] = list[i];
			}

            return str;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.CheckState == CheckState.Checked)
            {
                comboTempDesr.Enabled = true;
                textBoxDescription.Enabled = false;

                string[] str = FindeDecription(Presenter.ListStorage, comboBoxName.Text);

                for (int i = 0; i < str.Length; i++)
                {
                    comboTempDesr.Items.Add(str[i]);
                }
            }
            else
            {
                comboTempDesr.Enabled = false;
                textBoxDescription.Enabled = true;
            }
        }

        private string[] FindeDecription(List<SallonObject> _list, string _name)
        {
            List<string> list = new List<string>();

            for (int i = 0; i < _list.Count; i++)
            {
                if (_list[i].Name == _name)
                {
                    list.Add(_list[i].Description);
                }
            }

            string[] str = new string[list.Count];

            for (int j = 0; j < list.Count; j++)
            {
                str[j] = list[j];
            }

            return str;
        }

        private void comboBoxName_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkBox1.Enabled = true;
        }

        private void comboTempDesr_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxDescription.Enabled = false;

            int t = comboTempDesr.SelectedIndex;

            comboTempDesr.Text = "";

            string tempTextDescr = comboTempDesr.Items[t].ToString();

            string[] tempDescr = tempTextDescr.Split('(');

            string s = tempDescr[1].Remove(tempDescr[1].Length - 1);

            for (int i = 0; i < Presenter.ListStorage.Count; i++)
            {
                if (tempTextDescr == Presenter.ListStorage[i].Description)
                {
                    ID = Presenter.ListStorage[i].Id;
                    numericSumma.Value = Presenter.ListStorage[i].Money;
                    numericCount.Value = Presenter.ListStorage[i].Count;
                }
            }

            textBoxDescription.Text = tempDescr[0];
            textBoxChange.Text = s;
            textBoxID.Text = ID.ToString();
            checkBox1.Enabled = false;
        }

        private void textBoxChange_TextChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;

            int yCout = textBoxChange.Text.Split(',').Length;
            int xCout = textBoxChange.Text.Split('.').Length;

            yCout += xCout;
            yCout -= 1;

            numericCount.Value = yCout;
        }

        private int FindeID(List<SallonObject> _list)
        {
            int alpha = 0;

            for (int i = 0; i < _list.Count; i++)
            {
                if (alpha < _list[i].Id)
                    alpha = _list[i].Id;
            }

            return alpha;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBoxDescription.Text != "" && textBoxChange.Text != "" && numericSumma.Value != 0)
            {
                SallonObject so = new SallonObject(comboBoxName.Text, textBoxDescription.Text + "(" + textBoxChange.Text + ")",
                    dateTimePicker1.Value.Date, ID, Convert.ToInt32(numericSumma.Value), Convert.ToInt32(numericCount.Value));

                if (checkBox1.CheckState == CheckState.Unchecked)
                    Presenter.ListStorage.Add(so);
                else
                {
                    for (int i = 0; i < Presenter.ListStorage.Count; i++)
                    {
                        if (Presenter.ListStorage[i].Id == ID)
                            Presenter.ListStorage[i] = so;
                    }
                }

                this.DialogResult = DialogResult.OK;
            }
            else
                MessageBox.Show("Нужно внести значения!!!", "Ошибка пустых полей", MessageBoxButtons.OK, MessageBoxIcon.Error);

            comboBoxName.Text = "";
            comboTempDesr.Text = "";
            textBoxChange.Text = "";
            textBoxDescription.Text = "";
            textBoxID.Text = "";
            numericSumma.Value = 0;
            numericCount.Value = 1;
            button1.Enabled = false;
            checkBox1.Enabled = true;
            checkBox1.CheckState = CheckState.Unchecked;

            Presenter.ListStorage.Sort();
            Presenter.ListStorage.Reverse();
        }

        private void textBoxDescription_TextChanged(object sender, EventArgs e)
        {
            int id = 0;

            if (checkBox1.CheckState == CheckState.Unchecked)
            {
                id = FindeID(Presenter.ListStorage);
                id += 1;
                ID = id;
            }

            textBoxID.Text = ID.ToString();
        }
    }
}
