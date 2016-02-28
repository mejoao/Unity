using UnityEngine;
using System.Collections;

public class HowToPlayScreen : MonoBehaviour {

	public Texture2D howToPlay;
	private float px, py;


	public void Start() {
		px = GameValues.SCREEN_WIDTH  * 0.5F - howToPlay.width  * GameValues.HOW_TO_PLAY_IMAGE_WIDTH * 0.5F;
		py = GameValues.SCREEN_HEIGHT * 0.5F - howToPlay.height * GameValues.HOW_TO_PLAY_IMAGE_HEIGHT * 0.5F;
	}


	void Update () {
		if (Input.anyKey) Application.LoadLevel(2);
	}


	public void OnGUI() {
		GUI.DrawTexture(new Rect(px, py, howToPlay.width * 0.5F, howToPlay.height * 0.5F), howToPlay);
	}
}
