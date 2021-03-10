using System.Collections.Generic;
using System.Linq;
using VetsEngine.Systems;

namespace VetsEngine.LibCore.Notifications.Core {
    internal class CounterSaver {

        private readonly ILocalStorageProvider _storageProvider;

        private readonly HashSet<string> _notificationsNew;
        private readonly HashSet<string> _notificationsClear;
        private readonly string _idsClear;
        private readonly string _idsNew;

        public CounterSaver(ILocalStorageProvider storageProvider, string idsClear, string idsNew)
        {
            _storageProvider = storageProvider;
            _idsClear = idsClear;
            _idsNew = idsNew;
            _notificationsNew = new HashSet<string>(_storageProvider.LoadData<string[]>(_idsNew) ?? new string[]{});
            _notificationsClear = new HashSet<string>(_storageProvider.LoadData<string[]>(_idsClear) ?? new string[] { });
        }

        public void SetObject(ICounterId id, bool isNew) {
            if (isNew) {
                _notificationsNew.Add(id.StringId);
            } else {
                _notificationsNew.Remove(id.StringId);
                _notificationsClear.Add(id.StringId);
            }
            Save();
        }

        public void RefreshObject(ICounterId id)
        {
            _notificationsClear.Remove(id.StringId);
        }

        public bool HasNewObject(ICounterId id) {
            return _notificationsNew.Contains(id.StringId);
        }

        public bool HasClearObject(ICounterId id) {
            return _notificationsClear.Contains(id.StringId);
        }

        private void Save() {
            _storageProvider.SaveData(_notificationsNew.ToArray(), _idsNew);
            _storageProvider.SaveData(_notificationsClear.ToArray(), _idsClear);
        }

        public IEnumerable<ICounterId> GetClearIds(bool isNew) {
            if (isNew) {
                return _notificationsNew.Select(x => new CounterId(x));
            } else {
                return _notificationsClear.Select(x => new CounterId(x));
            }
        }
    }
}
