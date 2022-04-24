using System;
using UnityEngine;

namespace CCG.Core.Input
{
    public class InputController : MonoBehaviour, IInputController
    {
        public event Action MouseUp;
        public event Action<Vector2> MousePositionChanged;

        private Camera _camera;
        private Resolution _screenResolution;
        private float _screenScale;
        private Vector3 _bottomLeftScreenPoint;

        
        
        public Vector2 MousePosition => UnityEngine.Input.mousePosition * _screenScale + _bottomLeftScreenPoint;

        public void SetCamera(Camera mainCamera)
        {
            _camera = mainCamera;
        }

        public Vector3 ScreenPointToWorld(Vector3 screenPoint)
        {
            return _camera.ScreenToWorldPoint(screenPoint);
        }

        public void Update()
        {
            if (_screenResolution.width != Screen.currentResolution.width || _screenResolution.height != Screen.currentResolution.height) {
                
                _screenResolution = Screen.currentResolution;
                _bottomLeftScreenPoint = _camera.ScreenToWorldPoint(Vector3.zero);
                var topRightScreenPoint = _camera.ScreenToWorldPoint(new Vector3(_screenResolution.width, _screenResolution.height, 0f));
                _screenScale = (topRightScreenPoint.x - _bottomLeftScreenPoint.x) / _screenResolution.width;
            }
            
            if (UnityEngine.Input.GetMouseButtonUp(0))
            {
                MouseUp?.Invoke();
            }
            
            MousePositionChanged?.Invoke(UnityEngine.Input.mousePosition * _screenScale + _bottomLeftScreenPoint);
        }
    }
}
