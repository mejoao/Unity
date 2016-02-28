using UnityEngine;
using System.Collections;

public class ShockWaveBulletControl : MonoBehaviour {

	public  GameObject shockwave;


	public void Start () {
		GameObject.Destroy(gameObject, GameValues.TURRET4_BULLET_LIFE_TIME);
	}
	

	public void OnCollisionEnter(Collision hit) {
		GameObject p = GameObject.FindWithTag(GameValues.PLAYER_TAG);
		Vector3 dist = p.transform.position - transform.position;
		if (dist.sqrMagnitude <= GameValues.SHOCKWAVE_BULLET_DISTANCE_TO_DAMAGE) {
			float percentage = 1 - dist.sqrMagnitude / GameValues.SHOCKWAVE_BULLET_DISTANCE_TO_DAMAGE;
			p.GetComponent<PlayerCollisionControl>().ApplyShockWaveDamage(percentage);
		}

		GameObject.Instantiate(shockwave, transform.position, Quaternion.identity);
		GameObject.Destroy(gameObject);
	}
}
