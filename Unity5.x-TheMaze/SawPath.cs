using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TheMaze {
    public class SawPath : MonoBehaviour {
        public  List<Transform> waypoints;
        public  Color           waypointColor = Color.blue;
        public  float           waypointRadius = 0.3F;
        public  float           angle = 90;
        public  float           speed = 30;
        public  Collider        col;
        private Transform       trans;
        private int             currentWaypoint;
        private AudioSource     audioSource;
        private Animator        anim;

        public void ToggleAudio(bool toggle) { audioSource.enabled = toggle; }

        public void Deactivate() {
            audioSource.enabled = false;
            enabled             = false;
            col.enabled         = false;
        }

        public void Awake() {
            trans           = GetComponent<Transform>();
            audioSource     = GetComponent<AudioSource>();
            currentWaypoint = 0;
        }

        void Update() {
            Vector3 dir = waypoints[currentWaypoint].position - trans.position;
            if (dir.magnitude < 1F) {
                trans.position = waypoints[currentWaypoint].position;
                currentWaypoint = (currentWaypoint + 1) % waypoints.Count;
                trans.Rotate(0, angle, 0);
            } else {
                trans.Translate(speed * Time.deltaTime, 0, 0);
            }
        }

        public void OnDrawGizmos() {
            Gizmos.color = waypointColor;
            for (int i = 0; i < waypoints.Count; i++) {
                Gizmos.DrawSphere(waypoints[i].position, waypointRadius);
            }

            if (waypoints.Count > 1) {
                Gizmos.color = Color.green;
                int count = waypoints.Count;
                for (int i = 0; i < count; i++)
                    Gizmos.DrawLine(waypoints[i].position, waypoints[(i + 1) % count].position);
            }
        }
    }
}
