using CCG.Views.Card;

namespace CCG.Views.Hand
{
    public interface IHandModel
    {
        void AddCard(ICardViewModel card);
        ICardViewModel[] GetCards();
    }
}