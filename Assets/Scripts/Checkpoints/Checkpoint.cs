using UnityEngine;
using System.Collections;

public class Checkpoint {

	#region Save data
	C_Player player;
	C_Hand hand;
	C_Camera camera;

	CheckpointInstance latestCheckpoint;
	#endregion

	#region Set checkpoint
	public Checkpoint() {
		player = new C_Player ();
		camera = new C_Camera ();
	}
	#endregion

	#region Load checkpoint
	public void Load() {
		player.Load ();
		camera.Load ();
	}
	#endregion

}
