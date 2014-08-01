using System;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public abstract class State
    {
        public State(StateMachine machine, int id)
        {
            Machine = machine;
           
            Id = id;

            Transitions = new List<StateTransition>();

            this.ExecutionTime = 0f;
        }

        protected virtual void OnEnter() { }

        protected virtual void OnExecute() { }

        protected virtual void OnExit() { }

        public void Enter()
        {
            this.ExecutionTime = 0f;
            this.Completed = false;
            this.OnEnter();
        }

        public void Execute()
        {
            PerformTransitions();
            OnExecute();
            this.ExecutionTime += Time.deltaTime;
        }

        public void Exit()
        {
            this.Completed = true;
            this.OnExit();
        }

        protected bool PerformTransitions()
        {
            foreach (var tr in Transitions)
            {
                if (!tr.Check())
                    continue;
                this.Machine.SetState(tr.To.Id);
                return true;
            }
            return false;
        }

        #region Properties

        public int Id { get; private set; }

        public float ExecutionTime { get; private set; }

        public StateMachine Machine { get; private set; }

        public ICollection<StateTransition> Transitions { get; private set; }

        public bool Completed { get; protected set; }

        #endregion Properties
    }

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