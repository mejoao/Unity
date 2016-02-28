using UnityEngine;

public class SplashScreenControl : MonoBehaviour {
	public  Material logo;
	private float    timer;
	private bool     fadeOut;
	private int      sign;


	public void Start() {
		Color c      = logo.color;
		c.a          = 0;
		logo.color   = c;
		fadeOut      = true;
		sign         = 1;
		audio.volume = 0;
		audio.Play();
	}
	
	
	public void Update() {
		if (fadeOut) {
			timer += Time.deltaTime;
			if (timer >= GameValues.SPLASHSCREEN_FADE_SPEED) {
				timer         = 0;
				Color c       = logo.color;
				c.a          += sign * GameValues.SPLASHSCREEN_FADE_RATE;
				logo.color    = c;
				audio.volume += sign * GameValues.SPLASHSCREEN_AUDIO_VOLUME_SPEED;
				if (audio.volume > 1) audio.volume = 1;
				if (c.a >= 1) {
					fadeOut = false;
					timer   = 0;
				} else if (c.a <= 0) {
					Application.LoadLevel(1);
				}
			}
		} else {
			timer += Time.deltaTime;
			if (timer >= GameValues.SPLASHSCREEN_FADE_WAITING_TIME) {
				timer   = 0;
				fadeOut = true;
				sign    = -1;
			}
		}
	}
}
