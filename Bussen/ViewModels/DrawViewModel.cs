using Bussen.Models;
using Bussen.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Bussen.ViewModels
{
    /// <summary>
    /// The draw view model.
    /// </summary>
    public partial class DrawViewModel : ObservableObject
    {
        #region Fields

        private readonly GameService gameService;

        #endregion

        #region Properties

        [ObservableProperty]
        private ObservableCollection<Player> players;

        #endregion

        #region Constructors

        /// <summary>
        /// The default constructor.
        /// </summary>
        public DrawViewModel()
        {
            players = [];

            gameService = App.Services.GetService<GameService>() ?? throw new ArgumentNullException("Unable to find game service.");
        }

        #endregion

        #region Commands

        [RelayCommand]
        private void GivePlayerCard(Player player)
        {
            
        }

        #endregion
    }
}
