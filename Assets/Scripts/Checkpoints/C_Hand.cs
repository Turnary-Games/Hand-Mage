using UnityEngine;
using System.Collections;

public class C_Hand {

	HandController.States state;
	Transform target;
	Vector3 lastPosition;
	Vector3 position;
	
	public C_Hand () {
		// Find hand instance
		HandController hand = HandController.FindObjectOfType<HandController>();

		state = hand.GetState ();
		target = hand.target;

		lastPosition = hand.lastPosition;
		position = hand.transform.position;
	}

	public void Load() {
		// Find hand instance
		HandController hand = HandController.FindObjectOfType<HandController>();

		hand.SetState (state);
		hand.target = target;

		hand.lastPosition = lastPosition;
		hand.transform.position = position;
	}
}
