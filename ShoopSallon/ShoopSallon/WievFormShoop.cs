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
    public partial class WievFormShoop : Form
    {
        public WievFormShoop()
        {
            InitializeComponent();

            button1.Enabled = false;
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            dataTemp.Rows.Clear();

            string text = textBoxName.Text;

            foreach (SallonObject item in Presenter.ListStorage)
            {
                if (item.Name.ToLower().Contains(text.ToLower()) && text != "")
                {
                    dataTemp.Rows.Add(item.Id, item.Date.ToString("d"), item.Name, item.Description, item.Count);
                }
            }
        }

        private SallonObject SetSallonSalle(int _id)
        {
            SallonObject so = null;

            foreach (SallonObject item in Presenter.ListStorage)
            {
                if (item.Id == _id)
                {
                    so = item;
                    break;
                }
            }

            return so;
        }

        private void textBoxID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string s = "0";

                if (textBoxID.Text != "")
                    s = textBoxID.Text;

                int c = int.Parse(s);

                if (SetSallonSalle(c) != null)
                {
                    textBoxTitleName.Text = SetSallonSalle(c).Name;
                    textBoxDescription.Text = SetSallonSalle(c).Description;
                    numericCount.Maximum = SetSallonSalle(c).Count;

                    if (checkMoney.CheckState == CheckState.Checked)
                        numericMoney.Value = (int)SetSallonSalle(c).Money;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Название должно быть(" + textBoxID.Text + ") числом.", ex.Message,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxID.Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkMoney.CheckState == CheckState.Checked)
                {
                    if (numericMoney.Value >= SetSallonSalle(int.Parse(textBoxID.Text)).Money)
                    {
                        SallonObject so = new SallonObject(textBoxTitleName.Text, textBoxDescription.Text, dateTimeDate.Value.Date,
                            int.Parse(textBoxID.Text), Convert.ToInt32(numericMoney.Value), Convert.ToInt32(numericCount.Value));

                        Presenter.DecrementCountID(so.Id, so.Count,so.Description);

                        textBoxDescription.Text = "";
                        textBoxID.Text = "";
                        textBoxName.Text = "";
                        textBoxTitleName.Text = "";
                        numericMoney.Value = 0;
                        numericCount.Value = 1;

                        dataTemp.Rows.Clear();

                        button1.Enabled = false;

                        Presenter.ListSalle.Add(so);


                        this.DialogResult = DialogResult.OK;
                    }
                    else
                        MessageBox.Show("Цена должна быть больше или равно закупки");
                }
                else
                {
                    SallonObject so = new SallonObject(textBoxTitleName.Text, textBoxDescription.Text, dateTimeDate.Value.Date,
                            int.Parse(textBoxID.Text), Convert.ToInt32(numericMoney.Value), Convert.ToInt32(numericCount.Value));

                    Presenter.DecrementCountID(so.Id, so.Count, so.Description);

                    textBoxDescription.Text = "";
                    textBoxID.Text = "";
                    textBoxName.Text = "";
                    textBoxTitleName.Text = "";
                    numericMoney.Value = 0;
                    numericCount.Value = 1;

                    dataTemp.Rows.Clear();

                    button1.Enabled = false;

                    Presenter.ListSalle.Add(so);

                    this.DialogResult = DialogResult.OK;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка (конвертирование)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Presenter.ListSalle.Sort();
            Presenter.ListSalle.Reverse();
        }

        private void numericMoney_KeyPress(object sender, KeyPressEventArgs e)
        {
            button1.Enabled = true;
        }
    }
}
