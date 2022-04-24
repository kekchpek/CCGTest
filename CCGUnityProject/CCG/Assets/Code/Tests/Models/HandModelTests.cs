using System;
using System.Linq;
using CCG.MVVM.Card;
using CCG.MVVM.Hand;
using CCG.Tests.Auxiliary;
using NSubstitute;
using NUnit.Framework;

namespace CCG.Tests.Models
{
    public class HandModelTests
    {
        [Test]
        public void Creation_NoAction_HandIsEmpty()
        {
            // Arrange
            TestsHelper.CreateContainerFor<HandModel>(out var handModel);
            
            // Act
            // no act
            
            // Assert
            Assert.IsEmpty(handModel.GetCards());
        }
        
        [Test]
        public void AddCard_EmptyHand_CardAppearsInHand()
        {
            // Arrange
            TestsHelper.CreateContainerFor<HandModel>(out var handModel);
            var card = Substitute.For<ICardViewModel>();
            
            // Act
            handModel.AddCard(card);
            
            // Assert
            Assert.IsTrue(handModel.GetCards().Contains(card));
        }
        
        [Test]
        public void AddCard_FullHand_CardNotAppearsInHand()
        {
            // Arrange
            TestsHelper.CreateContainerFor<HandModel>(out var handModel);
            var card = Substitute.For<ICardViewModel>();
            
            // Act
            for (var i = 0; i < Config.Config.MaxCardsInHand; i++)
                handModel.AddCard(Substitute.For<ICardViewModel>());
            handModel.AddCard(card);
            
            // Assert
            Assert.IsFalse(handModel.GetCards().Contains(card));
        }
        
        [Test]
        public void AddCard_CardPlayed_CardRemovedFromHand()
        {
            // Arrange
            TestsHelper.CreateContainerFor<HandModel>(out var handModel);
            var card = Substitute.For<ICardViewModel>();
            
            // Act
            handModel.AddCard(card);
            card.Played += Raise.Event<Action>();
            
            // Assert
            Assert.IsFalse(handModel.GetCards().Contains(card));
        }
        
        [Test]
        public void AddCard_CardDestroyed_CardRemovedFromHand()
        {
            // Arrange
            TestsHelper.CreateContainerFor<HandModel>(out var handModel);
            var card = Substitute.For<ICardViewModel>();
            
            // Act
            handModel.AddCard(card);
            card.Destroyed += Raise.Event<Action>();
            
            // Assert
            Assert.IsFalse(handModel.GetCards().Contains(card));
        }
    }
}