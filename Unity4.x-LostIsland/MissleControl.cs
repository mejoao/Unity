using UnityEngine;
using System.Collections;

public class MissleControl : MonoBehaviour {

	public void Start() {
		Physics.IgnoreCollision(collider, GameObject.FindWithTag(GameValues.AIRPLANE_TAG).collider);
	}

	public void OnTriggerEnter(Collider hit) {
		GameObject.FindWithTag(GameValues.SOUND_CONTROL_TAG).GetComponent<SoundControl>().PlaySound(GameValues.EXPLOSION_SOUND_ID);
		GameObject.FindWithTag(GameValues.GAME_CONTROL_TAG).GetComponent<GameControl>().PlayFinalCutScene();
		GameObject.Destroy(gameObject);
	}

}
