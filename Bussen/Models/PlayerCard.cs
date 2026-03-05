namespace Bussen.Models
{
    /// <summary>
    /// A class containing a player and a card to use as a parameter
    /// for the front end.
    /// </summary>
    public class PlayerCard
    {
        #region Properties

        /// <summary>
        /// The player the card belongs to.
        /// </summary>
        public Player Player { get; set; }

        /// <summary>
        /// The selected card.
        /// </summary>
        public Card Card { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="card">The card.</param>
        public PlayerCard(Player player, Card card)
        {
            Player = player;
            Card = card;
        }

        #endregion
    }
}
