using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public int startHealth;

	private PlayerHealth playerHealth;

	void Start() {
		playerHealth = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerHealth> ();

		playerHealth.SetHealth (startHealth);
	}

	void Update () {
		// Quit the game
		if (Input.GetButtonDown ("Quit")) {
			Application.Quit();
		}
	}

	public static void RestartLevel() {
		Application.LoadLevel (Application.loadedLevel);
	}

}
