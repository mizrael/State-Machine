using System;
using UnityEngine;

namespace AI.States
{
    public class PlayerDefaultState : State
    {
        private Animator _animator;

        public PlayerDefaultState(StateMachine machine, int id)
            : base(machine, id)
        {
            _animator = Machine.Owner.GetComponent<Animator>();
        }

        protected override void OnExecute()
        {
            var transform = Machine.Owner.transform;

            var horizontal = Input.GetAxis("Horizontal");
            var movement = transform.right * horizontal * WalkingSpeed;
            transform.Translate(movement * Time.deltaTime);

            Machine.Owner.Flip(horizontal);

            var isWalking = Mathf.Abs(horizontal) > 0.1f;
            _animator.SetBool("isWalking", isWalking);
        }

        public float WalkingSpeed = 4.0f;
    }
}