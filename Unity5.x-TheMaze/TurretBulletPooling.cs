using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheMaze {

    // NOTE: This pooling class doesn't allow the pool
    //       to grow in real time. The implementation would be
    //       different (using a List instead of an array) for bullets
    public class TurretBulletPooling : MonoBehaviour {
        public  GameObject      bulletsContainer;
        private BulletControl[] bullets;
        private int             currentBulletIndex;

        public void Start() {
            SetBullets();
        }

        public void SetBullets() {
            currentBulletIndex = 0;
            bullets            = bulletsContainer.GetComponentsInChildren<BulletControl>();
        }

        public BulletControl GetBullet() {
            if (!bullets[currentBulletIndex].available) return null;

            bullets[currentBulletIndex].available = false;

            BulletControl bc = bullets[currentBulletIndex];
            bc.Show();

            currentBulletIndex = (currentBulletIndex + 1) % bullets.Length;
            if (!bullets[currentBulletIndex].available)
                Debug.LogError("ALIEN QUEEN BUG - Minimum Bullets depleted while trying to shoot");

            return bc;
        }

        public void Reset() {
            currentBulletIndex = 0;
        }
    }
}
