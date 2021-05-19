using System;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;

using Core.Common;
using Core.Extensions;

namespace Core.Emulators
{
    public abstract class Emulator
    {

        public static List<Type> GetEmulatorTypes()
        {
            return GetEmulatorTypes(Assembly.GetAssembly(typeof(Emulator)));
        }

        public static List<Type> GetEmulatorTypes(Assembly assembly)
        {
            return Utils.GetChildTypes<Emulator>(assembly);
        }

        public static Emulator GetInstanceByType(Type type)
        {
            if (type == null)
                return null;
            if (!type.IsSubclassOf(typeof(Emulator)))
                throw new Exception("给定类型不属于Emulator子类");
            var obj = Activator.CreateInstance(type);
            var emulatorInst = obj as Emulator;
            return emulatorInst;
        }

        public Emulator()
        {
            resolutionGetter = new FrequencyLimitGetter<EmulatorSize>(refreshResolutionCD, GetResolutionDirectly);
            //ConnectToAdbServer();
        }

        public abstract string Name { get; }

        public virtual bool IsAlive
        {
            get
            {
                var proc = GetMainProcess();
                return proc != null;
            }
        }

        public virtual Rectangle Area
        {
            get
            {
                var area = GetArea();
                AssertAreaValid(area);
                return area;
            }
        }

        public virtual int AdbPort { get; } = 5555;

        public bool IsConnected { get; protected set; } = false;

        public abstract Process GetMainProcess();

        public abstract IntPtr GetMainWindowHandle();

        public abstract string GetAdbExePath();

        protected string AheadWithName(string str)
        {
            return $"[{Name}]{str}";
        }

        public void AssertAlive()
        {
            if (!IsAlive)
                throw new Exception(AheadWithName("模拟器不在线"));
        }

        private Rectangle GetArea()
        {
            var handle = GetMainWindowHandle();
            return GetAreaWithWindowHandle(handle);
        }

        private Rectangle GetAreaWithWindowHandle(IntPtr handle)
        {
            var rect = Win32API.GetWindowRect(handle);
            return rect.ToRectangle();
        }

        public bool IsAreaValid()
        {
            if (!IsAlive)
                return false;
            var area = GetArea();
            return IsAreaValid(area);
        }

        private bool IsAreaValid(Rectangle area)
        {
            return area.X >= 0 && area.Y >= 0 && area.Width > 10 && area.Height > 10;
        }

        private void AssertAreaValid(Rectangle area)
        {
            if (!IsAreaValid(area))
                throw new Exception(AheadWithName("模拟器区域不合法"));
        }

        protected Process GetProcessByName(string processName)
        {
            return ProcessExtension.GetProcessByName(processName);
        }

        protected Process FindProcessByName(string name)
        {
            var processes = Process.GetProcesses();
            foreach (var process in processes)
            {
                var procName = process.ProcessName;
                if (Regex.IsMatch(procName, name))
                {
                    return process;
                }
            }
            return null;
        }

        private Bitmap CaptureScreen(Rectangle rect)
        {
            return Utils.CaptureScreen(rect);
        }

        public Bitmap GetScreenCapture()
        {
            var rect = Area;
            return CaptureScreen(rect);
        }

        private void FastAdbCmd(string arguments)
        {
            ShellUtils.DoShell(new ShellArgs()
            {
                FileName = GetAdbExePath(),
                Arguments = arguments,
                IgnoreOutput = true,
            });
        }

        private string AdbCmd(string arguments)
        {
            var result = ShellUtils.DoShell(new ShellArgs()
            {
                FileName = GetAdbExePath(),
                Arguments = arguments,
                IgnoreOutput = false,
            });
            Logger.GetInstance().Info("AdbCmd", AheadWithName($"arguments = {arguments}"));
            var output = result.GetOutput();
            Logger.GetInstance().Info("AdbCmd", AheadWithName($"output = {output.LimitLength(40)}"));
            result.Dispose();
            return output;
        }

        protected virtual string GetSpecificIdentity()
        {
            return $"127.0.0.1:{AdbPort}";
        }

        private string AdbShell(string args)
        {
            var fullArguments = $"-s {GetSpecificIdentity()} shell {args}";
            var output = AdbCmd(fullArguments);
            return output;
        }

        public List<AdbDevice> GetAdbDevices()
        {
            var output = AdbCmd("devices");
            var arr = output.Split(new char[] { '\r', '\n'}, StringSplitOptions.RemoveEmptyEntries);
            var list = new List<AdbDevice>();
            for (var i = 1; i < arr.Length; i++)
            {
                var line = arr[i];
                var a = line.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
                list.Add(new AdbDevice()
                {
                    SpecificIdentity = a[0],
                    State = a[1],
                });
            }
            return list;
        }

        public AdbDevice GetFirstOnlineDevice()
        {
            var devices = GetAdbDevices();
            foreach (var device in devices)
            {
                if (device.State == "device")
                    return device;
            }
            return null;
        }

        public void ConnectToAdbServer()
        {
            AdbCmd($"connect 127.0.0.1:{AdbPort}");
            IsConnected = true;
        }

