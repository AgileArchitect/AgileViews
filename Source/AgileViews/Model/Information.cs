using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocCoder.Model
{
    public class Information
    {
        public static string URL = "url";

        private static string[] EMPTY_ARRAY = {};

        public Dictionary<string, List<string>> _store = new Dictionary<string, List<string>>();

        public void Add(string kind, string value)
        {
            if( !_store.ContainsKey(kind))
                _store.Add(kind, new List<string>());

            _store[kind].Add(value);
        }

        public string[] GetInformation(string kind)
        {
            if (!_store.ContainsKey(kind))
                return EMPTY_ARRAY;
            return _store[kind].ToArray();
        }

        public string[] GetInformationKinds()
        {
            return _store.Keys.ToArray();
        }
    }
}
