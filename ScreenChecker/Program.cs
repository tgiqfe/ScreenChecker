using ScreenChecker;
using System.Text.Json;
using static ScreenChecker.ScreenCheck;

ArgsParam ap = new ArgsParam(args);

var targetPath = ap.ImagePath;

//  画面スクリーンショット
var screen = ScreenCheck.CaptureScreen();

//  テンプレートマッチ
if(!string.IsNullOrEmpty(ap.ImagePath))
{
    var result = ScreenCheck.LocateOnScreen(screen, targetPath, ap.Threshold);

    //  マッチした場所を囲って出力
    if (!string.IsNullOrEmpty(ap.OutputPath))
    {
        ScreenCheck.DrawRectTarget(ap.OutputPath, result, screen);
    }

    //  マッチした場所をクリック
    if (ap.Click)
    {
        var point = new Point(
            result.Location.X + result.Size.Width / 2,
            result.Location.Y + result.Size.Height / 2);
        MouseControl.Click(point);
    }

    //  結果を出力
    if(ap.ShowResult)
    {
        string json = JsonSerializer.Serialize(result, new JsonSerializerOptions()
        {
            WriteIndented = true,
        });
        Console.WriteLine(json);
    }
}


