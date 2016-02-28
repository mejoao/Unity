using UnityEngine;
using System.Collections;

public class PlayerCollisionControl : MonoBehaviour {

	public GameObject weapon;
	public GameObject hudControl;
	public GameObject soundControl;
	public Transform  spawnPoint;
	public Transform  turret1ResetPoint;
	public Transform  turret2ResetPoint;
	public Transform  turret3ResetPoint;
	public Transform  turret4ResetPoint;
	public Transform  electricRoomFloorResetPoint;
	public Transform  electricRoomUperFloorResetPoint;
	public Transform  trapsRoomGuillotineResetPoint;
	public Transform  trapsRoomShurikenResetPoint;
	public Transform  puzzleRoomColumnResetPoint;
	public Transform  puzzleRoomPlatformsResetPoint;

	private bool      hasNewKey;
	private bool      applyDamage;
	private float     currentDamageValue;
	private float     timer;
	private float     currentDamageFrequency;
	private Transform currentResetPoint;
	private enum Challange { Turret1, Turret2, Turret3, Turret4, TrapsRoom, ElectricRoom } ;
	private Challange currentChallenge;


	public void Start() {
		hudControl   = GameObject.FindWithTag(GameValues.HUD_CONTROL_TAG);
		weapon.SetActive(false);
	}


	public bool HasNewKey()   { return hasNewKey ; }
	public void ResetNewKey() { hasNewKey = false; }


	public void Update() {
		if (!applyDamage) return;

		timer += Time.deltaTime;
		if (timer >= currentDamageFrequency) {
			timer = 0;

			if (currentChallenge == Challange.ElectricRoom) {
				soundControl.GetComponent<SoundControl>().PlaySound(GameValues.ELECTRIC_ROOM_DAMAGE_SOUND_ID);
			}

			hudControl.GetComponent<HudControl>().ApplyDamage(currentDamageValue);

			// HP is related to the alpha channel of an image. If it's > 1, 
			//   damage is 100% and HP is zero
			if (hudControl.GetComponent<HudControl>().GetPlayerHP() >= 1) {
				transform.position = currentResetPoint.position;
				hudControl.GetComponent<HudControl>().RestorePlayerHP();
				ResetCurrentChallange();
			}
		}
	}


	public bool HasWeapon() { return weapon.activeInHierarchy;	}


	private void ResetCurrentChallange() {
		applyDamage = false;
		switch (currentChallenge) {
			case Challange.Turret1 : 
				GameObject.FindWithTag(GameValues.GAME_CONTROL_TAG).GetComponent<GameControl>().ResetFirstTurrets();
			break;

			case Challange.ElectricRoom:
				GameObject.FindWithTag(GameValues.GAME_CONTROL_TAG).GetComponent<GameControl>().ResetFourthMiniGame();
			break;

			default : break;
		}
	}


	public void ApplyFireBallDamage(float damage) {
		hudControl.GetComponent<HudControl>().ApplyDamage(GameValues.FIRE_BALL_DAMAGE);

		if (hudControl.GetComponent<HudControl>().GetPlayerHP() >= 1) {
			currentChallenge   = Challange.Turret3;
			transform.position = turret3ResetPoint.position;
			hudControl.GetComponent<HudControl>().RestorePlayerHP();
			ResetCurrentChallange();
		}
	}


	public void ApplyShockWaveDamage(float damage) {
		hudControl.GetComponent<HudControl>().ApplyDamage(damage);
		if (hudControl.GetComponent<HudControl>().GetPlayerHP() >= 1) {
			currentChallenge   = Challange.Turret4; 
			transform.position = turret4ResetPoint.position;
			hudControl.GetComponent<HudControl>().RestorePlayerHP();
			ResetCurrentChallange();
		}
	}


	public void OnCollisionEnter(Collision hit) {
		if (hit.gameObject.tag == GameValues.ELECTRIC_ROOM_TILE_TAG) {
			currentChallenge       = Challange.ElectricRoom;
			timer                  = 0;
			applyDamage            = true;
			currentDamageValue     = GameValues.ELECTRIC_ROOM_DAMAGE;
			currentDamageFrequency = GameValues.ELECTRIC_ROOM_DAMAGE_FREQUENCY;
			currentResetPoint      = electricRoomFloorResetPoint;
			soundControl.GetComponent<SoundControl>().PlaySound(GameValues.ELECTRIC_ROOM_DAMAGE_SOUND_ID);

			hudControl.GetComponent<HudControl>().ApplyDamage(currentDamageValue);
			
			if (hudControl.GetComponent<HudControl>().GetPlayerHP() >= 1) {
				transform.position = currentResetPoint.position;
				hudControl.GetComponent<HudControl>().RestorePlayerHP();
				ResetCurrentChallange();
			}
		}

		
	}

