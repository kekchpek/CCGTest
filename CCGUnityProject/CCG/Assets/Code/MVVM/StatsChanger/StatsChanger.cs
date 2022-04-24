using CCG.Core.MVVM;
using CCG.MVVM.Card;
using CCG.MVVM.Hand;
using Random = UnityEngine.Random;

namespace CCG.MVVM.StatsChanger
{
    public class StatsChanger : ViewModel, IStatsChanger
    {
        private readonly IHandModel _handModel;

        private int _changingCardIndex;

        public StatsChanger(IHandModel handModel)
        {
            _handModel = handModel;
        }

        public void ChangeCardStat()
        {
            var cards = _handModel.GetCards();
            if (cards.Length == 0)
                return;
            if (cards.Length - 1 < _changingCardIndex)
                _changingCardIndex = 0;
            ChangeCardRandomStat(cards[_changingCardIndex]);
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