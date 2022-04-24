using System;
using UnityEngine;

namespace CCG.Core.Input
{
    public interface IInputController
    {
        event Action MouseUp;
        event Action<Vector2> MousePositionChanged;
        
        Vector2 MousePosition { get; }
        float ScreenWidth { get; }
        void SetCamera(Camera mainCamera);
        Vector3 ScreenPointToWorld(Vector3 screenPoint);
    }
}