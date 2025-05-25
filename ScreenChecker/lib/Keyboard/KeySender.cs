using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;

namespace ScreenChecker.Lib.Keyboard
{
    internal class KeySender
    {
        const int SPACE_DELAY = 300;

        public List<KeySendItem> InputFlow { get; private set; }

        private List<KeySendItem> _endInputReserve = null;

        private static readonly string[] _reserveSpKeyName = new string[]
        {
            "Shift", "Sft",
            "Ctrl", "Ctr", "Ctl",
            "Alt",
            "Win", "Windows",
        };

        public KeySender(string text)
        {
            this.InputFlow = new List<KeySendItem>();
            this._endInputReserve = new List<KeySendItem>();

            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == '(')
                {
                    var endIndex1 = text.IndexOf(' ', i + 1);
                    var endIndex2 = text.IndexOf(')', i + 1);
                    var endIndex3 = text.IndexOf('(', i + 1);
                    if (endIndex1 < 0) endIndex1 = int.MaxValue;
                    if (endIndex2 < 0) endIndex2 = int.MaxValue;
                    if (endIndex3 < 0) endIndex3 = int.MaxValue;
                    var endIndex = new int[]
                    {
                        endIndex1,
                        endIndex2,
                        endIndex3
                    }.Min();

                    if (endIndex >= 0)
                    {
                        var spKeyName = text.Substring(i + 1, endIndex - i - 1);
                        this.InputFlow.Add(new KeySendItem(spKeyName, toDown: true));
                        if (_reserveSpKeyName.Any(x => x.Equals(spKeyName, StringComparison.OrdinalIgnoreCase)))
                        {
                            this._endInputReserve.Add(new KeySendItem(spKeyName, toDown: false));
                        }
                        else
                        {
                            this.InputFlow.Add(new KeySendItem(spKeyName, toDown: false));
                            this._endInputReserve.Add(null);
                        }
                        i = endIndex - 1;
                    }
                }
                else if (text[i] == ')')
                {
                    if (_endInputReserve.Count > 0)
                    {
                        var endInput = _endInputReserve.Last();
                        if (endInput != null)
                        {
                            this.InputFlow.Add(endInput);
                        }
                        _endInputReserve.RemoveAt(_endInputReserve.Count - 1);
                    }
                }
                else if (text[i] == ' ')
                {
                    if (_endInputReserve.Count == 0)
                    {
                        this.InputFlow.Add(new KeySendItem(SPACE_DELAY));
                    }
                }
                else
                {
                    this.InputFlow.Add(new KeySendItem(text[i], toDown: true));
                    this.InputFlow.Add(new KeySendItem(text[i], toDown: false));
                }
            }
        }

        public void SendKeyboard()
        {
            var inputList = new List<NativeMethods.Input>();
            foreach (var item in this.InputFlow.Where(x => x.Action != KeySendItem.KeyAction.None))
            {
                if (item.Action == KeySendItem.KeyAction.Delay)
                {
                    Thread.Sleep(item.Delay);
                    if (inputList.Count > 0)
                    {
                        var inputs1 = inputList.ToArray();
                        NativeMethods.SendInput((uint)inputs1.Length, ref inputs1[0], Marshal.SizeOf(inputs1[0]));
                        inputList.Clear();
                    }
                }
                else
                {
                    inputList.Add(item.Input);
                }
            }
            var inputs2 = inputList.ToArray();
            NativeMethods.SendInput((uint)inputs2.Length, ref inputs2[0], Marshal.SizeOf(inputs2[0]));
        }
    }
}
