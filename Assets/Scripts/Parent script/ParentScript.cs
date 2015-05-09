using UnityEngine;
using System.Collections;

public class ParentScript : MonoBehaviour {

	public GameObject[] childs;
	public bool active = true;
	
	private Vector3 lastPosition;
	
	// Use this for initialization
	void Start () {
		lastPosition = transform.position;
	}
	
	void LateUpdate () {
		
		if (active) {
			Vector3 deltaDist = transform.position - lastPosition;

			if (childs.Length > 0) {
				foreach (GameObject child in childs) {
					child.transform.position += deltaDist;
				}
			}
		}
		
		lastPosition = transform.position;
	}

}