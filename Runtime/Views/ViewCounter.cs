using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VetsEngine.LibCore.Notifications;

//using VetsEngine.Systems.Transitions.States;

namespace VetsEngine.Systems.Counters.Views
{
    public class ViewCounter : MonoBehaviour
    {
        [SerializeField]
        protected int _minValue = 0;

        [SerializeField]
        protected Text _counterText = null;
        
        [SerializeField]
        string _format = "{0}";

        //[SerializeField]
        //StateTransitionContainer _state;

        [SerializeField]
        List<State> _states;

        protected int _value;
        readonly Dictionary<StateCounter, string> _statesCounter = new Dictionary<StateCounter, string>();

        void Awake()
        {
            foreach (var temp in _states)
            {
                _statesCounter[temp.CounterState] = temp.TransactionState;
            }
        }

        public virtual void SetFormatValue(int value)
        {
            _value = value;
            //if (_state == null)
            //{
            //    gameObject.SetActive(_value > _minValue);
            //}
            //else
            //{
            //    _state.SetState(_value > _minValue ? _statesCounter[StateCounter.MoreMinValue] : _statesCounter[StateCounter.LessMinValue]);
            //}

            if (_counterText != null)
            {
                _counterText.text = _format == null ? value.ToString() : string.Format(_format, value);
            }
        }

        public enum StateCounter
        {
            MoreMinValue,
            LessMinValue,
        }

        [Serializable]
        public class State
        {
            public StateCounter CounterState;

            public string TransactionState;
        }

        public void SetValue(ICounterId counterId)
        {
            throw new NotImplementedException();
        }
    }
}
