namespace Scanbot_Classic_UI_Scan_Not_Resuming
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(NextPage), typeof(NextPage));
        }
    }
}
