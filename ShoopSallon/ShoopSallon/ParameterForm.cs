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
    public partial class ParameterForm : Form
    {
        IParameterForm1 ipf;

        public ParameterForm(Form1 _form)
        {
            InitializeComponent();

            ipf = _form;

            checkBoxCalendar.Checked = ipf.CalendarForm1().Visible;
        }

        private void checkBoxCalendar_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCalendar.CheckState == CheckState.Checked)
            {
                ipf.CalendarForm1(true);
            }
            else
            {
                ipf.CalendarForm1(false);
            }
        }
    }
}
