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
        /// The cards currently in the deck.
        /// </summary>
        public IList<Card> Deck { get; set; }

        /// <summary>
        /// The used cards from the deck.
        /// </summary>
        public IList<Card> UsedPile { get; set; }

        /// <summary>
        /// Contains the settings of the game.
        /// </summary>
        public GameSettings Settings { get; set; }

        #endregion

        #region Constructors

        public GameService()
        {
            Players = new List<Player>();
            Deck = new List<Card>();
            UsedPile = new List<Card>();
            Settings = new GameSettings();
        }

        #endregion

        #region Public Methods

        public void StartGame(IList<Player> players, GameSettings settings)
        {

        }

        #endregion
    }
}
