using System;
using CCG.Core.MVVM;
using CCG.MVVM.Card;
using CCG.MVVM.Hand;
using CCG.MVVM.StatsChanger;
using CCG.Tests.Auxiliary;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace CCG.Tests.Models
{
    public class StatsChangerViewModelTests
    {
        [Test]
        public void ChangeStat_NoCards_NoErrors()
        {
            // Arrange
            var container = TestsHelper.CreateContainerFor<StatsChangerViewModel>(out var statsChanger);
            var handModel = container.Resolve<IHandModel>();
            handModel.GetCards().Returns(Array.Empty<ICardViewModel>());

            // Act
            statsChanger.Initialize();

            // Assert
            Assert.DoesNotThrow(() => 
                statsChanger.ChangeCardStat());
        }
        
        [Test]
        public void ChangeStat_ThreeCards_ChangesCardsOneByOne(
            [NUnit.Framework.Range(0, 7)] int changesCount)
        {
            // Arrange
            var container = TestsHelper.CreateContainerFor<StatsChangerViewModel>(out var statsChanger);
            var handModel = container.Resolve<IHandModel>();
            var cards = new ICardViewModel[]
            {
                new CardStub(),
                new CardStub(),
                new CardStub(),
            };
            handModel.GetCards().Returns(cards);

            // Act
            statsChanger.Initialize();
            for (var i = 0; i < changesCount; i++)
            {
                statsChanger.ChangeCardStat();
            }
            
            // Assert
            Assert.AreEqual((changesCount + 2) / 3, ((CardStub)cards[0]).ChangedCounter);
            Assert.AreEqual((changesCount + 1) / 3, ((CardStub)cards[1]).ChangedCounter);
            Assert.AreEqual(changesCount / 3, ((CardStub)cards[2]).ChangedCounter);
        }
        
        // ReSharper disable ValueParameterNotUsed
        private class CardStub : ICardViewModel
        {
            public int ChangedCounter { get; private set; } = 0;

            public Vector2 Position { get; } = Vector2.down;
            public float Rotation => 0f;

            public int Health
            {
                get => 0;
                set
                {
                    ChangedCounter++;
                }
            }
            public int Attack
            {
                get => 0;
                set
                {
                    ChangedCounter++;
                }
            }
            public int Mana
            {
                get => 0;
                set
                {
                    ChangedCounter++;
                }
            }
            public string Description { get; set; }
            public string Title { get; set; }
            public Texture2D Icon { get; set; }
            public bool IsSelected => false;
            public bool IsOverBoard => false;
            public event Action Played;
            public event Action Destroyed;
            public void OnMouseClickDown()
            {
                throw new NotImplementedException();
            }

            public void OnCardEnterBoard()
            {
                throw new NotImplementedException();
            }

            public void OnCardExitBoard()
            {
                throw new NotImplementedException();
            }

            public void Destroy()
            {
                throw new NotImplementedException();
            }

            public void SetPositionAndRotationInHand(Vector2 position, float rotation)
            {
                throw new NotImplementedException();
            }
            void IViewModel.SubscribeForProperty<T>(string propertyName, Action<T> changeCallback)
            {
                throw new NotImplementedException();
            }

            public void ClearSubscriptions()
            {
                throw new NotImplementedException();
            }
        }
        // ReSharper restore ValueParameterNotUsed
    }
}