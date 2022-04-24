using System;
using UnityEngine;

namespace CCG.Core.Input
{
    public interface IInputController
    {
        event Action MouseUp;

        Vector2 MousePosition { get; }

        event Action<Vector2> MousePositionChanged;
    }
}