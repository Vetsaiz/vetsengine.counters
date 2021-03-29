using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VetsEngine.Systems.Counters.Views;

namespace Sample.Notifications.UI
{
    [RequireComponent(typeof(ViewCounter))]
    public class ViewSubObjectNotification : MonoBehaviour
    {
        [SerializeField]
        private ViewCounter _notification;

        [SerializeField]
        private string _id;

        [SerializeField]
        private string _subId;

        [SerializeField]
        private SampleNotificationsHolder _service;

        [SerializeField]
        private Button _clear;

        public void Start() {

            var id = _service.CreateSubComplexId(_id, _subId);

            _service.GetNotification(id).Subscribe(x => {
                _notification?.SetValue(x);
            }).AddTo(gameObject);
            _clear.onClick.AddListener(OnClear);
        }

        private void OnClear() {
            _service.ClearNotify(_service.CreateSubComplexId(_id, _subId));
        }
    }
}
