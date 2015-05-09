using UnityEngine;
using System.Collections;
using ExtensionMethods;

public class TurtleHeadController : MonoBehaviour {

	public LayerMask collideWith;

	void OnTrigger(Collider2D other) {
		if (collideWith.IsInLayerMask (other.gameObject)) {

			gameObject.GetComponentInParent<TurtleMovement>().Turn();

		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		OnTrigger (other);
	}

	void OnTriggerStay2D(Collider2D other) {
		OnTrigger (other);
	}

}
