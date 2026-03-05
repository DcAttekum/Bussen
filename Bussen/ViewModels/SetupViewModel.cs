using Bussen.Models;
using Bussen.Services;
using Bussen.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Bussen.ViewModels
{
    /// <summary>
    /// The setup viewmodel to handle the setup of the game.
    /// </summary>
    public partial class SetupViewModel : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        private ObservableCollection<Player> players;

        [ObservableProperty]
        private int amountOfDecks;

        [ObservableProperty]
        private int amountOfJokers;

        #endregion

        #region Constructors

        public SetupViewModel()
        {
            players = [];
            Players.Add(new Player());

            AmountOfDecks = 1;
            AmountOfJokers = 2;
        }

        #endregion

        #region Commands

        [RelayCommand]
        private void AddPlayer()
        {
            if (Players.Count < 12)
            {
                Players.Add(new Player());

                if (Players.Count >= 10 && AmountOfDecks < 2)
                {
                    AmountOfDecks++;
                }
            }
            else
            {
                App.AlertService.ShowAlert("Max players", "You have reached the maximum amount of players!");
            }
        }

        [RelayCommand]
        private void RemovePlayer(Player player)
        {
            if (Players.Contains(player))
            {
                Players.Remove(player);
            }
            else
            {
                App.AlertService.ShowAlert("Error", "Unable to delete player since player does not exist.");
            }
        }

        [RelayCommand]
        private void AddDeck()
        {
            if (AmountOfDecks < 10)
            {
                AmountOfDecks++;
            }
        }

        [RelayCommand]
        private void RemoveDeck()
        {
            if ((AmountOfDecks > 1 && Players.Count < 10) || 
                (AmountOfDecks > 2))
            {
                AmountOfDecks--;
            }
        }

        [RelayCommand]
        private void AddJoker()
        {
            if (AmountOfJokers < 20)
            {
                AmountOfJokers++;
            }
        }

        [RelayCommand]
        private void RemoveJoker()
        {
            if (AmountOfJokers > 0)
            {
                AmountOfJokers--;
            }
        }

        [RelayCommand]
        private async Task Play()
        {
            if (Players.Count() == 0)
            {
                App.AlertService.ShowAlert("Minimum players", "You need at least 1 player to start!");
                return;
            }

            if (Players
                .Select(p => p.Name)
                .Where(n => string.IsNullOrWhiteSpace(n))
                .Count() > 0)
            {
                App.AlertService.ShowAlert("Player names", "All players need to have a name!");
                return;
            }

            var service = App.Services.GetService<GameService>();
            if (service != null)
            {
                service.StartGame(Players.ToList(), new GameSettings
                {
                    AmountOfDecks = AmountOfDecks,
                    AmountOfJokers = AmountOfJokers
                });
                await Shell.Current.GoToAsync(nameof(DealPage));
            }
            else
            {
                App.AlertService.ShowAlert("Error", "An error occurred while trying to start the game!");
            }
        }

        #endregion

        #region Debugging

        [RelayCommand]
        private void AddSomePlayers()
        {
#if DEBUG
            while (Players.Count < 5)
            {
                Players.Add(new Player());
            }

            Players[0].Name = "Eleanor";
            Players[1].Name = "Henry";
            Players[2].Name = "Margaret";
            Players[3].Name = "Victoria";
            Players[4].Name = "Samuel";
#endif
        }

        [RelayCommand]
        private void AddMaxPlayers()
        {
#if DEBUG
            while (Players.Count < 12)
            {
                Players.Add(new Player());
            }

            Players[0].Name = "Eleanor";
            Players[1].Name = "Henry";
            Players[2].Name = "Margaret";
            Players[3].Name = "Victoria";
            Players[4].Name = "Samuel";
            Players[5].Name = "Thomas";
            Players[6].Name = "Caroline";
            Players[7].Name = "William";
            Players[8].Name = "Seraphine";
            Players[9].Name = "Alaric";
            Players[10].Name = "Thalion";
            Players[11].Name = "Jace";

            AmountOfDecks = 2;
#endif
        }

        #endregion
    }
}
