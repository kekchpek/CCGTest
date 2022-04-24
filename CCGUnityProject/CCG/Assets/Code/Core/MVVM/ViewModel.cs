using System;
using System.Collections.Generic;
using UnityEngine;

namespace CCG.Core.MVVM
{
    public abstract class ViewModel : IViewModel
    {
        private readonly Dictionary<string, Action<object>> _changeCallbacks = new Dictionary<string, Action<object>>();

        void IViewModel.SubscribeForProperty<T>(string propertyName, Action<T> changeCallback)
        {
            if (_changeCallbacks.ContainsKey(propertyName))
            {
                _changeCallbacks[propertyName] += o => CallChangeCallback(changeCallback, o);
            }
            else
            {
                _changeCallbacks.Add(propertyName, o => CallChangeCallback(changeCallback, o));
            }
        }

        protected void SetAndRaiseIfChanged<TVal>(string propertyName, TVal newValue, ref TVal propertyValue)
        {
            if (newValue.Equals(propertyValue)) 
                return;
            propertyValue = newValue;
            if (_changeCallbacks.TryGetValue(propertyName, out var changeCallback))
                changeCallback?.Invoke(newValue);
        }

        private void CallChangeCallback<T>(Action<T> changeCallback, object arg)
        {
            if (arg is T castedValue)
            {
                changeCallback?.Invoke(castedValue);
            }
            else
            {
                Debug.LogError("Can not call change callback due invalid callback argument type" +
                               $"\nProperty value is {arg.GetType().Name}" +
                               $"\nCallback argument type is {typeof(T).Name}");
            }
        }

        public void ClearSubscriptions()
        {
            _changeCallbacks.Clear();
        }
    }
}