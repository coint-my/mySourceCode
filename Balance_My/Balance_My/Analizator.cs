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
    public partial class Analizator : Form
    {
        private Graphics graphic;
        private List<ForAnalizator> myYearList;
        private List<ForAnalizator> myMonthList;

        public Analizator()
        {
            InitializeComponent();

            myMonthList = PresenterForm.my_get_parser_month();
            myInitializeListView(ref myMonthList);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Graphics g = pictureBox2.CreateGraphics();
            //Pen p = new Pen(Color.Blue, 5f);
            //Point[] points = new Point[12];

            //for (int ind = 0; ind < points.Length; ind++)
            //{
            //    points[ind] = new Point((int)(Math.Cos(ind * (3.1415 * 2 / points.Length)) * 100 + 100),
            //        (int)(Math.Sin(ind * (3.1415 * 2 / points.Length)) * 100 + 100));
            //}

            //g.DrawLines(p, points);

            //graphic = pictureBox1.CreateGraphics();
            //graphic.Clear(Color.White);

            //PresenterForm.my_graph_radial_show(ref graphic, 1100, 100, new Point(100, 140), "april");
            //PresenterForm.my_graph_radial_show(ref graphic, 1100, 5200, new Point(0, 140), "may");
            //PresenterForm.my_graph_radial_show(ref graphic, 1100, 1200, new Point(0, 20), "August");
            //PresenterForm.my_graph_radial_show(ref graphic, 1100, 600, new Point(100, 20), "desember");
        }

        private void myInitializeListView(ref List<ForAnalizator> _list)
        {
            myYearList = myParseListYearAnalizators(ref _list);
            Image img = Bitmap.FromFile("myImage//ico.bmp");
            ImageList imageList = new ImageList();
            listView1.Items.Clear();
            Font my_font = new Font("System", 8, FontStyle.Bold);

            for (int ind = 0; ind < myYearList.Count; ind++)
            {
                ListViewItem item_test = new ListViewItem("test", 0);
                item_test.Font = my_font;
                item_test.Tag = myYearList[ind];
                imageList.Images.Add(img);
                item_test.Text = myYearList[ind].date.Year + " г. доход = " + myYearList[ind].debit +
                    " расход = " + myYearList[ind].credit;

                listView1.LargeImageList = imageList;
                listView1.Items.Add(item_test);
            }
        }

        public List<ForAnalizator> myParseListYearAnalizators(ref List<ForAnalizator> _list)
        {
            List<ForAnalizator> list = null;

            if(_list.Count > 0)
            {
                list = new List<ForAnalizator>();
                int debit = 0;
                int credit = 0;
                DateTime old_dt = _list[0].date;
                for (int ind = 0; ind < _list.Count; ind++)
                {
                    DateTime curr_dt = _list[ind].date;
                    if(old_dt.Year == curr_dt.Year)
                    {
                        debit += _list[ind].debit;
                        credit += _list[ind].credit;
                    }
                    else
                    {
                        ForAnalizator fa = new ForAnalizator();
                        fa.credit = credit;
                        fa.debit = debit;
                        fa.date = old_dt;
                        old_dt = curr_dt;
                        debit = _list[ind].debit;
                        credit = _list[ind].credit;
                        list.Add(fa);
                    }
                }
                ForAnalizator fa_end = new ForAnalizator();
                fa_end.credit = credit;
                fa_end.debit = debit;
                fa_end.date = old_dt;
                list.Add(fa_end);
            }

            return list;
        }

        private void Analizator_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }

        private void myGraphYearDraw()
        {
            ListViewItem item = listView1.SelectedItems[0];
            ForAnalizator fa = (ForAnalizator)item.Tag;
            DateTime dt = fa.date;
            List<ForAnalizator> myYear = new List<ForAnalizator>();
            for (int ind = 0; ind < myMonthList.Count; ind++)
            {
                if (myMonthList[ind].date.Year == dt.Year)
                    myYear.Add(myMonthList[ind]);
            }

            graphic = pictureBox1.CreateGraphics();
            graphic.Clear(Color.White);
            for (int ind = 0; ind < myYear.Count; ind++)
            {
                int x = 0 + 100 * (ind % 6);
                int y = ind > 5 ? 140 : 20;
                PresenterForm.my_graph_radial_show(ref graphic, myYear[ind].debit, myYear[ind].credit,
                    new Point(x, y), myYear[ind].date.ToString("MMMM"));
            }
            myYear.Clear();            
            //PresenterForm.my_graph_radial_show(ref graphic, 1100, 100, new Point(100, 140), "april");
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            myGraphYearDraw();
        }

        private void listView1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                myGraphYearDraw();
        }
    }
}
