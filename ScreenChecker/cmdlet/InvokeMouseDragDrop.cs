using System.Management.Automation;
using System.Threading;
using ScreenChecker.Lib;
using ScreenChecker.Lib.Mouse;

namespace ScreenChecker.Cmdlet
{
    [Cmdlet(VerbsLifecycle.Invoke, "MouseDragDrop")]
    public class InvokeMouseDragDrop : PSCmdlet
    {
        [Parameter(Position = 0), Alias("X1")]
        public int? StartX { get; set; }

        [Parameter(Position = 1), Alias("Y1")]
        public int? StartY { get; set; }

        [Parameter(Position = 2), Alias("X2")]
        public int? EndX { get; set; }

        [Parameter(Position = 3), Alias("Y2")]
        public int? EndY { get; set; }

        [Parameter]
        public int ScreenNumber { get; set; }

        [Parameter]
        public SwitchParameter Fast { get; set; }

        [Parameter]
        public int Delay { get; set; } = 0;

        protected override void ProcessRecord()
        {
            if (this.Delay > 0) Thread.Sleep(this.Delay);

            var sender = new MouseSender();
            if (this.StartX == null || this.StartY == null)
            {
                sender.MouseLeftDrag2(
                    this.EndX ?? 0,
                    this.EndY ?? 0,
                    this.ScreenNumber,
                    this.Fast);
            }
            else
            {
                sender.MouseLeftDrag2(
                    this.StartX.Value,
                    this.StartY.Value,
                    this.EndX.Value,
                    this.EndY.Value,
                    this.ScreenNumber,
                    this.Fast);
            }
        }
    }
}
