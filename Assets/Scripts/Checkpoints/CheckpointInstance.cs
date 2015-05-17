using UnityEngine;
using System.Collections;

public class CheckpointInstance : MonoBehaviour {

	public SpriteRenderer poleRenderer;
	public string behindSortingLayer = "Objects";
	public string inFrontSortingLayer = "Objects";

	[HideInInspector]
	public bool done;

	void Update() {
		if (!done) {
			// Player to the left
			bool left = GameObject.FindGameObjectWithTag("Player").transform.position.x < transform.position.x;
			poleRenderer.sortingLayerName = left ? behindSortingLayer : inFrontSortingLayer;
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player" && !done) {
			done = true;
			poleRenderer.enabled = false;
			CheckpointController.SetCheckpoint();
		}
	}

}
