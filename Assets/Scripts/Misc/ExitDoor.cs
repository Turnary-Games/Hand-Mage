using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ExitDoor : MonoBehaviour {

	public Object targetRoom;
	public Text loadingText;
	private bool playerInside;

	void Start() {
		loadingText.text = "";
	}

	void Update () {
		if (Input.GetButtonDown ("EnterDoor") && playerInside) {
			StartCoroutine(LoadLevel());
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			playerInside = true;
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "Player") {
			playerInside = false;
		}
	}

	IEnumerator LoadLevel() {
		loadingText.text = "Loading level...";

		AsyncOperation async = Application.LoadLevelAsync (targetRoom.name);
		yield return async;
		Debug.Log("Loading complete");
		Debug.Log (async);
	}
}
