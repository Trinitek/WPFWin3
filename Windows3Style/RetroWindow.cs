using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Windows3Style
{
    public class RetroWindow : Window
    {
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
    }
}
