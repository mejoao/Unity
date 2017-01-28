using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheMaze {
    public class InitialCinematicCameraControl : MonoBehaviour {
        public GameControl gc;

        public void StartGame() { gc.InitialCinematicEnd(); }
    }
}
