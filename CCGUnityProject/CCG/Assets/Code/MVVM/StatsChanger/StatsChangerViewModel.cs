using CCG.Core.MVVM;
using CCG.MVVM.Card;
using CCG.MVVM.Hand;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CCG.MVVM.StatsChanger
{
    public class StatsChangerViewModel : ViewModel, IStatsChangerViewModel
    {
        private readonly IHandModel _handModel;

        private int _changingCardIndex;

        public StatsChangerViewModel(IHandModel handModel)
        {
            _handModel = handModel;
        }

        public void Initialize()
        {
            _handModel.SomeCardRemoved += OnSomeCardRemoved;
        }

        private void OnSomeCardRemoved(int cardIndex, ICardViewModel _)
        {
            if (_changingCardIndex >= cardIndex)
            {
                _changingCardIndex--;
                _changingCardIndex = Mathf.Max(_changingCardIndex, 0);
            }
        }

        public void ChangeCardStat()
        {
            var cards = _handModel.GetCards();
            if (cards.Length == 0)
                return;
            if (cards.Length - 1 < _changingCardIndex)
                _changingCardIndex = 0;
            ChangeCardRandomStat(cards[_changingCardIndex]);
            _changingCardIndex++;
        }

        private void ChangeCardRandomStat(ICardViewModel card)
        {
            var statIndex = Random.Range(0, 3);
            var statValue = Random.Range(-2, 10);
            switch (statIndex)
            {
                case 0:
                    card.Mana = statValue;
                    break;
                case 1:
                    card.Health = statValue;
                    break;
                case 2: 
                    card.Attack = statValue;
                    break;
            }
        }
    }
}