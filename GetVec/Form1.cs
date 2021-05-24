using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Core.Common;
using Core.Emulators;
using Core.Model;
using Core.PCR;
using EventSystem;

namespace GetVec
{
    public partial class Form1 : Form
    {

        FrmInfo frmInfo;

        public Form1()
        {
            InitializeComponent();
            ConfigUITool.AddConfigItemsToMenuStrip(menuStrip1);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //InitFrmInfo();
            InitEmulator();
            RegisterEvents();

            var keys = Unit.GetAllIds();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            ConfigMgr.GetInstance().SaveConfig();
        }

        void InitFrmInfo()
        {
            frmInfo = new FrmInfo();
            frmInfo.GetEmulatorInfoFunc = GetEmulatorInfo;
            frmInfo.GetRegionInfoFunc = GetRegionInfo;
            frmInfo.GetImageInfoFunc = GetPicInfo;
        }

        void ShowFrmInfo()
        {
            if (frmInfo == null || frmInfo.IsDisposed)
                InitFrmInfo();
            frmInfo.Show();
        }

        void DoInput(Action<Input> callback)
        {
            DoInput(null, callback);
        }

        void DoInput(Action<FrmInput> init, Action<Input> callback)
        {
            var frmInput = new FrmInput();
            init?.Invoke(frmInput);
            if (frmInput.ShowDialog() == DialogResult.OK)
            {
                callback(frmInput.Input);
            }
        }

        Emulator emulator;

        Emulator Emulator
        {
            get { return emulator; }
            set { emulator = value; }
        }

        void InitEmulator()
        {
            Emulator = Emulator.GetInstanceByType(ConfigMgr.GetConfig().EmulatorType);
        }

        void AssertEmulatorAlive()
        {
            if (Emulator == null)
                throw new Exception("未选择模拟器");
            Emulator.AssertAlive();
            if (!emulator.IsConnected) emulator.ConnectToAdbServer();
            ResourceManager.Default.SetAspectRatioByResolution(emulator.GetResolution());
        }

        string GetSizeInfo(Size size)
        {
            return $"W={size.Width},H={size.Height}";
        }

        string GetRectInfo(Rectangle rect)
        {
            return $"X={rect.X},Y={rect.Y},W={rect.Width},H={rect.Height}";
        }

        string GetEmulatorInfo()
        {
            if (Emulator == null)
                return "未选择模拟器";
            var msg = $"模拟器:{Emulator.Name}";
            var nl = Environment.NewLine;
            if (Emulator.IsAlive)
            {
                msg += "  状态:在线";
                var resolution = Emulator.GetResolution();
                msg += $"{nl}分辨率:{resolution}";
                var aspectRatio = AspectRatio.GetAspectRatio(resolution);
                msg += $"  宽高比:{(aspectRatio?.ToString() ?? "不支持")}";
                msg += $"{nl}实际窗口大小:" + (emulator.IsAreaValid() ? GetRectInfo(emulator.Area) : "模拟器区域不合法");
            }
            else
                msg += "  状态:离线";
            return msg;
        }

        string GetRegionInfo()
        {
            return $"区域: {ConfigMgr.GetConfig().Region}";
        }

        bool HasPic()
        {
            return pictureBox1.Image != null;
        }

        string GetPicInfo()
        {
            if (!HasPic())
                return "未选择图像";
            var nl = Environment.NewLine;
            var msg = $"图像:{GetSizeInfo(pictureBox1.Image.Size)} 容器:{GetSizeInfo(pictureBox1.Size)}";
            msg += $"{nl}所选区域:{GetRectInfo(GetRect())}{nl}{GetRVec4f()}";
            return msg;
        }

        void RegisterEvents()
        {
            EventMgr.RegisterListener(EventKeys.ConfigEmulatorTypeChanged, (args) =>
            {
                InitEmulator();
            });
        }

        void SetPic(Image image)
        {
            pictureBox1.Image = image;
            Size = new Size(1, 1);
        }

        private void menuCapture_Click(object sender, EventArgs e)
        {
            AssertEmulatorAlive();
            var capture = Emulator.GetScreenCapture();
            SetPic(capture);
        }

        bool press = false;
        int startX, startY;
        int endX, endY;

        Size GetContainerSize()
        {
            return new Size(pictureBox1.Width, pictureBox1.Height);
        }

        Rectangle GetRect()
        {
            var x1 = Math.Min(startX, endX);
            var y1 = Math.Min(startY, endY);
            var x2 = Math.Max(startX, endX);
            var y2 = Math.Max(startY, endY);
            return new Rectangle(x1, y1, x2 - x1, y2 - y1);
        }

        void SetRect(RVec4f rf)
        {
            if (!HasPic())
                return;
            var rect = GetContainerSize() * rf;
            startX = rect.Left;
            startY = rect.Top;
            endX = rect.Right;
            endY = rect.Bottom;
            DrawRect();
        }

        PVec2f GetPVec2f()
        {
            return PVec2f.Div(GetContainerSize(), GetRect());
        }

        RVec2f GetRVec2f()
        {
            return RVec2f.Div(GetContainerSize(), GetRect().Size);
        }

        RVec4f GetRVec4f()
        {
            return RVec4f.Div(GetContainerSize(), GetRect());
        }

        bool IsPoint()
        {
            if (!HasPic())
                return false;
            var rect = GetRect();
            if (rect.IsEmpty)
                return false;
            return rect.Width < 5 && rect.Height < 5;
        }

