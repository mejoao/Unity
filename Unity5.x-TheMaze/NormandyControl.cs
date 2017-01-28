using UnityEngine;

namespace TheMaze {
    public class NormandyControl : MonoBehaviour {
        private Animator anim;
        private Collider col;

        public void ActivateFinalSequency() { anim.enabled = true; }

        public void EndFinalSequency() { anim.enabled = false; }

        public void ActivateCollider() { col.enabled = true; }

        public void Awake() {
            anim         = GetComponent<Animator>();
            anim.enabled = false;
            col          = GetComponent<Collider>();
            col.enabled  = false;
        }
    }
}
