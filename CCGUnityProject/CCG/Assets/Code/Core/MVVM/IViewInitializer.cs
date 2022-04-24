namespace CCG.Core.MVVM.ViewFactory
{
    public interface IViewInitializer<in T> where T : IViewModel
    {
        void SetViewModel(T viewModel);
    }
}