        bool IsRect()
        {
            if (!HasPic())
                return false;
            var rect = GetRect();
            if (rect.IsEmpty)
                return false;
            return rect.Width >= 5 || rect.Height >= 5;
        }

        void ClearRect()
        {
            pictureBox1.Refresh();
        }

        void DrawRect()
        {
            ClearRect();
            if (!HasPic())
                return;
            var g = pictureBox1.CreateGraphics();
            var pen = new Pen(Color.Red, 1);
            var rect = GetRect();
            rect.Width = rect.Width > 0 ? rect.Width : 1;
            rect.Height = rect.Height > 0 ? rect.Height : 1;
            g.DrawRectangle(pen, rect);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            press = false;
            ClearRect();
            if (!HasPic())
                return;
            press = true;
            startX = e.X;
            startY = e.Y;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!press)
                return;
            endX = e.X;
            endY = e.Y;
            DrawRect();
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            press = false;
            endX = e.X;
            endY = e.Y;
            DrawRect();
        }

        private void menuRefreshInfo_Click(object sender, EventArgs e)
        {
            ShowFrmInfo();
        }

        void AccessModel(Action<ImageSamplingData> callback)
        {
            AssertEmulatorAlive();
            var path = ResourceManager.Default.GetFullPath("${G}/${R}/${A}/Json/" + ImageSamplingData.DefaultFileName);
            var vesc = ImageSamplingData.FromFile(path);
            callback(vesc);
            vesc.Save(path);
        }

        private void menuSaveImageSample_Click(object sender, EventArgs e)
        {
            if (!IsRect())
                throw new Exception("请先选择矩形区域");
            DoInput((frm) => { frm.ShowThreshold = true; }, (input) =>
            {
                AssertEmulatorAlive();
                var img = new Img(pictureBox1.Image);
                var partial = img.GetPartial(GetRect());
                var key = $"{input.Key}.png";
                var path = ResourceManager.Default.GetFullPath("${G}/${R}/${A}/Image/" + key);
                partial.Save(path);
                AccessModel((data) =>
                {
                    if (!data.ContainerSizes.ContainsKey(key) || MessageBox.Show($"ContainerSizes已存在相同的键:{key},继续吗?", "键名冲突", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        data.ContainerSizes.Set(key, GetContainerSize());
                    }
                    if (!data.MatchThresholds.ContainsKey(key) || MessageBox.Show($"MatchThresholds已存在相同的键:{key},继续吗?", "键名冲突", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        data.MatchThresholds.Set(key, input.Threshold);
                    }
                    if (!data.RVec4fs.ContainsKey(key) || MessageBox.Show($"RVec4fs已存在相同的键:{key},继续吗?", "键名冲突", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        data.RVec4fs.Set(key, GetRVec4f());
                    }
                });
            });
        }

        private void menuThreshold_Click(object sender, EventArgs e)
        {
            DoInput((frm) => { frm.ShowThreshold = true; }, (input) =>
            {
                AssertEmulatorAlive();
                var key = input.Key;
                AccessModel((data) =>
                {
                    if (!data.MatchThresholds.ContainsKey(key) || MessageBox.Show($"MatchThresholds已存在相同的键:{key},继续吗?", "键名冲突", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        data.MatchThresholds.Set(key, input.Threshold);
                    }
                });
            });
        }

        private void menuPoint_Click(object sender, EventArgs e)
        {
            if (!IsPoint())
                throw new Exception("请先选择点击区域");
            DoInput((input) =>
            {
                AssertEmulatorAlive();
                var key = input.Key;
                AccessModel((data) =>
                {
                    if (!data.PVec2fs.ContainsKey(key) || MessageBox.Show($"PVec2fs已存在相同的键:{key},继续吗?", "键名冲突", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        data.PVec2fs.Set(key, GetPVec2f());
                    }
                });
            });
        }

        private void menuRectSizeOnly_Click(object sender, EventArgs e)
        {
            if (!IsRect())
                throw new Exception("请先选择矩形区域");
            DoInput((input) =>
            {
                AssertEmulatorAlive();
                var key = input.Key;
                AccessModel((data) =>
                {
                    if (!data.RVec2fs.ContainsKey(key) || MessageBox.Show($"RVec2fs已存在相同的键:{key},继续吗?", "键名冲突", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        data.RVec2fs.Set(key, GetRVec2f());
                    }
                });
            });
        }

        private void menuRect_Click(object sender, EventArgs e)
        {
            if (!IsRect())
                throw new Exception("请先选择矩形区域");
            DoInput((input) =>
            {
                AssertEmulatorAlive();
                var key = input.Key;
                AccessModel((data) =>
                {
                    if (!data.RVec4fs.ContainsKey(key) || MessageBox.Show($"RVec4fs已存在相同的键:{key},继续吗?", "键名冲突", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        data.RVec4fs.Set(key, GetRVec4f());
                    }
                });
            });
        }

        private void menuSetRect_Click(object sender, EventArgs e)
        {

            DoInput((input) =>
            {
                var s = input.Key;
                try
                {
                    var rect = RVec4f.Parse(s);
                    SetRect(rect);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    MessageBox.Show("解析失败");
                }
            });
        }

        private void menuOpenAdbDir_Click(object sender, EventArgs e)
        {
            AssertEmulatorAlive();
            Utils.SelectFileInExplorer(Emulator.GetAdbExePath());
        }

        private void menuOpenResDir_Click(object sender, EventArgs e)
        {
            Utils.OpenDirectoryInExplorer(ConfigMgr.GetConfig().ResourceRootDirectory);
        }

    }
}
