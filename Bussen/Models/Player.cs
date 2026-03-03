using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace Bussen.Models
{
    public partial class Player : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private ObservableCollection<Card> cards;

        public bool NotMaxCards => Cards.Count < 4;

        #endregion

        #region Constructors

        /// <summary>
        /// The default player constructor.
        /// </summary>
        public Player()
        {
            name = string.Empty;
            cards = [];
            Cards.CollectionChanged += (_, __) =>
            {
                OnPropertyChanged(nameof(NotMaxCards));
            };
        }

        #endregion
    }
}