        private EmulatorSize GetResolutionDirectly()
        {
            var output = AdbShell("wm size");
            var match = Regex.Match(output, "(\\d+)x(\\d+)");
            if (!match.Success)
                throw new Exception(AheadWithName("获取模拟器分辨率失败"));
            var size = new EmulatorSize(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value));
            return size;
        }

        private int refreshResolutionCD = 60 * 1000; //ms
        private FrequencyLimitGetter<EmulatorSize> resolutionGetter;

        public EmulatorSize GetResolution()
        {
            return resolutionGetter.Get();
        }

        public void DoTap(PVec2f pf)
        {
            DoTap(GetResolution() * pf);
        }

        public void DoTap(EmulatorPoint point)
        {
            var output = AdbShell($"input tap {point.X} {point.Y}");
        }
    }

    public class EmulatorPoint
    {
        public int X { get; set; }

        public int Y { get; set; }

        public EmulatorPoint(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public class EmulatorSize
    {

        public int Width { get; set; }

        public int Height { get; set; }

        public EmulatorSize(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public static EmulatorPoint operator * (EmulatorSize size, PVec2f pf)
        {
            var x = (int)(size.Width * pf.X);
            var y = (int)(size.Height * pf.Y);
            return new EmulatorPoint(x, y);
        }

        public Tuple<int, int> Simplify()
        {
            var divisor = Utils.GetLargestCommonDivisor(Width, Height);
            return new Tuple<int, int>(Width / divisor, Height / divisor);
        }

        public override string ToString()
        {
            return $"{Width}x{Height}";
        }
    }

    public class AdbDevice
    {
        public string SpecificIdentity { get; set; }

        public string State { get; set; }
    }

    public class PVec2f
    {
        public static int DecimalPlaces { get; } = 4;

        public static PVec2f Parse(string s)
        {
            var arr = s.Split(',');
            return new PVec2f(double.Parse(arr[0]), double.Parse(arr[1]));
        }

        public double X { get; set; }

        public double Y { get; set; }

        public PVec2f(double x, double y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"{Math.Round(X, DecimalPlaces)},{Math.Round(Y, DecimalPlaces)}";
        }

        public static PVec2f Div(Size size, Point point)
        {
            var x = 1.0 * point.X / size.Width;
            var y = 1.0 * point.Y / size.Height;
            return new PVec2f(x, y);
        }

        public static PVec2f Div(Size size, Rectangle rect)
        {
            return Div(size, rect.GetCenterPoint());
        }
    }

    public class RVec4f
    {
        public static int DecimalPlaces { get; } = 4;

        public static RVec4f Parse(string s)
        {
            var arr = s.Split(',');
            return new RVec4f(double.Parse(arr[0]), double.Parse(arr[1]), double.Parse(arr[2]), double.Parse(arr[3]));
        }

        public double X { get; set; }

        public double Y { get; set; }

        public double W { get; set; }

        public double H { get; set; }

        public RVec4f(double x, double y, double w, double h)
        {
            X = x;
            Y = y;
            W = w;
            H = h;
        }

        public override string ToString()
        {
            return $"{Math.Round(X, DecimalPlaces)},{Math.Round(Y, DecimalPlaces)},{Math.Round(W, DecimalPlaces)},{Math.Round(H, DecimalPlaces)}";
        }

        public static Rectangle operator * (Size size, RVec4f rf)
        {
            var x = (int)(size.Width * rf.X);
            var y = (int)(size.Height * rf.Y);
            var w = (int)(size.Width * rf.W);
            var h = (int)(size.Height * rf.H);
            return new Rectangle(x, y, w, h);
        }

        public static RVec4f Div(Size size, Rectangle rect)
        {
            var x = 1.0 * rect.X / size.Width;
            var y = 1.0 * rect.Y / size.Height;
            var w = 1.0 * rect.Width / size.Width;
            var h = 1.0 * rect.Height / size.Height;
            return new RVec4f(x, y, w, h);
        }
    }

    public class AspectRatio
    {
        public static List<AspectRatio> SupportedAspectRatio { get; } = new List<AspectRatio>()
        {
            new AspectRatio(2,1),
            new AspectRatio(16,9),
        };

        public static AspectRatio GetAspectRatio(EmulatorSize resolution)
        {
            var simplified = resolution.Simplify();
            foreach (var ratio in SupportedAspectRatio)
            {
                if (ratio.W == simplified.Item1 && ratio.H == simplified.Item2)
                {
                    return ratio;
                }
            }
            return null;
        }

        public static void AssertResolutionIsSupported(EmulatorSize resolution)
        {
            var aspectRatio = GetAspectRatio(resolution);
            if (aspectRatio == null)
                throw new Exception($"不支持的分辨率: {resolution}");
        }

        public int W { get; private set; }

        public int H { get; private set; }

        public AspectRatio(int w, int h)
        {
            W = w;
            H = h;
        }

        public override string ToString()
        {
            return $"{W}x{H}";
        }
    }
}
