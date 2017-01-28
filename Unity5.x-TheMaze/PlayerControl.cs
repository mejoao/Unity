using UnityEngine;
using System.Collections;

namespace TheMaze {
    public class PlayerControl : MonoBehaviour {
        #region Generic Variables
        [Header("Generic Variables")]
        public  Camera       myCam;
        public  GameControl  gameControl;
        public  SoundControl soundControl;
        public  Transform    spawnPoint;
        private Transform    trans;
        private Rigidbody    rb;
        #endregion

        #region HUDMessages        
        [Space]
        [Header("HUD Messages")]
        public HUDMessages hud;
        #endregion

        #region FirstRoom Variables
        [Space]
        [Header("First")]
        public Transform firstRoomResetPoint;
        #endregion

        #region Second Room Variables
        [Space]
        [Header("Second")]
        public Transform secondRoomHallResetPoint;
        public Transform secondRoomBeamResetPoint;
        public Transform secondRoomHammerResetPont;
        #endregion

        #region Third Room Variables
        [Space]
        [Header("Third")]
        public  ThermalVision camThermalVision;
        private bool          thermalVisonOn;
        private bool          canUseThermalVision;

        public Transform thirdRoomFirstResetPoint;
        public Transform thirdRoomSecondResetPoint;
        public Transform thirdRoomThirdResetPoint;
        public Transform thirdRoomHardResetPoint;
        #endregion

        #region DEBUG FUNCTION It's only called if GamePlayHelper calss is active in the scene
        public void AllowThermalVision() {
            canUseThermalVision      = true;
            thermalVisonOn           = true;
			#if !UNITY_WEBGL
            	camThermalVision.enabled = thermalVisonOn;
            #endif
            gameControl.FinishThirdRoomPuzzle();
        }
        #endregion

        public void Awake() {
            rb                   = GetComponent<Rigidbody>();
            trans                = GetComponent<Transform>();
            thermalVisonOn       = false;
            canUseThermalVision  = false;
        }

        public void Update() {
            #region ThermalVision
            if (Input.GetKeyDown(KeyCode.E) && canUseThermalVision) {
				#if !UNITY_WEBGL
                	thermalVisonOn           = !thermalVisonOn;
                	camThermalVision.enabled = thermalVisonOn;
                #else
                	thermalVisonOn = true;
                #endif
                gameControl.ToggleFourthRoomThermalWalls(thermalVisonOn);
            }
            #endregion
        }

