using System.Collections.Generic;
using UniRx;

namespace VetsEngine.LibCore.Notifications {

    public interface ICounterManager
    {
        IReadOnlyReactiveProperty<int> GetNotification(ICounterId id);
        void ClearNotify(ICounterId id);

        void SetNewObject(ICounterId id);
        void SetClearObject(ICounterId id);
        void RefreshObject(ICounterId id);

        void SetCount(ICounterId id, int count);
    }

    public interface INotificationBuilder
    {
        INotificationBuilder AddNode(string node);
        ICounterId Build();
    }

    public interface ICounterId
    {
        IList<string> Nodes { get; }
        string StringId { get; }
    }
}
