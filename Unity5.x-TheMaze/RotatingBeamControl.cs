using UnityEngine;

namespace TheMaze {
    public class RotatingBeamControl : MonoBehaviour {
        public  bool        vertical = false;
        public  int         sign;
        private Transform   trans;
        private Vector3     axis;

        public void Toggle(bool toggle) { enabled = toggle; }

        public void Awake() {
            trans = GetComponent<Transform>();
            axis  = vertical ? Vector3.up : Vector3.forward;
        }

        public void Update() {
            trans.Rotate(axis, 
                         sign * GameConstants.SECOND_ROOM_BEAM_ROT_SPEED * Time.deltaTime, 
                         Space.World);
        }
    }
}
