using UnityEngine;
public class LookAtTarget : MonoBehaviour {
	public Transform target;

	public void Update() {
		transform.LookAt(target.position);
		transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
	}
}
