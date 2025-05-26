using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ScreenChecker.PackageManifest
{
    internal class ProjectInfo
    {
        public string BuildType { get; set; }

        public string ProjectName
        {
            get { return Assembly.GetExecutingAssembly().GetName().Name; }
        }
        public string ProjectDirectory
        {
            get { return @"..\..\.."; }
        }

        public string TargetDirectory
        {
            get { return Path.Combine(this.ProjectDirectory, ProjectName, "bin", BuildType; }
        }
        public string ModuleDirectory
        {
            get { return Path.Combine(this.ProjectDirectory, ProjectName, "bin", ProjectName; }
        }











        public ProjectInfo(string buildType = null)
        {
            if (buildType == null)
            {
                this.BuildType = "Release";
#if DEBUG
                this.BuildType = "Debug";
#endif
            }
            else
            {
                this.BuildType = buildType;
            }
        }
    }
}
