using UnityEngine;
using UnityEngine.UI;

namespace TheMaze {
    public class FinalCountDownControl : MonoBehaviour {
        public  GameControl gc;
        private Text        text;
        private float       timer;

        public void Toggle(bool toggle) {
            text.enabled = toggle;
            enabled      = toggle;
        }

        public void Awake() {
            text  = GetComponent<Text>();
            timer = GameConstants.END_GAME_COUNT_DOWN;
        }
        
        public void Update() {
            timer -= Time.deltaTime;
            if (timer <= 0) {
                gc.EndGame(false);
                return;
            }

            text.text = string.Format("{0:00}", timer);  
        }
    }
}
