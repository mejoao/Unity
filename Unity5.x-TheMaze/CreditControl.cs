using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

namespace TheMaze {
    public class CreditControl : MonoBehaviour {
        [System.Serializable]
        public class CreditLine {
            public string area;
            public string[] authors;
            public CreditLine() { }
        }

        public GUISkin       areaSkin;
        public GUISkin       authorSkin;
        public CreditLine[]  creditsElements;
        private float        py;
        private float        timer = 0;
        private float        halfWidth;
        private List<string> labelTexts;
        private List<float>  labelWidth;
        private List<float>  labelHeights;
        private Animator     anim;
        private bool         rollCredits;

        private Dictionary<string, string[]> creditDictionary;


        public void FinishFadeOut() {
            anim.enabled = false;
        }

        public void StartCredits() {
            anim.enabled = true;
            rollCredits  = true;
            enabled      = true;
        }

        public void Awake() {
            anim      = GetComponent<Animator>();
            py        = Screen.height;
            halfWidth = Screen.width * 0.5F;
            
            if (anim != null) {
                anim.enabled = false;
                rollCredits  = false;
            } else {
                rollCredits = true;
            }
            
            creditDictionary = new Dictionary<string, string[]>();
            for (int j = 0; j < creditsElements.Length; j++)
                creditDictionary.Add(creditsElements[j].area, creditsElements[j].authors);

            labelTexts   = new List<string>();
            labelWidth   = new List<float>();
            labelHeights = new List<float>();

            foreach (KeyValuePair<string, string[]> pair in creditDictionary) {
                labelTexts.Add(pair.Key);
                labelWidth.Add(pair.Key.Length * GameConstants.CREDITS_LETTER_WIDTH);
                labelHeights.Add(GameConstants.CREDITS_LINE_HEIGHT);
                labelTexts.Add(System.String.Join("\n", pair.Value)); // Make Multiple Lines;
                labelWidth.Add(BiggestString(pair.Value) * GameConstants.CREDITS_LETTER_WIDTH);
                labelHeights.Add(pair.Value.Length * GameConstants.CREDITS_LINE_HEIGHT);
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
            if (!rollCredits) return;

            timer += Time.deltaTime;
            if (timer >= GameConstants.CREDITS_VERTICAL_TIME) {
                timer = 0;
                py -= GameConstants.CREDITS_VERTICAL_SPACE;
            }

            if (Input.anyKey) SceneManager.LoadScene(0);
        }

        public void OnGUI() {
            if (!rollCredits) return;

            float deltaY = 0;
            for (int i = 0; i < labelTexts.Count; i++) {
                if (i % 2 == 0) { // Area
                    GUI.Label(new Rect(halfWidth - labelWidth[i] * 0.5F, py + deltaY, labelWidth[i], labelHeights[i]), labelTexts[i], areaSkin.label);
                    deltaY += labelHeights[i] + GameConstants.CREDITS_OFFSET_ELEMENT;
                } else {          // Author, maybe multiple lines
                    GUI.Label(new Rect(halfWidth - labelWidth[i] * 0.5F, py + deltaY, labelWidth[i], labelHeights[i]), labelTexts[i], authorSkin.label);
                    deltaY += labelHeights[i] + GameConstants.CREDITS_OFFSET_GROUP;
                }
            }

            if (py + deltaY < 0) SceneManager.LoadScene(0);
        }
    }
}
