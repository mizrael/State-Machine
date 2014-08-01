using System;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class StateMachine
    {
        private List<State> _states;
        private State _currentState;

        public StateMachine(GameObject owner)
        {
            Owner = owner;

            _states = new List<State>();
        }

        public void Execute()
        {
            if (null == _currentState)
                return;
            _currentState.Execute();
        }

        public TState AddState<TState>() where TState : State
        {
            var index = _states.Count;

            var newState = (TState)Activator.CreateInstance(typeof(TState), new object[] { this, index });

            _states.Add(newState);

            return newState;
        }

        public void SetState(int id)
        {
            if (null != _currentState)
                _currentState.Exit();

            if (id < 0 || id > _states.Count)
                return;

            _currentState = _states[id];

            if (null != _currentState)
                _currentState.Enter();
        }

        public GameObject Owner { get; private set; }
    }

}
