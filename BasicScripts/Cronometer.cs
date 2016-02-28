using UnityEngine;
using UnityEngine.UI;

public class Cronometer : MonoBehaviour {

	public  Text  hudTimer;
	public  float maxTime      = 120;
	public  bool  increaseTime = true;
	private float timer;
	private int   sign;

	void Start() {
		if (increaseTime) {
			timer = 0;
			sign  = 1;
		} else {
			timer = maxTime;
			sign  = -1;
		}
		hudTimer.text = "Zero";
	}
	

	void Update() {
		timer += sign * Time.deltaTime;
		int minutes   = (int)(timer / 60);
		int seconds   = (int)(timer % 60);
		hudTimer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
	} 
}
