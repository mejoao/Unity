using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using System.Collections;
using UnityEngine.SceneManagement;

namespace TheMaze {
    public class GameControl : MonoBehaviour {

        [Header("Generic Variables")]
        #region General
        public  GameObject                     initialCinematicGO;
        public  HUDMessages                    hud;
        public  FinalCountDownControl          countDown;
        public  SoundControl                   soundControl;
        public  PlayerControl                  playerControl;
        public  RigidbodyFirstPersonController playerFPC;
        public  Camera                         playerCamera;
        public  CreditControl                  credits;
        public  AudioSource                    musicControl;
        private Camera                         introCamera;

        public void Awake() { SetupInitialCinematic(); }

        private void SetupInitialCinematic() {
            paused                          = false;
            canPause                        = false;
            firstRoomCameraSuccess.enabled  = false;
            secondRoomCameraSuccess.enabled = false;
            cameraToSpaceShip.enabled       = false;
            credits.enabled                 = false;
            playerControl.enabled           = false;
            playerCamera.enabled            = false;
            introCamera                     = initialCinematicGO.GetComponent<Camera>();
            pauseCanvas.SetActive(false);
            finalCameraGO.SetActive(false);
            SetupFourthRoom();
            countDown.Toggle(false);
        }

        public void InitialCinematicEnd() {
            initialCinematicGO.SetActive(false);
            canPause                         = true;
            playerControl.transform.rotation = Quaternion.Euler(0, -180, 0);
            playerControl.enabled            = true;
            playerCamera.enabled             = true;
            ToggleSecondRoomTraps(false);
            ToggleThirdRoomTraps(false);
        }

        #region Pause Menu
        public  GameObject pauseCanvas;
        private bool       paused;
        private bool       canPause;

        public void PauseGame() {
            soundControl.PlaySound(GAME_SOUNDS.BUTTOM_PRESS_SOUND);
            Cursor.visible        = true;
            Time.timeScale        = 0;
            playerControl.enabled = false;
            playerFPC.enabled     = false;
            musicControl.Pause();
            if (!firstRoomSolved) {
                saw1Path.ToggleAudio(false);
                saw2Path.ToggleAudio(false);
            }
        }

        public void UnPauseGame() {
            soundControl.PlaySound(GAME_SOUNDS.BUTTOM_PRESS_SOUND);
            paused                = false;
            Cursor.visible        = false;
            Time.timeScale        = 1;
            playerControl.enabled = true;
            playerFPC.enabled     = true;
            musicControl.UnPause();
            if (!firstRoomSolved) {
                saw1Path.ToggleAudio(true);
                saw2Path.ToggleAudio(true);
            }
            pauseCanvas.SetActive(false);
        }

        public void ReloadGame() {
            soundControl.PlaySound(GAME_SOUNDS.BUTTOM_PRESS_SOUND);
            paused         = false;
            Cursor.visible = false;
            Time.timeScale = 1;
            SceneManager.LoadScene(2);
        }

        public void QuitGame() {
			#if !UNITY_WEBGL
            	soundControl.PlaySound(GAME_SOUNDS.BUTTOM_PRESS_SOUND);
            	Time.timeScale      = 1;
            	Time.fixedDeltaTime = 1;
            	#if UNITY_EDITOR
            		EditorApplication.isPlaying = false;
            	#endif
            	Application.Quit();
			#endif
        }

        public void Update() {
            if (Input.GetKeyDown(KeyCode.Escape) && canPause) {
                paused = !paused;
                pauseCanvas.SetActive(paused);

                if (paused)
                    PauseGame();
                else
                    UnPauseGame();
            }
        }

        #endregion

        public void OutputInfoForThePlayer(GAME_SOUNDS gameSound, HUD_MESSAGES hudMessage) {
            soundControl.PlaySound(gameSound);
            hud.ActivateMessage(hudMessage);
        }

        public void EndGame(bool success) {
            playerControl.enabled = false;
            playerCamera.enabled  = false;
            canPause              = false;
            countDown.Toggle(false);
 
            finalCameraGO.SetActive(true);
            if (success) {
                hud.ActivateMessage(HUD_MESSAGES.END_GAME_SUCCESS);
            } else {
                hud.ActivateMessage(HUD_MESSAGES.END_GAME_FAILED);
            }
            StartCoroutine(ShowCredits());
        }

