using OpenCvSharp;
using ScreenChecker.Lib;
using System;
using System.IO;
using System.Linq;
using System.Management.Automation;

namespace ScreenChecker
{
    [Cmdlet(VerbsCommon.Find, "LocateOnScreen")]
    public class FindLocateOnScreen : PSCmdlet
    {
        const double DEFAULT_CONFIDENCE = 0.95;
        
        #region PowerShell parameter

        [Parameter(Mandatory = true, Position = 0)]
        public string[] ImagePath { get; set; }

        [Parameter]
        public double Confidence { get; set; } = DEFAULT_CONFIDENCE;

        [Parameter(Position = 1)]
        public string OutputPath { get; set; }

        [Parameter]
        public int ScreenNumber { get; set; }

        #endregion

        private string _currentDirectory = null;

        protected override void BeginProcessing()
        {
            //  Set current directory. (tempolary)
            _currentDirectory = System.Environment.CurrentDirectory;
            Environment.CurrentDirectory = this.SessionState.Path.CurrentFileSystemLocation.Path;
        }

        protected override void ProcessRecord()
        {
            var capture = ScreenCheck.CaptureScreen(ScreenNumber);
            var results = this.ImagePath.Select(x => ScreenCheck.LocateOnScreen(capture, x, Confidence));

            if (!string.IsNullOrEmpty(this.OutputPath))
            {
                string parent = Path.GetDirectoryName(this.OutputPath);
                if (!Directory.Exists(parent))
                {
                    Directory.CreateDirectory(parent);
                }
                Mat tempOutputCapture = capture;
                foreach (var result in results)
                {
                    if (result.IsFound)
                    {
                        tempOutputCapture = ScreenCheck.DrawRectTarget(result, capture);
                    }
                }
                ScreenCheck.ImageWrite(this.OutputPath, tempOutputCapture);
            }

            WriteObject(results);
        }

        protected override void EndProcessing()
        {
            //  Restore current directory.
            Environment.CurrentDirectory = _currentDirectory;
        }
    }
}
