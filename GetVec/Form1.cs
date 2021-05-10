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
using EventSystem;

namespace GetVec
{
    public partial class Form1 : Form
    {

        Emulator emulator;

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
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            ConfigMgr.GetInstance().SaveConfig();
        }

        void InitEmulator()
        {
            emulator = Emulator.GetInstanceByType(ConfigMgr.GetConfig().EmulatorType);
        }

        void AssertEmulatorAlive()
        {
            if (emulator == null)
                throw new Exception("未选择模拟器");
            if (!emulator.Alive)
                throw new Exception("模拟器不在线");
        }

        string GetEmulatorInfo()
        {
            if (emulator == null)
                return "未选择模拟器";
            var msg = $"模拟器: {emulator.Name}";
            if (emulator.Alive)
            {
                msg += "[ON]";
                var resolution = emulator.GetResolution();
                var aspectRatio = AspectRatio.GetAspectRatio(resolution);
                msg += $"[{resolution},{(aspectRatio != null ? aspectRatio.ToString() : "未找到支持的宽高比")}]";
            }
            else
                msg += "[OFF]";
            return msg;
        }

        string GetRegionInfo()
        {
            return $"区域: {ConfigMgr.GetConfig().Region}";
        }

        string GetPicInfo()
        {
            if (pictureBox1.Image == null)
                return "未选择图像";
            return $"当前图像尺寸: {pictureBox1.Image.Size} 容器尺寸: {pictureBox1.Size}";
        }

        void RefreshTitle()
        {
            Invoke(new Action(() => {
                Text = $"{GetEmulatorInfo()}  {GetRegionInfo()} {GetPicInfo()}";
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
            RefreshTitle();
        }

        private void menuCapture_Click(object sender, EventArgs e)
        {
            AssertEmulatorAlive();
            var capture = emulator.GetScreenCapture();
            SetPic(capture);
        }


        bool press = false;
        int startX, startY;
        int endX, endY;

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {

        }

        



        //bool press = false;
        //int startX, startY;
        //Rectangle rectangle;

        //private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        //{
        //    rectangle = new Rectangle();
        //    press = true;
        //    startX = e.X;
        //    startY = e.Y;
        //}

        //void RefreshTitle(int x, int y)
        //{
        //    if (pictureBox1.Image == null)
        //        return;
        //    var head = $"Image:{pictureBox1.Image.Width},{pictureBox1.Image.Height} PicBox:{pictureBox1.Width},{pictureBox1.Height} ";
        //    Text = $"{head}{x},{y} {GetPointRate().Format()} {GetRectRate().Format()}";
        //}

        //private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        //{
        //    RefreshTitle(e.X, e.Y);
        //    if (!press) return;
        //    pictureBox1.Refresh();
        //    var g = pictureBox1.CreateGraphics();
        //    var pen = new Pen(Color.Red, 2);
        //    var x1 = Math.Min(startX, e.X);
        //    var y1 = Math.Min(startY, e.Y);
        //    var x2 = Math.Max(startX, e.X);
        //    var y2 = Math.Max(startY, e.Y);
        //    rectangle = new Rectangle(x1, y1, x2 - x1, y2 - y1);
        //    g.DrawRectangle(pen, rectangle);
        //}

        //private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        //{
        //    press = false;
        //    var width = pictureBox1.Width;
        //    var height = pictureBox1.Height;
        //    var rect = rectangle;
        //    if (rect.Width > 5 && rect.Height > 5)
        //    {
        //        var rectRate = GetRectRate();
        //        var s = $"\"{fileName}\": {GetRectRate().FormatAsJsonArray()},";
        //        Clipboard.SetText(s);
        //    }
        //    else
        //    {
        //        if (rectangle.X == 0 || rectangle.Y == 0)
        //        {
        //            rectangle = new Rectangle(e.X, e.Y, 0, 0);
        //        }
        //        var pointRate = GetPointRate();
        //        var s = pointRate.FormatAsJsonArray();
        //        Clipboard.SetText(s);
        //    }
        //    RefreshTitle(e.X, e.Y);
        //}

        //string FormatFloat(double f)
        //{
        //    return f.ToString("f4");
        //}

        //Vec2f GetPointRate()
        //{
        //    var width = pictureBox1.Width;
        //    var height = pictureBox1.Height;
        //    var midrx = 1.0f * (rectangle.X + 0.5f * rectangle.Width) / width;
        //    var midry = 1.0f * (rectangle.Y + 0.5f * rectangle.Height) / height;
        //    return new Vec2f(midrx, midry);
        //}

        //Vec4f GetRectRate()
        //{
        //    var width = pictureBox1.Width;
        //    var height = pictureBox1.Height;
        //    var rx1 = 1.0f * rectangle.X / width;
        //    var ry1 = 1.0f * rectangle.Y / height;
        //    var rx2 = 1.0f * (rectangle.X + rectangle.Width) / width;
        //    var ry2 = 1.0f * (rectangle.Y + rectangle.Height) / height;
        //    return new Vec4f(rx1, ry1, rx2, ry2);
        //}

        //void SaveImageByRectRate(Vec4f rectRate)
        //{
        //    if (pictureBox1.Image == null)
        //    {
        //        MessageBox.Show("请先捕获截图");
        //        return;
        //    }

        //    var mat = new Bitmap(pictureBox1.Image).ToOpenCvMat();
        //    var childMat = mat.GetChildMatByRectRate(rectRate);
        //    var isPartial = Math.Abs(rectRate.Item0 - rectRate.Item2) < 1
        //        || Math.Abs(rectRate.Item1 - rectRate.Item3) < 1;
        //    var saveDialog = new SaveFileDialog();
        //    saveDialog.Title = "选择图片保存路径(" + (isPartial ? "部分" : "完整");
        //    saveDialog.Filter = "*.png|*.png";
        //    if (saveDialog.ShowDialog() == DialogResult.OK)
        //    {
        //        childMat.SaveImage(saveDialog.FileName);
        //        var name = new FileInfo(saveDialog.FileName).Name;
        //        fileName = name;
        //        var s = $"\"{fileName}\": {GetRectRate().FormatAsJsonArray()},";
        //        Clipboard.SetText(s);
        //    }
        //}

        //string fileName = "";

        //private void Form1_KeyUp(object sender, KeyEventArgs e)
        //{
        //    if (e.Control && e.KeyCode == Keys.S)
        //    {
        //        var pwid = pictureBox1.Width;
        //        var phei = pictureBox1.Height;
        //        var isPartial = !e.Alt;
        //        var rect = isPartial ? rectangle : new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height);
        //        if (rect.Width == 0 || rect.Height == 0)
        //            return;
        //        var rectRate = GetRectRate();
        //        SaveImageByRectRate(rectRate);
        //    }
        //    else if (e.KeyCode == Keys.F5)
        //    {
        //        RefreshImageByViewportCapture();
        //    }
        //    else if (e.Control && e.KeyCode == Keys.I)
        //    {
        //        var dialog = new InputDialog();
        //        if (dialog.ShowDialog() == DialogResult.OK)
        //        {
        //            var rectRate = dialog.GetVec4f();
        //            if (Math.Abs(rectRate.Item2 - rectRate.Item0) < 0.001f || Math.Abs(rectRate.Item3 - rectRate.Item1) < 0.001f)
        //                return;
        //            SaveImageByRectRate(rectRate);
        //        }
        //    }
        //}
    }
}
