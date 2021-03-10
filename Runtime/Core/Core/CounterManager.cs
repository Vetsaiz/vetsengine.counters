using System.Collections.Generic;
using UniRx;
using VetsEngine.Systems;

namespace VetsEngine.LibCore.Notifications.Core
{
    internal class CounterManager : ICounterManager {

        private readonly CounterSaver _saver;
        private readonly NotificationTree _tree;
        
        public CounterManager(ILocalStorageProvider provider, string idsClear, string idsNew)
        {
            _saver = new CounterSaver(provider, idsClear, idsNew);
            _tree = new NotificationTree(_saver.GetClearIds(true), _saver.GetClearIds(false));
        }

        public IReadOnlyReactiveProperty<int> GetNotification(ICounterId id) {
            return _tree.GetNotificationValue(id);
        }

        public void ClearNotify(ICounterId id)
        {
            IEnumerable<ICounterId> ids;
            _tree.Clear(id, out ids);
            foreach (var clearId in ids) {
                _saver.SetObject(clearId, false);
            }
        }

        public void SetNewObject(ICounterId id) {
            if (_saver.HasNewObject(id) || _saver.HasClearObject(id))
                return;
            _tree.SetObjectId(id, true);
            _saver.SetObject(id, true);
        }

        public void SetClearObject(ICounterId id) {
            if (_saver.HasClearObject(id))
                return;
            _tree.SetObjectId(id, false);
            _saver.SetObject(id, false);
        }

        public void RefreshObject(ICounterId id)
        {
            _saver.RefreshObject(id);
        }

        public void SetCount(ICounterId id, int count)
        {
            _tree.SetValue(id, count);
        }
    }
}

