using Bussen.Views;

namespace Bussen
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(SetupPage), typeof(SetupPage));
        }
    }
}
