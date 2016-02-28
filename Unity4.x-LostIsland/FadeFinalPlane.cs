using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FadeFinalPlane : MonoBehaviour {

	public bool fade = false;

	void Update () {
		if (!fade) return;
		StartCoroutine("Fade");
		fade = false;
	}

	public IEnumerator Fade() {
		Color c;
		for (float f = 0; f <= 1; f += GameValues.FADE_PLANE_FADE_SPEED) {
			c                       = renderer.material.color;
			c.a                     = f;
			renderer.material.color = c;
			yield return new WaitForSeconds(GameValues.FADE_PLANE_YIELD_TIME);
		}
		c                       = renderer.material.color;
		c.a                     = 1;
		renderer.material.color = c;
	}
}
