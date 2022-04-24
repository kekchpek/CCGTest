using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CCG.Core.MVVM
{
    public class ViewBehaviour<T> : MonoBehaviour, IViewInitializer<T> where T : IViewModel
    {

        // ReSharper disable once StaticMemberInGenericType
        // This field will be created for every generic class.
        private static readonly HashSet<string> AvailablePropertyNames;

        static ViewBehaviour()
        {
            var viewModelType = typeof(T);
            AvailablePropertyNames = new HashSet<string>(
                viewModelType.GetProperties()
                    .Where(x => x.CanRead)
                    .Select(x => x.Name));
        }
        
        protected T ViewModel { get; private set; }

        protected void SubscribeForPropertyChange<TVal>(string propertyName, Action<TVal> changedCallback)
        {
            if (AvailablePropertyNames.Contains(propertyName))
                ViewModel.SubscribeForProperty(propertyName, changedCallback);
            else
                throw new InvalidOperationException($"View model {typeof(T).Name} has no property: {propertyName}");
        }


        void IViewInitializer<T>.SetViewModel(T viewModel)
        {
            ViewModel?.ClearSubscriptions();
            ViewModel = viewModel;
            OnViewModelSet();
        }

        protected virtual void OnViewModelSet()
        {
            
        }
    }
}