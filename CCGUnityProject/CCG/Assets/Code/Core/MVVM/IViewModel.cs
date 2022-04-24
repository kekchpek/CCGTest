using System;

namespace CCG.Core.MVVM
{
    public interface IViewModel
    {
        internal void SubscribeForProperty<T>(string propertyName, Action<T> changeCallback);
        void ClearSubscriptions();
    }
}