using System;
using CCG.Core.CameraService;
using UnityEngine;
using Zenject;

namespace CCG.Core.Input
{
    public class InputController : MonoBehaviour, IInputController
    {
        private bool _isInitialized;

        public event Action MouseUp;
        public event Action<Vector2> MousePositionChanged;

        public Vector2 MousePosition => UnityEngine.Input.mousePosition;

        public void Update()
        {
            if (UnityEngine.Input.GetMouseButtonUp(0))
            {
                MouseUp?.Invoke();
            }
            
            MousePositionChanged?.Invoke(UnityEngine.Input.mousePosition);
        }
    }
}
