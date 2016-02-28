using UnityEngine;
using System.Collections;

public class FireBallControl : MonoBehaviour {
	public AudioClip  explosion;
	public GameObject audioEffect;
	public Vector3    impactPoint;

	public void OnParticleCollision(GameObject hit) {
		Vector3 pos = impactPoint != Vector3.zero ? impactPoint : hit.transform.position;
		GameObject obj = GameObject.Instantiate(audioEffect, pos, Quaternion.identity) as GameObject;
		obj.audio.clip = explosion;
		obj.audio.Play();

		if (hit.gameObject.tag == GameValues.PLAYER_TAG) 
			hit.gameObject.GetComponent<PlayerCollisionControl>().ApplyFireBallDamage(GameValues.FIRE_BALL_DAMAGE);

		GameObject.Destroy(gameObject);
	}
}
