using UnityEngine;
using System.Collections;
using AI;
using AI.States;

public class PlayerController : MonoBehaviour {
	private StateMachine _fsm;

    private void Start() 
    {
		_fsm = new StateMachine (this.gameObject);

		var walkState = _fsm.AddState<PlayerDefaultState> ();
		walkState.WalkingSpeed = WalkingSpeed;

		var jumpState = _fsm.AddState<PlayerJumpState> ();
		jumpState.JumpForce = JumpForce;
		jumpState.WalkingSpeed = WalkingSpeed;

		var fallingState = _fsm.AddState<PlayerFallingState> ();
		fallingState.Gravity = Gravity;
		fallingState.WalkingSpeed = WalkingSpeed;
        fallingState.GroundCollisionLayer = GroundCollisionLayer;

		walkState.Transitions.Add (new StateTransition (jumpState, () => Input.GetButton ("Jump")));		
		walkState.Transitions.Add (new StateTransition (fallingState, () => {
			var downCast = this.gameObject.CheckIsFalling(GroundCollisionLayer);
			bool isHit = (null == downCast.collider);
			return isHit;
		}));		
		jumpState.Transitions.Add (new StateTransition (fallingState, () => jumpState.ExecutionTime > 0.25f));		
		fallingState.Transitions.Add (new StateTransition (walkState, () => fallingState.Completed));		

		_fsm.SetState (0);
	}

    private void Update()
    {
		_fsm.Execute ();
	}

    #region Properties

    public float JumpForce = 25.0f;
    public float Gravity = 10.0f;

    public float WalkingSpeed = 4.0f;
    public float RunningSpeed = 8.0f;

    public int GroundCollisionLayer = 8;

    #endregion Properties
}

