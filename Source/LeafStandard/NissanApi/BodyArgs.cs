using System.Collections.Generic;

namespace LeafStandard.NissanApi
{
    public class BodyArgs
    {
        private List<KeyValuePair<string, string>> _args;
        public IEnumerable<KeyValuePair<string, string>> Args => _args;

        public BodyArgs()
        {
            _args = new List<KeyValuePair<string, string>>();
        }

        public void Add(string key, string value = null)
        {
            var kvp = new KeyValuePair<string, string>(key, value ?? string.Empty);
            _args.Add(kvp);
        }
    }
}
