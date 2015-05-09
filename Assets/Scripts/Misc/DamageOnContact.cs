using UnityEngine;
using System.Collections;

public class DamageOnContact : MonoBehaviour {

	public int damage = 1;
	public bool useColliders = true;
	public bool useTriggers = true;

	private static string playerTag = "Player";
	private PlayerHealth playerHealth;

	// ----------------------------------------

	void Start() {
		playerHealth = FindObjectOfType<PlayerHealth> ();
	}

	// ----------------------------------------

	private void ColliderCollision(Collision2D coll) {
		if (useColliders && coll.gameObject.tag == playerTag)
			playerHealth.Damage (damage, transform.position);
	}

	private void CollisionTrigger(Collider2D other) {
		if (useTriggers && other.gameObject.tag == playerTag)
			playerHealth.Damage (damage, transform.position);
	}

	// ----------------------------------------

	void OnCollisionEnter2D(Collision2D coll) {
		ColliderCollision (coll);
	}

	void OnCollisionStay2D(Collision2D coll) {
		ColliderCollision (coll);
	}

	void OnTriggerEnter2D(Collider2D other) {
		CollisionTrigger (other);
	}

	void OnTriggerStay2D(Collider2D other) {
		CollisionTrigger (other);
	}
}
