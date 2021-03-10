using System;
using System.Collections.Generic;
using UniRx;

namespace VetsEngine.LibCore.Notifications.Core
{
    internal class NotificationTree
    {
        private NotificationBaseNode Root { get; }

        public NotificationTree(IEnumerable<ICounterId> newIds, IEnumerable<ICounterId> clearIds) {
            Root = new NotificationBaseNode("root", null);
            foreach (var id in newIds) {
                SetObjectId(id, true);
            }
            foreach (var id in clearIds)
            {
                SetObjectId(id, true);
                SetObjectId(id, false);
            }
        }

        public void SetValue(ICounterId id, int value) {
            var node = TryCreateNotificationNode(id);
            if (!node.IsFinished) {
                throw new Exception("Attempting to set a value for a summing counter");
            }
            var change = value - node.Count.Value;

            if (change == 0) {
                return;
            }
            SetTreeValue(id, change);
        }

        public void SetObjectId(ICounterId id, bool isNew) {
            var node = TryCreateNotificationNode(id);
            if (!node.IsFinished) {
                throw new Exception("Attempting to set a value for a summing counter");
            }
            node.SetObject();
            SetTreeValue(id, isNew ? 1 : -1);
        }

        public IReadOnlyReactiveProperty<int> GetNotificationValue(ICounterId id)
        {
            var node = TryCreateNotificationNode(id);
            return node.Count;
        }

        public void Clear(ICounterId id, out IEnumerable<ICounterId> clearObjects) {

            var list = new List<ICounterId>();
            var listClear = new List<NotificationBaseNode>();
            var node = TryCreateNotificationNode(id);
            if (node.IsObject) {
                list.Add(id);
            }
            if (node.IsFinished)
            {
                listClear.Add(node);
            }
            RecurciveFindFinished(node, listClear);
            foreach (var clearNode in listClear) {
                if (clearNode.IsObject) {
                    list.Add(clearNode.NotifyId);
                }
                SetTreeValue(clearNode.NotifyId, -clearNode.Count.Value);
            }
            clearObjects = list;
        }

        private void RecurciveFindFinished(NotificationBaseNode node, List<NotificationBaseNode> findChilds) {
            foreach (var child in node.Childs)
            {
                if (child.Value.IsFinished) {
                    findChilds.Add(child.Value);
                } else {
                    RecurciveFindFinished(child.Value, findChilds);
                }
            }
        }

        private void SetTreeValue(ICounterId id, int value)
        {
            var parent = Root;
            foreach (var nodeId in id.Nodes)
            {
                NotificationBaseNode child;
                if (!parent.Childs.TryGetValue(nodeId, out child))
                {
                    throw new Exception($"Unknow id = {id.StringId}");
                }
                child.ChangeValue(value);
                parent = child;
            }
        }

        public NotificationBaseNode TryCreateNotificationNode(ICounterId id)
        {
            var parent = Root;
            foreach (var nodeId in id.Nodes)
            {
                NotificationBaseNode child;
                if (!parent.Childs.TryGetValue(nodeId, out child))
                {
                    child = new NotificationBaseNode(nodeId, parent);
                    parent.Childs.Add(nodeId, child);
                }
                parent = child;
            }
            return parent;
        }
    }
}
