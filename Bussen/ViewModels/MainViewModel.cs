using Bussen.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Bussen.ViewModels
{
    /// <summary>
    /// The main viewmodel.
    /// </summary>
    public partial class MainViewModel : ObservableObject
    {
        #region Commands

        [RelayCommand]
        private async Task Play()
        {
            await Shell.Current.GoToAsync(nameof(SetupPage));
        }

        [RelayCommand]
        private void Quit()
        {
            Application.Current!.Quit();
        }

        #endregion
    }
}
