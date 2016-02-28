using UnityEngine;
using System.Collections;


public class HudControl : MonoBehaviour {

	public  GameObject player;
	public  GameObject playerWepon;
	public  GameObject playerCamera;
    public  GameObject fadePlane;
	public  GameObject damage;
	public  GameObject miniMap;
	public  GUIText    hudMessages;
	public  GUISkin    hudSkin;
	public  Texture2D  bullet;
	public  Texture2D  ank;
	public  bool       showMiniMapNext;
	public  bool       showCrossAir;
	private string     currentCoroutine;
    private string     currentHUDMessage;
    private float      waitingTimeValue;
    private string     nextCoroutine;
	private int        numberOfKeys;
	private bool       showAmmo;


	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// This is here just not to mess with the code and force the weapon not to be visible in the beginning of the game.
	// Ugly, but it's this or rewrite the whole start code in GameControl class.
	private bool gameInit = true;
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	public void OnGUI() {
		for (int i = 0; i < numberOfKeys; i++) {
			GUI.DrawTexture(new Rect(GameValues.KEY_START_X + i * (GameValues.KEY_WIDTH + GameValues.KEY_OFFSET_X), 
			                         GameValues.KEY_START_Y, 
			                         GameValues.KEY_WIDTH * 2, GameValues.KEY_HEIGHT * 2), ank);
		}

		if (showAmmo) {
			GUI.DrawTexture(new Rect(GameValues.AMMO_START_X + GameValues.AMMO_OFFSET_X, 
			                         GameValues.AMMO_START_Y + GameValues.AMMO_OFFSET_Y, 
			                         bullet.width * 0.2F, bullet.height * 0.2F), bullet);

			string s = string.Format("X{0:00}", playerWepon.GetComponent<WeaponControl>().GetAmmoAmmountValue());
			GUI.Label(new Rect(GameValues.AMMO_START_X + bullet.width * 0.2F + GameValues.AMMO_TEXT_OFFSET_X, 
			                   GameValues.AMMO_START_Y + bullet.height * 0.1F + GameValues.AMMO_TEXT_OFFSET_Y, 
			                   100, 100), s, hudSkin.label);
		}

	}


	private void StartHUDCoroutine(string coroutine, float timeToWait, string next) {
		currentCoroutine = coroutine;
		waitingTimeValue = timeToWait;
		nextCoroutine    = next;
		StartCoroutine(currentCoroutine);
	}
	

	public void Reset() {
		Color c;
		
		currentHUDMessage   = GameValues.HUD_MESSAGE_START_GAME;
		hudMessages.text    = currentHUDMessage;
		c                   = hudMessages.color;
		c.a                 = 1;
		hudMessages.color   = c;
		hudMessages.enabled = true;
		
		c                              = damage.renderer.material.color;
		c.a                            = 0;
		damage.renderer.material.color = c;
		
		miniMap.SetActive(false);
		numberOfKeys = 0;
		showAmmo     = false;
	}


	public void ToggleInterface(bool toggle) {
		if (player.GetComponent<PlayerCollisionControl>().HasWeapon()) {
			showAmmo = toggle;
		}

		miniMap.SetActive(toggle);
	}
	

	public void ShowAmmo() {
		SetHudMessage(GameValues.HUD_MESSAGE_COLLECT_ANKS);
		showAmmo = true;
	}


	public void ShowMiniMap(bool show) { miniMap.SetActive(show); }

	public void InvertMiniMapStatus() {
		if (miniMap.activeInHierarchy) {
			miniMap.SetActive(false);
			GameObject.FindWithTag(GameValues.GAME_CONTROL_TAG).GetComponent<GameControl>().ToggleBlackBird(false);
		} else {
			miniMap.SetActive(true);
			GameObject.FindWithTag(GameValues.GAME_CONTROL_TAG).GetComponent<GameControl>().ToggleBlackBird(true);
		}
	}


	private void FreezePlayer() {
		player.GetComponent<FirstPersonCharacter>().enabled     = false;
		player.GetComponent<SimpleMouseRotator>().enabled       = false;
		playerCamera.GetComponent<SimpleMouseRotator>().enabled = false;
		playerWepon.SetActive(false);
	}
	
	
	private void ReleasePlayer() {
		player.GetComponent<FirstPersonCharacter>().enabled     = true;
		player.GetComponent<SimpleMouseRotator>().enabled       = true;
		playerCamera.GetComponent<SimpleMouseRotator>().enabled = true;

		if (gameInit) {
			gameInit = false;
			return;
		}
		playerWepon.SetActive(true);
	}


