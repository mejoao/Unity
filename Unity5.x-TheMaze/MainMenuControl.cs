using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.SceneManagement;

namespace TheMaze {
    public class MainMenuControl : MonoBehaviour {

        public void Awake() { Cursor.visible = true; }

        public void Start() { Cursor.visible = true; }

        public void NewGame() { SceneManager.LoadScene(2); }

        public void ShowCredits() { SceneManager.LoadScene(3); }

        public void QuitGame() {
			#if !UNITY_WEBGL
            	#if UNITY_EDITOR
            		EditorApplication.isPlaying = false;
            	#endif
            		Application.Quit();
			#endif
        }
    }
}
