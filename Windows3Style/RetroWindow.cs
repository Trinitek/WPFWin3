using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;

namespace Windows3Style
{
    public class RetroWindow : Window
    {
        private const int GWL_STYLE = -16;
        private const int WS_MAXIMIZEBOX = 0x10000;
        private const int WS_MINIMIZEBOX = 0x20000;
        private const int WM_NCLBUTTONDBLCLK = 0x00A3;  // double-click on non-client area

        [DllImport("user32.dll")]
        extern private static int GetWindowLong(IntPtr hwnd, int index);

        [DllImport("user32.dll")]
        extern private static int SetWindowLong(IntPtr hwnd, int index, int value);

        public RetroWindow()
        {
            CommandBindings.AddRange(new CommandBinding[] {
                new CommandBinding(
                    command: SystemCommands.ShowSystemMenuCommand,
                    executed: (s, e) =>
                {
                    var source = e.OriginalSource as Control;
                    var sourcePoint = source.PointToScreen(new Point(0, 0));
                    var p = new Point(sourcePoint.X, sourcePoint.Y + source.ActualHeight);
                    SystemCommands.ShowSystemMenu(e.Parameter as Window, p);
                }),
                new CommandBinding(
                    command: SystemCommands.CloseWindowCommand,
                    executed: (s, e) =>
                {
                    SystemCommands.CloseWindow(e.Parameter as Window);
                }),
                new CommandBinding(
                    command: SystemCommands.MaximizeWindowCommand,
                    executed: (s, e) =>
                {
                    SystemCommands.MaximizeWindow(e.Parameter as Window);
                }),
                new CommandBinding(
                    command: SystemCommands.RestoreWindowCommand,
                    executed: (s, e) =>
                {
                    SystemCommands.RestoreWindow(e.Parameter as Window);
                }),
                new CommandBinding(
                    command: SystemCommands.MinimizeWindowCommand,
                    executed: (s, e) =>
                {
                    SystemCommands.MinimizeWindow(e.Parameter as Window);
                })
            });
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            var currentStyle = GetWindowLong(hwnd, GWL_STYLE);
        
            // Disable maximize and minimize buttons to prevent click events from passing through
            SetWindowLong(hwnd, GWL_STYLE, currentStyle & ~WS_MAXIMIZEBOX & ~WS_MINIMIZEBOX);

            var hwndSource = HwndSource.FromHwnd(hwnd);
            hwndSource.AddHook(new HwndSourceHook(WndProc));
        
            base.OnSourceInitialized(e);
        }

        private static IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == WM_NCLBUTTONDBLCLK)
            {
                if (HwndSource.FromHwnd(hwnd).RootVisual is RetroWindow w)
                {
                    w.WindowState = WindowState.Maximized;
                    handled = true;
                }
            }

            return IntPtr.Zero;
        }
    }
}
