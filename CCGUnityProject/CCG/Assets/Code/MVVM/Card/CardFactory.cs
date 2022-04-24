using CCG.Core.Input;
using CCG.Core.MVVM;
using UnityEngine;
using Zenject;

namespace CCG.MVVM.Card
{
    public class CardFactory : ICardFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly IInputController _inputController;
        private readonly Transform _mainCanvas;

        private const string ViewPath = "Prefabs/CardView";

        public CardFactory(IInstantiator instantiator,
            IInputController inputController)
        {
            _mainCanvas = GameObject.FindGameObjectWithTag("MainCanvas").transform;
            _instantiator = instantiator;
            _inputController = inputController;
        }
        
        public ICardViewModel CreateCard(int health, int attack, int mana,
            string description, string title, Texture2D icon)
        {
            var card = new CardViewModel(_inputController)
            {
                Health = health,
                Attack = attack,
                Mana = mana,
                Description = description,
                Title = title,
                Icon = icon
            };
            var viewPrefab = Resources.Load<GameObject>(ViewPath);
            var gameObject = _instantiator.InstantiatePrefab(viewPrefab, _mainCanvas);
            gameObject.transform.localScale = Vector3.one * Config.Config.CardScale;
            gameObject.GetComponent<IViewInitializer<ICardViewModel>>()
                .SetViewModel(card);
            return card;
        }
    }
}