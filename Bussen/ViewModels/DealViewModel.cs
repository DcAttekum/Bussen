using Bussen.Models;
using Bussen.Services;
using Bussen.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Bussen.ViewModels
{
    /// <summary>
    /// The deal view model.
    /// </summary>
    public partial class DealViewModel : ObservableObject
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
        /// <exception cref="NullReferenceException">When the game service cannot be found.</exception>
        public DealViewModel()
        {
            players = [];

            gameService = App.Services.GetService<GameService>() ?? throw new ArgumentNullException("Game service cannot be found.");
            gameService.Deck = gameService.Deck.Shuffle().ToList();

            foreach (var player in gameService.Players)
            {
                Players.Add(player);
            }
        }

        #endregion

        #region Commands

        [RelayCommand]
        private void DealCardToPlayer(Player player)
        {
            if (player.NotMaxCards)
            {
                var card = gameService.Deck.First();
                gameService.Deck.Remove(card);

                card.FacingUp = true;
                player.Cards.Add(card);
            }
        }

        // TODO: Handle removal of jokers.

        [RelayCommand]
        private async Task NextStage()
        {
            bool finished = true;

            foreach (var player in Players)
            {
                if (player.NotMaxCards)
                {
                    finished = false;
                    break;
                }
            }

            if (finished)
            {
                await Shell.Current.GoToAsync(nameof(SharePage));
            }
            else
            {
                App.AlertService.ShowAlert("Players need cards", "Some players still need one or more cards!");
            }
        }

        #endregion

        #region Debugging

        [RelayCommand]
        private void AddCards()
        {
#if DEBUG
            foreach (var player in Players)
            {
                while (player.NotMaxCards)
                {
                    var card = gameService.Deck.First();
                    gameService.Deck.Remove(card);

                    card.FacingUp = true;
                    player.Cards.Add(card);
                }
            }
#endif
        }

        #endregion
    }
}
