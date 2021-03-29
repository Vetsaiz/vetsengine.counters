using UniRx;
using UnityEngine;
using VetsEngine.Systems.Counters.Views;

namespace Sample.Notifications.UI
{
    [RequireComponent(typeof(ViewCounter))]
    public class ViewNotification : MonoBehaviour
    {
        [SerializeField]
        private ViewCounter _notification = null;

        [SerializeField]
        private string _format = null;

        [SerializeField]
        private string _id;

        [SerializeField]
        private SampleNotificationsHolder _service = null;

        public void Start() {

            var id = NotificationCreator.CreateId(_id);
            _service.GetNotification(id).Subscribe(x => {
                _notification?.SetValue(x);
            }).AddTo(gameObject);
        }
    }
}
