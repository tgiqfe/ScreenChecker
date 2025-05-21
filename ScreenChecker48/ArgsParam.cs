using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenChecker
{
    internal class ArgsParam
    {
        public string ImagePath { get; set; }
        public string OutputPath { get; set; }
        public double Threshold { get; set; } = 0.95;
        public bool Click { get; set; }
        public bool ShowResult { get; set; }

        public ArgsParam(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i].ToLower())
                {
                    case "/i":
                    case "-i":
                    case "--image":
                        if (i + 1 < args.Length)
                        {
                            this.ImagePath = args[i + 1];
                            i++;
                        }
                        break;
                    case "/o":
                    case "-o":
                    case "--output":
                        if (i + 1 < args.Length)
                        {
                            this.OutputPath = args[i + 1];
                            i++;
                        }
                        break;
                    case "/t":
                    case "-t":
                    case "--threshold":
                        if (i + 1 < args.Length)
                        {
                            if (double.TryParse(args[i + 1], out double threshold))
                            {
                                this.Threshold = threshold;
                            }
                            i++;
                        }
                        break;
                    case "/c":
                    case "-c":
                    case "--click":
                        this.Click = true;
                        break;
                    case "/s":
                    case "-s":
                    case "--show":
                        this.ShowResult = true;
                        break;
                }
            }
        }
    }
}
