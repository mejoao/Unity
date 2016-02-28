using UnityEngine;
using System.Collections;

public class TurretControl4 : MonoBehaviour {

	public  GameObject target;
	public  Rigidbody  shockWaveBomb;
	public  Transform  muzzle;
	private float      timer;
	private float      frequency;


	public void Start() {
		target = GameObject.FindWithTag(GameValues.PLAYER_TAG);
		timer  = 0;
		frequency = UnityEngine.Random.Range(GameValues.TURRET4_BULLET_FREQUENCY1, GameValues.TURRET4_BULLET_FREQUENCY2);
	}


	public void Update() {
		Vector3 dist = target.transform.position - transform.position;
		if (dist.sqrMagnitude > GameValues.TURRET1_MIN_SQUARE_DISTANCE) return;

		timer += Time.deltaTime;
		if (timer >= frequency) {
			timer = 0;
			muzzle.transform.LookAt(target.transform.position);
			Rigidbody rb = GameObject.Instantiate(shockWaveBomb, muzzle.position, muzzle.rotation) as Rigidbody;
			Physics.IgnoreCollision(collider, rb.gameObject.collider);
			rb.AddForce(muzzle.forward * GameValues.TURRET4_BULLET_FORCE);
		}
	}
}
