using CCG.Core.MVVM;
using CCG.MVVM.Card;
using UnityEngine;

namespace CCG.MVVM.Board
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class BoardView : ViewBehaviour<IViewModel>
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            other.GetComponent<ICardView>().OnEnterToBoard();
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            other.GetComponent<ICardView>().OnExitFromBoard();
        }
    }
}