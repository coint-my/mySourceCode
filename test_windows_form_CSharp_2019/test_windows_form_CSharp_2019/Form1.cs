using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test_windows_form_CSharp_2019
{
    public partial class Form1 : Form
    {
        private Label label2;
        private Label label4;
        private Label label5;
        private Card tempCard1;
        private Card tempCard2;
        private Deck deck;
        public Form1()
        {
            InitializeComponent(); 
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            deck = new Deck();
            Label label = new Label();
            label.AutoSize = true;
            label.Text = "obj_1 = ";
            Button button = new Button();
            button.Text = "TestButton";
            DateTimePicker date = new DateTimePicker();

            label2 = new Label();
            label2.AutoSize = true;

            Label label3 = new Label();
            label3.AutoSize = true;
            label3.Text = "obj_2 = ";

            label4 = new Label();
            label4.AutoSize = true;
            label5 = new Label();
            label5.AutoSize = true;

            flowLayoutPanel1.Controls.Add(label);
            flowLayoutPanel1.Controls.Add(label2);
            flowLayoutPanel1.Controls.Add(label3);
            flowLayoutPanel1.Controls.Add(label4);
            flowLayoutPanel1.Controls.Add(label5);

            deck.Reshuffle_Deck(DateTime.Now.Millisecond);

            //for (int d = 0; d < deck.Count; d++)
            //{
            //    flowLayoutPanel1.Controls.Add(deck.GetCardRef(d).panel);
            //}

            for (int i = 0; i < 5; i++)
            {
                Card card = deck.GetCardRef(i);
                int wid = card.panel.Width;
                int hei = card.panel.Height;

                card.picture.MouseUp += Panel_MouseUp;

                card.SetPosition(wid * i, 0);
                panel_table.Controls.Add(card.panel);
            }

            splitContainer1.Panel1.Controls.Add(button);

            splitContainer1.Panel2.Controls.Add(date);
            date.Name = "dateTime";
        }

        private void Panel_MouseUp(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left && (sender is PictureBox))
            {
                object obj = (sender as PictureBox).Tag;
                tempCard1 = (Card)obj;
                label2.Text = "" + tempCard1.rank + " " + tempCard1.suit;

                if((tempCard1.panel != null) && (tempCard2.panel != null))
                {
                    label5.Text = "(" + tempCard1.ToString() + " больше чем " + tempCard2.ToString() + ") = " + (tempCard1 > tempCard2);
                }
            }
            else if (e.Button == MouseButtons.Right && (sender is PictureBox))
            {
                object obj = (sender as PictureBox).Tag;
                tempCard2 = (Card)obj;
                label4.Text = "" + tempCard2.rank + " " + tempCard2.suit;

                if ((tempCard1.panel != null) && (tempCard2.panel != null))
                {
                    label5.Text = "(" + tempCard1.ToString() + " больше чем " + tempCard2.ToString() + ") = " + (tempCard1 > tempCard2);
                }
            }
        }

        private void Label1_DragEnter(object sender, DragEventArgs e)
        {
            //if(e.Data.GetDataPresent(DataFormats.Text))
            //{
            //    e.Effect = DragDropEffects.Move;
            //}
            if (e.Data.GetDataPresent(DataFormats.Text))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void Label1_MouseDown(object sender, MouseEventArgs e)
        {
            label1.DoDragDrop(label1.Text, DragDropEffects.Copy |
            DragDropEffects.Move);
        }

        private void Label1_DragDrop(object sender, DragEventArgs e)
        {
            label1.Text = " tc " + (sender as Control).Name + e.Data.GetData(DataFormats.Text);
        }

        private void Panel1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void Panel1_DragDrop(object sender, DragEventArgs e)
        {
            Panel p = (sender as Panel);
            string[] data = e.Data.GetFormats();
            Control lb = (Control)e.Data.GetData(DataFormats.FileDrop);

            p.Controls.Add(lb);
        }
    }
}
