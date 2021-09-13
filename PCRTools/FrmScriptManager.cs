using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Core.Common;
using Core.Exceptions;
using Core.Script;
using EventSystem;

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
            openFileDialog1.InitialDirectory = ResourceManager.Default.GetFullPath("${G}/Script");
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
                item.SubItems.Add(info.Priority.ToString());
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

        private void FrmScriptManager_FormClosed(object sender, FormClosedEventArgs e)
        {
            ConfigMgr.GetInstance().SaveConfig();
        }

        int AssertHasSelectedItem()
        {
            var indices = listView1.SelectedIndices;
            if (indices.Count == 0)
                throw new NotPrintStacktraceException("请先选择一项");
            return indices[0];
        }

        void RefreshPriority()
        {
            var i = 0;
            var infos = ScriptMetaInfos;
            foreach (var info in infos)
            {
                if (info.Priority <= infos.Count)
                {
                    info.Priority = ++i;
                }
            }
        }

        private void menuMoveUp_Click(object sender, EventArgs e)
        {
            var index = AssertHasSelectedItem();
            if (index == 0)
            {
                Logger.GetInstance().Warn("FrmScriptManager", $"cannot move up first item");
                return;
            }
            var infos = ScriptMetaInfos;
            var t = infos[index - 1];
            infos[index - 1] = infos[index];
            infos[index] = t;
            RefreshPriority();
            RefreshList();
            listView1.Items[index - 1].Selected = true;
            EventMgr.FireEvent(EventKeys.ScriptMetaInfosChanged);
        }

        private void menuMoveDown_Click(object sender, EventArgs e)
        {
            var index = AssertHasSelectedItem();
            var infos = ScriptMetaInfos;
            if (index == infos.Count - 1)
            {
                Logger.GetInstance().Warn("FrmScriptManager", $"cannot move down last item");
                return;
            }
            var t = infos[index + 1];
            infos[index + 1] = infos[index];
            infos[index] = t;
            RefreshPriority();
            RefreshList();
            listView1.Items[index + 1].Selected = true;
            EventMgr.FireEvent(EventKeys.ScriptMetaInfosChanged);
        }

        private void menuSwitchEnabled_Click(object sender, EventArgs e)
        {
            var index = AssertHasSelectedItem();
            var info = ScriptMetaInfos[index];
            info.Enabled = !info.Enabled;
            RefreshPriority();
            RefreshList();
            EventMgr.FireEvent(EventKeys.ScriptMetaInfosChanged);
        }

        private void menuReloadScript_Click(object sender, EventArgs e)
        {
            var index = AssertHasSelectedItem();
            var info = ScriptMetaInfos[index];
            ScriptMgr.GetInstance().ReloadScript(info.Identity);
            MessageBox.Show("重载完成");
        }

        private void menuOpenScriptInExplorer_Click(object sender, EventArgs e)
        {
            var index = AssertHasSelectedItem();
            var info = ScriptMetaInfos[index];
            Utils.SelectFileInExplorer(info.FilePath);
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