	public void OnCollisionExit(Collision hit) {
		if (hit.gameObject.tag == GameValues.ELECTRIC_ROOM_TILE_TAG) {
			applyDamage = false;
		}
	}



	public void OnTriggerEnter(Collider hit) {
		if (hit.tag == GameValues.SEA_RESET_TAG) {
			transform.position = spawnPoint.position;
			transform.rotation = spawnPoint.rotation;
			soundControl.GetComponent<SoundControl>().PlaySound(GameValues.RESET_SOUND_ID);
			return;
		}

		if (hit.gameObject.tag == GameValues.WEAPON_TAG) {
			weapon.SetActive(true);
			GameObject.Destroy(hit.gameObject);
			soundControl.GetComponent<SoundControl>().PlaySound(GameValues.WEAPON_PICKUP_SOUND_ID);
			hudControl.GetComponent<HudControl>().ShowAmmo();
			return;
		}

		if (hit.tag == GameValues.TURRET_FLAME_TAG) {
			currentChallenge       = Challange.Turret1;
			timer                  = 0;
			applyDamage            = true;
			currentDamageValue     = GameValues.FLAME_DAMAGE;
			currentDamageFrequency = GameValues.FLAME_DAMAGE_FREQUENCY;
			currentResetPoint      = turret1ResetPoint;
			return;
		}

		if (hit.tag == GameValues.TURRET_PHOTON_TORP_TAG) {
			hudControl.GetComponent<HudControl>().ApplyDamage(GameValues.PHOTON_DAMAGE);
			
			if (hudControl.GetComponent<HudControl>().GetPlayerHP() >= 1) {
				currentChallenge   = Challange.Turret2;
				transform.position = turret2ResetPoint.position;
				hudControl.GetComponent<HudControl>().RestorePlayerHP();
				ResetCurrentChallange();
			}
			return;
		}

		if (hit.tag == GameValues.BLACK_BIRD_LASER_TAG) {
			hudControl.GetComponent<HudControl>().ApplyDamage(GameValues.BLACK_BIRD_LASER_DAMAGE);
			soundControl.GetComponent<SoundControl>().PlaySound(GameValues.LASER_COLLISION_SOUND_ID);

			if (hudControl.GetComponent<HudControl>().GetPlayerHP() >= 1) {
				transform.position = spawnPoint.position;
				soundControl.GetComponent<SoundControl>().PlaySound(GameValues.LASER_COLLISION_SOUND_ID);
				hudControl.GetComponent<HudControl>().RestorePlayerHP();

			}
			return;
		}

		if (hit.tag == GameValues.ELECTRIC_ROOM_TILE_TAG) {
			currentChallenge       = Challange.ElectricRoom;
			timer                  = 0;
			applyDamage            = true;
			currentDamageValue     = GameValues.ELECTRIC_ROOM_DAMAGE;
			currentDamageFrequency = GameValues.ELECTRIC_ROOM_DAMAGE_FREQUENCY;
			currentResetPoint      = electricRoomFloorResetPoint;
			return;
		}

		if (hit.tag == GameValues.ELECTRIC_ROOM_FLOOR_LASER_TAG) {
			hudControl.GetComponent<HudControl>().ApplyDamage(GameValues.BLACK_BIRD_LASER_DAMAGE);
			soundControl.GetComponent<SoundControl>().PlaySound(GameValues.LASER_COLLISION_SOUND_ID);

			if (hudControl.GetComponent<HudControl>().GetPlayerHP() >= 1) {
				currentChallenge   = Challange.ElectricRoom;
				transform.position = electricRoomFloorResetPoint.position;
				hudControl.GetComponent<HudControl>().RestorePlayerHP();
				ResetCurrentChallange();
			}
			return;
		}

		if (hit.tag == GameValues.ELECTRIC_ROOM_TOP_LASER_TAG) {
			hudControl.GetComponent<HudControl>().ApplyDamage(GameValues.BLACK_BIRD_LASER_DAMAGE);
			soundControl.GetComponent<SoundControl>().PlaySound(GameValues.LASER_COLLISION_SOUND_ID);
			
			if (hudControl.GetComponent<HudControl>().GetPlayerHP() >= 1) {
				currentChallenge   = Challange.ElectricRoom;
				transform.position = electricRoomUperFloorResetPoint.position;
				hudControl.GetComponent<HudControl>().RestorePlayerHP();
				ResetCurrentChallange();
			}
			return;
		}


		if (hit.tag == GameValues.TRAPS_ROOM_GUILLOTINE_TAG) {
			transform.rotation = trapsRoomGuillotineResetPoint.rotation;
			transform.position = trapsRoomGuillotineResetPoint.position;
			rigidbody.velocity = Vector3.zero;
			soundControl.GetComponent<SoundControl>().PlaySound(GameValues.RESET_SOUND_ID);
			return;
		}

		if (hit.tag == GameValues.TRAPS_ROOM_SHURIKEN_TAG) {
			transform.rotation = trapsRoomShurikenResetPoint.rotation;
			transform.position = trapsRoomShurikenResetPoint.position;
			rigidbody.velocity = Vector3.zero;
			soundControl.GetComponent<SoundControl>().PlaySound(GameValues.RESET_SOUND_ID);
			return;
		}

		if (hit.tag == GameValues.PUZZLE_ROOM_C_LAVA_FLOOR_TAG) {
			transform.rotation = puzzleRoomColumnResetPoint.rotation;
			transform.position = puzzleRoomColumnResetPoint.position;
			rigidbody.velocity = Vector3.zero;
			soundControl.GetComponent<SoundControl>().PlaySound(GameValues.RESET_SOUND_ID);
			return;
		}

		if (hit.tag == GameValues.PUZZLE_ROOM_P_LAVA_FLOOR_TAG) {
			transform.rotation = puzzleRoomPlatformsResetPoint.rotation;
			transform.position = puzzleRoomPlatformsResetPoint.position;
			rigidbody.velocity = Vector3.zero;
			soundControl.GetComponent<SoundControl>().PlaySound(GameValues.RESET_SOUND_ID);
			return;
		}

		if (hit.tag == GameValues.FIRST_AID_TAG) {
			GameObject.Destroy(hit.gameObject);
			soundControl.GetComponent<SoundControl>().PlaySound(GameValues.FIRST_AID_PICKUP_SOUND_ID);
			return;
		}

		if (hit.tag == GameValues.AMMO_TAG) {
			GameObject.Destroy(hit.gameObject);
			soundControl.GetComponent<SoundControl>().PlaySound(GameValues.AMMO_PICKUP_SOUND_ID);
		}


		if (hit.tag == GameValues.KEY_TAG) {
			hasNewKey = true;
			hit.gameObject.SetActive(false);
			hudControl.GetComponent<HudControl>().SetHudMessage(GameValues.HUD_MESSAGE_NEW_ANK);
			hudControl.GetComponent<HudControl>().IncreaseNumberOfKeys();
			soundControl.GetComponent<SoundControl>().PlaySound(GameValues.KEY_PICKUP_SOUND_ID);
			return;
		}

		if (hit.tag == GameValues.TURN_OFF_MINI_MAP_TAG) {
			hudControl.GetComponent<HudControl>().InvertMiniMapStatus();
		}

		if (hit.gameObject.tag == GameValues.AIRPLANE_STUB_TAG) {
			print ("Colisao com trigger de AirWolf");
			if (hudControl.GetComponent<HudControl>().GetNumberOfKeys() >= GameValues.MAX_NUMBER_OF_KEYS)
				GameObject.FindWithTag(GameValues.GAME_CONTROL_TAG).GetComponent<GameControl>().StartAirPlanePhase();
		}
	}


	public void OnTriggerExit(Collider hit) {
		if (hit.tag == GameValues.TURRET_FLAME_TAG) {
			applyDamage = false;
		}

		if (hit.tag == GameValues.ELECTRIC_ROOM_TILE_TAG) {
			applyDamage = false;
		}
	}
}
