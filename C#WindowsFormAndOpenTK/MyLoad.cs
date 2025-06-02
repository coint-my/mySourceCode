using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C_WindowsFormAndOpenTK
{
    public partial class MyLoad : Form
    {
        public MyLoad()
        {
            InitializeComponent();
        }

        public void MyLoadProgressBar(string _nameLoad, int _valuePrecentLoad)
        {
            label1.Text = _nameLoad;
            if (progressBar1.Value + _valuePrecentLoad > 100)
                progressBar1.Value = 100;
            else
                progressBar1.Value += _valuePrecentLoad;
        }
    }
}
