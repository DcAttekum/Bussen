using Bussen.Models;

namespace Bussen.Services
{
    /// <summary>
    /// The game service contains the main data for the game.
    /// </summary>
    public class GameService
    {
        #region Properties

        /// <summary>
        /// The players in the game.
        /// </summary>
        public IList<Player> Players { get; set; }

        /// <summary>
        /// The active players in the game right now.
        /// </summary>
        public IList<Player> ActivePlayers { get; set; }

        /// <summary>
        /// The cards currently in the deck.
        /// </summary>
        public IList<Card> Deck { get; set; }

        /// <summary>
        /// The cards currently in active use of part of a game.
        /// </summary>
        public IList<Card> ActivePile { get; set; }

        /// <summary>
        /// The used cards from the deck.
        /// </summary>
        public IList<Card> DiscardPile { get; set; }

        /// <summary>
        /// Contains the settings of the game.
        /// </summary>
        public GameSettings Settings { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// The default constructor.
        /// </summary>
        public GameService()
        {
            Players = new List<Player>();
            Deck = new List<Card>();
            ActivePile = new List<Card>();
            DiscardPile = new List<Card>();
            Settings = new GameSettings();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Sets everything up for the game to start.
        /// </summary>
        /// <param name="players">A list with the players.</param>
        /// <param name="settings">The settings of the game.</param>
        public void StartGame(IList<Player> players, GameSettings settings)
        {
            Players = players;
            Settings = settings;

            Deck = CardService.CreateDeck(settings.AmountOfDecks, settings.AmountOfJokers);
        }

        /// <summary>
        /// Adds a card to the active pile.
        /// </summary>
        public bool AddToActivePile(bool includeJokers = false)
        {
            var returnValue = false;

            if (Deck.Any())
            {
                var card = Deck.First();
                Deck.Remove(card);

                if (!includeJokers && card.Suit == Suits.Joker)
                {
                    DiscardPile.Add(card);
                    if (Deck.Any())
                    {
                        returnValue = AddToActivePile(includeJokers);
                    }
                    else
                    {
                        App.AlertService.ShowAlert("Empty deck", "The deck is empty!");
                    }
                }
                else
                {
                    ActivePile.Add(card);
                    returnValue = true;
                }
            }
            else
            {
                App.AlertService.ShowAlert("Empty deck", "The deck is empty!");
            }

            return returnValue;
        }

        /// <summary>
        /// Removes a card from the active pile.
        /// </summary>
        public void RemoveFromActivePile(bool backToDeck = false)
        {
            if (ActivePile.Count > 10)
            {
                var card = ActivePile.Last();
                ActivePile.Remove(card);
                
                if (backToDeck)
                {
                    Deck.Add(card);
                }
                else
                {
                    DiscardPile.Add(card);
                }
            }
        }

        public void RemoveAllCardsFromPlayers(bool backToDeck = false)
        {
            foreach (var player in Players)
            {
                foreach (var card in player.Cards)
                {
                    if (backToDeck)
                    {
                        Deck.Add(card);
                    }
                    else
                    {
                        DiscardPile.Add(card);
                    }
                }

                player.Cards.Clear();
            }
        }

        public void DiscardPileBackToDeck()
        {
            foreach (var card in DiscardPile)
            {
                Deck.Add(card);
            }

            DiscardPile.Clear();
        }

        #endregion
    }
}
