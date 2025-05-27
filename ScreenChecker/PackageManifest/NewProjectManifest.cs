using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace ScreenChecker.PackageManifest
{
    [Cmdlet(VerbsCommon.New, "ProjectManifest")]
    public class NewProjectManifest : PSCmdlet
    {
        [Parameter]
        [ValidateSet("Debug", "Release", "Both")]
        public string BuildType { get; set; }

        private string _currentDirectory = null;

        protected override void BeginProcessing()
        {
            //  Set current directory. (tempolary)
            _currentDirectory = System.Environment.CurrentDirectory;
            Environment.CurrentDirectory = this.SessionState.Path.CurrentFileSystemLocation.Path;
        }

        protected override void ProcessRecord()
        {

        }

        protected override void EndProcessing()
        {
            //  Restore current directory.
            Environment.CurrentDirectory = _currentDirectory;
        }
    }
}
