using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class PauseMenu : MonoBehaviour {

  public  KeyCode pauseKey = KeyCode.Escape;
	public  string  levelToLoad;
	private bool    pause;

    public void Start() {
        pause          = false;
        Cursor.visible = false;

    }


  public void PauseGame() {
    Cursor.visible       = true;
  	Time.timeScale       = 0;
  	Time.fixedDeltaTime  = 0;

  		// Pause the other objects if needed
  }


  public void UnPauseGame() {
    Cursor.visible       = false;
  	Time.timeScale       = 1;
  	Time.fixedDeltaTime  = 1;

  	// Pause the other objects if needed
  }

  public void ReloadGame() {
  	paused         = false;
    Cursor.visible = false;
    Time.timeScale = 1;
    SceneManager.LoadScene(levelToLoad);
  }


  public void QuitGame() {
   	Time.timeScale      = 1;
   	Time.fixedDeltaTime = 1;
   	#if UNITY_EDITOR
   	  EditorApplication.isPlaying = false;
   	#endif
     	Application.Quit();
  	#endif
  }


  public void Update() {
    if (Input.GetKeyUp(pauseKey)) {
      pause = !pause;
      if (pause) {
  	   PauseGame();
      } else {
        UnPauseGame();
      }
    }
  }
}