using UnityEngine;

namespace TheMaze {

    public class TurretControl : MonoBehaviour {

        public  Transform[] muzzles;
        private Transform   trans;
        private Transform   target;
        private Animator    anim;
        private bool        active;
        private bool        disable;
        private float       timer;
        private AudioSource audioSource;

        private static TurretBulletPooling pooling = null;

        public void Start() {
            trans        = GetComponent<Transform>();
            anim         = GetComponent<Animator>();
            audioSource  = GetComponent<AudioSource>();
            timer        = 0;
            target       = GameObject.FindWithTag(GameConstants.PLAYER_TAG).GetComponent<Transform>();
           
            anim.enabled = false;
            active       = false;
            disable      = false;

            if (pooling == null)
                pooling = GameObject.FindWithTag(GameConstants.BULLET_FACTORY_TAG).GetComponent<TurretBulletPooling>();
            
        }

        public void Update() {
            if (!active || disable) return;

            trans.LookAt(target.position);

            timer += Time.deltaTime;
            if (timer >= GameConstants.TURRET_SHOOT_INTERVAL) {
                timer = 0;

                audioSource.Play();
                for (int i = 0; i < muzzles.Length; i++) {
                    BulletControl b1 = pooling.GetBullet();
                    if (b1 != null) b1.ApplyForce(muzzles[i].position, muzzles[i].forward);
                }
            }
        }

        public void OnTriggerEnter(Collider hit) {
            if (hit.gameObject.tag == GameConstants.PLAYER_TAG) {
                if (!disable) active = true;
            }
        }

        public void OnTriggerExit(Collider hit) {
            if (hit.gameObject.tag == GameConstants.PLAYER_TAG) active = false; 
        }

        public void DisableTurret() {
            disable      = true;
            anim.enabled = true;
        }

        public void DisableAnimator() {
            anim.enabled = false;
            enabled      = false;
        }
    }


}
