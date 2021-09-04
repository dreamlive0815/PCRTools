
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
using SimpleHTTPClient;

namespace PCRTools
{
    public partial class FrmPCRArenaTimer : Form
    {
        public FrmPCRArenaTimer()
        {
            InitializeComponent();
        }

        private void FrmPCRArenaTimer_Load(object sender, EventArgs e)
        {
            SetInputTime(GetNowTime().AddMinutes(5));

            InitOffsetAsync();
        }

        void InitOffsetAsync()
        {
            Task.Run(() =>
            {
                long timeStamp = DateTimeOffset.Now.ToUnixTimeSeconds();
                Console.WriteLine(timeStamp);
                var startTicks = DateTime.Now.Ticks;
                using (var client = new Client())
                {
                    var s = client.Get("http://bjtime.cn/nt3.php");
                    Logger.GetInstance().Info("PCRArenaTimer", "http://bjtime.cn/nt3.php: " + s);
                    var endTicks = DateTime.Now.Ticks;
                    var spanInMS = (endTicks - startTicks) / 10000;
                    var ss = s.Split(' ');
                    var bjTimeStamp0 = double.Parse(ss[0]);
                    var bjTimeStamp1 = long.Parse(ss[1]);
                    var diff = bjTimeStamp0 + bjTimeStamp1 - timeStamp;

                    Invoke(new Action(() =>
                    {
                        var offset = (int)(diff * 1000 - spanInMS);
                        txtOffset.Text = offset.ToString();
                        label1.Text = $"{diff.ToString("0.00")}s {spanInMS}ms";
                    }));
                    
                }
            });
        }

        DateTime GetNowTime()
        {
            var offsetMS = 0;
            int.TryParse(txtOffset.Text, out offsetMS);
            return DateTime.Now.AddMilliseconds(offsetMS);
        }

        DateTime GetInputTime()
        {
            var now = GetNowTime();
            var hour = int.Parse(txtHour.Text);
            var minute = int.Parse(txtMinute.Text);
            var secondF = double.Parse(txtSecond.Text);
            var second = (int)Math.Floor(secondF);
            var ms = (int)((secondF - second) * 1000);
            var target = new DateTime(now.Year, now.Month, now.Day, hour, minute, second);
            target = target.AddMilliseconds(ms);
            if (target.Ticks < now.Ticks)
            {
                target = target.AddDays(1);
            }
            return target;
        }

        void SetInputTime(DateTime datetime)
        {
            txtHour.Text = datetime.Hour.ToString();
            txtMinute.Text = datetime.Minute.ToString();
            txtSecond.Text = datetime.Second.ToString();
            if (datetime.Millisecond > 100)
            {
                var n = datetime.Millisecond / 100;
                txtSecond.Text += $".{n}";
            }
        }

        DateTime target;
        bool bPreTap;

        void StartCountDown()
        {
            Emulator.AssertDefaultAliveAndInit();
            bPreTap = false;
            StopCountDown();
            target = GetInputTime();
            timer1.Start();
        }

        void StopCountDown()
        {
            timer1.Stop();
            lblMsg.Text = "";
        }

        void RefreshMsg(DateTime target)
        {
            var now = GetNowTime();
            var span = target - now;
            if (span.TotalMilliseconds > 0)
                lblMsg.Text = $"现在: {now.ToHMSString()} 距离：{target.ToHMSString()} 剩余：{span.ToHMSString()}";
            else
                lblMsg.Text = "";
        }

        void Tap(PVec2f pf)
        {
            Emulator.AssertDefaultAliveAndInit();
            var before = GetNowTime();
            Emulator.Default.DoTap(pf);
            var span = GetNowTime() - before;
            Logger.GetInstance().Info("PCRArenaTimer", $"TapBackButton takes {span.TotalMilliseconds} ms");
        }

        void TapBackButton()
        {
            Tap(new PVec2f(0.3656f, 0.6859f));
        }

        void TapCenter()
        {
            Tap(new PVec2f(0.5f, 0.5f));
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            StartCountDown();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            StopCountDown();
        }

        private void btnTestTap_Click(object sender, EventArgs e)
        {
            TapCenter();
        }

        private void btnAdd5Minutes_Click(object sender, EventArgs e)
        {
            SetInputTime(GetNowTime().AddMinutes(5));
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var now = GetNowTime();
            var span = target - now;
            if (!bPreTap && span.TotalMilliseconds <= 8000)
            {
                Task.Run(() =>
                {
                    TapCenter();
                });
                bPreTap = true;
            }
            RefreshMsg(target);
            if (span.TotalMilliseconds <= 0)
            {
                StopCountDown();
                TapBackButton();
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            Text = GetNowTime().ToHMSString(true);
        }

        private void btnAddMinute_Click(object sender, EventArgs e)
        {
            var inputTime = GetInputTime();
            SetInputTime(inputTime.AddMinutes(1));
        }

        private void btnSubMinute_Click(object sender, EventArgs e)
        {
            var inputTime = GetInputTime();
            SetInputTime(inputTime.AddMinutes(-1));
        }

        private void btnAddSecond_Click(object sender, EventArgs e)
        {
            var inputTime = GetInputTime();
            SetInputTime(inputTime.AddSeconds(1));
        }

        private void btnSubSecond_Click(object sender, EventArgs e)
        {
            var inputTime = GetInputTime();
            SetInputTime(inputTime.AddSeconds(-1));
        }

        private void btnTapBack_Click(object sender, EventArgs e)
        {
            TapBackButton();
        }
    }


    public static class DateTimeExtension
    {
        public static string ToHMSString(this DateTime datetime, bool withMilliseconds = false)
        {
            return datetime.ToString("hh:mm:ss" + (withMilliseconds ? ":fff" : ""));
        }

        public static string ToHMSString(this TimeSpan timespan)
        {
            return string.Format("{0:D2}:{1:D2}:{2:D2}:{3:D3}", timespan.Hours, timespan.Minutes, timespan.Seconds, timespan.Milliseconds);
        }
    }
}
