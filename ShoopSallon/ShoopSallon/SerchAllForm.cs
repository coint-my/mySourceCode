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
    public partial class SerchAllForm : Form
    {
        private List<SallonObject> lLeft;
        private List<SallonObject> lRight;

        public SerchAllForm()
        {
            InitializeComponent();

            lLeft = Presenter.ListSalle;
            lRight = Presenter.ListStorage;
        }

        private void groupBox1_SizeChanged(object sender, EventArgs e)
        {
            int xWidth = groupBox1.Size.Width;

            int result = xWidth / 2;

            Point point = new Point(result, groupBox2.Location.Y);
            Size size = new Size(result - 6, groupBox2.Size.Height);
            Size oneSize = new Size(result - 10, groupBoxOne.Size.Height);

            groupBox2.Location = point;
            groupBox2.Size = size;
            groupBoxOne.Size = oneSize;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();

            for (int i = 0; i < lLeft.Count; i++)
            {
                string s = lLeft[i].ToString().ToLower();

                if (s.Contains(textBoxSerch.Text.ToLower()) && textBoxSerch.Text != "")
                {
                    dataGridView1.Rows.Add(lLeft[i].Id, lLeft[i].Date.ToString("d"), lLeft[i].Name,
                        lLeft[i].Description, lLeft[i].Money, lLeft[i].Count);
                }
            }

            dataGridView2.Rows.Clear();

            for (int i = 0; i < lRight.Count; i++)
            {
                string s = lRight[i].ToString().ToLower();

                if (s.Contains(textBoxSerch.Text.ToLower()) && textBoxSerch.Text != "")
                {
                    dataGridView2.Rows.Add(lRight[i].Id, lRight[i].Date.ToString("d"), lRight[i].Name,
                        lRight[i].Description, lRight[i].Money, lRight[i].Count);
                }
            }
        }
    }
}
