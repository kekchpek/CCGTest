using UnityEngine;

namespace CCG.Core.Input
{
    public interface ISelectable
    {
        void Select();

        void PositionChanged(Vector2 selectorPosition);
        
        void Release();
    }
}