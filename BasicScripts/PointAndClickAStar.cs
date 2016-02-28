using UnityEngine;
using System.Collections;

public class PointAndClickAStar : MonoBehaviour {

	private NavMeshAgent agent;
	public  Transform    target;
	public  Camera       myCamera;

	public void Start() {
		agent = GetComponent<NavMeshAgent>();
		agent.SetDestination(target.position);
	}

	public void Update() {
		if (Input.GetMouseButtonDown(0)) {
			Ray ray = myCamera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 200)) {
				target.position = hit.point;
			}
		}

		agent.SetDestination(target.position);
	}
}
