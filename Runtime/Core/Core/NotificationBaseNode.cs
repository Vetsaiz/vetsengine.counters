using System;
using System.Collections.Generic;
using UniRx;

namespace VetsEngine.LibCore.Notifications.Core
{
    internal class NotificationBaseNode {

        public readonly string Id;

        public readonly CounterId NotifyId;

        public IReactiveProperty<int> Count { get; } = new ReactiveProperty<int>();

        public NotificationBaseNode Parent { get; }

        public bool IsFinished => Childs.Count == 0;

        public bool IsObject;

        public  Dictionary<string, NotificationBaseNode> Childs { get; }

        public NotificationBaseNode(string id, NotificationBaseNode parent) {
            Id = id;
            var list = new List<string>();
            if (parent != null) {
                list.Add(id);
            }
            Parent = parent;
            while (parent != null) { 
                if (parent.Parent != null) {
                    list.Insert(0, parent.Id);
                }
                parent = parent.Parent;
            }
            NotifyId = new CounterId(list);
            Childs = new Dictionary<string, NotificationBaseNode>();
        }

        public void SetObject() {
            IsObject = true;
        }

        public void AddChild(string id, NotificationBaseNode child) {
            if (Childs.ContainsKey(id)){
                throw new Exception("");
            }
            Childs[id] = child;
        }

        public void ChangeValue(int changeValue) {
            Count.Value = Count.Value + changeValue;
        }
    }
}
