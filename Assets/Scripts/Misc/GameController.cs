using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

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