        public void OnCollisionEnter(Collision hit) {
            #region First Room
            if (hit.gameObject.CompareTag(GameConstants.FIRST_ROOM_SCREEN_TAG)) {
                hit.gameObject.tag = GameConstants.USED_SCREEN_TAG;
                gameControl.FirstRoomUpdatePuzzle();
                return;
            }
            #endregion

            #region Second Room
            if (hit.gameObject.CompareTag(GameConstants.SECOND_ROOM_DOOR_TAG)) {
                hit.gameObject.GetComponent<Animator>().enabled = true;
                hit.gameObject.GetComponent<Collider>().enabled = false;
                gameControl.StartSecondRoomPuzzle();
                return;
            }

            if (hit.gameObject.CompareTag(GameConstants.SECOND_ROOM_LASER_TAG)) {
                ResetPlayer(secondRoomHallResetPoint, 
                            GAME_SOUNDS.BEAM_CAUGHT_SOUND, 
                            HUD_MESSAGES.BEAM_CAUGHT);
                return;
            }

            if (hit.gameObject.CompareTag(GameConstants.SECOND_ROOM_BEAM_TAG)) {
                ResetPlayer(secondRoomBeamResetPoint,
                            GAME_SOUNDS.BEAM_CAUGHT_SOUND, 
                            HUD_MESSAGES.BEAM_CAUGHT);
                return;
            }

            if (hit.gameObject.CompareTag(GameConstants.SECOND_ROOM_HAMMER_TAG)) {
                ResetPlayer(secondRoomHammerResetPont, 
                            GAME_SOUNDS.HAMMER_SMASH_SOUND, 
                            HUD_MESSAGES.HAMMER_SMASHED);
                return;
            }

            if (hit.gameObject.CompareTag(GameConstants.SECOND_ROOM_SCREEN_TAG)) {
                hit.gameObject.tag = GameConstants.USED_SCREEN_TAG;
                gameControl.SecondRoomPuzzleSolved();
                return;
            }
            #endregion

            #region Third Room
            if (hit.gameObject.CompareTag(GameConstants.THIRD_ROOM_FIRST_TRAP_DOOR_TAG)) {
                ResetPlayer(thirdRoomFirstResetPoint, 
                            GAME_SOUNDS.DOOR_TRAP_COLLISION_SOUND, 
                            HUD_MESSAGES.DOOR_TRAP_COLLISION);
                return;
            }

            if (hit.gameObject.CompareTag(GameConstants.THIRD_ROOM_SECOND_TRAP_DOOR_TAG)) {
                ResetPlayer(thirdRoomSecondResetPoint,
                            GAME_SOUNDS.DOOR_TRAP_COLLISION_SOUND,
                            HUD_MESSAGES.DOOR_TRAP_COLLISION);
                return;
            }

            if (hit.gameObject.CompareTag(GameConstants.THIRD_ROOM_THIRD_TRAP_DOOR_TAG)) {
                ResetPlayer(thirdRoomThirdResetPoint,
                            GAME_SOUNDS.DOOR_TRAP_COLLISION_SOUND,
                            HUD_MESSAGES.DOOR_TRAP_COLLISION);
                return;
            }

            if (hit.gameObject.CompareTag(GameConstants.THIRD_ROOM_HARD_TRAP_DOOR_TAG)) {
                ResetPlayer(thirdRoomFirstResetPoint,
                            GAME_SOUNDS.DOOR_TRAP_COLLISION_SOUND,
                            HUD_MESSAGES.DOOR_TRAP_COLLISION);
                return;
            }

            if (hit.gameObject.CompareTag(GameConstants.THIRD_ROOM_HARD_TRAP_TAG)) {
                ResetPlayer(thirdRoomHardResetPoint,
                            GAME_SOUNDS.BEAM_CAUGHT_SOUND,
                            HUD_MESSAGES.DOOR_TRAP_COLLISION);
                return;
            }
            #endregion

            #region Fourth Room
            if (hit.gameObject.CompareTag(GameConstants.FOURTH_ROOM_SCREEN_TAG)) {
                hit.gameObject.tag       = GameConstants.USED_SCREEN_TAG;
                canUseThermalVision      = false;
                thermalVisonOn           = false;
				#if !UNITY_WEBGL
                	camThermalVision.enabled = false;
                #endif
                gameControl.FinishFourthPuzzle();
            }
            #endregion
        }
        
        public void OnTriggerEnter(Collider hit) {
            if (hit.gameObject.CompareTag(GameConstants.MASK_TAG)) {
                hit.gameObject.SetActive(false);
                thermalVisonOn      = true;
                canUseThermalVision = true;
				#if !UNITY_WEBGL
                	camThermalVision.enabled = true;
                #endif
                gameControl.FinishThirdRoomPuzzle();
                return;
            }

            if (hit.gameObject.CompareTag(GameConstants.SAW_TRAP_TAG)) {
                ResetPlayer(firstRoomResetPoint, 
                            GAME_SOUNDS.SAW_CAUGHT_SOUND, 
                            HUD_MESSAGES.SAW_CAUGHT);
                return;
            }

            if (hit.gameObject.CompareTag(GameConstants.TURRET_BULLET_TAG)) {
                ResetPlayer(spawnPoint, 
                            GAME_SOUNDS.TURRET_BULLET_HIT_SOUND, 
                            HUD_MESSAGES.BULLET_HIT);
                return;
            }

            if (hit.gameObject.CompareTag(GameConstants.NORMANDY_TAG)) {
                gameControl.EndGame(true);
            }
        }

        public void CaughtByCamera() {
            ResetPlayer(spawnPoint, 
                        GAME_SOUNDS.CAMERA_CAUGHT_SOUND, 
                        HUD_MESSAGES.CAMERA_CAUGHT);
        }

        public void ResetPlayer(Transform t, GAME_SOUNDS soundToPlay, HUD_MESSAGES message) {
            rb.velocity    = Vector3.zero;
            trans.position = t.position;
            trans.rotation = t.rotation;
            gameControl.OutputInfoForThePlayer(soundToPlay, message);
        }
    }
}