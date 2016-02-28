using UnityEngine;
using System.Collections;

public class WeaponAnimation : MonoBehaviour {

	private bool running = false;



	public void Update() {

		if (Input.GetAxis("Run") != 0 && !running) {
			running = true;
			animation.Play();
		}

	}


}

