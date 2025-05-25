namespace ScreenChecker.Lib
{
    internal class VirtualKey
    {
        //  https://learn.microsoft.com/ja-jp/windows/win32/inputdev/virtual-key-codes

        public const ushort Key_None = 0x00; // No key pressed

        public const ushort Key_MouseLeftButton = 0x01;
        public const ushort Key_MouseRightButton = 0x02;
        public const ushort Key_MouseMiddleButton = 0x04;
        public const ushort Key_Backspace = 0x08;
        public const ushort Key_Tab = 0x09;
        public const ushort Key_Enter = 0x0D;
        public const ushort Key_Shift = 0x10;
        public const ushort Key_Control = 0x11;
        public const ushort Key_Alt = 0x12;
        public const ushort Key_Pause = 0x13;
        public const ushort Key_CapsLock = 0x14;
        public const ushort Key_Kana = 0x15;
        public const ushort Key_IMEOn = 0x16;
        public const ushort Key_IMEOff = 0x1A;
        public const ushort Key_Escape = 0x1B;
        public const ushort Key_Convert = 0x1C;
        public const ushort Key_NonConvert = 0x1D;
        public const ushort Key_Space = 0x20;
        public const ushort Key_PageUp = 0x21;
        public const ushort Key_PageDown = 0x22;
        public const ushort Key_End = 0x23;
        public const ushort Key_Home = 0x24;
        public const ushort Key_Left = 0x25;
        public const ushort Key_Up = 0x26;
        public const ushort Key_Right = 0x27;
        public const ushort Key_Down = 0x28;
        public const ushort Key_PrintScreen = 0x2C;
        public const ushort Key_Insert = 0x2D;
        public const ushort Key_Delete = 0x2E;

        public const ushort Key_0 = 0x30;
        public const ushort Key_1 = 0x31;
        public const ushort Key_2 = 0x32;
        public const ushort Key_3 = 0x33;
        public const ushort Key_4 = 0x34;
        public const ushort Key_5 = 0x35;
        public const ushort Key_6 = 0x36;
        public const ushort Key_7 = 0x37;
        public const ushort Key_8 = 0x38;
        public const ushort Key_9 = 0x39;

        public const ushort Key_A = 0x41;
        public const ushort Key_B = 0x42;
        public const ushort Key_C = 0x43;
        public const ushort Key_D = 0x44;
        public const ushort Key_E = 0x45;
        public const ushort Key_F = 0x46;
        public const ushort Key_G = 0x47;
        public const ushort Key_H = 0x48;
        public const ushort Key_I = 0x49;
        public const ushort Key_J = 0x4A;
        public const ushort Key_K = 0x4B;
        public const ushort Key_L = 0x4C;
        public const ushort Key_M = 0x4D;
        public const ushort Key_N = 0x4E;
        public const ushort Key_O = 0x4F;
        public const ushort Key_P = 0x50;
        public const ushort Key_Q = 0x51;
        public const ushort Key_R = 0x52;
        public const ushort Key_S = 0x53;
        public const ushort Key_T = 0x54;
        public const ushort Key_U = 0x55;
        public const ushort Key_V = 0x56;
        public const ushort Key_W = 0x57;
        public const ushort Key_X = 0x58;
        public const ushort Key_Y = 0x59;
        public const ushort Key_Z = 0x5A;

        public const ushort Key_LWin = 0x5B;
        public const ushort Key_RWin = 0x5C;
        public const ushort Key_Apps = 0x5D;

        public const ushort Key_F1 = 0x70;
        public const ushort Key_F2 = 0x71;
        public const ushort Key_F3 = 0x72;
        public const ushort Key_F4 = 0x73;
        public const ushort Key_F5 = 0x74;
        public const ushort Key_F6 = 0x75;
        public const ushort Key_F7 = 0x76;
        public const ushort Key_F8 = 0x77;
        public const ushort Key_F9 = 0x78;
        public const ushort Key_F10 = 0x79;
        public const ushort Key_F11 = 0x7A;
        public const ushort Key_F12 = 0x7B;
        public const ushort Key_F13 = 0x7C;
        public const ushort Key_F14 = 0x7D;
        public const ushort Key_F15 = 0x7E;
        public const ushort Key_F16 = 0x7F;
        public const ushort Key_F17 = 0x80;
        public const ushort Key_F18 = 0x81;
        public const ushort Key_F19 = 0x82;
        public const ushort Key_F20 = 0x83;
        public const ushort Key_F21 = 0x84;
        public const ushort Key_F22 = 0x85;
        public const ushort Key_F23 = 0x86;
        public const ushort Key_F24 = 0x87;

        public const ushort Key_NumLock = 0x90;
        public const ushort Key_Scroll = 0x91;
        public const ushort Key_LeftShift = 0xA0;
        public const ushort Key_RightShift = 0xA1;
        public const ushort Key_LeftControl = 0xA2;
        public const ushort Key_RightControl = 0xA3;
        public const ushort Key_BrowersBack = 0xA6;
        public const ushort Key_BrowersForward = 0xA7;
        public const ushort Key_BrowersRefresh = 0xA8;

        public const ushort Key_OEM1 = 0xBA; // ';:' for US
        public const ushort Key_OEMPlus = 0xBB; // '+' for US
        public const ushort Key_OEMComma = 0xBC; // ',' for US
        public const ushort Key_OEMMinus = 0xBD; // '-' for US
        public const ushort Key_OEMPeriod = 0xBE; // '.' for US
        public const ushort Key_OEM2 = 0xBF; // '/?' for US
        public const ushort Key_OEM3 = 0xC0; // '`~' for US
        public const ushort Key_OEM4 = 0xDB; // '[{' for US
        public const ushort Key_OEM5 = 0xDC; // '\|' for US
        public const ushort Key_OEM6 = 0xDD; // ']}' for US
        public const ushort Key_OEM7 = 0xDE; // ''"' for US
        public const ushort Key_OEM8 = 0xDF; // Used for miscellaneous characters; it can vary by keyboard.
        public const ushort Key_OEM102 = 0xE2; // '<>' or '\|' on RT 102-key keyboard (Non-US)
    }
}
