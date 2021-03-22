
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

            SetInputTime(DateTime.Now.AddMinutes(5));
        }

        DateTime GetInputTime()
        {
            var now = DateTime.Now;
            var hour = int.Parse(txtHour.Text);
            var minute = int.Parse(txtMinute.Text);
            var second = int.Parse(txtSecond.Text);
            var target = new DateTime(now.Year, now.Month, now.Day, hour, minute, second);
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
        }

        void Tap(PVec2f pf)
        {
            var before = DateTime.Now;
            emulator.DoTap(pf);
            var span = DateTime.Now - before;
            Console.WriteLine($"TapBackButton takes {span.TotalMilliseconds} ms");
        }

        void TapBackButton()
        {
            Tap(new PVec2f(0.3656f, 0.6859f));
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
            lblMsg.Text = "";
        }

        private void btnTestTap_Click(object sender, EventArgs e)
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            var now = DateTime.Now;
            var span = target - now;
            if (!bPreTap && span.TotalMilliseconds <= 8000)
            {
                Task.Run(() =>
                {
                    Tap(new PVec2f(0.5f, 0.5f));
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
            Text = DateTime.Now.ToHMSString();
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
    }

    public static class DateTimeExtension
    {
        public static string ToHMSString(this DateTime datetime)
        {
            return datetime.ToString("hh:mm:ss");
        }

        public static string ToHMSString(this TimeSpan timespan)
        {
            return string.Format("{0:D2}:{1:D2}:{2:D2}", timespan.Hours, timespan.Minutes, timespan.Seconds);
        }
    }
}
