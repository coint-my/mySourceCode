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
    public partial class ParametersForm : Form
    {
        public static bool isMounthCalendar = false;
        public static bool isMyBrause = false;

        public ParametersForm()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.CheckState == CheckState.Checked)
                isMounthCalendar = true;

            else if (checkBox1.CheckState == CheckState.Unchecked)
                isMounthCalendar = false;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.CheckState == CheckState.Checked)
                isMyBrause = true;

            else if (checkBox2.CheckState == CheckState.Unchecked)
                isMyBrause = false;
        }
    }
}
