using CCG.MVVM.Card;

namespace CCG.MVVM.Hand
{
    public interface IHandModel
    {
        void AddCard(ICardViewModel card);
        ICardViewModel[] GetCards();
    }
}