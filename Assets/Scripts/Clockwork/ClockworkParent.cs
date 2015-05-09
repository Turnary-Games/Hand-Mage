using UnityEngine;
using System.Collections;

public class ClockworkParent : MonoBehaviour {

	public float motorSpeed;
	public float startPercentage;

	public enum States { up, down }
	protected States state;

	public void SetState(States newState) { state = newState; }
	public void SetStateUp() { state = States.up; }
	public void SetStateDown() { state = States.down; }

	void Awake() {
		Initiate ();
	}

	void Update() {
		CustomUpdate ();
	}

	void FixedUpdate() {
		CustomFixedUpdate ();
	}

	protected virtual void Initiate() {}
	protected virtual void CustomUpdate() {}
	protected virtual void CustomFixedUpdate() {}
}