        public IEnumerator ShowCredits() {
            yield return new WaitForSeconds(GameConstants.WAIT_FOR_CREDITS_TIME);
            credits.StartCredits();
            normandyControl.ActivateFinalSequency();
        }
        #endregion

        #region First Room
        [Space]
        [Header("First Room")]
        public SecurityCameraProperties[] securityCameras;

        public  Animator   saw1Animator;
        public  SawPath    saw1Path;
        public  Animator   saw2Animator;
        public  SawPath    saw2Path;
        public  GameObject secondRoomDoor;
        public  Camera     firstRoomCameraSuccess;
        private int        numberOfScreensPressed = 0;
        private bool       firstRoomSolved = false;

		#region DEBUG FUNCTION It's only called if GamePlayHelper calss is active in the scene
        public void SolveFirstPuzzle() {
            firstRoomSolved        = true;
            numberOfScreensPressed = GameConstants.FIRST_ROOM_NUMBER_OF_SCREENS;
            StartCoroutine(FirstRoomSolved());
        }
        #endregion

        public void FirstRoomUpdatePuzzle() {
            if (firstRoomSolved) return;

            numberOfScreensPressed++;
            if (numberOfScreensPressed >= GameConstants.FIRST_ROOM_NUMBER_OF_SCREENS) {
                firstRoomSolved = true;
                StartCoroutine(FirstRoomSolved());
            } else {
                OutputInfoForThePlayer(GAME_SOUNDS.SCREEN_TOUCHED_SOUND, HUD_MESSAGES.SCREEN_TOUCHED);
            }
        }

        private IEnumerator FirstRoomSolved() {
            OutputInfoForThePlayer(GAME_SOUNDS.PUZZLE_SOLVED_SOUND, HUD_MESSAGES.PUZZLE_SOLVED);

            var puzzleSuccessTime = new WaitForSeconds(GameConstants.PUZZLE_SUCCESS_YIELD_TIME);
            var puzzleDeltaTime   = new WaitForSeconds(GameConstants.PUZZLE_DELTA_TIME);

            canPause = false;

            // HACK: Hiding the Hammers in the second room because of the
            //       ceiling doesn't hide them in this part of the game
            for (int i = 0; i < hammers.Length; i++) hammers[i].Toggle(false);

            playerControl.enabled          = false;
            playerCamera.enabled           = false;
            firstRoomCameraSuccess.enabled = true;
            saw1Animator.enabled           = false;
            saw2Animator.enabled           = false;
            saw1Path.Deactivate();
            saw2Path.Deactivate();

            yield return puzzleSuccessTime;

            for (int i = 0; i < securityCameras.Length; i++) {
                securityCameras[i].col.enabled             = false;
                securityCameras[i].light.enabled           = false;
                securityCameras[i].volumetricLight.enabled = false;
                securityCameras[i].animator.enabled        = false;
                yield return puzzleDeltaTime;
            }

            secondRoomDoor.SetActive(false);

            StartCoroutine(FinishFirstRoomPuzzle(GameConstants.PUZZLE_SUCCESS_RESET_TIME));
        }

        public IEnumerator FinishFirstRoomPuzzle(float seconds) {
            yield return new WaitForSeconds(seconds);
            canPause                       = true;
            playerControl.enabled          = true;
            playerCamera.enabled           = true;
            firstRoomCameraSuccess.enabled = false;

            //for (int i = 0; i < hammers.Length; i++) hammers[i].Deactivate(true);
        }

        [System.Serializable]
        public class SecurityCameraProperties {
            public Collider     col;
            public MeshRenderer volumetricLight;
            public Light        light;
            public Animator     animator;
        }
        #endregion

        #region Second Room
        [Header("Second Room")]
        public TurretControl[]       turrets;
        public CapsuleLaserControl[] capsuleLasers;
        public RotatingBeamControl[] rotatingBeams;
        public HammerControl[]       hammers;
        public GameObject            secondRoomExitDoor;
        public GameObject            thirdRoomDoor;
        public Camera                secondRoomCameraSuccess;
        public Transform             firstSuccessCameraTransform;
        public Transform             secondSuccessCameraTransform;

