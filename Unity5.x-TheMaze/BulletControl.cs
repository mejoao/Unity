using UnityEngine;

namespace TheMaze {
    // NOTE: The GameObject containing this class should be placed as child of 
    //       a container in a region not seen by the player
    public class BulletControl : MonoBehaviour {
        private Rigidbody rb;
        private Transform trans;
        private Renderer  rend;

        //[HideInInspector]
        public bool available;

        public void Start() {
            rb           = GetComponent<Rigidbody>();
            trans        = GetComponent<Transform>();
            rend         = GetComponent<Renderer>();
            rend.enabled = false;
            available    = true;
        }

        public void ApplyForce(Vector3 initialPos, Vector3 dir) {
            trans.position = initialPos;
            rb.AddForce(dir * GameConstants.TURRET_BULLET_FORCE);
        }

        public void OnTriggerEnter(Collider hit) { Reset(); }

        public void Reset() {
            available    = true;
            rb.velocity  = Vector3.zero;
            rend.enabled = false;

            // Read the note in the beginning to know why the reset point is (0, 0, 0)
            trans.position = Vector3.zero;
        }

        public void Show() { rend.enabled = true; }
    }
}
