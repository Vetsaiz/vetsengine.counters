using System.Collections.Generic;
using UniRx;
using UnityEngine;
using VetsEngine.Systems.Counters.Views;

namespace Sample.Notifications.UI
{
    [RequireComponent(typeof(ViewCounter))]
    public class ViewInitNotification : MonoBehaviour
    {
        [SerializeField]
        private ViewCounter _notification;

        [SerializeField]
        private string _format;

        [SerializeField]
        private string _root;

        [SerializeField]
        private SampleNotificationsHolder _service;

        public void Init(IEnumerable<string> ids) {

            foreach (var temp in ids) {
                _root += $".{temp}";
            }
            var id = _service.CreateId(_root);
            _service.GetNotification(id).Subscribe(x => {
                _notification?.SetValue(x);
            }).AddTo(gameObject);
        }
    }
}
