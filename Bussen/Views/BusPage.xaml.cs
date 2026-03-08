using Bussen.ViewModels;

namespace Bussen.Views;

public partial class BusPage : ContentPage
{
	public BusPage()
	{
		InitializeComponent();
		BindingContext = App.Services.GetService<BusViewModel>();
	}
}