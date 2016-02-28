using UnityEngine;
public class SelfDestruction : MonoBehaviour {
	public float lifeTime = 5;
	
	public void Start() {
		GameObject.Destroy(gameObject, lifeTime);
	}
}
