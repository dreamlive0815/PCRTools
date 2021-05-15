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
            ShowThreshold = false;
        }
        
        public bool ShowThreshold
        {
            get { return panel1.Visible; }
            set { panel1.Visible = value; }
        }

        int GetThreshold()
        {
            int threshold = 0;
            int.TryParse(txtThreshold.Text, out threshold);
            return threshold;
        }

        public Input Input
        {
            get
            {
                return new Input()
                {
                    Key = txtKey.Text,
                    Threshold = GetThreshold(),
                };
            }
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

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtKey.Text))
            {
                MessageBox.Show("键名不能为空");
                return;
            }
            if (ShowThreshold && GetThreshold() == 0)
            {
                MessageBox.Show("阈值不能为0");
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void FrmInput_Load(object sender, EventArgs e)
        {
        }
    }

    public class Input
    {
        public string Key { get; set; }

        public int Threshold { get; set; }
    }
}
