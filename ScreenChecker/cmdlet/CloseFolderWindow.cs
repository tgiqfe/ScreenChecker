using ScreenChecker.Lib.Folder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace ScreenChecker.Cmdlet
{
    [Cmdlet(VerbsCommon.Close, "FolderWindow")]
    public class CloseFolderWindow : PSCmdlet
    {
        [Parameter(Mandatory = true, Position = 0)]
        public string FolderPath { get; set; }

        private string _currentDirectory = null;

        protected override void BeginProcessing()
        {
            //  Set current directory. (tempolary)
            _currentDirectory = System.Environment.CurrentDirectory;
            Environment.CurrentDirectory = this.SessionState.Path.CurrentFileSystemLocation.Path;
        }

        protected override void ProcessRecord()
        {
            FolderControl.Close(FolderPath);
        }

        protected override void EndProcessing()
        {
            //  Restore current directory.
            Environment.CurrentDirectory = _currentDirectory;
        }
    }
}
