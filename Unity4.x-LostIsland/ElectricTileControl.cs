using UnityEngine;
using System.Collections;

public class ElectricTileControl : MonoBehaviour {

	public  Material offMaterial;
	public  Material onMaterial;
	private bool     on;

	public void Start() {
		renderer.material = onMaterial;
		on                = false;
	}


	public void ToggleTile(bool on) {
		if (on) {
			renderer.material = onMaterial;
			gameObject.tag = GameValues.ELECTRIC_ROOM_TILE_TAG;
		} else {
			renderer.material = offMaterial;
			gameObject.tag = GameValues.ELECTRIC_ROOM_TILE_OFF_TAG;
		}
	}
}
