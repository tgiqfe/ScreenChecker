using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenChecker
{
    public class Image
    {
        public static ScreenCheck.ImageCheckResult LocateOnScreen(string imagePath, double threshold, string outputPath = null)
        {
            var screen = ScreenCheck.CaptureScreen();

            if (!string.IsNullOrEmpty(imagePath))
            {
                var result = ScreenCheck.LocateOnScreen(screen, imagePath, threshold);

                //  マッチした場所を囲って出力
                if (!string.IsNullOrEmpty(outputPath))
                {
                    ScreenCheck.DrawRectTarget(outputPath, result, screen);
                }

                //  結果を出力
                return result;
            }

            return null;
        }

        public static ScreenCheck.ImageCheckResult ClickToLocate(string imagePath, double threshold, string outputPath = null)
        {
            var screen = ScreenCheck.CaptureScreen();

            if (!string.IsNullOrEmpty(imagePath))
            {
                var result = ScreenCheck.LocateOnScreen(screen, imagePath, threshold);

                //  マッチした場所を囲って出力
                if (!string.IsNullOrEmpty(outputPath))
                {
                    ScreenCheck.DrawRectTarget(outputPath, result, screen);
                }

                //  マッチした場所をクリック
                if(result.IsFound)
                {
                    var point = new Point(
                        result.Location.X + result.Size.Width / 2,
                        result.Location.Y + result.Size.Height / 2);
                    MouseControl.Click(point);
                }

                //  結果を出力
                return result;
            }

            return null;
        }
    }
}
