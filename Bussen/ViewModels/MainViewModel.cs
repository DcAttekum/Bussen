using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Bussen.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [RelayCommand]
        private void ShowAlert()
        {
            App.AlertService.ShowAlert("Test", "Testing the alert service.");
        }
    }
}
