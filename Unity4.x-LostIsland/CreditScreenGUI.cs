using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreditScreenGUI : MonoBehaviour {
	public  bool                         loop;
	public  GUISkin                      areaSkin;
	public  GUISkin                      authorSkin;
	public  CreditLine[]                 creditsElements;
	private float                        py;
	private int                          numberOfCreditsShown;
	private float                        timer  = 0;
	private float                        halfWidth;
	private List<string>                 labelTexts;
	private List<float>                  labelWidth;
	private List<float>                  labelHeights;
	private Dictionary<string, string[]> creditDictionary;


	public void Start() {
		halfWidth            = Screen.width * 0.5F;
		py                   = Screen.height;
		numberOfCreditsShown = 0;

		creditDictionary = new Dictionary<string, string[]>();
		for (int j = 0; j < creditsElements.Length; j++)
			creditDictionary.Add(creditsElements[j].area, creditsElements[j].authors);

		labelTexts   = new List<string>();
		labelWidth   = new List<float> ();
		labelHeights = new List<float>();
		foreach (KeyValuePair<string, string[]> pair in creditDictionary) {
			labelTexts.Add(pair.Key);
			labelWidth.Add(pair.Key.Length * GameValues.CREDITS_LETTER_WIDTH);
			labelHeights.Add(GameValues.CREDITS_LINE_HEIGHT);
			labelTexts.Add(System.String.Join("\n", pair.Value)); // Make Multiple Lines;
			labelWidth.Add(BiggestString(pair.Value) * GameValues.CREDITS_LETTER_WIDTH);
			labelHeights.Add(pair.Value.Length * GameValues.CREDITS_LINE_HEIGHT);
		}
	}


	private float BiggestString(string[] authors) {
		float len = 0;
		for (int i = 0; i < authors.Length; i++) {
			if (authors[i].Length > len) len = authors[i].Length;
		}
		return len;
	}


	public void Update() {
		timer += Time.deltaTime;
		if (timer >= GameValues.CREDITS_VERTICAL_TIME) {
			timer = 0;
			py -= GameValues.CREDITS_VERTICAL_SPACE;
		}

		if (Input.anyKey && numberOfCreditsShown > 0) Application.LoadLevel(0);
	}


	public void OnGUI() {
		float deltaY = 0;
		for (int i = 0; i < labelTexts.Count; i++) {
			if (i % 2 == 0) { // Area
				GUI.Label(new Rect(halfWidth - labelWidth[i] * 0.5F, py + deltaY, labelWidth[i], labelHeights[i]), labelTexts[i], areaSkin.label);
				deltaY += labelHeights[i] + GameValues.CREDITS_OFFSET_ELEMENT;
			} else {          // Author, maybe multiple lines
				GUI.Label(new Rect(halfWidth - labelWidth[i] * 0.5F, py + deltaY, labelWidth[i], labelHeights[i]), labelTexts[i], authorSkin.label);
				deltaY += labelHeights[i] + GameValues.CREDITS_OFFSET_GROUP;
			}
		}

		if (py + deltaY < 0) {
			if (loop) {
				py = Screen.height;
				numberOfCreditsShown++;
			} else 
				Application.LoadLevel(0);
		} 
	}


	[System.Serializable]
	public class CreditLine{
		public string   area;
		public string[] authors;
		public CreditLine() {}
	}

}
