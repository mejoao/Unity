using UnityEngine;
using System.Collections;

public class SoundControl : MonoBehaviour {

	public AudioClip teleport;
	public AudioClip firstaidPickup;
	public AudioClip keyPickup;
	public AudioClip weaponPickup;
	public AudioClip ammoPickup;
	public AudioClip menuOpen;
	public AudioClip menuClick;
	public AudioClip electricShock;
	public AudioClip laserCollision;
	public AudioClip reset;
	public AudioClip missle;
	public AudioClip explosion;
	
	public void PlaySound(int sound) {
		if (sound == GameValues.TELEPORT_SOUND_ID)             audio.PlayOneShot(teleport);	
		if (sound == GameValues.FIRST_AID_PICKUP_SOUND_ID)     audio.PlayOneShot(firstaidPickup);	
		if (sound == GameValues.KEY_PICKUP_SOUND_ID)           audio.PlayOneShot(keyPickup);
		if (sound == GameValues.WEAPON_PICKUP_SOUND_ID)        audio.PlayOneShot(weaponPickup);
		if (sound == GameValues.AMMO_PICKUP_SOUND_ID)          audio.PlayOneShot(ammoPickup);
		if (sound == GameValues.MENU_SOUND_ID)                 audio.PlayOneShot(menuOpen);
		if (sound == GameValues.MENU_CLICK_SOUND_ID)           audio.PlayOneShot(menuClick);
		if (sound == GameValues.ELECTRIC_ROOM_DAMAGE_SOUND_ID) audio.PlayOneShot(electricShock);
		if (sound == GameValues.LASER_COLLISION_SOUND_ID)      audio.PlayOneShot(laserCollision);
		if (sound == GameValues.RESET_SOUND_ID)                audio.PlayOneShot(reset);
		if (sound == GameValues.MISSLE_SOUND_ID)               audio.PlayOneShot(missle);
		if (sound == GameValues.EXPLOSION_SOUND_ID)            audio.PlayOneShot(explosion);
	}
}