using UnityEngine;
public class SoundControl : MonoBehaviour {
	public AudioClip keyPressed;
	public AudioClip button;
	public void PlaySound(string sound) {
		if (sound == "key")    audio.PlayOneShot(keyPressed);	
		if (sound == "button") audio.PlayOneShot(button);	
	}
}