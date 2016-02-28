using UnityEngine;

public class WeaponBob : MonoBehaviour {
	public  GameObject           owner;
	public  float                bobbingSpeed   = 0.3F;
	public  float                bobbingAmmount = 0.02F;
	public  float                midPoint       = 0F;
	private float                timer          = 0;
	private float                waveSlice;
	private float                horizontal;
	private float                vertical;
	private float                totalAxes;
	private float                translateChange;
	private FirstPersonCharacter fpc;


	public void Start() {
		fpc = owner.GetComponent<FirstPersonCharacter>();
	}

	public void Update() {
		horizontal = Input.GetAxis("Horizontal");
		vertical   = Input.GetAxis("Vertical");
	}
	
	
	public void FixedUpdate() {
		//////////////////////////////////////////////////////////
		// If some kind of Sprint limitation would be implemented,
		//   but use the correct script, not FPSWalker
		if (Input.GetButton("Run")) {
		     bobbingSpeed = 0.25F;
		} else {
		     bobbingSpeed = 0.2F;
		 }
		//////////////////////////////////////////////////////////

		waveSlice  = 0.0F;
		if (Mathf.Abs(horizontal) == 0 && Mathf.Abs(vertical) == 0) {
			timer = 0.0F;
		} else {
			waveSlice = Mathf.Sin(timer);
			timer     = timer + bobbingSpeed;
			if (timer > Mathf.PI * 2) timer = timer - (Mathf.PI * 2);
		}
		
		if (waveSlice != 0 && fpc.grounded) {
			translateChange           = waveSlice * bobbingAmmount;
			totalAxes                 = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
			totalAxes                 = Mathf.Clamp(totalAxes, 0.0F, 1.0F);
			translateChange           = totalAxes * translateChange;
			transform.localPosition   = new Vector3(transform.localPosition.x, midPoint + translateChange, transform.localPosition.z);
		} else {
			transform.localPosition   = new Vector3(transform.localPosition.x, midPoint, transform.localPosition.z);
		}
	}
}
