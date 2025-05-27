using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ScreenChecker.Lib;
using ScreenChecker.Lib.Mouse;

namespace ScreenChecker.Cmdlet
{
    [Cmdlet(VerbsLifecycle.Invoke, "MouseMove")]
    public class InvokeMouseMove : PSCmdlet
    {
        [Parameter(Mandatory = true, Position = 0)]
        public int X { get; set; }

        [Parameter(Mandatory = true, Position = 1)]
        public int Y { get; set; }

        [Parameter]
        public int ScreenNumber { get; set; } = 0;

        [Parameter]
        public SwitchParameter Fast { get; set; }

        [Parameter]
        public int Delay { get; set; } = 0;

        protected override void ProcessRecord()
        {
            if (this.Delay > 0) Thread.Sleep(this.Delay);

            MouseSender.SendMouseMove(this.X, this.Y, this.ScreenNumber, this.Fast);
        }
    }
}
