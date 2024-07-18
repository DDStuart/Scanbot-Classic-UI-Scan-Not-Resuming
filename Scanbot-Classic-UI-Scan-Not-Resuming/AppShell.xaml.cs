using System.Reflection;

namespace Scanbot_Classic_UI_Scan_Not_Resuming
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(NextPage), typeof(NextPage));
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        }

        ShellContent _previousShellContent;

        protected override void OnNavigated(ShellNavigatedEventArgs args)
        {
            base.OnNavigated(args);
            if (CurrentItem?.CurrentItem?.CurrentItem is not null &&
                _previousShellContent is not null)
            {
                var property = typeof(ShellContent)
                    .GetProperty("ContentCache", BindingFlags.Public |
                                                 BindingFlags.NonPublic | BindingFlags.Instance |
                                                 BindingFlags.FlattenHierarchy);
                property.SetValue(_previousShellContent, null);
            }

            _previousShellContent = CurrentItem?.CurrentItem?.CurrentItem;
        }
    }
}
