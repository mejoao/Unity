using UnityEngine;
using System.Collections;

namespace TheMaze {

    public class SecurityCameraControl : MonoBehaviour {

        public  Light         myLight;
        public  Animator      animator;
        public  Renderer      area;
        private float         lookingTimer;
        private float         caughtTimer;
        private bool          allertOn;
        private bool          insideArea;
        private PlayerControl playerControl;
        private Color         allertColor;
        private Color         normalColor;

        public void Start() {
            allertOn      = false;
            playerControl = GameObject.FindWithTag(GameConstants.PLAYER_TAG).GetComponent<PlayerControl>();
            allertColor   = new Color(1.0F, 0.0F, 0.0F, 0.1F);
            normalColor   = new Color(1.0F, 1.0F, 1.0F, 0.1F);
        }

        public void Update() {
            if (!allertOn) return;

            var deltaTime = Time.deltaTime;
            lookingTimer += deltaTime;

            if (insideArea) {
                caughtTimer += deltaTime;
                if (caughtTimer >= GameConstants.CAMERA_CAUGHT_PERIOD) {
                    playerControl.CaughtByCamera();
                    ToggleAllert(false);
                    ToggleCollor(false);
                    return;
                }
            }

            if (lookingTimer >= GameConstants.CAMERA_LOOKING_PERIOD) {
                if (insideArea) playerControl.CaughtByCamera();
                ToggleAllert(false);
                ToggleCollor(false);
            }
        }

        public void OnTriggerEnter(Collider hit) {
            if (hit.gameObject.tag == GameConstants.PLAYER_TAG) {
                insideArea = true;
                ToggleCollor(true);
                if (!allertOn) {
                    ToggleAllert(true);
                    caughtTimer  = 0;
                    lookingTimer = 0;
                }
            }
        }

        public void OnTriggerExit(Collider hit) {
            if (hit.gameObject.tag == GameConstants.PLAYER_TAG) {
                insideArea = false;
                ToggleCollor(false);
            }
        }

        private void ToggleAllert(bool toggle) {
            allertOn         = toggle;
            animator.enabled = !toggle;
        }

        private void ToggleCollor(bool toggle) {
            if (toggle) {
                myLight.color       = allertColor;
                area.material.color = allertColor;
            } else {
                myLight.color       = normalColor;
                area.material.color = normalColor;
            }
             
        }
    }
}