using UnityEngine;

namespace CCG.Views.Card
{
    public interface ICardFactory
    {
        ICardViewModel CreateCard(int health, int attack, int mana,
            string description, string title, Texture2D icon);
    }
}