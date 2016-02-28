using UnityEngine;
using System.Collections;

public class MusicControl : MonoBehaviour {
    public AudioClip island;
    public AudioClip minigame;
	public AudioClip chopter;

    public void Start() {
        audio.loop   = true;
        audio.volume = 0.1F;
        audio.clip   = island;
        audio.Play();
    }


    public void Reset() {

    }


    public void PlayMusic(int music) {
        if (music == GameValues.ISLAND_MUSIC_ID) {
            audio.volume = 0.1F;
            audio.clip   = island;
            audio.Play();
        }

		if (music == GameValues.MINI_GAME_MUSIC_ID) {
			audio.volume = 0.1F;
			audio.clip   = minigame;
			audio.Play();
		}

		if (music == GameValues.CHOPTER_MUSIC_ID) {
			audio.volume = 0.1F;
			audio.clip   = chopter;
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
