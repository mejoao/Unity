using UnityEngine;
using System.Collections;

public class TurretControl3 : MonoBehaviour {

	public  GameObject target;
	public  GameObject bullet;
	public  Transform  muzzle;
	private float      timer;


	public void Start() {
		target = GameObject.FindWithTag(GameValues.PLAYER_TAG);
		timer  = 0;
	}


	public void Reset() {

	}


	public void Update() {
		Vector3 dist = target.transform.position - transform.position;
		if (dist.sqrMagnitude > GameValues.TURRET1_MIN_SQUARE_DISTANCE)
			return;

		timer += Time.deltaTime;
		if (timer >= GameValues.TURRET3_BULLET_FREQUENCY) {
			timer = 0;
			muzzle.LookAt(target.transform.position);
			RaycastHit hit;
			Physics.Raycast(muzzle.position, muzzle.forward, out hit);
			GameObject go = GameObject.Instantiate(bullet, muzzle.position, muzzle.rotation) as GameObject;
			go.GetComponent<FireBallControl>().impactPoint = hit.point;
			audio.Play();
		}

		transform.LookAt(target.transform.position);
		transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

	}
}

