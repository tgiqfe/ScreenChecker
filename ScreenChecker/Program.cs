using ScreenChecker;
using System.Text.Json;

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
