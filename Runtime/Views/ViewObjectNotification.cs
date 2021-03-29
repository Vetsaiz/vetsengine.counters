using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VetsEngine.Systems.Counters.Views;

namespace Sample.Notifications.UI
{
    [RequireComponent(typeof(ViewCounter))]
    public class ViewObjectNotification : MonoBehaviour
    {
        [SerializeField]
        private ViewCounter _notification;

        [SerializeField]
        private string _id;
        
        [SerializeField]
        private SampleNotificationsHolder _service;

        [SerializeField]
        private Button _clear;

        public void Start() {

            var id = _service.CreateComplexId(_id);
            _service.GetNotification(id).Subscribe(x => {
                _notification?.SetValue(x);
            }).AddTo(gameObject);
            _clear.onClick.AddListener(OnClear);
        }

        private void OnClear() {
            _service.ClearNotify(_service.CreateComplexId(_id));
        }
    }
}
