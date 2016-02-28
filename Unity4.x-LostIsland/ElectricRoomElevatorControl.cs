using UnityEngine;
using System.Collections;

public class ElectricRoomElevatorControl : MonoBehaviour {
	public  GameObject elevator;
	public  Transform  bottonPosition;
	public  Transform  topPosition;
	private int        elevatorState;  // 0 = steady, 1 = up, 2 = down
	private int        lastState;


	public void Start() {
		elevatorState = 0;
		lastState     = 2;  // elevator starts on the ground
	}


	public void Reset() {
		transform.position = bottonPosition.position;
		elevatorState      = 0;
		lastState          = 2;
	}


	public void FixedUpdate() {
		if (elevatorState == 1) {         // UP
			Vector3 dir = topPosition.position - elevator.transform.position;
			if (dir.sqrMagnitude < 0.0001F) {
				lastState     = elevatorState;
				elevatorState = 0;
			} else {
				elevator.transform.Translate(0, 3 * Time.deltaTime, 0);
			}
		} else if (elevatorState == 2) {  // Down
			Vector3 dir = bottonPosition.position - elevator.transform.position;
			if (dir.sqrMagnitude < 0.0001F) {
				lastState     = elevatorState;
				elevatorState = 0;
			} else {
				elevator.transform.Translate(0, -3F * Time.deltaTime, 0);
			}
		}
	}


	public void OnTriggerEnter(Collider hit) {
		if (hit.tag == GameValues.PLAYER_TAG) {
			if (elevatorState == 0) {
				elevatorState = lastState == 1 ? 2 : 1;
			}
		}
	}

	public void OnTriggerExit(Collider hit) {
		if (hit.tag == GameValues.PLAYER_TAG) {
			if (elevatorState == 1 || elevatorState == 0) {
				elevatorState = 2;
			}
		}
	}

}
