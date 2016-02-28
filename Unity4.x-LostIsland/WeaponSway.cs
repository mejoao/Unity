using UnityEngine;

public class WeaponSway : MonoBehaviour {
	public float      moveAmmount = 3;
	public float      moveSpeed   = 3;
	public GameObject gun;
	public float      moveOnX;
	public float      moveOnY;
	public Vector3    defaultPos;
	public Vector3    newGunPos;
	
	
	public void Start() {
		defaultPos = transform.localPosition;
	}
	
	
	public void FixedUpdate() {
		moveOnX   = Input.GetAxis("Mouse X") * Time.deltaTime * moveAmmount;
		moveOnY   = Input.GetAxis("Mouse Y") * Time.deltaTime * moveAmmount;
		newGunPos = new Vector3(defaultPos.x + moveOnX, defaultPos.y + moveOnY, defaultPos.z);
		
		gun.transform.localPosition = Vector3.Lerp(gun.transform.localPosition, newGunPos, moveSpeed * Time.deltaTime);
	}
}