using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	public AudioSource audio;
	public AudioClip   ammo;
	public AudioClip   life;
	public AudioClip   keys;
	public AudioClip   damage;

	public void PlaySound(int sound) {
		if (sound == 1) {
			audio.PlayOneShot(ammo);
		} 

		if (sound == 2) {
			audio.PlayOneShot(life);
		}

		if (sound == 3) {
			audio.PlayOneShot(damage);
		}
	
	}

}
