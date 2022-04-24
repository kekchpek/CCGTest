using CCG.Core.MVVM;
using UnityEngine;
using UnityEngine.UI;

namespace CCG.MVVM.StatsChanger
{
    public class StatsChangerView : ViewBehaviour<IStatsChangerViewModel>
    {

        [SerializeField] private Button _changeStatsButton;

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            _changeStatsButton.onClick.RemoveAllListeners();
            _changeStatsButton.onClick.AddListener(() => ViewModel.ChangeCardStat());
        }
    }
}