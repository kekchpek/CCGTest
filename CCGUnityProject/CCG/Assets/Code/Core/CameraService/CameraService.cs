using UnityEngine;

namespace CCG.Core.CameraService
{
    public class CameraService : ICameraService
    {

        public Camera GetMainCamera()
        {
            return GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        }
    }
}