using System;
using UnityEngine;

namespace AI.States
{
    public class PlayerJumpState : State
    {
        private Animator _animator;

        public PlayerJumpState(StateMachine machine, int id)
            : base(machine, id)
        {
            _animator = Machine.Owner.GetComponent<Animator>();
        }

        protected override void OnEnter()
        {
            _animator.SetBool("isJumping", true);
            _animator.SetBool("isWalking", false);
        }

        protected override void OnExecute()
        {
            var transform = Machine.Owner.transform;
            var horizontal = Input.GetAxis("Horizontal");

            var movement = transform.right * horizontal * WalkingSpeed + Vector3.up * JumpForce;
            transform.Translate(movement * Time.deltaTime);

            Machine.Owner.Flip(horizontal);
        }

        protected override void OnExit()
        {
            _animator.SetBool("isJumping", false);
        }

        public float JumpForce = 25.0f;
        public float WalkingSpeed = 4.0f;
    }
}
