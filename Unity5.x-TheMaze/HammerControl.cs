using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheMaze {
    public class HammerControl : MonoBehaviour {
        private AudioSource audioSource;
        private Animator    anim;
        private Renderer    rend;

        public void PlaySound() {
            audioSource.Play();
        }

        public void Awake() {
            audioSource = GetComponent<AudioSource>();
            anim        = GetComponent<Animator>();
            rend        = GetComponent<Renderer>();
        }

        public void Toggle(bool toggle) {
            audioSource.enabled = toggle;
            anim.enabled        = toggle;
            rend.enabled        = toggle;
        }
    }
}