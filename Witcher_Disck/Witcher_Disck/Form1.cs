using System.Drawing;
using System.Windows.Forms;

namespace Witcher_Disck
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void ScreenShot(string _p, int _counter)
        {
            Bitmap printscreen = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

            Graphics graphics = Graphics.FromImage(printscreen as Image);

            graphics.CopyFromScreen(0, 0, 0, 0, printscreen.Size);

            printscreen.Save(_p + "img" + _counter + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

            //++_counter;
        }

        private void timer1_Tick(object sender, System.EventArgs e)
        {
                            
        }
    }
}
