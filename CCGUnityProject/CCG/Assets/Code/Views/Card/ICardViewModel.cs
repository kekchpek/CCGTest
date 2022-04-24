
using System;
using CCG.Core.MVVM;
using UnityEngine;

namespace CCG.Views.Card
{
    public interface ICardViewModel : IViewModel
    {
        Vector2 Position { get; }
        
        float Rotation { get; }
        
        int Health { get; set; }
        
        int Attack { get; set; }
        
        int Mana { get; set; }
        
        string Description { get; set; }
        
        string Title { get; set; }

        Texture2D Icon { get; set; }
        
        bool IsSelected { get; }

        event Action Played;

        void OnMouseClickDown();

        void OnCardEnterBoard();

        void OnCardExitBoard();

        void SetPositionAndRotationInHand(Vector2 position, float rotation);

    }
}