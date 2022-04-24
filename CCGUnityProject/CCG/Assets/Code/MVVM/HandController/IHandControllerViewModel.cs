using CCG.Core.MVVM;

namespace CCG.MVVM.HandController
{
    public interface IHandControllerViewModel : IViewModel
    {
        void AddRandomCardToHand();
        void SwitchCardsPattern();
    }
}