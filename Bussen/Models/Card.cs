using CommunityToolkit.Mvvm.ComponentModel;

namespace Bussen.Models
{
    /// <summary>
    /// The suits enum.
    /// </summary>
    public enum Suits
    {
        Hearts,
        Diamonds,
        Spades,
        Clubs,
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

        // TODO: Probably doesn't need to be an observable property.
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Image))]
        private bool facingUp;

        /// <summary>
        /// Whether or not the card is rotated.
        /// </summary>
        public bool Rotated
        {
            get { return rotated; }
            set 
            { 
                rotated = value;
                Rotation = value ? 90 : 0;
            }
        }
        private bool rotated;

        [ObservableProperty]
        private int rotation;

        /// <summary>
        /// The image string to use when displaying on screen.
        /// </summary>
        public ImageSource Image => FacingUp ? front : back;
        private ImageSource front { get; }
        private ImageSource back { get; }

        [ObservableProperty]
        private int width;

        [ObservableProperty]
        private int height;

        [ObservableProperty]
        private bool matches;

        #endregion

        #region Constructors

        public Card(Suits suit, int index)
        {
            Suit = suit;
            Index = index;
            facingUp = false;
            Rotated = false;
            matches = false;

            front = ImageSource.FromFile($"{Suit.ToString() + Index}.png");
            back = ImageSource.FromFile("cardback.png");

            width = 700;
            height = 500;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Turns the card over.
        /// </summary>
        public void Turn()
        {
            FacingUp = !FacingUp;
        }

        /// <summary>
        /// Rotates the card sideways.
        /// </summary>
        public void Rotate()
        {
            var temp = Width;
            Width = Height;
            Height = temp;

            Rotated = !Rotated;
        }

        /// <summary>
        /// Sets the dimensions for the card depending on rotation.
        /// </summary>
        /// <param name="width">The width of the card.</param>
        /// <param name="height">The height of the card.</param>
        public void SetDimensions(int width, int height)
        {
            Width = Rotated ? height : width;
            Height = Rotated ? width : height;
        }

        #endregion
    }
}
