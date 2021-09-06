
using System;
using System.Windows.Forms;

using Core.Common;

namespace PCRTools
{
    public partial class FrmRuntimeInfo : Form
    {
        public FrmRuntimeInfo()
        {
            InitializeComponent();
            GetRuntimeInfoFunc = DefaultRuntimeInfoFunc;
        }

        private void FrmRuntimeInfo_Load(object sender, EventArgs e)
        {
        }

        public Func<string> GetRuntimeInfoFunc { get; set; }


        string DefaultRuntimeInfoFunc()
        {
            return "!!!RuntimeInfoFunc is not set!!!";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                txtOutput.Text = GetRuntimeInfoFunc();
            }
            catch (Exception ex)
            {
                txtOutput.Text = Utils.GetErrorDescription(ex);
            }
        }

        void WhenKeyUp(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                timer1.Enabled = !timer1.Enabled;
                Text = timer1.Enabled ? "Running" : "Stoped";
            }
        }

        private void FrmRuntimeInfo_KeyUp(object sender, KeyEventArgs e)
        {
            WhenKeyUp(e);
        }

        private void txtOutput_KeyUp(object sender, KeyEventArgs e)
        {
            WhenKeyUp(e);
        }
    }
}
