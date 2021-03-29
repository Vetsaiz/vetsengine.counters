using UniRx;
using UnityEngine;
using VetsEngine.LibCore.Notifications;

public interface ICounterHandler
{
    IReactiveProperty<int> GetNotification(ICounterId notifyId);
    void ClearNotify(ICounterId notifyId);
    CounterCreator Creator { get; set; }
}

public class SampleNotificationsHolder : MonoBehaviour
{
    public object CreateComplexId(string id)
    {
        throw new System.NotImplementedException();
    }

    public IReactiveProperty<ICounterId> GetNotification(object id)
    {
        throw new System.NotImplementedException();
    }

    public void ClearNotify(object createComplexId)
    {
        throw new System.NotImplementedException();
    }

    public object CreateSubComplexId(string id, string subId)
    {
        throw new System.NotImplementedException();
    }

    public object CreateId(string root)
    {
        throw new System.NotImplementedException();
    }
}
