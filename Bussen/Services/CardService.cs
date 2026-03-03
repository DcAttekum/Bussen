using Bussen.Models;

namespace Bussen.Services
{
    /// <summary>
    /// The card service.
    /// </summary>
    public static class CardService
    {
        #region Private Methods

        private static Suits GetSuit(int index)
        {
            switch (index)
            {
                case 0:
                    return Suits.Diamonds;
                case 1:
                    return Suits.Spades;
                case 2:
                    return Suits.Clubs;
                case 3:
                    return Suits.Hearts;
                default:
                    throw new ArgumentException($"Not suitable suit found for index {index}");
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Create deck.
        /// </summary>
        /// <param name="amountOfDecks">The amount times a regular deck goes into the final deck.</param>
        /// <param name="amountOfJokers">The amount of jokers to put into the deck.</param>
        /// <returns></returns>
        public static IList<Card> CreateDeck(int amountOfDecks, int amountOfJokers)
        {
            var deck = new List<Card>();

            for (int i = 0; i < amountOfDecks; i++) // Runs once for each deck.
            {
                for (int j = 0; j < 4; j++) // Runs once per suit.
                {
                    var suit = GetSuit(j);

                    for (int k = 2; k <= 14; k++) // Runs once per card in each suit.
                    {
                        deck.Add(new Card(suit, k));
                    }
                }
            }

            for (int i = 0; i < amountOfJokers; i++) // Runs once for each joker
            {
                deck.Add(new Card(Suits.Joker, i % 2));
            }

            return deck;
        }

        #endregion
    }
}
