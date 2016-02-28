using UnityEngine;
public class MusicControl : MonoBehaviour {
	public AudioClip island;
	public AudioClip temple;

	public void Start()	{
		audio.loop   = true;
		audio.volume = 0.3F;
		audio.clip   = island;
		audio.Play();					
	}

	public void PlayMusic(string music) {
		if (music == "Island") {
			audio.volume = 0.3F;
			audio.clip   = island;
			audio.Play();			
		}
		
		if (music == "temple") {
			audio.volume = 0.5F;
			audio.clip   = secretRoom;
			audio.Play();
		}
	}
	
	public void Resume() {
		audio.Play();	
	}
	
	public void PauseMusic() {
		audio.Pause();
	}
	
	public void StopMusic() {
		audio.Stop();
	}
}