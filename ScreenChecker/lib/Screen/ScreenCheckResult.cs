
namespace ScreenChecker.Lib
{
    public class ScreenCheckResult
    {
        public bool IsFound { get; set; }

        public string ImagePath { get; set; }

        public double Confidence { get; set; }

        public double MatchValue { get; set; }

        public OpenCvSharp.Point Location { get; set; }

        public OpenCvSharp.Size Size { get; set; }

        public int Left { get; set; }

        public int Top { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }
    }
}
