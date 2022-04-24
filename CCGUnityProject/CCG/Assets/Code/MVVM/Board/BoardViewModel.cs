using CCG.Core.MVVM;
using CCG.MVVM.Card;
using UnityEngine;

namespace CCG.MVVM.Board
{
    public class BoardViewModel : ViewModel, IBoardViewModel
    {
        public void Play(ICardViewModel card)
        {
            Debug.Log("Card played!");
        }
    }
}