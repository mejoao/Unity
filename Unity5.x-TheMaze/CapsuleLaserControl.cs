using UnityEngine;
using Random = UnityEngine.Random;

namespace TheMaze {
    public class CapsuleLaserControl : MonoBehaviour {
        public  Transform pivot;
        private Transform trans;
        private float     speed = 5;

        public void Toggle(bool toggle) {
            enabled = toggle;

        }

        public void Awake() {
            trans = GetComponent<Transform>();
            speed = Random.Range(GameConstants.SECOND_ROOM_MIN_ROT_SPEED,
                                 GameConstants.SECOND_ROOM_MAX_ROT_SPEED);
            speed *= Random.Range(0, 10) >= 5 ? 1 : -1;
        }

        public void Update() {
            trans.RotateAround(pivot.position, Vector3.up, speed * Time.deltaTime);
        }
    }
}