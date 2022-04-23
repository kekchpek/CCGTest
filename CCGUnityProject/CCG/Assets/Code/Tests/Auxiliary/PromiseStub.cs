using System;
using UnityAuxiliaryTools.Promises;

namespace CCG.Tests.Auxiliary
{
    public class PromiseStub : IPromise
    {

        private Action<Exception> _onFail = e => {};
        private Action _onSuccess = () => {};
        private Action _onFinally = () => {};

        public IBasePromise OnFail(Action<Exception> callback)
        {
            _onFail += callback;
            return this;
        }

        public IBasePromise Finally(Action callback)
        {
            _onFinally += callback;
            return this;
        }

        public IPromise OnSuccess(Action callback)
        {
            _onSuccess += callback;
            return this;
        }

        public void ExecuteSuccessCallbacks()
        {
            _onSuccess?.Invoke();
        }

        public void ExecuteFailCallbacks(Exception e)
        {
            _onFail?.Invoke(e);
        }

        public void ExecuteFinallyCallbacks()
        {
            _onFinally?.Invoke();
        }
    }
    
    public class PromiseStub<T> : IPromise<T>
    {

        private Action<Exception> _onFail = e => {};
        private Action<T> _onSuccess = o => {};
        private Action _onFinally = () => {};

        public IBasePromise OnFail(Action<Exception> callback)
        {
            _onFail += callback;
            return this;
        }

        public IBasePromise Finally(Action callback)
        {
            _onFinally += callback;
            return this;
        }

        public IPromise<T> OnSuccess(Action<T> callback)
        {
            _onSuccess += callback;
            return this;
        }

        public void ExecuteSuccessCallbacks(T result)
        {
            _onSuccess?.Invoke(result);
        }

        public void ExecuteFailCallbacks(Exception e)
        {
            _onFail?.Invoke(e);
        }

        public void ExecuteFinallyCallbacks()
        {
            _onFinally?.Invoke();
        }
    }
}