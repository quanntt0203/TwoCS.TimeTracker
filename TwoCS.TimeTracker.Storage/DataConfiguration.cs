namespace TwoCS.TimeTracker.Data
{
    using System;
    using System.Collections.Generic;
    using TwoCS.TimeTracker.Core;
    using TwoCS.TimeTracker.Domain.Models;

    public class DataConfiguration
    {
        private static readonly Dictionary<Type, string> _collectionMappers;

        static DataConfiguration()
        {
            _collectionMappers = InitialMappers();
        }

        public static string GetCollectionName<T>() where T : ModelBase
        {
            return _collectionMappers[typeof(T)];
        }

        private static Dictionary<Type, string> InitialMappers()
        {
            var mappers = new Dictionary<Type, string>
            {
                { typeof(User), string.Format("{0}s", nameof(User)) },
                { typeof(Project), string.Format("{0}s", nameof(Project)) },
                { typeof(TimeRecord), string.Format("{0}s", nameof(TimeRecord)) }
            };


            return mappers;
        }
    }
}
