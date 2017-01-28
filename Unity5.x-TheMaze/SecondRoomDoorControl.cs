using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheMaze {
    public class SecondRoomDoorControl : MonoBehaviour {
        public void Start() { GetComponent<Animator>().enabled = false; }

        public void DesableAnimation() { gameObject.SetActive(false); }
    }
}
