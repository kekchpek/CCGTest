using System;
using CCG.Core.MVVM;
using CCG.Views.Card;
using UnityEngine;

namespace CCG.Views.Board
{
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