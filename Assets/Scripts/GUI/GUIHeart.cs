using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIHeart : MonoBehaviour {

	public int assignedHealth;

	private Image image;

	void Awake() {
		// Get the image component
		image = GetComponent<Image> ();
	}

	public void UpdateHealth(int health) {
		SetVisable (health >= assignedHealth);
	}
	
	public void SetVisable(bool visable) {
		image.enabled = visable;
	}
}
