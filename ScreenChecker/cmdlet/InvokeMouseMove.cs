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
        [Parameter(ValueFromPipeline = true)]
        public ScreenCheckResult[] InputScreenCheckResult { get; set; }

        [Parameter(Position = 0)]
        public int? X { get; set; }

        [Parameter(Position = 1)]
        public int? Y { get; set; }

        [Parameter]
        public int ScreenNumber { get; set; } = 0;

        [Parameter]
        public SwitchParameter Fast { get; set; }

        [Parameter]
        public int Delay { get; set; } = 0;

        protected override void ProcessRecord()
        {
            if (this.Delay > 0) Thread.Sleep(this.Delay);

            var sender = new MouseSender();

            if (InputScreenCheckResult?.Length > 0)
            {
                var result = InputScreenCheckResult.Where(x => x.IsFound).OrderBy(x => x.MatchValue).FirstOrDefault();
                if (result == null)
                {
                    return;
                }
                this.X = result.Location.X + (result.Size.Width / 2);
                this.Y = result.Location.Y + (result.Size.Height / 2);
            }
            if (this.X != null && this.Y != null)
            {
                sender.MouseMove(this.X.Value, this.Y.Value, this.ScreenNumber, this.Fast);
            }  
        }
    }
}
