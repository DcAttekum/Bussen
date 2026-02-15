using CommunityToolkit.Mvvm.ComponentModel;

namespace Bussen.Models
{
    /// <summary>
    /// The suits enum.
    /// </summary>
    public enum Suits
    {
        Heart,
        Diamond,
        Spade,
        Club,
        Joker
    }

    /// <summary>
    /// The card object.
    /// </summary>
    public partial class Card : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Image))]
        private Suits suit;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Image))]
        private int index;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Image))]
        private bool facingUp;

        /// <summary>
        /// The image string to use when displaying on screen.
        /// </summary>
        public string Image
        {
            get
            {
                if (FacingUp)
                {
                    return $"{Suit.ToString() + Index}.png";
                }
                else
                {
                    return "FaceDown.png";
                }
            }
        }

        #endregion

        #region Constructors

        public Card(Suits suit, int index)
        {
            Suit = suit;
            Index = index;
        }

        #endregion

        #region Public Methods

        public void Turn()
        {
            FacingUp = !FacingUp;
        }

        #endregion
    }
}
