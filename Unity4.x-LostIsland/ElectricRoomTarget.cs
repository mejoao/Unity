using UnityEngine;
using System.Collections;

public class ElectricRoomTarget : MonoBehaviour {

	public  int        pathToActivate = -1;
	public  GameObject electricRommControl;
	public  Transform  player;
	private float      floatingPos;
	private float      floatingSpeed;
	private float      floatingAmplitude;

	public void Reset() {
		floatingPos = 0;
	}

	public void Start() {
		player              = GameObject.FindWithTag(GameValues.PLAYER_TAG).transform;
		electricRommControl = GameObject.FindWithTag(GameValues.ELECTRIC_ROOM_TAG);
		floatingSpeed       = UnityEngine.Random.Range(GameValues.ELECTRIC_ROM_TARGET_OSCILATING_SPEED0, GameValues.ELECTRIC_ROM_TARGET_OSCILATING_SPEED1);
		floatingAmplitude   = UnityEngine.Random.Range(GameValues.ELECTRIC_ROM_TARGET_OSCILATING_AMPLITUDE0, GameValues.ELECTRIC_ROM_TARGET_OSCILATING_AMPLITUDE1);

	}


	public void LateUpdate() {
		transform.LookAt(player.position);
		transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
		floatingPos           = floatingPos >= 360 ? floatingPos - 360 : floatingPos + floatingSpeed;
		transform.Translate(0, Mathf.Sin(floatingPos) * Time.deltaTime * floatingAmplitude, 0);		
	}
}
