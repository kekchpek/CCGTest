using CCG.Core.MVVM;
using CCG.MVVM.Card;

namespace CCG.MVVM.Board
{
    public interface IBoardViewModel : IViewModel
    {
        void Play(ICardViewModel card);
    }
}