using CCG.Core.Input;
using CCG.Core.Models.ImageModel;
using CCG.Views.Card;
using CCG.Views.Hand;
using UnityEngine;
using Zenject;

namespace CCG.Core
{
    public class StartupBehaviour : MonoBehaviour
    {
        private ICardFactory _cardFactory;
        private IImageModel _imageModel;
        private IHandModel _handModel;

        [Inject]
        public void Construct(
            ICardFactory cardFactory,
            IImageModel imageModel,
            IHandModel handModel)
        {
            _cardFactory = cardFactory;
            _imageModel = imageModel;
            _handModel = handModel;
        }
        
        private void Start()
        {
            var cardsCount = Random.Range(4, 7);
            _imageModel.Initialize().OnSuccess(() =>
            {
                var imageIds = _imageModel.GetAllImageIds();
                for (var i = 0; i < cardsCount; i++)
                {
                    _handModel.AddCard(_cardFactory.CreateCard(
                        2*(i+1),
                        i+1, 
                        i+1,
                        $"bla vla fierf oa fere grgrgtu nvirnir ksnmkr ring nvweorif fin {i+1}",
                        $"{i+1} CARD",
                        _imageModel.GetImage(imageIds[i])));
                }
            });
        }
    }
}
