using UnityEngine;
using System.Collections;

public class RotatingItem : MonoBehaviour {
	public void Update () {	transform.Rotate(0, GameValues.ITEM_ROTATION_SPEED * Time.deltaTime, 0); }
}