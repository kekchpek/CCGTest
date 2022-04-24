using CCG.Core.MVVM;

namespace CCG.MVVM.StatsChanger
{
    public interface IStatsChanger : IViewModel
    {
        void ChangeCardStat();
    }
}