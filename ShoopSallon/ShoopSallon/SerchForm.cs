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
    public partial class SerchForm : Form
    {
        private List<SallonObject> listObject;

        public SerchForm(bool _isYes)
        {
            InitializeComponent();

            if (_isYes)
            {
                listObject = Presenter.ListStorage;
                this.Text = "Поиск закупки товара";
            }
            else
            {
                listObject = Presenter.ListSalle;
                this.Text = "Поиск продажа товара";
            }
        }

        private void textBoxSerch_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();

            for (int i = 0; i < listObject.Count; i++)
            {
                string s = listObject[i].ToString().ToLower();

                if (s.Contains(textBoxSerch.Text.ToLower()) && textBoxSerch.Text != "")
                {
                    dataGridView1.Rows.Add(listObject[i].Id, listObject[i].Date.ToString("d"), listObject[i].Name, 
                        listObject[i].Description, listObject[i].Money, listObject[i].Count);
                }
            }
        }
    }
}
