using UnityEngine;
using System.Collections;

public class WeaponControl : MonoBehaviour {

	public  AudioClip       reloadSound;
	public  AudioClip       outOfAmmoSound;
	public  Transform       muzzle;
	public  ParticleSystem  muzzleFlash;
	public  ParticleEmitter sparks;
	public  Transform       laserStart;
	public  LayerMask       targetLayer;
	private int             ammoAmmount;
	private LineRenderer    laser;
	private float           timer;
	private bool            isShooting;
	private bool            isReloading;


	public int GetAmmoAmmountValue() { return ammoAmmount; }

	public void Start() {
		ammoAmmount = GameValues.PLAYER_WEAPON_CLIP_SIZE;
		laser    = GetComponent<LineRenderer>();
		laser.SetWidth(0.03F, 0.03F);
	}
	

	public void LateUpdate() {
		UpdateLaserPoints();

		
		if (Input.GetKeyDown(KeyCode.R) && !isShooting) {
			audio.PlayOneShot(reloadSound);
			animation.PlayQueued("MachineGun_reload");
			isReloading = true;
			StartCoroutine("ResetReloadingPeriod");
		}

		if (Input.GetMouseButton(0)) {
			timer += Time.deltaTime;
			if (timer >= GameValues.PLAYER_SHOOT_FREQUENCY) {
				timer = 0;

				if (ammoAmmount <= 0) {
					audio.Stop();
					muzzleFlash.Stop();
					audio.PlayOneShot(outOfAmmoSound);
				}

				if (!isShooting && !isReloading) {
					isShooting = true;
					animation.PlayQueued("MachineGin_shoot");
					if (ammoAmmount > 0) {
						muzzleFlash.Play();
						audio.Play();
					}
				}

				if (isShooting && ammoAmmount > 0) {
					ammoAmmount--;
					RaycastHit hit;
					if (Physics.Raycast(muzzle.position, muzzle.forward, out hit, targetLayer)) {
						if (hit.transform.gameObject.GetComponent<ReceiveDamage>() != null) {
							hit.transform.gameObject.GetComponent<ReceiveDamage>().ApplyDamage(GameValues.PLAYER_DAMAGE);
						}

						ParticleEmitter go = GameObject.Instantiate(sparks, hit.point, Quaternion.identity) as ParticleEmitter;
						go.transform.rotation = Quaternion.LookRotation(hit.normal);
					}
				}
			}
		}

		if (Input.GetMouseButtonUp(0)) {
			isShooting = false;
			animation.Stop();
			muzzleFlash.Stop();
			audio.Stop();
		}
	}


	private void UpdateLaserPoints() {
		laser.SetPosition(0, laserStart.position);
		laser.SetPosition(1, laserStart.position + laserStart.forward * 1000);
	}


	public IEnumerator ResetReloadingPeriod() {
		yield return new WaitForSeconds(1);//animation.clip.length);
		isReloading = false;
		ammoAmmount    = GameValues.PLAYER_WEAPON_CLIP_SIZE;
	}
}
