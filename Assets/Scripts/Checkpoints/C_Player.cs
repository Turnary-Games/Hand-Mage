using UnityEngine;
using System.Collections;

public class C_Player {

	int health;
	Vector2 position;
	Vector2 velocity;
	
	public C_Player () {
		// Find player instance
		PlayerHealth playerHealthScript = PlayerHealth.GetInstance ();
		Rigidbody2D playerRigidbody = playerHealthScript.GetComponent<Rigidbody2D> ();
		
		// Get player variables
		health = playerHealthScript.GetHealth ();
		position = playerRigidbody.position;
		velocity = playerRigidbody.velocity;
	}

	public void Load() {
		// Find player instance
		PlayerHealth playerHealthScript = PlayerHealth.GetInstance ();
		Rigidbody2D playerRigidbody = playerHealthScript.GetComponent<Rigidbody2D> ();
		
		// Get player variables
		playerHealthScript.SetHealth (health);
		playerRigidbody.position = position;
		playerRigidbody.velocity = velocity;
	}
}
