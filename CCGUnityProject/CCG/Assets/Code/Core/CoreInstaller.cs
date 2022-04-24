using CCG.Core.CameraService;
using CCG.Core.Input;
using CCG.Core.Models.ImageModel;
using CCG.Services.ImageLoaderService;
using CCG.Views.Card;
using CCG.Views.Hand;
using UnityAuxiliaryTools.Promises.Factory;
using UnityAuxiliaryTools.UnityExecutor;
using UnityEngine;
using Zenject;

namespace CCG.Core
{
    [CreateAssetMenu(fileName = "CoreInstaller", menuName = "Installers/CoreInstaller")]
    public class CoreInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private InputController _inputControllerPrefab;
        
        public override void InstallBindings()
        {
            base.InstallBindings();
            Container.Bind<ICameraService>().To<CameraService.CameraService>().AsSingle();
            Container.Bind<IInputController>().FromComponentInNewPrefab(_inputControllerPrefab).AsSingle();
            Container.Bind<IUnityExecutor>().To<UnityExecutor>().FromNewComponentOnNewGameObject().AsSingle();
            Container.Bind<IPromiseFactory>().To<PromiseFactory>().AsSingle();
            
            Container.Bind<IImageModel>().To<ImageModel>().AsSingle();
            Container.Bind<IImageLoaderService>().To<ImageLoaderService>().AsSingle();

            Container.Bind<ICardFactory>().To<CardFactory>().AsSingle();

            Container.Bind<IHandModel>().To<HandModel>().AsSingle();
        }
    }
}