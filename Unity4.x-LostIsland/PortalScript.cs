using UnityEngine;
using System.Collections;

public class PortalScript : MonoBehaviour {
	public enum PortalNumber { 
		One, 
		Two, 
		Three, 
		Four,
		InternalOne,
		InternalTwo,
		InternalThree,
		InternalFour
	};

	public PortalNumber portalNumber = PortalNumber.One;


    public void OnTriggerEnter(Collider hit) {
        if (hit.tag == GameValues.PLAYER_TAG) {

			if (!hit.gameObject.GetComponent<PlayerCollisionControl>().HasWeapon()) {
				GameObject.FindWithTag(GameValues.HUD_CONTROL_TAG).GetComponent<HudControl>().SetHudMessage(GameValues.HUD_MESSAGE_PLAYER_WITH_NO_WEAPON);
				return;
			}

			switch (portalNumber) {
				case PortalNumber.One : 
					GameObject.FindWithTag(GameValues.GAME_CONTROL_TAG).GetComponent<GameControl>().StartFirstMiniGame(); 
				break;

				case PortalNumber.Two : 
					GameObject.FindWithTag(GameValues.GAME_CONTROL_TAG).GetComponent<GameControl>().StartSecondMiniGame(); 
				break;

				case PortalNumber.Three : 
					GameObject.FindWithTag(GameValues.GAME_CONTROL_TAG).GetComponent<GameControl>().StartThirdMiniGame(); 
				break;

				case PortalNumber.Four : 
					GameObject.FindWithTag(GameValues.GAME_CONTROL_TAG).GetComponent<GameControl>().StartFourthMiniGame(); 
				break;

				case PortalNumber.InternalOne :
					if (hit.gameObject.GetComponent<PlayerCollisionControl>().HasNewKey()) {
						hit.gameObject.GetComponent<PlayerCollisionControl>().ResetNewKey();
						GameObject.FindWithTag(GameValues.GAME_CONTROL_TAG).GetComponent<GameControl>().EndFirstMiniGame();
					} else {
						GameObject.FindWithTag(GameValues.HUD_CONTROL_TAG).GetComponent<HudControl>().SetHudMessage(GameValues.HUD_MESSAGE_PORTAL_LOCKED);
					}
				break;

				case PortalNumber.InternalTwo :
					if (hit.gameObject.GetComponent<PlayerCollisionControl>().HasNewKey()) {
						hit.gameObject.GetComponent<PlayerCollisionControl>().ResetNewKey();
						GameObject.FindWithTag(GameValues.GAME_CONTROL_TAG).GetComponent<GameControl>().EndSecondMiniGame();
					} else {
						GameObject.FindWithTag(GameValues.HUD_CONTROL_TAG).GetComponent<HudControl>().SetHudMessage(GameValues.HUD_MESSAGE_PORTAL_LOCKED);
					}
				break;

				case PortalNumber.InternalThree :
					if (hit.gameObject.GetComponent<PlayerCollisionControl>().HasNewKey()) {
						hit.gameObject.GetComponent<PlayerCollisionControl>().ResetNewKey();
						GameObject.FindWithTag(GameValues.GAME_CONTROL_TAG).GetComponent<GameControl>().EndThirdMiniGame();
					} else {
						GameObject.FindWithTag(GameValues.HUD_CONTROL_TAG).GetComponent<HudControl>().SetHudMessage(GameValues.HUD_MESSAGE_PORTAL_LOCKED);
					}
				break;

				case PortalNumber.InternalFour :
					if (hit.gameObject.GetComponent<PlayerCollisionControl>().HasNewKey()) {
						hit.gameObject.GetComponent<PlayerCollisionControl>().ResetNewKey();
						GameObject.FindWithTag(GameValues.GAME_CONTROL_TAG).GetComponent<GameControl>().EndFourthMiniGame();
					} else {
						GameObject.FindWithTag(GameValues.HUD_CONTROL_TAG).GetComponent<HudControl>().SetHudMessage(GameValues.HUD_MESSAGE_PORTAL_LOCKED);
					}
				break;

				default : print("ALIEN QUEEN BUG: Portal Teleport should never reach default clause"); break;
			}
        }
    }
}
