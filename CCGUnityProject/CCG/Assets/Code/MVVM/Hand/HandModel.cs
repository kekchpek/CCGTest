using System;
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

        private const int HandMaxCardsCount = Config.Config.MaxCardsInHand;
        private const float HndArcMaxAngle = 120;
        private const float HandArcWidth = 100 * HandMaxCardsCount * Config.Config.CardScale;

        private readonly IList<ICardViewModel> _cards = new List<ICardViewModel>();

        private bool _isArcPattern = true;

        public HandModel(IInputController inputController,
            IBoardViewModel boardViewModel)
        {
            _inputController = inputController;
            _boardViewModel = boardViewModel;
        }

        public event Action<int, ICardViewModel> SomeCardRemoved;

        public void AddCard(ICardViewModel card)
        {
            if (_cards.Count == HandMaxCardsCount)
            {
                card.Destroy();
                return;
            }

            _cards.Add(card);
            void OnCardPlayed()
            {
                card.Played -= OnCardPlayed;
                card.Destroyed -= OnCardDestroyed;
                RemoveCard(card);
                _boardViewModel.Play(card);
                UpdateCardsPositions();
            }
            void OnCardDestroyed()
            {
                card.Played -= OnCardPlayed;
                card.Destroyed -= OnCardDestroyed;
                RemoveCard(card);
                UpdateCardsPositions();
            }
            card.Played += OnCardPlayed;
            card.Destroyed += OnCardDestroyed;
            UpdateCardsPositions();
        }

        private void RemoveCard(ICardViewModel card)
        {
            var cardIndex = _cards.IndexOf(card);
            _cards.Remove(card);
            SomeCardRemoved?.Invoke(cardIndex, card);
        }

        public ICardViewModel[] GetCards()
        {
            return _cards.ToArray();
        }

        public void SwitchCardsPattern()
        {
            _isArcPattern = !_isArcPattern;
            UpdateCardsPositions();
        }

        private void UpdateCardsPositions()
        {
            var screenCenter = _inputController.ScreenWidth / 2f;
            if (_isArcPattern)
            {
                var rotationStep = HndArcMaxAngle / HandMaxCardsCount;
                var startRotation = 0 - rotationStep * 0.5f * (_cards.Count - 1);
                var rotation = startRotation;
                foreach (var card in _cards)
                {
                    var y = 141 * Config.Config.CardScale * Mathf.Cos(rotation * Mathf.PI / 180f);
                    var x = screenCenter + HandArcWidth / 2f * Mathf.Sin(rotation * Mathf.PI / 180f);
                    card.SetPositionAndRotationInHand(
                        _inputController.ScreenPointToWorld(new Vector2(x, y)), -rotation);
                    rotation += rotationStep;
                }
            }
            else
            {
                var positionStepX = 200f * Config.Config.CardScale;
                var positionX = screenCenter - positionStepX * (_cards.Count - 1) / 2f;
                foreach (var card in _cards)
                {
                    card.SetPositionAndRotationInHand(
                        _inputController.ScreenPointToWorld(new Vector2(positionX, 
                            141f * Config.Config.CardScale)), 0f);
                    positionX += positionStepX;
                }
            }
        }
    }
}