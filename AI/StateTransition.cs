using System;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{ 
    public class StateTransition
    {
        private Func<bool> _condition;

        public StateTransition(State to, Func<bool> condition)
        {
            To = to;
            _condition = condition;
        }

        public bool Check()
        {
            return (_condition());
        }

        public State To { get; private set; }
    }
}