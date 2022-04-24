using CCG.Core.Input;
using CCG.Core.MVVM;
using CCG.Models.ImageModel;
using CCG.MVVM.Board;
using CCG.MVVM.Card;
using CCG.MVVM.Hand;
using CCG.MVVM.HandController;
using CCG.MVVM.StatsChanger;
using UnityEngine;
using Zenject;

namespace CCG.Core
{
    public class StartupBehaviour : MonoBehaviour
    {
        private ICardFactory _cardFactory;
        private IImageModel _imageModel;
        private IHandModel _handModel;
        private IInputController _inputController;
        private IBoardViewModel _boardViewModel;
        private IStatsChanger _statsChanger;
        private IHandControllerViewModel _handControllerViewModel;

        [SerializeField] private Camera _mainCamera;
        [SerializeField] private GameObject _loadingPanel;
        [SerializeField] private BoardView _boardView;
        [SerializeField] private StatsChangerView _statsChangerView;
        [SerializeField] private HandControllerView _handControllerView;

        [Inject]
        public void Construct(
            ICardFactory cardFactory,
            IImageModel imageModel,
            IHandModel handModel,
            IInputController inputController,
            IBoardViewModel boardViewModel,
            IStatsChanger statsChanger,
            IHandControllerViewModel handControllerViewModel)
        {
            _cardFactory = cardFactory;
            _imageModel = imageModel;
            _handModel = handModel;
            _inputController = inputController;
            _boardViewModel = boardViewModel;
            _statsChanger = statsChanger;
            _handControllerViewModel = handControllerViewModel;
        }
        
        private void Start()
        {
            ((IViewInitializer<IBoardViewModel>)_boardView).SetViewModel(_boardViewModel);
            ((IViewInitializer<IHandControllerViewModel>)_handControllerView).SetViewModel(_handControllerViewModel);
            ((IViewInitializer<IStatsChanger>)_statsChangerView).SetViewModel(_statsChanger);
            _inputController.SetCamera(_mainCamera);
            var cardsCount = Random.Range(4, 7);
            _imageModel.Initialize().OnSuccess(() =>
            {
                _loadingPanel.SetActive(false);
                var imageIds = _imageModel.GetAllImageIds();
                for (var i = 0; i < cardsCount; i++)
                {
                    _handModel.AddCard(_cardFactory.CreateCard(
                        2*(i+1),
                        i+1, 
                        i+1,
                        $"bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla {i+1}",
                        $"{i+1} CARD",
                        _imageModel.GetImage(imageIds[i])));
                }
            });
        }
    }
}