        private void ToggleSecondRoomTraps(bool toggle) {
            for (int i = 0; i < capsuleLasers.Length; i++) capsuleLasers[i].Toggle(toggle);
            for (int i = 0; i < rotatingBeams.Length; i++) rotatingBeams[i].Toggle(toggle);
            for (int i = 0; i < hammers.Length;       i++) hammers[i].Toggle(toggle);
        }

        public void StartSecondRoomPuzzle() {
            soundControl.PlaySound(GAME_SOUNDS.DOOR_OPENING_SOUND);
            ToggleSecondRoomTraps(true);
        }

        public void SecondRoomPuzzleSolved() {
            StartCoroutine(FinishSecondRoomPuzzle());
        }

        public IEnumerator FinishSecondRoomPuzzle() {
            OutputInfoForThePlayer(GAME_SOUNDS.PUZZLE_SOLVED_SOUND, HUD_MESSAGES.PUZZLE_SOLVED);

            var puzzleSuccessTime = new WaitForSeconds(GameConstants.PUZZLE_SUCCESS_YIELD_TIME);
            var puzzleDeltaTime   = new WaitForSeconds(GameConstants.PUZZLE_DELTA_TIME);
            var puzzleResetTime   = new WaitForSeconds(GameConstants.PUZZLE_SUCCESS_RESET_TIME);

            canPause = false;

            ToggleSecondRoomTraps(false);

            // HACK: Hiding the Hammers in the second room because of the
            //       ceiling doesn't hide them in this part of the game
            for (int i = 0; i < hammers.Length; i++) hammers[i].Toggle(false);

            playerControl.enabled = false;
            playerCamera.enabled  = false;

            secondRoomCameraSuccess.transform.position = firstSuccessCameraTransform.position;
            secondRoomCameraSuccess.transform.rotation = firstSuccessCameraTransform.rotation;
            secondRoomCameraSuccess.enabled            = true;

            yield return puzzleSuccessTime;
            secondRoomExitDoor.SetActive(false);

            yield return puzzleSuccessTime;
            secondRoomCameraSuccess.transform.position = secondSuccessCameraTransform.position;
            secondRoomCameraSuccess.transform.rotation = secondSuccessCameraTransform.rotation;

            yield return puzzleSuccessTime;
            thirdRoomDoor.SetActive(false);

            yield return puzzleDeltaTime;
            for (int i = 0; i < turrets.Length; i++) {
                turrets[i].DisableTurret();
                yield return puzzleDeltaTime;
            }

            yield return puzzleResetTime;
            playerControl.enabled           = true;
            playerCamera.enabled            = true;
            secondRoomCameraSuccess.enabled = false;
            canPause                        = true;
            ToggleThirdRoomTraps(true);
        }
        #endregion

        #region Third Room
        [Header("Third Room")]
        public GameObject thirdRoomTraps;

        private void ToggleThirdRoomTraps(bool toggle) { thirdRoomTraps.SetActive(toggle); }


        public void FinishThirdRoomPuzzle() {
            OutputInfoForThePlayer(GAME_SOUNDS.PREDATOR_LAUGH_SOUND, HUD_MESSAGES.PREDATOR_VISION_USE);
            fourthRoomScreen.enabled       = true;
            blockingDoorRenderer.enabled = true;
            finalWall.SetActive(false);
            ToggleThirdRoomTraps(false);
            ToggleFourthRoomThermalWalls(true);
        }

        #endregion

        #region Fourth Room
        [Header("Fourth Room")]
        public  GameObject      innerWalls;
        public  Camera          cameraToSpaceShip;
        public  GameObject      finalCameraGO;
        public  Renderer        fourthRoomScreen;
        public  NormandyControl normandyControl;
        public  GameObject      finalWall;
        public  GameObject      shipDoor;
        public  GameObject      blockingDoor;
        public  Transform       blockingDoorCameraTransform;
        public  Transform       shipDoorCameraTransform;
        private Renderer[]      thermalWallsRenderer;
        private Renderer        blockingDoorRenderer;

