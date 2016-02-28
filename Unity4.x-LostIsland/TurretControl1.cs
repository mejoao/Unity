using UnityEngine;
using System.Collections;

public class TurretControl1 : MonoBehaviour {
	public  GameObject        flame;
	private GameObject        target;
	private ParticleEmitter[] flames;


	public void Start() {
		target = GameObject.FindWithTag(GameValues.PLAYER_TAG);
		flames = flame.GetComponentsInChildren<ParticleEmitter>();
		SetFlameMode(false);
	}


	private void SetFlameMode(bool toggle) {
		if (toggle  &&  flames[0].emit || !toggle  && !flames[0].emit) return;

		if (toggle  && !flames[0].emit) audio.Play();
		if (!toggle &&  flames[0].emit) audio.Stop();

		for (int i = 0; i < flames.Length; i++) {
			flames[i].emit = toggle;
		}

		flame.SetActive(toggle);
	}


	public void Reset() {
		SetFlameMode(false);
	}


	public void Update() {
		Vector3 dist = target.transform.position - transform.position;
		if (dist.sqrMagnitude > GameValues.TURRET1_MIN_SQUARE_DISTANCE) {
			if (flame.activeInHierarchy) SetFlameMode(false);
			return;
		} else {
			SetFlameMode(true);
		}
		transform.LookAt(target.transform.position);
		transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
	}
}
