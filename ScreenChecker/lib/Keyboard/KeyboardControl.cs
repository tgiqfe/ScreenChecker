using System;
using System.Runtime.InteropServices;

namespace ScreenChecker.Lib
{
    internal class KeyboardControl
    {
        #region Create Input methods

        internal static NativeMethods.Input KeyboardKeyDown(ushort key, IntPtr extraInfo)
        {
            var input = new NativeMethods.Input();
            input.Type = NativeMethods.INPUT_KEYBOARD;
            input.ui.Keyboard.VirtualKey = key;
            input.ui.Keyboard.ScanCode = (ushort)NativeMethods.MapVirtualKey(key, 0);
            input.ui.Keyboard.Flags = NativeMethods.KEYEVENTF_EXTENDEDKEY | NativeMethods.KEYEVENTF_KEYDOWN;
            input.ui.Keyboard.Time = 0;
            input.ui.Keyboard.ExtraInfo = extraInfo;
            return input;
        }

        internal static NativeMethods.Input KeyboardKeyUp(ushort key, IntPtr extraInfo)
        {
            var input = new NativeMethods.Input();
            input.Type = NativeMethods.INPUT_KEYBOARD;
            input.ui.Keyboard.VirtualKey = key;
            input.ui.Keyboard.ScanCode = (ushort)NativeMethods.MapVirtualKey(key, 0);
            input.ui.Keyboard.Flags = NativeMethods.KEYEVENTF_EXTENDEDKEY | NativeMethods.KEYEVENTF_KEYUP;
            input.ui.Keyboard.Time = 0;
            input.ui.Keyboard.ExtraInfo = extraInfo;
            return input;
        }

        #endregion

        internal static ushort ConvertChar(char c)
        {
            switch (c)
            {
                case '0': return VirtualKey.Key_0;
                case '1': return VirtualKey.Key_1;
                case '2': return VirtualKey.Key_2;
                case '3': return VirtualKey.Key_3;
                case '4': return VirtualKey.Key_4;
                case '5': return VirtualKey.Key_5;
                case '6': return VirtualKey.Key_6;
                case '7': return VirtualKey.Key_7;
                case '8': return VirtualKey.Key_8;
                case '9': return VirtualKey.Key_9;
                case 'a': case 'A': return VirtualKey.Key_A;
                case 'b': case 'B': return VirtualKey.Key_B;
                case 'c': case 'C': return VirtualKey.Key_C;
                case 'd': case 'D': return VirtualKey.Key_D;
                case 'e': case 'E': return VirtualKey.Key_E;
                case 'f': case 'F': return VirtualKey.Key_F;
                case 'g': case 'G': return VirtualKey.Key_G;
                case 'h': case 'H': return VirtualKey.Key_H;
                case 'i': case 'I': return VirtualKey.Key_I;
                case 'j': case 'J': return VirtualKey.Key_J;
                case 'k': case 'K': return VirtualKey.Key_K;
                case 'l': case 'L': return VirtualKey.Key_L;
                case 'm': case 'M': return VirtualKey.Key_M;
                case 'n': case 'N': return VirtualKey.Key_N;
                case 'o': case 'O': return VirtualKey.Key_O;
                case 'p': case 'P': return VirtualKey.Key_P;
                case 'q': case 'Q': return VirtualKey.Key_Q;
                case 'r': case 'R': return VirtualKey.Key_R;
                case 's': case 'S': return VirtualKey.Key_S;
                case 't': case 'T': return VirtualKey.Key_T;
                case 'u': case 'U': return VirtualKey.Key_U;
                case 'v': case 'V': return VirtualKey.Key_V;
                case 'w': case 'W': return VirtualKey.Key_W;
                case 'x': case 'X': return VirtualKey.Key_X;
                case 'y': case 'Y': return VirtualKey.Key_Y;
                case 'z': case 'Z': return VirtualKey.Key_Z;
                case '-': return VirtualKey.Key_OEMMinus;
                case '^': return VirtualKey.Key_OEM7; // Assuming this is the correct key for '^'
                case '\\': return VirtualKey.Key_OEM5;
                case '@': return VirtualKey.Key_OEM8; // Assuming this is the correct key for '@'
                case '[': return VirtualKey.Key_OEM4;
                case ';': return VirtualKey.Key_OEM1;
                case ':': return VirtualKey.Key_OEM3;
                case ']': return VirtualKey.Key_OEM6; // Assuming this is the correct key for ']'
                case ',': return VirtualKey.Key_OEMComma;
                case '.': return VirtualKey.Key_OEMPeriod;
                case '/': return VirtualKey.Key_OEM2;
                case '_': return VirtualKey.Key_OEMMinus; // Assuming '_' maps to the same key as '-'
                default: return VirtualKey.Key_None;
            }
        }

        internal static ushort ConvertSpecialKey(string spkeyName)
        {
            switch (spkeyName.ToLower())
            {
                case "shift": case "sft": return VirtualKey.Key_Shift;
                case "ctrl": case "ctr": case "ctl": return VirtualKey.Key_Control;
                case "alt": return VirtualKey.Key_Alt;
                case "win": case "windows": return VirtualKey.Key_LWin;
                case "tab": return VirtualKey.Key_Tab;
                case "capslock": return VirtualKey.Key_CapsLock;
                case "convert": return VirtualKey.Key_Convert;
                case "nonconvert": return VirtualKey.Key_NonConvert;
                case "kana": return VirtualKey.Key_Kana;
                case "app": case "apps": case "application": return VirtualKey.Key_Apps;
                case "esc": return VirtualKey.Key_Escape;
                case "insert": return VirtualKey.Key_Insert;
                case "delete": return VirtualKey.Key_Delete;
                case "backspace": return VirtualKey.Key_Backspace;
                case "enter": return VirtualKey.Key_Enter;
                case "space": return VirtualKey.Key_Space;
                case "home": return VirtualKey.Key_Home;
                case "end": return VirtualKey.Key_End;
                case "pageup": return VirtualKey.Key_PageUp;
                case "pagedown": return VirtualKey.Key_PageDown;
                case "left": return VirtualKey.Key_Left;
                case "right": return VirtualKey.Key_Right;
                case "up": return VirtualKey.Key_Up;
                case "down": return VirtualKey.Key_Down;
                case "pause": return VirtualKey.Key_Pause;
                case "printscreen": return VirtualKey.Key_PrintScreen;
                case "scrolllock": return VirtualKey.Key_Scroll;
                case "numlock": return VirtualKey.Key_NumLock;
                default: return VirtualKey.Key_None;
            }
        }
    }
}