		#region DEBUG FUNCTION It's only called if GamePlayHelper calss is active in the scene
        public void SolveFourthPuzzle() {
            normandyControl.ActivateCollider();
            EndGame(true);
        }
        #endregion

        public void ToggleFourthRoomThermalWalls(bool toggle) {
            var c = blockingDoorRenderer.material.color;
            c.a   = 1;
            blockingDoorRenderer.material.color = c;
            blockingDoorRenderer.enabled        = toggle;
            for (int i = 0; i < thermalWallsRenderer.Length; i++) {
                var cor = thermalWallsRenderer[i].material.color;
                cor.a   = 1;
                thermalWallsRenderer[i].material.color = cor;
                thermalWallsRenderer[i].enabled        = toggle;
            }

			#if UNITY_WEBGL
            	StopAllCoroutines();
            	if (!stopAnimatingWalls) StartCoroutine(MakeWallsInvisible());
            #endif
        }

        #region Function to simulate something similar to the Thermal Vision when running on the Web
		#if UNITY_WEBGL
		    private bool stopAnimatingWalls = false;

		    public IEnumerator MakeWallsInvisible() {
		        var deltaTime = new WaitForSeconds(GameConstants.WALL_INVISIBLE_DELTA_TIME);
		        while (true) {
		            for (int i = 0; i < thermalWallsRenderer.Length; i++) {
		                var cor = thermalWallsRenderer[i].material.color;
		                cor.a  -= GameConstants.WALLS_INVISIBLE_SPEED;
		                thermalWallsRenderer[i].material.color = cor;
		            }

		            var c = blockingDoorRenderer.material.color;
		            c.a  -= GameConstants.WALLS_INVISIBLE_SPEED;
		            blockingDoorRenderer.material.color = c;

		            if (thermalWallsRenderer[0].material.color.a <= 0) break;
		            yield return deltaTime;
		        }
		        yield return null;
		    }
        #endif
        #endregion

        private void SetupFourthRoom() {
			#if UNITY_WEBGL
            	stopAnimatingWalls = false;
            #endif

            fourthRoomScreen.enabled = false;
            blockingDoorRenderer     = blockingDoor.GetComponent<Renderer>();
            thermalWallsRenderer     = innerWalls.GetComponentsInChildren<Renderer>();
            ToggleFourthRoomThermalWalls(false);
        }

        public void FinishFourthPuzzle() {
            OutputInfoForThePlayer(GAME_SOUNDS.SCREEN_TOUCHED_SOUND, HUD_MESSAGES.GOTO_THE_SHIP);
           
			#if UNITY_WEBGL
            	stopAnimatingWalls = true;
            #endif

            ToggleFourthRoomThermalWalls(true);

            canPause              = false;
            playerControl.enabled = false;
            playerCamera.enabled  = false;
            StartCoroutine(ShipCutScene());
        }

        public IEnumerator ShipCutScene() {
            var puzzleSuccessTime   = new WaitForSeconds(GameConstants.PUZZLE_SUCCESS_YIELD_TIME);
            var puzzleDeltaTime     = new WaitForSeconds(GameConstants.PUZZLE_DELTA_TIME);
            var puzzleDelta3Times   = new WaitForSeconds(3 * GameConstants.PUZZLE_DELTA_TIME);
            var puzzleResetTime     = new WaitForSeconds(GameConstants.PUZZLE_SUCCESS_RESET_TIME);

            cameraToSpaceShip.transform.position = blockingDoorCameraTransform.position;
            cameraToSpaceShip.transform.rotation = blockingDoorCameraTransform.rotation;
            cameraToSpaceShip.enabled            = true;
            yield return puzzleSuccessTime;
            blockingDoor.SetActive(false);

            yield return puzzleSuccessTime;
            cameraToSpaceShip.transform.position = shipDoorCameraTransform.position;
            cameraToSpaceShip.transform.rotation = shipDoorCameraTransform.rotation;

            yield return puzzleDelta3Times;
            shipDoor.SetActive(false);

            yield return puzzleResetTime;
            playerControl.enabled     = true;
            playerCamera.enabled      = true;
            cameraToSpaceShip.enabled = false;
            canPause                  = true;
            normandyControl.ActivateCollider();
            countDown.Toggle(true);
        }
        #endregion
    }
}