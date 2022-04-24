using System;
using CCG.MVVM.Card;

namespace CCG.MVVM.Hand
{
    public interface IHandModel
    {

        /// <summary>
        /// Fired when some card was removed from hand.
        /// Passes to an arguments an index of the removed card and the removed card.
        /// </summary>
        event Action<int, ICardViewModel> SomeCardRemoved;
        
        void AddCard(ICardViewModel card);
        ICardViewModel[] GetCards();
        
        /// <summary>
        /// Changes a representation of cards. Either shows it in a line or in an arc pattern.
        /// </summary>
        void SwitchCardsPattern();
    }
}