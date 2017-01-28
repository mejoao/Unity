using UnityEngine;

public class TestPlacing : MonoBehaviour {
    public Transform  trap;
    public GameObject trapPrefab;
    public Camera     myCam;
    public LayerMask  mask;

    public void Start() {
        trap.gameObject.SetActive(false);
    }

	public void Update () {
	    if (Input.GetMouseButton(0)) {
            RaycastHit hit;
            Ray ray = myCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 300, mask)) {
                trap.gameObject.SetActive(true);
                trap.position = hit.point;
                trap.up = hit.normal;
            }
        }

        if (Input.GetMouseButtonUp(0) && trap.gameObject.activeInHierarchy) {
           // GameObject t = GameObject.Instantiate(trapPrefab, trap.position, trap.rotation) as GameObject;
            trap.gameObject.SetActive(false);
        }
	}
}
