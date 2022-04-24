using System;
using CCG.Core.MVVM;
using UnityEngine;

namespace CCG.MVVM.Card
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
        
        bool IsOverBoard { get; }

        event Action Played;
        event Action Destroyed;

        void OnMouseClickDown();

        void OnCardEnterBoard();

        void OnCardExitBoard();

        void Destroy();
        
        void SetPositionAndRotationInHand(Vector2 position, float rotation);

    }
}