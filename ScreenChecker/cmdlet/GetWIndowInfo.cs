using ScreenChecker.Lib.Folder;
using System.Management.Automation;

namespace ScreenChecker.Cmdlet
{
    [Cmdlet(VerbsCommon.Get, "WIndowInfo")]
    public class GetWIndowInfo : PSCmdlet
    {
        protected override void ProcessRecord()
        {
            var collection = new WindowInfoCollection();
            WriteObject(collection.List);
        }
    }
}
