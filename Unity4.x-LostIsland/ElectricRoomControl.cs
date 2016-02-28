using UnityEngine;
using System.Collections;

public class ElectricRoomControl : MonoBehaviour {

	public GameObject[] firstPath;
	public GameObject[] secondPath;
	public GameObject[] thirdPath;
	public GameObject[] fourthPath;

	private string pathActive;


	public void Start() {
		pathActive = "";
	}


	public void ActivatePath(string path) {
		if (path == pathActive) return;

		pathActive = path;

		if (path == "FirstPathTarget") {
			for (int i = 0; i < firstPath.Length;  i++) firstPath[i].GetComponent<ElectricTileControl>().ToggleTile(false);
			for (int i = 0; i < secondPath.Length; i++) secondPath[i].GetComponent<ElectricTileControl>().ToggleTile(true);
			for (int i = 0; i < thirdPath.Length;  i++) thirdPath[i].GetComponent<ElectricTileControl>().ToggleTile(true);
			for (int i = 0; i < fourthPath.Length; i++) fourthPath[i].GetComponent<ElectricTileControl>().ToggleTile(true);
			return;
		}

		if (path == "SecondPathTarget") {
			for (int i = 0; i < firstPath.Length;  i++) firstPath[i].GetComponent<ElectricTileControl>().ToggleTile(true);
			for (int i = 0; i < secondPath.Length; i++) secondPath[i].GetComponent<ElectricTileControl>().ToggleTile(false);
			for (int i = 0; i < thirdPath.Length;  i++) thirdPath[i].GetComponent<ElectricTileControl>().ToggleTile(true);
			for (int i = 0; i < fourthPath.Length; i++) fourthPath[i].GetComponent<ElectricTileControl>().ToggleTile(true);
			return;
		}

		if (path == "ThirdPathTarget") {
			for (int i = 0; i < firstPath.Length;  i++) firstPath[i].GetComponent<ElectricTileControl>().ToggleTile(true);
			for (int i = 0; i < secondPath.Length; i++) secondPath[i].GetComponent<ElectricTileControl>().ToggleTile(true);
			for (int i = 0; i < thirdPath.Length;  i++) thirdPath[i].GetComponent<ElectricTileControl>().ToggleTile(false);
			for (int i = 0; i < fourthPath.Length; i++) fourthPath[i].GetComponent<ElectricTileControl>().ToggleTile(true);
			return;
		}

		if (path == "FourthPathTarget") {
			for (int i = 0; i < firstPath.Length;  i++) firstPath[i].GetComponent<ElectricTileControl>().ToggleTile(true);
			for (int i = 0; i < secondPath.Length; i++) secondPath[i].GetComponent<ElectricTileControl>().ToggleTile(true);
			for (int i = 0; i < thirdPath.Length;  i++) thirdPath[i].GetComponent<ElectricTileControl>().ToggleTile(true);
			for (int i = 0; i < fourthPath.Length; i++) fourthPath[i].GetComponent<ElectricTileControl>().ToggleTile(false);
			return;
		}
	}
}
