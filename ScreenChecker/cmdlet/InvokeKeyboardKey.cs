using System.Management.Automation;
using ScreenChecker.Lib.Keyboard;

namespace ScreenChecker.Cmdlet
{
    [Cmdlet(VerbsLifecycle.Invoke, "KeyboardKey")]
    public class InvokeKeyboardKey : PSCmdlet
    {
        [Parameter(Mandatory = true, Position = 0)]
        public string Text { get; set; }

        protected override void ProcessRecord()
        {
            var sender = new KeySender(this.Text);
            sender.SendKeyboard();
        }
    }
}
