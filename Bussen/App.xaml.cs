using Bussen.Services;

namespace Bussen
{
    public partial class App : Application
    {
        public static IServiceProvider Services { get; private set; } = default!;
        public static IAlertService AlertService { get; private set; } = default!;

        public App(IServiceProvider serviceProvider)
        {
            Services = serviceProvider;
            AlertService = Services.GetService<IAlertService>() ?? new AlertService();

            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}