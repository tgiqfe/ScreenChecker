using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ScreenChecker.Lib.NativeMethods;
using System.Drawing.Imaging;
using OpenCvSharp.Extensions;

namespace ScreenChecker.Lib
{
    internal class ScreenCheck
    {
        public static Mat CaptureScreen(int screenNum = 0)
        {
            var screen = screenNum == 0 ?
                Screen.PrimaryScreen :
                Screen.AllScreens[screenNum];

            IntPtr desktopDC = GetDC(IntPtr.Zero);
            using (Bitmap screenCapture = new Bitmap(screen.Bounds.Width, screen.Bounds.Height, PixelFormat.Format32bppRgb))
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

        public static ScreenCheckResult LocateOnScreen(Mat screen, string imagePath, double threshold)
        {
            ScreenCheckResult ret = new ScreenCheckResult();

            using (Mat template = new Mat(imagePath, ImreadModes.Unchanged))
            using (Mat result = new Mat())
            {
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
                ret.Confidence = threshold;
                ret.MatchValue = maxVal;
                if (ret.IsFound)
                {
                    ret.Location = maxLoc;
                    ret.Size = new OpenCvSharp.Size(template.Width, template.Height);

                    ret.Left = maxLoc.X;
                    ret.Top = maxLoc.Y;
                    ret.Width = template.Width;
                    ret.Height = template.Height;
                }
                return ret;
            }
        }

        public static Mat DrawRectTarget(ScreenCheckResult icresult, Mat screen)
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

            return screen;
        }

        public static void ImageWrite(string path, Mat screen)
        {
            screen.ImWrite(path);
        }
    }
}
