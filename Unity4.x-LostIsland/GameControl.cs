using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameControl : MonoBehaviour {

	public GameObject   player;
	public GameObject   playerCamera;
	public GameObject   playerDamagePlane;
	public GameObject   playerFadeInPlane;
	public GameObject   timerHUD;
	public GameObject   hudControl;
	public GameObject   hudMessages;
	public GameObject   soundControl;
	public GameObject   musicControl;
	public GameObject   blackBird;
	public GameObject   airPlaneStub;
	public GameObject   controllableAirPlane;
	public GameObject   airPlaneCamera;
	public GameObject   airPlaneCinematicCamera;
	public GameObject   airPlaneCinematicFadePlane;
	public Transform    airPlaneCinematicCameraFinalTransform;
	public GameObject   miniMap;
	public GameObject[] firstTurrets;
	public GameObject[] secondTurrets;
	public GameObject[] thirdTurrets;
	public GameObject[] fourthTurrets;
	public GameObject[] portals;
	public MiniGame[]   miniGames;


	public void Start () {
		StartOfTheGame();
	}


	public void Update() {
		if (Input.GetKeyDown(KeyCode.H)) {
			hudControl.GetComponent<HudControl>().RestorePlayerHP();
			soundControl.GetComponent<SoundControl>().PlaySound(GameValues.FIRST_AID_PICKUP_SOUND_ID);
		}
	}


	private void StartOfTheGame() {
		//for (int i = 0; i < miniGames.Length; i++) miniGames[i].miniGameObject.SetActive(false);
		hudControl.GetComponent<HudControl>().showMiniMapNext = true;
		hudControl.GetComponent<HudControl>().Reset();
		hudControl.GetComponent<HudControl>().StartGameHUD();

		for (int i = 0; i < miniGames.Length; i++) {
			miniGames[i].miniGameObject.SetActive(false);
		}


		airPlaneCamera.SetActive(false);
		airPlaneCinematicCamera.SetActive(false);
		controllableAirPlane.GetComponent<AudioListener>().enabled = false;
		controllableAirPlane.SetActive(false);
	}


	public void StartFirstMiniGame() {
		musicControl.GetComponent<MusicControl>().StopMusic();

		hudControl.GetComponent<HudControl>().showMiniMapNext = false;
		hudControl.GetComponent<HudControl>().SetPlaneAndText(GameValues.HUD_MESSAGE_MEMORY_ROOM_MINI_GAME);
		blackBird.SetActive(false);
		miniGames[0].miniGameObject.SetActive(true);
		StartCoroutine(miniGames[0].miniGameObject.GetComponent<MemoryRoomControl>().WaitToStartMiniGame());


		soundControl.GetComponent<SoundControl>().PlaySound(GameValues.TELEPORT_SOUND_ID);
		player.transform.position = miniGames[0].miniGameEntryPoint.position;
		player.rigidbody.velocity = Vector3.zero;

		musicControl.GetComponent<MusicControl>().PlayMusic(GameValues.MINI_GAME_MUSIC_ID);
	}
	

	public void StartSecondMiniGame() {
		musicControl.GetComponent<MusicControl>().StopMusic();

		hudControl.GetComponent<HudControl>().showMiniMapNext = false;
		hudControl.GetComponent<HudControl>().SetPlaneAndText(GameValues.HUD_MESSAGE_PUZZLE_ROOM_MINI_GAME);
		blackBird.SetActive(false);
		miniGames[1].miniGameObject.SetActive(true);

		soundControl.GetComponent<SoundControl>().PlaySound(GameValues.TELEPORT_SOUND_ID);
		player.transform.position = miniGames[1].miniGameEntryPoint.position;
		player.rigidbody.velocity = Vector3.zero;

		musicControl.GetComponent<MusicControl>().PlayMusic(GameValues.MINI_GAME_MUSIC_ID);
	}


	public void StartThirdMiniGame() {
		musicControl.GetComponent<MusicControl>().StopMusic();

		hudControl.GetComponent<HudControl>().showMiniMapNext = false;
		hudControl.GetComponent<HudControl>().SetPlaneAndText(GameValues.HUD_MESSAGE_TRAPS_ROOM_MINI_GAME);
		blackBird.SetActive(false);
		miniGames[2].miniGameObject.SetActive(true);

		soundControl.GetComponent<SoundControl>().PlaySound(GameValues.TELEPORT_SOUND_ID);
		player.transform.position = miniGames[2].miniGameEntryPoint.position;
		player.rigidbody.velocity = Vector3.zero;

		musicControl.GetComponent<MusicControl>().PlayMusic(GameValues.MINI_GAME_MUSIC_ID);
	}


	public void StartFourthMiniGame() {
		musicControl.GetComponent<MusicControl>().StopMusic();


		hudControl.GetComponent<HudControl>().showMiniMapNext = false;
		hudControl.GetComponent<HudControl>().SetPlaneAndText(GameValues.HUD_MESSAGE_ELECTRIC_ROOM_MINI_GAME);
		blackBird.SetActive(false);
		miniGames[3].miniGameObject.SetActive(true);

		soundControl.GetComponent<SoundControl>().PlaySound(GameValues.TELEPORT_SOUND_ID);
		player.transform.position = miniGames[3].miniGameEntryPoint.position;
		player.rigidbody.velocity = Vector3.zero;

		musicControl.GetComponent<MusicControl>().PlayMusic(GameValues.MINI_GAME_MUSIC_ID);
	}


	public void ResetFirstTurrets() {
		for (int i = 0; i < firstTurrets.Length; i++) firstTurrets[i].GetComponent<TurretControl1>().Reset();
	}


	public void ResetSecondTurrets() {
		
	}


	public void ResetThirdTurrets() {
		
	}


	public void ResetFourthTurrets() {


	}


	public void ResetFirstMiniGame() {


	}


	public void ResetSecondMiniGame() {


	}


	public void ResetThirdMiniGame() {

	}


	public void ResetFourthMiniGame() {

	}


	public void EndFirstMiniGame() {
		musicControl.GetComponent<MusicControl>().StopMusic();

		soundControl.GetComponent<SoundControl>().PlaySound(GameValues.TELEPORT_SOUND_ID);
		player.rigidbody.velocity = Vector3.zero;
		player.transform.position = miniGames[0].miniGameExitPoint.position;

		hudControl.GetComponent<HudControl>().ShowMiniMap(true);
		blackBird.SetActive(true);
		miniGames[0].miniGameObject.SetActive(false);
		portals[0].SetActive(false);

		if (hudControl.GetComponent<HudControl>().GetNumberOfKeys() >= GameValues.MAX_NUMBER_OF_KEYS) {
			hudControl.GetComponent<HudControl>().SetHudMessage(GameValues.HUD_MESSAGE_PROCEED_TO_SECRET_ROOM);
		}

		musicControl.GetComponent<MusicControl>().PlayMusic(GameValues.ISLAND_MUSIC_ID);
	}

	public void EndSecondMiniGame() {
		musicControl.GetComponent<MusicControl>().StopMusic();

		soundControl.GetComponent<SoundControl>().PlaySound(GameValues.TELEPORT_SOUND_ID);
		player.rigidbody.velocity = Vector3.zero;
		player.transform.position = miniGames[1].miniGameExitPoint.position;

		hudControl.GetComponent<HudControl>().ShowMiniMap(true);
		blackBird.SetActive(true);
		miniGames[1].miniGameObject.SetActive(false);
		portals[1].SetActive(false);

		if (hudControl.GetComponent<HudControl>().GetNumberOfKeys() >= GameValues.MAX_NUMBER_OF_KEYS) {
			hudControl.GetComponent<HudControl>().SetHudMessage(GameValues.HUD_MESSAGE_PROCEED_TO_SECRET_ROOM);
		}

		musicControl.GetComponent<MusicControl>().PlayMusic(GameValues.ISLAND_MUSIC_ID);
	}


	public void EndThirdMiniGame() {
		musicControl.GetComponent<MusicControl>().StopMusic();

		soundControl.GetComponent<SoundControl>().PlaySound(GameValues.TELEPORT_SOUND_ID);
		player.rigidbody.velocity = Vector3.zero;
		player.transform.position = miniGames[2].miniGameExitPoint.position;

		hudControl.GetComponent<HudControl>().ShowMiniMap(true);
		blackBird.SetActive(true);
		miniGames[2].miniGameObject.SetActive(false);
		portals[2].SetActive(false);

		if (hudControl.GetComponent<HudControl>().GetNumberOfKeys() >= GameValues.MAX_NUMBER_OF_KEYS) {
			hudControl.GetComponent<HudControl>().SetHudMessage(GameValues.HUD_MESSAGE_PROCEED_TO_SECRET_ROOM);
		}

		musicControl.GetComponent<MusicControl>().PlayMusic(GameValues.ISLAND_MUSIC_ID);
	}


	public void EndFourthMiniGame() {
		musicControl.GetComponent<MusicControl>().StopMusic();

		soundControl.GetComponent<SoundControl>().PlaySound(GameValues.TELEPORT_SOUND_ID);
		player.rigidbody.velocity = Vector3.zero;
		player.transform.position = miniGames[3].miniGameExitPoint.position;

		hudControl.GetComponent<HudControl>().ShowMiniMap(true);
		blackBird.SetActive(true);
		miniGames[3].miniGameObject.SetActive(false);
		portals[3].SetActive(false);

		if (hudControl.GetComponent<HudControl>().GetNumberOfKeys() >= GameValues.MAX_NUMBER_OF_KEYS) {
			hudControl.GetComponent<HudControl>().SetHudMessage(GameValues.HUD_MESSAGE_PROCEED_TO_SECRET_ROOM);
		}

		musicControl.GetComponent<MusicControl>().PlayMusic(GameValues.ISLAND_MUSIC_ID);
	}


	public void ToggleBlackBird(bool toggle) {
		blackBird.GetComponent<BlackBirdControl>().enabled = toggle;
	}


	public void StartAirPlanePhase() {
		GetComponent<PauseMenu>().SetAirPlaneHowToPlay();

		blackBird.GetComponent<BlackBirdControl>().useLaser = false;
		hudControl.GetComponent<HudControl>().ShowMiniMap(false);

		soundControl.transform.parent        = controllableAirPlane.transform;
		soundControl.transform.localPosition = Vector3.zero;
		musicControl.transform.parent        = controllableAirPlane.transform;
		musicControl.transform.localPosition = Vector3.zero;

		airPlaneStub.SetActive(false);
		playerCamera.GetComponent<AudioListener>().enabled = false;


		controllableAirPlane.SetActive(true);
		controllableAirPlane.GetComponent<AirWolfControl>().enabled = false;
		controllableAirPlane.GetComponent<AudioListener>().enabled  = true;
		controllableAirPlane.audio.Play();

		musicControl.GetComponent<MusicControl>().StopMusic();
		//musicControl.GetComponent<MusicControl>().PlayMusic(GameValues.CHOPTER_MUSIC_ID);

		player.SetActive(false);

		airPlaneCinematicCamera.SetActive(true);
		controllableAirPlane.animation.Play();
	}


	public void DestroyTheIsland() {
		controllableAirPlane.GetComponent<AirWolfControl>().canDestroyTheIsland = true;
		hudControl.GetComponent<HudControl>().SetHudMessage(GameValues.HUD_MESSAGE_DESTROY_THE_TOTEM);
	}


	public void PlayFinalCutScene() {
		airPlaneCamera.SetActive(false);
		airPlaneCinematicCamera.transform.position = airPlaneCinematicCameraFinalTransform.position;
		airPlaneCinematicCamera.transform.rotation = airPlaneCinematicCameraFinalTransform.rotation;
		airPlaneCinematicCamera.SetActive(true);
		controllableAirPlane.GetComponent<AirWolfControl>().PlayFinalScene();
		StartCoroutine("ReleaseCredits");
	}


	public IEnumerator ReleaseCredits() {
		yield return new WaitForSeconds(GameValues.AIR_WOLF_TIME_TO_FADE_SCENE);
		airPlaneCinematicFadePlane.GetComponent<FadeFinalPlane>().fade = true;
		yield return new WaitForSeconds(GameValues.END_GAME_TIME_TO_RELEASE_CREDITS);
		airPlaneCinematicCamera.GetComponent<CreditScreenGUI>().enabled = true;
	}


	[System.Serializable]
	public class MiniGame {
		public GameObject miniGameObject;
		public Transform  miniGameEntryPoint;
		public Transform  miniGameExitPoint;
		public MiniGame() { }
	}
}
