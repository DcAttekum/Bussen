namespace Bussen.Models
{
    /// <summary>
    /// Settings of the game.
    /// </summary>
    public class GameSettings
    {
        #region Properties

        /// <summary>
        /// The amount of decks to be used.
        /// </summary>
        public int AmountOfDecks { get; set; }

        /// <summary>
        /// The amount of jokers to be used.
        /// </summary>
        public int AmountOfJokers { get; set; }

        #endregion
    }
}
