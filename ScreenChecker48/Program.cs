using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ScreenChecker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ArgsParam ap = new ArgsParam(args);

            var targetPath = ap.ImagePath;

            var screen = ScreenCheck.CaptureScreen();
            var ret = ScreenCheck.LocateOnScreen(screen, targetPath, ap.Threshold);
            if (!string.IsNullOrEmpty(ap.OutputPath))
            {
                ScreenCheck.DrawRectTarget(ap.OutputPath, ret, screen);
            }

            string json = JsonSerializer.Serialize(ret, new JsonSerializerOptions()
            {
                WriteIndented = true,
            });
            Console.WriteLine(json);

        }
    }
}
