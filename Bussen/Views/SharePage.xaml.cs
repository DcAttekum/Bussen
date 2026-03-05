using Bussen.ViewModels;

namespace Bussen.Views;

public partial class SharePage : ContentPage
{
	public SharePage()
	{
		InitializeComponent();
		BindingContext = App.Services.GetService<ShareViewModel>();
	}
}