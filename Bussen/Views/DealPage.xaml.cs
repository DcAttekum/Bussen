using Bussen.ViewModels;

namespace Bussen.Views;

public partial class DealPage : ContentPage
{
	public DealPage()
	{
		InitializeComponent();
		BindingContext = App.Services.GetService<DealViewModel>();
	}
}