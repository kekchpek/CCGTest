using CCG.Core.CameraService;
using CCG.Core.Input;
using CCG.Models.ImageModel;
using CCG.MVVM.Board;
using CCG.MVVM.Card;
using CCG.MVVM.Hand;
using CCG.MVVM.HandController;
using CCG.MVVM.StatsChanger;
using CCG.Services.ImageLoaderService;
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
            
            Container.Bind<IBoardViewModel>().To<BoardViewModel>().AsSingle();
            
            Container.Bind<IStatsChanger>().To<StatsChanger>().AsSingle()
                .OnInstantiated<StatsChanger>((_, o) => o.Initialize());
            
            Container.Bind<IHandControllerViewModel>().To<HandControllerViewModel>().AsSingle();
        }
    }
}