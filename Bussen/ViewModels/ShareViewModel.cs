using Bussen.Models;
using Bussen.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Bussen.ViewModels
{
    /// <summary>
    /// The share view model.
    /// </summary>
    public partial class ShareViewModel : ObservableObject
    {
        #region Fields

        private readonly GameService gameService;

        #endregion

        #region Properties

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CardWidth))]
        private int cardHeight;
        public int CardWidth { get { return CardHeight / 7 * 5; } }

        [ObservableProperty]
        private bool matchingCard;

        [ObservableProperty]
        private ObservableCollection<ObservableCollection<Card>> pyramid;

        [ObservableProperty]
        private ObservableCollection<Player> players;

        #endregion

        #region Constructors

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <exception cref="NullReferenceException">When the game service cannot be found.</exception>
        public ShareViewModel()
        {
            gameService = App.Services.GetService<GameService>() ?? throw new ArgumentNullException("Game service cannot be found.");
            while (gameService.ActivePile.Count < 15)
            {
                gameService.AddToActivePile();
            }

            players = [];
            foreach (var player in gameService.Players)
            {
                Players.Add(player);
            }

            pyramid = [];
            BuildPyramid();
        }

        #endregion

        #region Private Methods

        private void BuildPyramid()
        {
            Pyramid.Clear();

            int rowCount = 1;
            ObservableCollection<Card> currentRow = [];

            for (int i = 0; i < gameService.ActivePile.Count; i++)
            {
                if (currentRow.Count == rowCount)
                {
                    rowCount++;
                    Pyramid.Add(currentRow);
                    currentRow = [];
                }

                currentRow.Add(gameService.ActivePile[i]);
            }

            if (!Pyramid.Contains(currentRow) && currentRow.Any())
            {
                Pyramid.Add(currentRow);
            }

            CardHeight = 890 / Pyramid.Count;

            foreach (var row in Pyramid)
            {
                foreach (var card in row)
                {
                    card.SetDimensions(CardWidth, CardHeight);
                }
            }
        }

        private void RemoveMatchingCardFromPlayer(PlayerCard data)
        {
            foreach (var row in Pyramid)
            {
                foreach (var card in row)
                {
                    if (card.Index == data.Card.Index && card.FacingUp)
                    {
                        gameService.DiscardPile.Add(data.Card);
                        data.Player.Cards.Remove(data.Card);
                        break;
                    }
                }
            }
        }

        private void CheckIfMatchingCardsLeft()
        {
            foreach (var player in Players)
            {
                foreach (var playerCard in player.Cards)
                {
                    foreach (var row in Pyramid)
                    {
                        foreach (var pyramidCard in row)
                        {
                            if (pyramidCard.FacingUp && pyramidCard.Index == playerCard.Index)
                            {
                                return;
                            }
                        }
                    }
                }
            }

            MatchingCard = false;
        }

        #endregion

        #region Commands

        [RelayCommand]
        private void TurnCard(Card card)
        {
            card.Turn();
            
            foreach (var player in Players)
            {
                foreach (var playerCard in player.Cards)
                {
                    if (playerCard.Index == card.Index)
                    {
                        playerCard.Matches = true;
                        MatchingCard = true;
                    }
                }
            }
        }

        [RelayCommand]
        private void RotateCard(Card card)
        {
            card.Rotate();
        }

        [RelayCommand]
        private void RemoveMatchingCard(PlayerCard data)
        {
            RemoveMatchingCardFromPlayer(data);
            CheckIfMatchingCardsLeft();
        }

        [RelayCommand]
        private void AddRow()
        {
            if (gameService.Deck.Any() && Pyramid.Count < 12)
            {
                var rowCount = Pyramid.Count;
                var toAdd = 0;

                if (Pyramid.Last().Count == rowCount) // Last row is full.
                {
                    toAdd = Pyramid.Last().Count + 1;
                }
                else // Last row is not full.
                {
                    toAdd = rowCount - Pyramid.Last().Count;
                }

                bool cardsLeft = true;
                for (int i = 0; i < toAdd && cardsLeft; i++)
                {
                    cardsLeft = gameService.AddToActivePile();
                }

                BuildPyramid();
            }
        }

        [RelayCommand]
        private void RemoveRow()
        {
            if (Pyramid.Count > 4)
            {
                var toRemove = Pyramid.Last().Count;
                for (int i = 0; i < toRemove; i++)
                {
                    gameService.RemoveFromActivePile(true);
                }

                BuildPyramid();
            }
        }

        [RelayCommand]
        private void AddCard()
        {
            if (gameService.Deck.Any() && (
                Pyramid.Count < 12 || Pyramid.Count != Pyramid.Last().Count))
            {
                gameService.AddToActivePile();
                BuildPyramid();
            }
        }

        [RelayCommand]
        private void RemoveCard()
        {
            if (Pyramid.Count > 4)
            {
                gameService.RemoveFromActivePile(true);
                BuildPyramid();
            }
        }

        #endregion
    }
}
