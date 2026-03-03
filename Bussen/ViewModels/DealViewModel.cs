using Bussen.Models;
using Bussen.Services;
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

            var service = App.Services.GetService<GameService>();
            if (service != null)
            {
                gameService = service;
                gameService.Deck = gameService.Deck.Shuffle().ToList();

                foreach (var player in gameService.Players)
                {
                    Players.Add(player);
                }
            }
            else
            {
                throw new NullReferenceException("Error finding game service.");
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

        [RelayCommand]
        private void NextStage()
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
                // TODO: Move to next stage logic
            }
            else
            {
                App.AlertService.ShowAlert("Players need cards", "Some players still need one or more cards!");
            }
        }

        #endregion
    }
}
