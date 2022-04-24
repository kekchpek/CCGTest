using System;
using System.Collections.Generic;
using System.Linq;
using CCG.Core.Models.ImageModel;
using CCG.Services.ImageLoaderService;
using CCG.Tests.Auxiliary;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;
using UnityAuxiliaryTools.Promises;
using UnityAuxiliaryTools.Promises.Factory;
using UnityEngine;

namespace CCG.Tests.Models
{
    public class ImageModelTests
    {

        [Test]
        public void Initialization_NoErrors()
        {
            // Arrange
            TestsHelper.CreateContainerFor<ImageModel>(out var imageModel);
            
            // Act & Assert
            Assert.DoesNotThrow(() => imageModel.Initialize());
        }
        
        [Test]
        public void Initialization_ImagesLoadStarts()
        {
            // Arrange
            var container = TestsHelper.CreateContainerFor<ImageModel>(out var imageModel);
            var imageLoaderService = container.Resolve<IImageLoaderService>();
            
            // Act
            imageModel.Initialize();
            
            // Assert
            imageLoaderService.Received(Quantity.Exactly(Config.Config.ImageInitBuffer)).LoadRandomImage();
        }
        
        [Test]
        public void Initialization_InitPromiseTookFromFactory()
        {
            // Arrange
            var container = TestsHelper.CreateContainerFor<ImageModel>(out var imageModel);
            var promisesFactory = container.Resolve<IPromiseFactory>();
            var initPromiseSub = Substitute.For<IControllablePromise>();
            promisesFactory.CreatePromise().Returns(initPromiseSub);
            
            // Act
            var initPromise = imageModel.Initialize();
            
            // Assert
            Assert.AreEqual(initPromise, initPromiseSub);
        }
        
        [Test]
        public void Initialization_ImagesLoaded_PromiseReleased()
        {
            // Arrange
            var container = TestsHelper.CreateContainerFor<ImageModel>(out var imageModel);
            var imageLoaderService = container.Resolve<IImageLoaderService>();
            var promisesFactory = container.Resolve<IPromiseFactory>();
            
            var initPromiseSub = Substitute.For<IControllablePromise>();
            promisesFactory.CreatePromise().Returns(initPromiseSub);
            
            var loadPromises = new PromiseStub<Texture2D>[Config.Config.ImageInitBuffer];
            for (var i = 0; i < Config.Config.ImageInitBuffer; i++)
            {
                loadPromises[i] = new PromiseStub<Texture2D>();
            }
            var loadPromisesStack = new Stack<IPromise<Texture2D>>(loadPromises);
            imageLoaderService.LoadRandomImage().Returns(x => loadPromisesStack.Pop());
            
            // Act
            imageModel.Initialize();
            foreach (var loadPromise in loadPromises)
            {
                loadPromise.ExecuteSuccessCallbacks(Texture2D.blackTexture);
            }
            
            // Assert
            initPromiseSub.Received().Success();
        }
        
        [Test]
        public void Initialization_NotAllImagesLoaded_PromiseNotReleased()
        {
            // Arrange
            var container = TestsHelper.CreateContainerFor<ImageModel>(out var imageModel);
            var imageLoaderService = container.Resolve<IImageLoaderService>();
            var promisesFactory = container.Resolve<IPromiseFactory>();
            
            var initPromiseSub = Substitute.For<IControllablePromise>();
            promisesFactory.CreatePromise().Returns(initPromiseSub);
            
            var loadPromises = new PromiseStub<Texture2D>[Config.Config.ImageInitBuffer];
            for (var i = 0; i < Config.Config.ImageInitBuffer; i++)
            {
                loadPromises[i] = new PromiseStub<Texture2D>();
            }
            var loadPromisesStack = new Stack<IPromise<Texture2D>>(loadPromises);
            imageLoaderService.LoadRandomImage().Returns(x => loadPromisesStack.Pop());
            
            // Act
            imageModel.Initialize();
            loadPromises[0].ExecuteSuccessCallbacks(Texture2D.blackTexture);
            loadPromises[loadPromises.Length - 1].ExecuteSuccessCallbacks(Texture2D.blackTexture);
            
            // Assert
            initPromiseSub.DidNotReceive().Success();
        }
        
        
        [Test]
        public void Initialization_OneLoadingFailed_PromiseFlailed()
        {
            // Arrange
            var container = TestsHelper.CreateContainerFor<ImageModel>(out var imageModel);
            var imageLoaderService = container.Resolve<IImageLoaderService>();
            var promisesFactory = container.Resolve<IPromiseFactory>();
            
            var initPromiseSub = Substitute.For<IControllablePromise>();
            promisesFactory.CreatePromise().Returns(initPromiseSub);
            
            var loadPromises = new PromiseStub<Texture2D>[Config.Config.ImageInitBuffer];
            for (var i = 0; i < Config.Config.ImageInitBuffer; i++)
            {
                loadPromises[i] = new PromiseStub<Texture2D>();
            }
            var loadPromisesStack = new Stack<IPromise<Texture2D>>(loadPromises);
            imageLoaderService.LoadRandomImage().Returns(x => loadPromisesStack.Pop());
            
            // Act
            imageModel.Initialize();
            loadPromises[0].ExecuteFailCallbacks(new Exception("Test exception"));
            
            // Assert
            initPromiseSub.ReceivedWithAnyArgs().Fail(default);
        }
        
