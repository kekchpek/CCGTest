using CCG.Core.CameraService;
using CCG.Core.Input;
using UnityAuxiliaryTools.Promises.Factory;
using UnityAuxiliaryTools.UnityExecutor;
using UnityEngine;
using Zenject;

namespace CCG.Core
{
    [CreateAssetMenu(fileName = "CoreInstaller", menuName = "Installers/CoreInstaller")]
    public class CoreInstaller : ScriptableObjectInstaller
    {

        [SerializeField] private CameraService.CameraService _cameraServicePrefab;
        [SerializeField] private InputController _inputControllerPrefab;
        
        public override void InstallBindings()
        {
            base.InstallBindings();
            Container.Bind<ICameraService>().FromComponentInNewPrefab(_cameraServicePrefab).AsSingle();
            Container.Bind<IInputController>().FromComponentInNewPrefab(_inputControllerPrefab).AsSingle();
            Container.Bind<IUnityExecutor>().To<UnityExecutor>().FromNewComponentOnNewGameObject().AsSingle();
            Container.Bind<IPromiseFactory>().To<PromiseFactory>().AsSingle();
        }
    }
}