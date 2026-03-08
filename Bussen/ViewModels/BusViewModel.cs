using Bussen.Models;
using Bussen.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Bussen.ViewModels
{
    /// <summary>
    /// The bus view model.
    /// </summary>
    public partial class BusViewModel : ObservableObject
    {
        #region Fields

        private readonly GameService gameService;

        #endregion

        #region Properties

        [ObservableProperty]
        private Player player;

        #endregion

        #region Constructors

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <exception cref="ArgumentNullException">In case of no game service or active player.</exception>
        public BusViewModel()
        {
            gameService = App.Services.GetService<GameService>() ?? throw new ArgumentNullException("The game service could not be found.");
            player = gameService.ActivePlayers.First() ?? throw new ArgumentNullException("No active players were found.");
        }

        #endregion

        #region Commands

        #endregion
    }
}
