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
        private IStatsChangerViewModel _statsChangerViewModel;
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
            IStatsChangerViewModel statsChangerViewModel,
            IHandControllerViewModel handControllerViewModel)
        {
            _cardFactory = cardFactory;
            _imageModel = imageModel;
            _handModel = handModel;
            _inputController = inputController;
            _boardViewModel = boardViewModel;
            _statsChangerViewModel = statsChangerViewModel;
            _handControllerViewModel = handControllerViewModel;
        }
        
        private void Start()
        {
            ((IViewInitializer<IBoardViewModel>)_boardView).SetViewModel(_boardViewModel);
            ((IViewInitializer<IHandControllerViewModel>)_handControllerView).SetViewModel(_handControllerViewModel);
            ((IViewInitializer<IStatsChangerViewModel>)_statsChangerView).SetViewModel(_statsChangerViewModel);
            _inputController.SetCamera(_mainCamera);
            InitBoard();
        }

        private void InitBoard()
        {
            _imageModel.Initialize().OnSuccess(() =>
            {
                var cardsCount = Random.Range(4, 7);
                _loadingPanel.SetActive(false);
                var imageIds = _imageModel.GetAllImageIds();
                for (var i = 0; i < cardsCount; i++)
                {
                    _handModel.AddCard(_cardFactory.CreateCard(
                        2 * (i + 1),
                        i + 1,
                        i + 1,
                        $"bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla {i + 1}",
                        $"{i + 1} CARD",
                        _imageModel.GetImage(imageIds[i])));
                }
            }).OnFail(e =>
            {
                Debug.LogError(e.Message);
                InitBoard();
            });
        }
    }
}
