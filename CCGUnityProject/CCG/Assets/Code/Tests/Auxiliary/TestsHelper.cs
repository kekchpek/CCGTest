using System;
using System.Linq;
using NSubstitute;
using Zenject;

namespace CCG.Tests.Auxiliary
{
    public static class TestsHelper
    {
        /// <summary>
        /// Creates a container filled with substitutes and one specified real object.
        /// </summary>
        /// <param name="createdObj">Created object</param>
        /// <typeparam name="T">The type of real object that should be created.</typeparam>
        /// <returns>The container filled with substitutes and one specified real object.</returns>
        public static DiContainer CreateContainerFor<T>(out T createdObj)
        {
            var type = typeof(T);
            var container = new DiContainer();
            var constructorInfos = type.GetConstructors();
            if (constructorInfos.Length != 1)
            {
                throw new InvalidOperationException(
                    "Can not determine what constructor should be used for mocks creating");
            }
            foreach (var argType in constructorInfos.First().GetParameters().Select(x => x.ParameterType))
            {
                // IInstantiator should be rebind because it is bind by default
                if (argType == typeof(IInstantiator)) 
                {
                    container.Rebind(argType)
                        .FromInstance(Substitute.For(new[] {argType}, Array.Empty<object>()))
                        .AsSingle();
                }
                else
                {
                    container.Bind(argType)
                        .FromInstance(Substitute.For(new[] {argType}, Array.Empty<object>()))
                        .AsSingle();
                }
            }
            container.Bind<T>().ToSelf().AsSingle();
            createdObj = container.Resolve<T>();
            return container;
        }
    }
}