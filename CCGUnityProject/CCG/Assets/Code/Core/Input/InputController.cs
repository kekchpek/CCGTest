using System;
using CCG.Core.CameraService;
using UnityEngine;
using Zenject;

namespace CCG.Core.Input
{
    public class InputController : MonoBehaviour, IInputController
    {
        private ICameraService _cameraService;

        private Camera _camera;
        
        private bool _isInitialized;

        private ISelectable _selectedObj;

        [Inject]
        public void Construct(ICameraService cameraService)
        {
            _cameraService = cameraService;
        }

        public void Initialize()
        {
            if (_isInitialized)
                throw new InvalidOperationException("Already initialized");
            _isInitialized = true;
            _camera = _cameraService.GetMainCamera();
        }
        
        public void Update()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                var ray = _camera.ScreenPointToRay(UnityEngine.Input.mousePosition);
                if (Physics.Raycast(ray, out var hit))
                {
                    var hitTrans = hit.transform;
                    SelectObjectIfNeeded(hitTrans);
                    HandleClickDown(hitTrans);
                }
            }
            
            UpdateSelected(UnityEngine.Input.mousePosition);
            
            if (UnityEngine.Input.GetMouseButtonUp(0))
            {
                ReleaseSelected();
            }
        }

        private void HandleClickDown(Transform objectTransform)
        {
            var clickDownHandler = objectTransform.GetComponent<IClickDownHandler>();
            clickDownHandler?.OnClickDown();
        }

        private void SelectObjectIfNeeded(Transform objTransform)
        {
            if (_selectedObj == null)
            {
                var selectable = objTransform.GetComponent<ISelectable>();
                if (selectable != null)
                {
                    selectable.Select();
                    _selectedObj = selectable;
                }
            }
        }

        private void UpdateSelected(Vector2 mousePosition)
        {
            _selectedObj?.PositionChanged(mousePosition);
        }

        private void ReleaseSelected()
        {
            if (_selectedObj != null)
            {
                _selectedObj.Release();
                _selectedObj = null;
            }
        }
    }
}
