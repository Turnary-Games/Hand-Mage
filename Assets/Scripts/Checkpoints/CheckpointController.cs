using UnityEngine;
using System.Collections;
using System.Collections.Generic; // List<>
using System.Runtime.Serialization;

public static class CheckpointController {

	public static Checkpoint checkpoint = new Checkpoint();

	public static void SetCheckpoint() {
		checkpoint = new Checkpoint ();
	}

	public static void LoadCheckpoint() {
		checkpoint.Load ();
	}

}
