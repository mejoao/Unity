using UnityEngine;
using System.Collections;

public class PuzzleRoomPlataformControl : MonoBehaviour {
	public  Transform rotPoint;
	private int       sign;
	private float     speed;
	private bool      goingUp;
	private bool      goingDown;


	public void Start() {
		sign     = 1;
		goingUp  = false;
		goingDown = false;
		speed   = UnityEngine.Random.Range(GameValues.PUZZLE_ROOM_PLATFORM_MIN_SPEED, 
		                                   GameValues.PUZZLE_ROOM_PLATFORM_MAX_SPEED);
	}


	public void FixedUpdate() {
		if (goingDown) {
			transform.RotateAround(rotPoint.position, transform.right, sign * speed * Time.deltaTime);
			if (transform.eulerAngles.x > 85) {
				goingDown = false;
				sign      = -1;
				goingUp   = true;
				return;
			}
		}

		if (goingUp) {
			transform.RotateAround(rotPoint.position, transform.right, sign * speed * Time.deltaTime);
			if (transform.eulerAngles.x > 90) {
				goingUp = false;
				transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);
			}
		}
	}


	public void OnCollisionEnter(Collision hit) {
		if (hit.gameObject.tag == GameValues.PLAYER_TAG) {
			if (!goingUp && !goingDown) {
				goingDown = true;
				sign      = 1;
			}
		}
	}
}
