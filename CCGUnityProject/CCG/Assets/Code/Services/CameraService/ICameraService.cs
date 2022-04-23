using UnityEngine;

namespace CCG.Core.CameraService
{
    public interface ICameraService
    {
        /// <summary>
        /// Cant be changed during a game.
        /// </summary>
        /// <returns>Returns main game camera</returns>
        Camera GetMainCamera();
    }
}