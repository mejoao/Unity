using UnityEngine;
using System.Collections;

public class ShurikenRotation : MonoBehaviour 
{
	void Update () { transform.Rotate(0, GameValues.TRAPS_ROOM_SHURIKEN_ROT_SPEED * Time.deltaTime, 0);	}
}
