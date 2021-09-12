using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Core.Common;
using Core.Script;

namespace PCRTools
{
    public partial class FrmScriptManager : Form
    {
        public FrmScriptManager()
        {
            InitializeComponent();
        }

        private void FrmScriptManager_Load(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Environment.CurrentDirectory;
            RefreshList();
        }

         List<ScriptMetaInfo> ScriptMetaInfos
        {
            get { return ConfigMgr.GetConfig().ScriptMetaInfos; }
            set { ConfigMgr.GetConfig().ScriptMetaInfos = value; }
        }

        void RefreshList()
        {
            listView1.Items.Clear();

            foreach (var info in ScriptMetaInfos)
            {
                var item = new ScriptListViewItem(info.Identity);
                var script = ScriptMgr.GetInstance().GetScript(info.Identity);
                item.SubItems.Add(script.Name);
                item.SubItems.Add(info.Enabled ? "开启" : "禁用");
                item.SubItems.Add(info.FilePath);
                item.ScriptMetaInfo = info;
                listView1.Items.Add(item);
            }

        }

        private void menuAdd_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ScriptMgr.GetInstance().AddScript(openFileDialog1.FileName);
                RefreshList();
            }
        }

        
    }

    public class ScriptListViewItem : ListViewItem
    {
        public ScriptListViewItem(string identity) : base(identity)
        {

        }

        public ScriptMetaInfo ScriptMetaInfo { get; set; }
    }
}
