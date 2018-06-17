namespace TwoCS.TimeTracker.Core.Factories
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;

    public static class ResolverFactory
    {
        private static IServiceProvider _service{ set; get; }

        public static void SetProvider(IServiceCollection service)
        {
            _service = service.BuildServiceProvider();
        }

        public static T GetService<T>()
           where T : class
        {
            return (T)_service.GetService(typeof(T));
        }

        public static T GetService<T>(Type type)
           where T : class
        {
            return (T)_service.GetService(type);
        }

        public static T CreateInstance<T>(string typeName)
            where T : class
        {

            Type type = Type.GetType(typeName);

            T instance = (T)Activator.CreateInstance(type);

            return instance;
        }

        public static T CreateInstance<T>(string typeName, params object[] args)
            where T : class
        {

            Type type = Type.GetType(typeName);

            T instance = (T)Activator.CreateInstance(type, args);

            return instance;
        }

        public static object CreateInstance(Type type, params object[] args)
        {
            return Activator.CreateInstance(type, args);
        }
    }
}
