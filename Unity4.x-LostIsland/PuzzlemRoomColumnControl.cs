using UnityEngine;
using System.Collections;

public class PuzzlemRoomColumnControl : MonoBehaviour {
	
	public  bool  down;
	private float speed;
	private int   sign;
	private float originalHeight;
	private bool  originalDirection;
	private float maxHeight;
	private float minHeight;


	public void Reset() {
		down = originalDirection;
		sign = down ? -1 : 1;
		transform.localPosition = new Vector3(transform.localPosition.x, originalHeight, transform.localPosition.z);
	}


	public void Start() {
		originalHeight    = transform.position.y;
		originalDirection = down;
		speed             = UnityEngine.Random.Range(GameValues.PUZZLE_ROOM_COLUMN_MIN_SPEED, GameValues.PUZZLE_ROOM_COLUMN_MAX_SPEED);
		sign              = down ? -1 : 1;
	}


	public void FixedUpdate() {
		rigidbody.MovePosition(transform.position + sign * speed * transform.up * Time.deltaTime);
		if (transform.localPosition.y < GameValues.PUZZLE_ROOM_COLUMN_MIN_HEIGHT && down) {
			transform.localPosition = new Vector3(transform.localPosition.x, GameValues.PUZZLE_ROOM_COLUMN_MIN_HEIGHT, transform.localPosition.z);
			down                    = !down;
			sign                   *= (-1);
		}
		if (transform.localPosition.y > GameValues.PUZZLE_ROOM_COLUMN_MAX_HEIGHT && !down) {
			transform.localPosition = new Vector3(transform.localPosition.x, GameValues.PUZZLE_ROOM_COLUMN_MAX_HEIGHT, transform.localPosition.z);
			down                    = !down;
			sign                   *= (-1);
		}
	}
}
