using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GetVec
{
    public partial class FrmInput : Form
    {
        public FrmInput()
        {
            InitializeComponent();
        }

        

        private void txtThreshold_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back)
            {
            }
            else if (e.KeyChar < '0' || e.KeyChar > '9')
            {
                e.Handled = true;
            }
            else
            {
                var i = int.Parse(txtThreshold.Text + e.KeyChar);
                if (i > 100)
                    e.Handled = true;
            }
        }
    }
}
