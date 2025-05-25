
namespace ScreenChecker.Lib
{
    public class ImageCheckResult
    {
        public bool IsFound { get; set; }

        public string ImagePath { get; set; }

        public double Confidence { get; set; }

        public double MatchValue { get; set; }

        public OpenCvSharp.Point Location { get; set; }

        public OpenCvSharp.Size Size { get; set; }
    }
}
