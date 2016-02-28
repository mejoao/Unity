using UnityEngine;
using System.Collections;

public class BlackBirdControl : MonoBehaviour {

	public  GameObject  target;
	public  Transform   center;
	public  GameObject  laser;
	public  bool        useLaser;
	public  Transform[] waypoints;
	public  GameObject  smoke;
	private int         currentWaypoint;
	private float       frequency;
	private float       timer;
	private bool        shootAtPlayer;
	private Vector3     initialShootPos;
	private int         coord;
	private bool        stopWaypoints;
	public float       hp;

	public void Start() {
		hp            = GameValues.BLACK_BIRD_HP;
		stopWaypoints = false;
		frequency     = UnityEngine.Random.Range(GameValues.BLACK_BIRD_FREQUENCY1, GameValues.BLACK_BIRD_FREQUENCY2);
		shootAtPlayer = false;
		useLaser      = true;
		laser.SetActive(false);
		smoke.particleSystem.Stop();
		rigidbody.isKinematic = true;
		rigidbody.useGravity  = false;
	}

	
	public void LateUpdate() {

		if (stopWaypoints) {
			transform.Translate(0, -10 * Time.deltaTime, 20 * Time.deltaTime);
			if (transform.eulerAngles.x > 80) return;
			transform.Rotate(5 * Time.deltaTime, 0, 0);
			return;
		}

		Vector3 dir = waypoints[currentWaypoint].position - transform.position;
		if (dir.sqrMagnitude < 1) {
			currentWaypoint++;
			if (currentWaypoint >= waypoints.Length) currentWaypoint = 0;
		} else {
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 0.5F * Time.deltaTime);
			transform.Translate(transform.forward * 10 * Time.deltaTime);
		}


		if (useLaser) {
			timer += Time.deltaTime;
			if (!shootAtPlayer && timer >= frequency) {
				timer = 0;

				laser.transform.LookAt(target.transform.position);
				RaycastHit hit;
				if (Physics.Raycast(laser.transform.position, laser.transform.forward, out hit)) {
					if (hit.transform.tag == target.tag) {
						shootAtPlayer = true;
						laser.SetActive(true);
						initialShootPos = target.transform.position;
						coord           = UnityEngine.Random.Range(0, 10) < 5 ? 1 : 2;
						if (coord == 1)  // X
							initialShootPos.x -= 1F;
						else           // Y
							initialShootPos.z -= 1F;
						
						laser.transform.LookAt(initialShootPos);
					}
				}
			} else if (shootAtPlayer) {
				laser.transform.LookAt(initialShootPos);

				if (timer >= GameValues.BLACK_BIRD_SHOOT_TIME) {
					timer = 0;
					laser.SetActive(false);
					shootAtPlayer = false;
				}
			}
		}
	}


	public void OnCollisionEnter(Collision hit) {
		smoke.transform.parent = null;
		smoke.GetComponent<ParticleSystemDestroyer>().enabled = true;
		GameObject.Destroy(gameObject);
	}


	public void ApplyDamage() {
		hp -= GameValues.PLAYER_AIR_WOLF_DAMAGE;
		if (hp <= 0 && !stopWaypoints) Explode();
	}


	public void Explode() {
		hp                      = 0;
		rigidbody.isKinematic   = false;
		rigidbody.useGravity    = true;
		stopWaypoints           = true;
		smoke.particleSystem.Play();
		GameObject.FindWithTag(GameValues.GAME_CONTROL_TAG).GetComponent<GameControl>().DestroyTheIsland();
	}
}