        [Test]
        public void GetAllIds_Initialized_IdsUnique()
        {
            // Arrange
            var container = TestsHelper.CreateContainerFor<ImageModel>(out var imageModel);
            var imageLoaderService = container.Resolve<IImageLoaderService>();
            var promisesFactory = container.Resolve<IPromiseFactory>();
            
            var initPromiseSub = Substitute.For<IControllablePromise>();
            promisesFactory.CreatePromise().Returns(initPromiseSub);
            
            var loadPromises = new PromiseStub<Texture2D>[Config.Config.ImageInitBuffer];
            for (var i = 0; i < Config.Config.ImageInitBuffer; i++)
            {
                loadPromises[i] = new PromiseStub<Texture2D>();
            }
            var loadPromisesStack = new Stack<IPromise<Texture2D>>(loadPromises);
            imageLoaderService.LoadRandomImage().Returns(x => loadPromisesStack.Pop());
            
            // Act
            imageModel.Initialize();
            foreach (var loadPromise in loadPromises)
            {
                loadPromise.ExecuteSuccessCallbacks(Texture2D.blackTexture);
            }
            var ids = imageModel.GetAllImageIds();
            
            // Assert
            Assert.IsTrue(ids.Length == ids.Distinct().Count());
        }
        
        [Test]
        public void GetAllIds_Initialized_IdsCountEqualsImagesCount()
        {
            // Arrange
            var container = TestsHelper.CreateContainerFor<ImageModel>(out var imageModel);
            var imageLoaderService = container.Resolve<IImageLoaderService>();
            var promisesFactory = container.Resolve<IPromiseFactory>();
            
            var initPromiseSub = Substitute.For<IControllablePromise>();
            promisesFactory.CreatePromise().Returns(initPromiseSub);
            
            var loadPromises = new PromiseStub<Texture2D>[Config.Config.ImageInitBuffer];
            for (var i = 0; i < Config.Config.ImageInitBuffer; i++)
            {
                loadPromises[i] = new PromiseStub<Texture2D>();
            }
            var loadPromisesStack = new Stack<IPromise<Texture2D>>(loadPromises);
            imageLoaderService.LoadRandomImage().Returns(x => loadPromisesStack.Pop());
            
            // Act
            imageModel.Initialize();
            foreach (var loadPromise in loadPromises)
            {
                loadPromise.ExecuteSuccessCallbacks(Texture2D.blackTexture);
            }
            var ids = imageModel.GetAllImageIds();
            
            // Assert
            Assert.AreEqual(Config.Config.ImageInitBuffer, ids.Length);
        }
        
        
        [Test]
        public void GetImage_Initialized_AllIdsHasImage()
        {
            // Arrange
            var container = TestsHelper.CreateContainerFor<ImageModel>(out var imageModel);
            var imageLoaderService = container.Resolve<IImageLoaderService>();
            var promisesFactory = container.Resolve<IPromiseFactory>();
            
            var initPromiseSub = Substitute.For<IControllablePromise>();
            promisesFactory.CreatePromise().Returns(initPromiseSub);
            
            var loadPromises = new PromiseStub<Texture2D>[Config.Config.ImageInitBuffer];
            for (var i = 0; i < Config.Config.ImageInitBuffer; i++)
            {
                loadPromises[i] = new PromiseStub<Texture2D>();
            }
            var loadPromisesStack = new Stack<IPromise<Texture2D>>(loadPromises);
            imageLoaderService.LoadRandomImage().Returns(x => loadPromisesStack.Pop());
            
            // Act
            imageModel.Initialize();
            foreach (var loadPromise in loadPromises)
            {
                loadPromise.ExecuteSuccessCallbacks(Texture2D.blackTexture);
            }
            var ids = imageModel.GetAllImageIds();
            var images = ids.Select(x => imageModel.GetImage(x));

            // Assert
            foreach (var image in images)
            {
                Assert.AreEqual(Texture2D.blackTexture, image);
            }
        }
    }
}