using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using OpenCvSharp.Extensions;
using System.Drawing;
using System.Windows.Forms;

namespace ScreenChecker
{
    internal class ScreenCheck
    {
        #region unmanaged

        internal const int SRCCOPY = 13369376;
        internal const int CAPTUREBLT = 1073741824;

        [DllImport("user32.dll")]
        internal static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("gdi32.dll")]
        internal static extern int BitBlt(IntPtr hDeskDC,
            int x,
            int y,
            int nWidth,
            int nHeight,
            IntPtr hSrcDC,
            int xSrc,
            int ySrc,
            int dwRop);

        [DllImport("user32.dll")]
        internal static extern IntPtr ReleaseDC(IntPtr hwnd, IntPtr hDC);

        [StructLayout(LayoutKind.Sequential)]
        internal struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [DllImport("user32.dll")]
        internal static extern IntPtr GetWindowDC(IntPtr hwnd);

        [DllImport("user32.dll")]
        internal static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        internal static extern int GetWindowRect(IntPtr hwnd, ref RECT lpRect);

        #endregion
        #region Result object

        internal class ImageCheckResult
        {
            public bool IsFound { get; set; }
            public string ImagePath { get; set; }
            public double Threshold { get; set; }
            public double MatchValue { get; set; }
            [JsonIgnore]
            public OpenCvSharp.Point Location { get; set; }
            [JsonIgnore]
            public OpenCvSharp.Size Size { get; set; }
        }

        #endregion

        public static Mat CaptureScreen()
        {
            IntPtr desktopDC = GetDC(IntPtr.Zero);
            using (Bitmap screenCapture = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppRgb))
            using (Graphics g = Graphics.FromImage(screenCapture))
            {
                IntPtr srcDC = g.GetHdc();
                BitBlt(srcDC, 0, 0, screenCapture.Width, screenCapture.Height, desktopDC, 0, 0, SRCCOPY);
                g.ReleaseHdc(srcDC);
                ReleaseDC(IntPtr.Zero, srcDC);
                ReleaseDC(IntPtr.Zero, desktopDC);

                return screenCapture.ToMat();
            }
        }

        public static ImageCheckResult LocateOnScreen(Mat screen, string imagePath, double threshold)
        {
            ImageCheckResult ret = new ImageCheckResult();

            using (Mat template = new Mat(imagePath, ImreadModes.Unchanged))
            using (Mat result = new Mat())
            {
                Console.WriteLine(imagePath + ": " + template.Type());

                if (template.Type() == MatType.CV_8UC4)
                {
                    Cv2.CvtColor(template, template, ColorConversionCodes.BGRA2BGR);
                }
                if (template.Type() == MatType.CV_8UC1)
                {
                    Cv2.CvtColor(template, template, ColorConversionCodes.GRAY2RGB);
                }
                if (template.Type() != MatType.CV_8UC3)
                {
                    template.ConvertTo(template, MatType.CV_8UC3);
                }

                Cv2.MatchTemplate(screen, template, result, TemplateMatchModes.CCorrNormed);
                OpenCvSharp.Point minLoc, maxLoc;
                double minVal, maxVal;
                Cv2.MinMaxLoc(result, out minVal, out maxVal, out minLoc, out maxLoc);

                ret.IsFound = maxVal >= threshold;
                ret.ImagePath = imagePath;
                ret.Threshold = threshold;
                ret.MatchValue = maxVal;
                ret.Location = maxLoc;
                ret.Size = new OpenCvSharp.Size(template.Width, template.Height);
                return ret;
            }
        }

        public static void DrawRectTarget(string path, ImageCheckResult icresult, Mat screen)
        {
            if (icresult.IsFound)
            {
                screen.Rectangle(new Rect(icresult.Location, icresult.Size), Scalar.Lime, 2);
                var point = icresult.Location;
                if (point.Y < 25)
                {
                    point.Y += 25;
                }
                screen.PutText("Hit", point, HersheyFonts.HersheyDuplex, 1, Scalar.Lime);
            }

            screen.ImWrite(path);
        }
    }
}
