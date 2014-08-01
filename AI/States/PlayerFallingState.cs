using System;
using UnityEngine;

namespace AI.States
{
    public class PlayerFallingState : State
    {
        private Animator _animator;

        public PlayerFallingState(StateMachine machine, int id)
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

            var downCast = this.Machine.Owner.CheckIsFalling(GroundCollisionLayer);
            if (null == downCast.collider)
            {
                var horizontal = Input.GetAxis("Horizontal");
                var movement = transform.right * horizontal * WalkingSpeed - Vector3.up * Gravity;
                transform.Translate(movement * Time.deltaTime);
                Machine.Owner.Flip(horizontal);
            }
            else
                this.Completed = true;
        }

        protected override void OnExit()
        {
            _animator.SetBool("isJumping", false);
        }

        public float Gravity = 10.0f;
        public float WalkingSpeed = 4.0f;
        public int GroundCollisionLayer = 8;
    }
}