	public void ApplyDamage(float dam) {
		Color c             = damage.renderer.material.color;
		c.a                 += dam;
		damage.renderer.material.color = c;
	}


	public float GetPlayerHP() {
		Color c = damage.renderer.material.color;
		return c.a;
	}


	public void RestorePlayerHP() {
		Color c                        = damage.renderer.material.color;
		c.a                            = 0;
		damage.renderer.material.color = c;
	}


	public void IncreaseNumberOfKeys() {
		numberOfKeys++;
		if (numberOfKeys >= GameValues.MAX_NUMBER_OF_KEYS) {
			// Release Secret Room Entrance
		}
	}


	public int GetNumberOfKeys() { return numberOfKeys; }

	/////////////////////////////////////////////////////////////////////////////////////// 
	/// Functions to Control the appearance and effects of the Hud elements
	/////////////////////////////////////////////////////////////////////////////////////// 
    public void StartGameHUD() {
        if (fadePlane.activeInHierarchy) return;

        fadePlane.SetActive(true);
		FreezePlayer();
		StartHUDCoroutine("WaitSomeTime", GameValues.FADE_PLANE_WAITING_TIME, "FadeOutPlane");
    }


	public void SetPlaneAndText(string message) {
		FreezePlayer();
		Color c;
		c                                    = fadePlane.renderer.material.color;
		c.a                                  = 1;
		fadePlane.renderer.material.color = c;
		fadePlane.SetActive(true);

		currentHUDMessage   = message;
		hudMessages.text    = currentHUDMessage;
		c                   = hudMessages.color;
		c.a                 = 1;
		hudMessages.color   = c;
		hudMessages.enabled = true;

		miniMap.SetActive(false);

		StartHUDCoroutine("WaitSomeTime", GameValues.FADE_PLANE_WAITING_TIME, "FadeOutPlane");
	}


	public IEnumerator WaitSomeTime() {
        yield return new WaitForSeconds(waitingTimeValue);
        currentCoroutine    = nextCoroutine;
        StartCoroutine(currentCoroutine);
    }

	
	public IEnumerator FadeOutPlane() {
        Color c;
        for (float f = 1f; f >= 0; f -= GameValues.FADE_PLANE_FADE_SPEED) {
            c                                    = fadePlane.renderer.material.color;
            c.a                                  = f;
            fadePlane.renderer.material.color = c;
            yield return new WaitForSeconds(GameValues.FADE_PLANE_YIELD_TIME);
        }
        c                                    = fadePlane.renderer.material.color;
        c.a                                  = 0;
        fadePlane.renderer.material.color = c;
        fadePlane.SetActive(false);
        currentCoroutine = "FadeText";
        StartCoroutine(currentCoroutine);
		ReleasePlayer();
		miniMap.SetActive(showMiniMapNext);
    }


	public IEnumerator FadeInPlane() {
		Color c;
		for (float f = 0; f <= 1; f += GameValues.FADE_PLANE_FADE_SPEED) {
			c                                 = fadePlane.renderer.material.color;
			c.a                               = f;
			fadePlane.renderer.material.color = c;
			yield return new WaitForSeconds(GameValues.FADE_PLANE_YIELD_TIME);
		}
		c                                    = fadePlane.renderer.material.color;
		c.a                                  = 0;
		fadePlane.renderer.material.color = c;
	}


	public void SetHudMessage(string message) {
        if (currentCoroutine != null) StopCoroutine(currentCoroutine);

        currentHUDMessage   = message;
        hudMessages.text    = currentHUDMessage;
        hudMessages.enabled = true;
        Color c             = hudMessages.color;
        c.a                 = 1;
        hudMessages.color   = c;

		StartHUDCoroutine("WaitSomeTime", GameValues.TEXT_WAITING_TIME, "FadeText");
    }


    public IEnumerator FadeText() {
        Color c;
		for (float f = 1f; f >= 0; f -= GameValues.TEXT_FADE_SPEED) {
            c                 = hudMessages.color;
            c.a               = f;
            hudMessages.color = c;
			yield return new WaitForSeconds(GameValues.TEXT_YIELD_TIME);
        }
        c                   = hudMessages.color;
        c.a                 = 0;
        hudMessages.color   = c;
        hudMessages.text    = null;
        currentCoroutine    = null;
		hudMessages.enabled = false;
    }
}