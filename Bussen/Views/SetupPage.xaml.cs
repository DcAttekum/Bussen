using Bussen.ViewModels;

namespace Bussen.Views;

public partial class SetupPage : ContentPage
{
	public SetupPage()
	{
		InitializeComponent();
		BindingContext = App.Services.GetService<SetupViewModel>();
	}
}