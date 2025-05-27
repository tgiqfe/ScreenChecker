using System;
using System.Text;
using System.Linq;
using System.Diagnostics;
using OpenCvSharp.Internal;

namespace ScreenChecker.Lib.Folder
{
    internal class WindowInfo
    {
        #region Parameters

        public IntPtr Handle { get; set; }
        public int ProcessID { get; set; }
        public string Title { get; set; }
        public string ClassName { get; set; }
        public string ProcessName { get; set; }
        public bool Foreground { get; set; }
        public bool Minimized { get; set; }
        public string FolderPath { get; set; }

        public int Left { get; set; }
        public int Top { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public bool Enabled { get; set; }

        #endregion
        #region Private parameter

        private static readonly string[] excludeClassName = new string[]
        {
            "Progman",              // デスクトップ
            "WorkerW",              // デスクトップの背景
            "Shell_TrayWnd",        // タスクバー
            "Button",               // ボタン
            "Static",               // 静的テキスト
            "ComboBox",             // コンボボックス
            "Edit",                 // エディットボックス
            "SysListView32",        // システムリストビュー
            "ReBarWindow32",        // リボンバー
            "ToolbarWindow32",      // ツールバー
            "SHELLDLL_DefView",     // シェルビュー
            "Shell_SecondaryTrayWnd", // セカンダリトレイ
            "ApplicationFrameWindow", // アプリケーションフレーム
            "Windows.UI.Core.CoreWindow", // UWPアプリのウィンドウ
            "Windows.UI.Xaml.Window", // UWPアプリのウィンドウ
            "Windows.UI.Xaml.Controls.Primitives.ButtonBase", // UWPアプリのボタン
            "Windows.UI.Xaml.Controls.TextBox", // UWPアプリのテキストボックス
            "Windows.UI.Xaml.Controls.ListView", // UWPアプリのリストビュー
            "Windows.UI.Xaml.Controls.ComboBox", // UWPアプリのコンボボックス
            "Windows.UI.Xaml.Controls.Primitives.ToggleButton", // UWPアプリのトグルボタン
            "Windows.UI.Xaml.Controls.Primitives.TextBoxBase", // UWPアプリのテキストボックスベース
        };

        private static readonly string[] excludeProcessName = new string[]
        {
            "SearchUI",             //  Windows検索UI
            "SearchProtocolHost",   //  検索プロトコルホスト
            "RuntimeBroker",        //  ランタイムブローカー
            "ShellExperienceHost",  //  シェルエクスペリエンスホスト
            "ApplicationFrameHost", //  アプリケーションフレームホスト
        };

        const string PROCESSNAME_EXPLORER = "explorer";

        #endregion

        public WindowInfo(IntPtr hWnd, IntPtr foregroundHandle, bool isExplorer = false)
        {
            //  Handle
            this.Handle = hWnd;

            //  Title
            int textLen = NativeMethods.GetWindowTextLength(hWnd);
            if (textLen == 0 || !NativeMethods.IsWindowVisible(hWnd))
            {
                return;
            }
            StringBuilder sb_title = new StringBuilder(textLen + 1);
            NativeMethods.GetWindowText(hWnd, sb_title, sb_title.Capacity);
            this.Title = sb_title.ToString();

            // ClassName
            StringBuilder sb_class = new StringBuilder(256);
            NativeMethods.GetClassName(hWnd, sb_class, sb_class.Capacity);
            this.ClassName = sb_class.ToString();
            if (excludeClassName.Any(x => x.Equals(this.ClassName, StringComparison.OrdinalIgnoreCase)))
            {
                return;
            }

            //  ProcessName
            int processId;
            NativeMethods.GetWindowThreadProcessId(hWnd, out processId);
            this.ProcessName = Process.GetProcessById(processId).ProcessName;
            this.ProcessID = processId;
            if (excludeProcessName.Any(x => x.Equals(this.ProcessName, StringComparison.OrdinalIgnoreCase)))
            {
                return;
            }
            if (!isExplorer && ProcessName == PROCESSNAME_EXPLORER)
            {
                return;
            }

            //  Foreground and Minimized
            this.Foreground = hWnd == foregroundHandle;
            this.Minimized = NativeMethods.IsIconic(hWnd);

            //  WindowSize
            var rect = new NativeMethods.RECT
            {
                Left = 0,
                Top = 0,
                Right = 1,
                Bottom = 1,
            };
            if (!this.Minimized)
            {
                NativeMethods.GetWindowRect(hWnd, ref rect);
                this.Left = rect.Left;
                this.Top = rect.Top;
                this.Width = rect.Right - rect.Left;
                this.Height = rect.Bottom - rect.Top;
            }

            this.Enabled = true;
        }

        public void Show()
        {
            Console.WriteLine($"Handle      : {this.Handle}");
            Console.WriteLine($"ProcessID   : {this.ProcessID}");
            Console.WriteLine($"Title       : {this.Title}");
            Console.WriteLine($"ClassName   : {this.ClassName}");
            Console.WriteLine($"ProcessName : {this.ProcessName}");
            Console.WriteLine($"Foreground  : {this.Foreground}");
            Console.WriteLine($"Minimized   : {this.Minimized}");
            Console.WriteLine($"FolderPath  : {this.FolderPath}");
            Console.WriteLine($"Location    : [{this.Left}, {this.Top}]");
            Console.WriteLine($"Size        : [{this.Width} x {this.Height}]");
        }
    }
}
