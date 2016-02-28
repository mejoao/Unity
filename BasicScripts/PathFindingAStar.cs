using UnityEngine;
public class PathFindingAStar : MonoBehaviour {
	public NavMeshAgent agent;
	public Transform    target;

	public void Start() {
		agent = GetComponent<NavMeshAgent>();
		agent.SetDestination(target.position);
	}

	public void Update() {
		agent.SetDestination(target.position);	
	}
}