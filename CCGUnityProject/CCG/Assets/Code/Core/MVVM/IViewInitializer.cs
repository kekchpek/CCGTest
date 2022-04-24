namespace CCG.Core.MVVM
{
    public interface IViewInitializer<in T> where T : IViewModel
    {
        void SetViewModel(T viewModel);
    }
}