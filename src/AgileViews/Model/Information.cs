using System;
using System.Collections.Generic;
using System.Linq;

namespace AgileViews.Model
{
    public static class ExtensionsToType
    {
        public static string Kind(this Type type)
        {
            return type.Name.ToLowerInvariant();
        }
    }

    public class Information
    {
        public static string External = "external";
        public static string Url = "url";
        public static string Description = "description";
        public static string Technology = "technology";
        public static string Kind = "kind";

        private static readonly string[] EMPTY_ARRAY = {};

        public Dictionary<string, List<object>> _store = new Dictionary<string, List<object>>();

        public void Add(string kind, object value)
        {
            if (!_store.ContainsKey(kind))
                _store.Add(kind, new List<object>());

            _store[kind].Add(value);
        }

        public object[] GetInformation(string kind)
        {
            if (!_store.ContainsKey(kind))
                return EMPTY_ARRAY;
            return _store[kind].ToArray();
        }

        public IEnumerable<T> GetInformation<T>(string attribute)
        {
            return GetInformation(attribute).OfType<T>();
        }

        public string[] GetInformationKinds()
        {
            return _store.Keys.ToArray();
        }
    }
}