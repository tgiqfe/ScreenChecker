using System;

namespace ScreenChecker.Lib.Keyboard
{
    internal class KeySendItem
    {
        public string Name { get; private set; }
        public ushort Key { get; private set; }
        public KeyAction Action { get; private set; }
        public NativeMethods.Input Input { get; private set; }
        public int Delay { get; private set; }

        internal enum KeyAction
        {
            None,
            KeyDown,
            KeyUp,
            Delay,
        }

        public KeySendItem(int delay)
        {
            this.Name = "Delay";
            this.Action = KeyAction.Delay;
            this.Delay = delay;
        }

        public KeySendItem(string spKeyName, bool toDown)
        {
            this.Name = spKeyName;
            this.Key = KeyboardControl.ConvertSpecialKey(spKeyName);
            if (this.Key != VirtualKey.Key_None)
            {
                IntPtr extraInfo = NativeMethods.GetMessageExtraInfo();
                this.Action = toDown ? KeyAction.KeyDown : KeyAction.KeyUp;
                this.Input = toDown ?
                    KeyboardControl.KeyboardKeyDown(this.Key, extraInfo) :
                    KeyboardControl.KeyboardKeyUp(this.Key, extraInfo);
            }
        }

        public KeySendItem(char c, bool toDown)
        {
            this.Name = c.ToString();
            this.Key = KeyboardControl.ConvertChar(c);
            if (this.Key != VirtualKey.Key_None)
            {
                IntPtr extraInfo = NativeMethods.GetMessageExtraInfo();
                this.Action = toDown ? KeyAction.KeyDown : KeyAction.KeyUp;
                this.Input = toDown ?
                    KeyboardControl.KeyboardKeyDown(this.Key, extraInfo) :
                    KeyboardControl.KeyboardKeyUp(this.Key, extraInfo);
            }
        }

        public override string ToString()
        {
            string actionText = this.Action == KeyAction.None ?
                "-" :
                this.Action.ToString();
            return $"Name: {Name}, Key: 0x{Key:x4}, Action: {actionText}, Delay: {Delay}";
        }
    }
}
