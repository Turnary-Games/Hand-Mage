using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

	// Public variables
	public int startHeath;
	public float invTime;
	public float knockback;

	public Collider2D[] colliders;
	public PhysicsMaterial2D deadMaterial;

	// Private variables
	private Animator anim;
	private PlayerMovement move;

	private int health;
	private bool damaged = false;
	private float damagedTimer;

	// Scheduled methods
	void Start() {
		anim = GetComponent<Animator> ();
		move = GetComponent<PlayerMovement> ();
		health = startHeath;
	}

	void Update() {
		UpdateDamagedTimer ();

		if (!IsDead ()) {
			return;
		}

		if (move.isGrounded ()) {
			// Stuff to do once landed
			if (colliders.Length > 0) {
				foreach (Collider2D col in colliders) {
					col.sharedMaterial = deadMaterial;
				}
			}
		}
	}

	// Health methods
	public void SetHealth(int num) {
		if (num - health == 0)
			return;

		int newHealth = Mathf.Max(num, 0);
		if (newHealth == health)
			return;

		health = newHealth;

		UpdateHealth ();

		anim.SetBool ("Dead", health == 0);
		if (health == 0)
			gameObject.layer = LayerMask.NameToLayer ("Dead player");
		else
			gameObject.layer = LayerMask.NameToLayer ("Player");
	}

	public void AddHealth(int deltaHealth) {
		SetHealth (health + deltaHealth);
	}

	public int GetHealth() {
		return health;
	}

	public static PlayerHealth GetInstance() {
		return FindObjectOfType<PlayerHealth> ();
	}

	public void UpdateHealth() {
		// Loop each heart
		foreach (GameObject obj in GameObject.FindGameObjectsWithTag("GUI Heart")) {
			// Get the script
			GUIHeart heart = obj.GetComponent<GUIHeart>();

			if (heart == null)
				continue;

			// Give the script the current health
			heart.UpdateHealth(health);
		}
	}

	// Damage methods
	public void StartDamagedTimer () {
		// Start of invincibility
		damaged = true;
		damagedTimer = 0;

		anim.SetBool ("Hurt", true);
	}

	public void UpdateDamagedTimer() {
		if (!damaged)
			return;

		damagedTimer += Time.deltaTime;

		if (damagedTimer >= invTime) {
			// End of invincibility
			damaged = false;

			anim.SetBool("Hurt",false);
		}
	}

	public void Damage(int dmg, Vector3 source) {
		if (health == 0)
			return;

		if (damaged)
			return;
	
		StartDamagedTimer ();
		SetHealth (health - dmg);

		// Knockback
		Vector3 pos = transform.position;
		GetComponent<Rigidbody2D>().velocity = new Vector2((pos.x - source.x), (pos.y - source.y)).normalized * knockback;

		// Turn towards the source
		move.FlipTowards(source.x);
	}

	public bool IsDamaged() {
		if (health <= 0)
			return true;

		return damaged;
	}

	public float DamagedScale() {
		if (health <= 0)
			return 0;

		if (!damaged)
			return 1f;

		return damagedTimer / invTime;
	}


	public bool IsDead() {
		return health <= 0;
	}

	void Restart() {
		CheckpointController.LoadCheckpoint ();
		//GameController.RestartLevel ();
	}
}
