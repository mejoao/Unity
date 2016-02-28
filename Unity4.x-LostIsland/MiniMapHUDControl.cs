using UnityEngine;
using System.Collections;

public class MiniMapHUDControl : MonoBehaviour {

	public RenderTexture miniMapTexture;
	public Material      miniMapMaterial;

	public void OnGUI() {
		if (Event.current.type == EventType.Repaint) {
			Graphics.DrawTexture(new Rect (Screen.width - GameValues.MINI_MAP_WIDTH - GameValues.MINI_MAP_OFFSET, 0,
		                                   GameValues.MINI_MAP_WIDTH, GameValues.MINI_MAP_HEIGHT), 
		                         miniMapTexture,
		                         miniMapMaterial);
		}
	}

}
