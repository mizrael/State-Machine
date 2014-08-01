using System;
using UnityEngine;

public static class GameObjectExtensions
{
	public static void Flip (this GameObject gameObject, float horizontal)
	{
		var theScale = gameObject.transform.localScale;
		if (horizontal > 0f) {
			theScale.x = 1f;
		}
		else
		if (horizontal < 0f) {
			theScale.x = -1f;
		}
		gameObject.transform.localScale = theScale;
	}	

	public static RaycastHit2D CheckIsFalling (this GameObject gameObject, int layer){
		var downCast = Physics2D.Raycast (gameObject.transform.position, -Vector2.up, 0.05f, 1 << layer);
		return downCast;
	}
}


