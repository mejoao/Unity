using UnityEngine;
using System.Collections;

public class SelfDestruction : MonoBehaviour {
	public float lifeTime = 0;


	public void Start() {
		GameObject.Destroy(gameObject, lifeTime); 
	}	
}