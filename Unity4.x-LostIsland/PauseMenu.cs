using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

    public  GameObject gameCamera;
	public  GameObject soundControl;
	public  GameObject musicControl;
	public  GameObject hudControl;
	public  GameObject airPlane;
	public  Texture2D  firstPersonHowToPlay;
	public  Texture2D  airplaneHowToPlay;
	private Texture2D  howToPlay;
	private bool       pause;
    private float      timer;
    private float      originalFixedDeltaTime;
	private bool       showHowToPlay;
	private float      howToPlayWidth;
	private float      howToPlayHeight;

	// ANOTHER HACK: This is here in order not to show the minimap
	//               when the Helicopter part starts
	private bool dontShowMiniMap = false;

    public void Reset() {

    }


    void Start () {
		howToPlay                               = firstPersonHowToPlay;
        originalFixedDeltaTime                  = Time.fixedDeltaTime;
        gameCamera.GetComponent<Blur>().enabled = false;
        Screen.showCursor                       = false;
		showHowToPlay                           = false;
		howToPlayWidth                          = howToPlay.width  * GameValues.HOW_TO_PLAY_IMAGE_WIDTH;
		howToPlayHeight                         = howToPlay.height * GameValues.HOW_TO_PLAY_IMAGE_HEIGHT;
    }


	public void SetAirPlaneHowToPlay() {
		pause           = true;
		showHowToPlay   = true;
		howToPlay       = airplaneHowToPlay;
		dontShowMiniMap = true;
		PauseGame();
	}


	public void PauseGame() {
		Screen.showCursor    = true;
		Time.timeScale       = 0;
		Time.fixedDeltaTime  = 0;
		hudControl.GetComponent<HudControl>().ToggleInterface(false);
		
		// Pause the other objects
		if (airPlane.activeInHierarchy) {
			airPlane.audio.Pause();	
		}
		
		gameCamera.GetComponent<SimpleMouseRotator>().enabled = false;
		gameCamera.GetComponent<Blur>().enabled               = true;
		
		musicControl.GetComponent<MusicControl>().PauseMusic();
	}


	public void UnPauseGame() {
		Screen.showCursor   = false;
		Time.timeScale      = 1;
		Time.fixedDeltaTime = originalFixedDeltaTime;
		showHowToPlay       = false;
		hudControl.GetComponent<HudControl>().ToggleInterface(true);

		////////////////////////////////////////////////////////////////
		// HACK
		if (dontShowMiniMap) {
			dontShowMiniMap = false;
			hudControl.GetComponent<HudControl>().ShowMiniMap(false);
			GameObject.FindWithTag(GameValues.MUSIC_CONTROL_TAG).GetComponent<MusicControl>().PlayMusic(GameValues.CHOPTER_MUSIC_ID);
		}
		////////////////////////////////////////////////////////////////


		// Unpause the other objects
		if (airPlane.activeInHierarchy) {
			airPlane.audio.Play();
		}
		
		gameCamera.GetComponent<SimpleMouseRotator>().enabled = true;
		gameCamera.GetComponent<Blur>().enabled               = false;
		
		musicControl.GetComponent<MusicControl>().Resume();
	}


    public void Update() {
        if (Input.GetKeyUp(KeyCode.P)) {
            pause = !pause;

			soundControl.GetComponent<SoundControl>().PlaySound(GameValues.MENU_SOUND_ID);

			if (pause) PauseGame();
			else       UnPauseGame();
        }
    }

    public void OnGUI() {
        if (pause) {
            Rect backRect     = new Rect(GameValues.BACK_POSX,    GameValues.BACK_POSY,
                                         GameValues.BACK_WIDTH,   GameValues.BACK_HEIGHT);
            Rect newRect      = new Rect(GameValues.BUTTON_POSX,  GameValues.NEW_BUTTON_POSY,
                                         GameValues.BUTTON_WIDTH, GameValues.BUTTON_HEIGHT);
            Rect continueRect = new Rect(GameValues.BUTTON_POSX,  GameValues.CONTINUE_BUTTON_POSY,
                                         GameValues.BUTTON_WIDTH, GameValues.BUTTON_HEIGHT);
            Rect howtoRect    = new Rect(GameValues.BUTTON_POSX,  GameValues.HOWTO_BUTTON_POSY,
                                         GameValues.BUTTON_WIDTH, GameValues.BUTTON_HEIGHT);
            Rect exitRect     = new Rect(GameValues.BUTTON_POSX,  GameValues.EXIT_BUTTON_POSY,
                                         GameValues.BUTTON_WIDTH, GameValues.BUTTON_HEIGHT);

            GUI.Box(backRect, GameValues.MENU_LABLE);

            if (GUI.Button(newRect, GameValues.NEW_BUTTON_LABLE)) {
                Time.timeScale      = 1;
                Time.fixedDeltaTime = originalFixedDeltaTime;
				Application.LoadLevel(2);
            }

            if (GUI.Button(continueRect, GameValues.CONTINUE_BUTTON_LABLE)) {
				showHowToPlay = false;
				hudControl.GetComponent<HudControl>().ToggleInterface(true);

				////////////////////////////////////////////////////////////////
				// HACK
				if (dontShowMiniMap) {
					dontShowMiniMap = false;
					hudControl.GetComponent<HudControl>().ShowMiniMap(false);
					GameObject.FindWithTag(GameValues.MUSIC_CONTROL_TAG).GetComponent<MusicControl>().PlayMusic(GameValues.CHOPTER_MUSIC_ID);
				}
				////////////////////////////////////////////////////////////////

				soundControl.GetComponent<SoundControl>().PlaySound(GameValues.MENU_CLICK_SOUND_ID);
                ResetGeneralValues();
            }

            if (GUI.Button(howtoRect, GameValues.HOWTO_BUTTON_LABLE)) {
				showHowToPlay = !showHowToPlay;
				soundControl.GetComponent<SoundControl>().PlaySound(GameValues.MENU_CLICK_SOUND_ID);
            }

            if (GUI.Button(exitRect, GameValues.EXIT_BUTTON_LABLE)) {
                Application.Quit();
            }
        }

		if (showHowToPlay) {
			GUI.DrawTexture(new Rect(GameValues.HOW_TO_PLAY_IMAGE_X, GameValues.HOW_TO_PLAY_IMAGE_Y, howToPlayWidth, howToPlayHeight), howToPlay);
		}

    }


    private void ResetGeneralValues() {
        Screen.showCursor   = false;
        pause               = false;
        Time.timeScale      = 1;
        Time.fixedDeltaTime = originalFixedDeltaTime;

        // Unpause the other objects

	
        gameCamera.GetComponent<SimpleMouseRotator>().enabled = true;
        gameCamera.GetComponent<Blur>().enabled               = false;
		musicControl.GetComponent<MusicControl>().Resume();
    }
}
