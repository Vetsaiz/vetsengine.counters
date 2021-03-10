using System;
using System.Collections.Generic;
using System.Linq;

namespace VetsEngine.LibCore.Notifications.Core {

    [Serializable]
    internal class CounterId : ICounterId{

        private readonly List<string> _nodes;

        public string StringId { get; }

        public IList<string> Nodes => _nodes;

        public CounterId(IEnumerable<string> nodes) {
            _nodes = new List<string>(nodes);
            StringId = string.Join(".", _nodes.ToArray());
        }

        public CounterId(string id)
        {
            _nodes = id.Split('.').ToList();
            StringId = id;
        }
    }
}
