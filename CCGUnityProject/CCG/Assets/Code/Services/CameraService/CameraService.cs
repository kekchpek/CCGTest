using UnityEngine;

namespace CCG.Core.CameraService
{
    public class CameraService : MonoBehaviour, ICameraService
    {

        [SerializeField]
        private Camera _mainCamera;
        
        public Camera GetMainCamera()
        {
            // Add change event if camera will become changeable.
            return _mainCamera;
        }
    }
}