using UnityEngine;

public class MemoryRoomTarget : MonoBehaviour {
	public  GameObject memoryRommControl;
	public  Transform  player;


	public void Reset() {
	}


	public void Start() {
		player            = GameObject.FindWithTag(GameValues.PLAYER_TAG).transform;
		memoryRommControl = GameObject.FindWithTag(GameValues.ELECTRIC_ROOM_TAG);

	}
	
	
	public void LateUpdate() {
		transform.LookAt(player.position);
		transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
	}
}
