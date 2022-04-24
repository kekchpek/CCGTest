using CCG.Core.MVVM;
using CCG.Models.ImageModel;
using CCG.MVVM.Card;
using CCG.MVVM.Hand;
using UnityEngine;

namespace CCG.MVVM.HandController
{
    public class HandControllerViewModel : ViewModel, IHandControllerViewModel
    {
        private readonly ICardFactory _cardFactory;
        private readonly IHandModel _handModel;
        private readonly IImageModel _imageModel;

        public HandControllerViewModel(
            ICardFactory cardFactory,
            IHandModel handModel,
            IImageModel imageModel)
        {
            _cardFactory = cardFactory;
            _handModel = handModel;
            _imageModel = imageModel;
        }
        
        public void AddRandomCardToHand()
        {
            var imageIds = _imageModel.GetAllImageIds();
            var randomImageId = imageIds[Random.Range(0, imageIds.Length)];
            _handModel.AddCard(
                _cardFactory.CreateCard(
                    Random.Range(1, 10),
                    Random.Range(0, 100),
                    Random.Range(0, 100),
                    "Random description Random description Random description Random description",
                    "RANDOM CARD",
                    _imageModel.GetImage(randomImageId)));
        }

        public void SwitchCardsPattern()
        {
            _handModel.SwitchCardsPattern();
        }
    }
}