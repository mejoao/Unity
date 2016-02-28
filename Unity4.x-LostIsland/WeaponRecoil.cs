using UnityEngine;

public class WeaponRecoil : MonoBehaviour {
	public  bool    fullAuto        = false;
	public  Vector3 recoilBack      = new Vector3(0, 0, -1.5F);
	public  float   recoilSpeed     = 5;
	public  float   recoilBackSpeed = 2;
	public  Vector3 defaultPos      = Vector3.zero;
	public  float   fireSpeed       = 6;
	private float   timer;
	
	
	public void Start() {
	}
	
	
	public void Update() {
		timer += Time.deltaTime * fireSpeed;
		
		if (!Input.GetButton("Run")) {
			if (Input.GetButtonDown("Fire1") && !fullAuto) {
				transform.localPosition = Vector3.Lerp(transform.localPosition, recoilBack, Time.deltaTime * recoilSpeed);
			} else {
				transform.localPosition = Vector3.Lerp(transform.localPosition, defaultPos, Time.deltaTime * recoilBackSpeed);
			}
			
			if (Input.GetButton("Fire1") && fullAuto && timer >= 1) {
				transform.localPosition = Vector3.Lerp(transform.localPosition, recoilBack, Time.deltaTime * recoilSpeed);
				timer                   = 0;
			} else if (!Input.GetButton("Fire1") && fullAuto && timer < 1) {
				transform.localPosition = Vector3.Lerp(transform.localPosition, defaultPos, Time.deltaTime * recoilBackSpeed);
			}
		}
	}
}
