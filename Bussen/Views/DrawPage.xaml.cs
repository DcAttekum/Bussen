using Bussen.ViewModels;

namespace Bussen.Views;

public partial class DrawPage : ContentPage
{
	public DrawPage()
	{
		InitializeComponent();
		BindingContext = App.Services.GetService<DrawViewModel>();
	}
}