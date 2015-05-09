using UnityEngine;
using System.Collections;

[RequireComponent (typeof(TrailRenderer))]

public class TrailScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<TrailRenderer> ().sortingLayerName = "Effects";
	}
}
