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

        #endregion

        #region Constructors

        public Player()
        {
            name = string.Empty;
            cards = [];
        }

        #endregion
    }
}
