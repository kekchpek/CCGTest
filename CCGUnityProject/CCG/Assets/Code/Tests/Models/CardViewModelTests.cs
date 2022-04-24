using System;
using CCG.Core.Input;
using CCG.MVVM.Card;
using CCG.Tests.Auxiliary;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace CCG.Tests.Models
{
    public class CardViewModelTests
    {
        [Test]
        public void Creation_NoAct_IsNotSelected()
        {
            // Arrange
            TestsHelper.CreateContainerFor<CardViewModel>(out var card);
            
            // Act
            // no act
            
            // Assert 
            Assert.IsFalse(card.IsSelected);
        }
        
        [Test]
        public void OnMouseDown_CardSelected()
        {
            // Arrange
            TestsHelper.CreateContainerFor<CardViewModel>(out var card);
            
            // Act
            card.OnMouseClickDown();
            
            // Assert 
            Assert.IsTrue(card.IsSelected);
        }
        
        [Test]
        public void OnMouseDown_MouseUpNotOverBoard_CardDeselected()
        {
            // Arrange
            var container = TestsHelper.CreateContainerFor<CardViewModel>(out var card);
            var inputController = container.Resolve<IInputController>();
            
            // Act
            card.OnMouseClickDown();
            inputController.MouseUp += Raise.Event<Action>();
            
            // Assert 
            Assert.IsFalse(card.IsSelected);
        }
        
        [Test]
        public void OnMouseDown_MousePositionChanged_CardPositionChanged()
        {
            // Arrange
            var container = TestsHelper.CreateContainerFor<CardViewModel>(out var card);
            var inputController = container.Resolve<IInputController>();
            var mousePosition = new Vector2(23f, 33f);
            
            // Act
            card.OnMouseClickDown();
            inputController.MousePositionChanged += Raise.Event<Action<Vector2>>(mousePosition);
            
            // Assert 
            Assert.AreEqual(mousePosition, card.Position);
        }
        
        [Test]
        public void OnMouseDown_MouseUpOverBoard_CardPlayed()
        {
            // Arrange
            var container = TestsHelper.CreateContainerFor<CardViewModel>(out var card);
            var inputController = container.Resolve<IInputController>();
            var isPlayed = false;

            // Act
            card.Played += () => isPlayed = true;
            card.OnMouseClickDown();
            card.OnCardEnterBoard();
            inputController.MouseUp += Raise.Event<Action>();
            
            // Assert 
            Assert.IsTrue(isPlayed);
        }
        
        [Test]
        public void OnMouseDown_MouseUpExitBoard_CardNotPlayed()
        {
            // Arrange
            var container = TestsHelper.CreateContainerFor<CardViewModel>(out var card);
            var inputController = container.Resolve<IInputController>();
            var isPlayed = false;

            // Act
            card.Played += () => isPlayed = true;
            card.OnMouseClickDown();
            card.OnCardEnterBoard();
            card.OnCardExitBoard();
            inputController.MouseUp += Raise.Event<Action>();
            
            // Assert 
            Assert.IsFalse(isPlayed);
        }
        
        [Test]
        public void SetHealth_BelowOne_CardDestroyed(
            [NUnit.Framework.Range(-1, 0)] int health)
        {
            // Arrange
            TestsHelper.CreateContainerFor<CardViewModel>(out var card);
            var isDestroyed = false;

            // Act
            card.Destroyed += () => isDestroyed = true;
            card.Health = health;
            
            // Assert 
            Assert.IsTrue(isDestroyed);
        }
    }
}