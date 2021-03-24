
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Core.Emulators;

namespace TimedTask
{
    public partial class Form1 : Form
    {

        Emulator emulator;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            emulator = new MuMuEmulator();
            emulator.ConnectToAdbServer();

            SetInputTime(GetNowTime().AddMinutes(5));


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

        void Tap(PVec2f pf)
        {
            var before = GetNowTime();
            emulator.DoTap(pf);
            var span = GetNowTime() - before;
            Console.WriteLine($"TapBackButton takes {span.TotalMilliseconds} ms");
        }

        void TapBackButton()
        {
            Tap(new PVec2f(0.3656f, 0.6859f));
        }

        void TapCenter()
        {
            Tap(new PVec2f(0.5f, 0.5f));
        }

        void TapStartBattle()
        {
            Tap(new PVec2f(0.8289f, 0.8359f));
        }

        void TapMenuButton()
        {
            Tap(new PVec2f(0.9398f, 0.0406f));
        }

        void DoTimedThing()
        {
            TapBackButton();
        }

        DateTime target;
        bool bPreTap;

        void StartCountDown()
        {
            bPreTap = false;
            StopCountDown();
            target = GetInputTime();
            timer1.Start();
        }

        void StopCountDown()
        {
            timer1.Stop();
            timerw.Stop();
            timerb.Stop();
            waitTarget = GetNowTime();
            lblMsg.Text = "";
        }

        private void btnTestTap_Click(object sender, EventArgs e)
        {
            TapCenter();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            StartCountDown();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            StopCountDown();
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
            if (span.TotalMilliseconds > 0)
            {
                lblMsg.Text = $"现在: {now.ToHMSString()} 距离：{target.ToHMSString()} 剩余：{span.ToHMSString()}";
            }
            else
            {
                StopCountDown();
                DoTimedThing();
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            Text = GetNowTime().ToString("hh:mm:ss:fff");

            var span = waitTarget - GetNowTime();
            if (span.TotalMilliseconds > 0)
            {
                lblMsg.Text = string.Format("等待{0:N1}秒", span.TotalMilliseconds / 1000);
            }
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

        private void btnAdd5Minutes_Click(object sender, EventArgs e)
        {
            SetInputTime(GetNowTime().AddMinutes(5));
        }

        DateTime waitTarget;

        void WaitAndBattle()
        {
            StopCountDown();
            var seconds = int.Parse(txtWait.Text);
            var battleSeconds = int.Parse(txtBattle.Text);
            timerw.Interval = seconds * 1000;
            timerb.Interval = 15 * 1000;
            waitTarget = GetNowTime().AddSeconds(seconds);
            lblMsg.Text = $"等待{seconds}秒";
            timerw.Start();
        }

        private void btnWaitAndBattle_Click(object sender, EventArgs e)
        {
            WaitAndBattle();
        }

        private void timerw_Tick(object sender, EventArgs e)
        {
            timerw.Enabled = false;
            SetInputTime(GetNowTime().AddSeconds(int.Parse(txtBattle.Text)));
            StartCountDown();
            TapStartBattle();
            timerb.Start();
        }

        private void timerb_Tick(object sender, EventArgs e)
        {
            timerb.Enabled = false;
            TapMenuButton();
        }
    }

    public static class DateTimeExtension
    {
        public static string ToHMSString(this DateTime datetime)
        {
            return datetime.ToString("hh:mm:ss");
        }

        public static string ToHMSString(this TimeSpan timespan)
        {
            return string.Format("{0:D2}:{1:D2}:{2:D2}:{3:D3}", timespan.Hours, timespan.Minutes, timespan.Seconds, timespan.Milliseconds);
        }
    }
}
