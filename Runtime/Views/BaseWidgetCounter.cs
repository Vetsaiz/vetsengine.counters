using System.Collections.Generic;
using UniRx;
using UnityEngine;
using VetsEngine.LibCore.Notifications;

namespace VetsEngine.Systems.Counters.Views
{
    [RequireComponent(typeof(ViewCounter))]
    public class BaseWidgetCounter : MonoBehaviour, IInjecteble
    {
        [SerializeField]
        ViewCounter _notification;

        //[SerializeField]
        public List<string> _root { get; }

        [Inject]
        ICounterHandler _service;

        ICounterId _notifyId;

        public void Start() {

            var builder = _service.Creator.Builder;
            _root.ForEach(x => builder.AddNode(x.ToString()));
            _notifyId = builder.Build();
            _service.GetNotification(_notifyId).Subscribe(x => {
                _notification?.SetFormatValue(x);
            }).AddTo(gameObject);
        }

        public void Clear() {
            _service.ClearNotify(_notifyId);
        }
    }
}
