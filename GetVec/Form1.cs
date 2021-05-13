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

using EventSystem;

namespace GetVec
{
    public partial class Form1 : Form
    {

        

        public Form1()
        {
            InitializeComponent();
            ConfigUITool.AddConfigItemsToMenuStrip(menuStrip1);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitEmulator();
            RefreshTitle();
            RegisterEvents();

            AccessModel((v) => { });
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            ConfigMgr.GetInstance().SaveConfig();
        }

        Emulator emulator;

        Emulator Emulator
        {
            get
            {
                if (emulator != null && emulator.IsAlive && !emulator.IsConnected)
                    emulator.ConnectToAdbServer();
                return emulator;
            }
            set
            {
                emulator = value;
            }
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
            var msg = $"模拟器: {Emulator.Name}";
            if (Emulator.IsAlive)
            {
                msg += "[ON]";
                var resolution = Emulator.GetResolution();
                var aspectRatio = AspectRatio.GetAspectRatio(resolution);
                msg += $"[{resolution}|{(aspectRatio != null ? aspectRatio.ToString() : "未找到支持的宽高比")}]";
                msg += $"[{GetRectInfo(emulator.Area)}]";
            }
            else
                msg += "[OFF]";
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
            return $"图像:{GetSizeInfo(pictureBox1.Image.Size)} 容器:{GetSizeInfo(pictureBox1.Size)} 所选区域:{startX},{startY},{endX},{endY}";
        }

        void RefreshTitle()
        {
            Invoke(new Action(() => {
                Text = $"{GetEmulatorInfo()} {GetRegionInfo()} {GetPicInfo()}";
            }));
        }

        void RegisterEvents()
        {
            EventMgr.RegisterListener(EventKeys.ConfigEmulatorTypeChanged, (args) =>
            {
                InitEmulator();
                RefreshTitle();
            });
            EventMgr.RegisterListener(EventKeys.ConfigRegionChanged, (args) => { RefreshTitle(); });
        }

        void SetPic(Image image)
        {
            pictureBox1.Image = image;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                RefreshTitle();
            }
            catch (Exception ex)
            {
                Text = $"发生错误: {ex.Message}";
            }
        }

        private void menuCapture_Click(object sender, EventArgs e)
        {
            AssertEmulatorAlive();
            var capture = Emulator.GetScreenCapture();
            SetPic(capture);
        }

        private void menuSelectAdbInExplorer_Click(object sender, EventArgs e)
        {
            AssertEmulatorAlive();
            Utils.SelectFileInExplorer(Emulator.GetAdbExePath());
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

        PVec2f GetPVec2f()
        {
            return PVec2f.Div(GetContainerSize(), GetRect());
        }

        RVec4f GetRVec4f()
        {
            return RVec4f.Div(GetContainerSize(), GetRect());
        }

        string GetValidKey()
        {
            var s = txtKey.Text.Trim();
            if (string.IsNullOrWhiteSpace(s))
                throw new Exception("Key不能为空");
            return s;
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
            RefreshTitle();
        }

        void AccessModel(Action<Vecs> callback)
        {
            AssertEmulatorAlive();
            var path = ResourceMgr.GetInstance().GetResourcePath(Emulator.GetResolution(), "vecs.json");
            var vesc = Vecs.Parse(path);
            callback(vesc);
            vesc.Save(path);
        }

        private void menuSaveImageSample_Click(object sender, EventArgs e)
        {
            var key = GetValidKey();
            AssertEmulatorAlive();
            if (!IsRect())
                throw new Exception("请先选择矩形区域");
            var img = new Img(pictureBox1.Image);
            var partial = img.GetPartial(GetRect());
            key = $"{key}.png";
            var path = ResourceMgr.GetInstance().GetResourcePath(Emulator.GetResolution(), ResourceType.Image, key);
            partial.Save(path);
            AccessModel((vecs) =>
            {
                if (!vecs.ContainerSizes.ContainsKey(key) || MessageBox.Show($"ContainerSizes已存在相同的键:{key},继续吗?", "键名冲突", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    vecs.ContainerSizes.Set(key, GetContainerSize());
                }
                if (!vecs.RVec4fs.ContainsKey(key) || MessageBox.Show($"RVec4fs已存在相同的键:{key},继续吗?", "键名冲突", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    vecs.RVec4fs.Set(key, RVec4f.Div(GetContainerSize(), GetRect()));
                }
            });
        }

        private void menuPoint_Click(object sender, EventArgs e)
        {
            var key = GetValidKey();
            AssertEmulatorAlive();
            if (!IsPoint())
                throw new Exception("请先选择点击区域");
            AccessModel((vecs) =>
            {
                if (!vecs.PVec2fs.ContainsKey(key) || MessageBox.Show($"PVec2fs已存在相同的键:{key},继续吗?", "键名冲突", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    vecs.PVec2fs.Set(key, PVec2f.Div(GetContainerSize(), GetRect()));
                }
            });
        }

        private void menuRect_Click(object sender, EventArgs e)
        {
            var key = GetValidKey();
            AssertEmulatorAlive();
            if (!IsRect())
                throw new Exception("请先选择矩形区域");
            AccessModel((vecs) =>
            {
                if (!vecs.RVec4fs.ContainsKey(key) || MessageBox.Show($"RVec4fs已存在相同的键:{key},继续吗?", "键名冲突", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    vecs.RVec4fs.Set(key, RVec4f.Div(GetContainerSize(), GetRect()));
                }
            });
        }
    }
}
