using System.Collections.Generic;
using System.Linq;
using CCG.Core.Input;
using CCG.MVVM.Board;
using CCG.MVVM.Card;
using UnityEngine;

namespace CCG.MVVM.Hand
{
    public class HandModel : IHandModel
    {
        private readonly IInputController _inputController;
        private readonly IBoardViewModel _boardViewModel;

        private const int HandMaxCardsCount = 10;
        private const float HndArcMaxAngle = 120;
        private const float HandArcWidth = 100 * HandMaxCardsCount * Config.Config.CardScale;
        
        private readonly float _screenWidth;

        private readonly IList<ICardViewModel> _cards = new List<ICardViewModel>();

        public HandModel(IInputController inputController,
            IBoardViewModel boardViewModel)
        {
            _inputController = inputController;
            _boardViewModel = boardViewModel;
            _screenWidth = Screen.width;
        }
        
        public void AddCard(ICardViewModel card)
        {
            _cards.Add(card);
            void OnCardPlayed()
            {
                card.Played -= OnCardPlayed;
                card.Destroyed -= OnCardDestroyed;
                _boardViewModel.Play(card);
                _cards.Remove(card);
                UpdateCardsPositions();
            }
            void OnCardDestroyed()
            {
                card.Played -= OnCardPlayed;
                card.Destroyed -= OnCardDestroyed;
                _cards.Remove(card);
                UpdateCardsPositions();
            }
            card.Played += OnCardPlayed;
            card.Destroyed += OnCardDestroyed;
            UpdateCardsPositions();
        }

        public ICardViewModel[] GetCards()
        {
            return _cards.ToArray();
        }

        private void UpdateCardsPositions()
        {
            var screenCenter = _screenWidth / 2f;

            var rotationStep = HndArcMaxAngle / HandMaxCardsCount;

            var startRotation = 0 - rotationStep * 0.5f * (_cards.Count - 1);
            var rotation = startRotation;
            foreach (var card in _cards)
            {
                var y = 141 * Config.Config.CardScale * Mathf.Cos(rotation * Mathf.PI / 180f);
                var x = screenCenter - HandArcWidth / 2f * Mathf.Sin(rotation * Mathf.PI / 180f);
                card.SetPositionAndRotationInHand(_inputController.ScreenPointToWorld(new Vector2(x, y)), rotation);
                rotation += rotationStep;
            }
        }
    }
}