using UnityEngine;
using System.Collections;

public class TurretControl2 : MonoBehaviour {
	public  Transform  muzzle1;
	public  Transform  muzzle2;
	public  Rigidbody  bullet;
	public  GameObject target;
	private float      timer;
	private float      frequency;


	public void Start() {
		target = GameObject.FindWithTag(GameValues.PLAYER_TAG);
		timer  = 0;
		frequency = UnityEngine.Random.Range(GameValues.TURRET2_BULLET_FREQUENCY1, GameValues.TURRET2_BULLET_FREQUENCY2);
	}
	
	
	public void Reset() {
		
	}


	public void Update() {
		Vector3 dist = target.transform.position - transform.position;
		if (dist.sqrMagnitude > GameValues.TURRET2_MIN_SQUARE_DISTANCE) return;

		muzzle1.LookAt(target.transform.position);
		muzzle2.LookAt(target.transform.position);
		transform.LookAt(target.transform.position);
		transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
		timer += Time.deltaTime;
		if (timer >= frequency) {
			timer = 0;
			Rigidbody b1 = GameObject.Instantiate(bullet, muzzle1.position, muzzle1.rotation) as Rigidbody;
			Rigidbody b2 = GameObject.Instantiate(bullet, muzzle2.position, muzzle2.rotation) as Rigidbody;
			b1.AddForce(muzzle1.forward * GameValues.TURRET2_BULLET_FORCE);
			b2.AddForce(muzzle1.forward * GameValues.TURRET2_BULLET_FORCE);
			audio.Play();
		}
	}
}

