using Bussen.ViewModels;

namespace Bussen.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = App.Services.GetService<MainViewModel>();
        }
    }
}
