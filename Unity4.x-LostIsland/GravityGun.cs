using UnityEngine;
using System.Collections;

public class GravityGun : MonoBehaviour {

    private enum         GunStates { Free, Dragging, Carrying, Throwing }
    private GunStates    gunState;
    private GameObject   carryingObject;
    private float        timer;
    private bool         pressing;
    private bool         fire;
    private LineRenderer laser;
    private Vector3      laserSpot;
    public  Transform    contact;
    public  LayerMask    movableObjects;
    public  float        gunRange     = 100;
    public  float        dragForce    = 10;
    public  float        throwForce   = 500;
    public  float        coolDownTime = 0.7F;
    public  float        laserWidth   = 0.05F;
    public  Color        laserColor1  = Color.red;
    public  Color        laserColor2  = Color.red;

    public void Start() {
        gunState             = GunStates.Free;
        pressing             = false;
        laserSpot            = new Vector3(0, 0, gunRange);
        timer                = 0;
        laser                = gameObject.AddComponent<LineRenderer>();
        laser.material       = new Material(Shader.Find("Particles/Additive"));
        laser.useWorldSpace  = false;
        laser.enabled        = false;
        laser.SetPosition(0, laserSpot);
        laser.SetWidth(laserWidth, laserWidth);
        laser.SetColors(laserColor1, laserColor2);
    }

	public void Update() {
        if (gunState == GunStates.Throwing) {
            timer += Time.deltaTime;
            if (timer >= coolDownTime) {
                timer    = 0;
                gunState = GunStates.Free;
            }
        }

        if (Input.GetMouseButtonDown(1)) {
            pressing = false;
            if (carryingObject != null) fire = true;
        }

        if (Input.GetMouseButton(0)) {
            pressing = true;
            if (!laser.enabled) laser.enabled = true;
        }
        else if (Input.GetMouseButtonUp(0)) {
            pressing      = false;
            laser.enabled = false;
        }
	}

    public void FixedUpdate() {
        if (pressing) {
            if (gunState == GunStates.Free) {
                RaycastHit hit;
                if (Physics.Raycast(contact.position, contact.forward, out hit, gunRange, movableObjects)) {
                    gunState                 = GunStates.Dragging;
                    hit.rigidbody.useGravity = false;
                    carryingObject           = hit.transform.gameObject;
                    SetLaserSopt(Vector3.Distance(hit.point, transform.position));
                }
            } else if (gunState == GunStates.Dragging) {
                carryingObject.transform.position = Vector3.Lerp(carryingObject.transform.position,
                                                                 contact.position,
                                                                 dragForce * Time.deltaTime);
                Vector3 dir = contact.position - carryingObject.transform.position;

                // TODO: This test to place the object in front of the gun,
                //       only works when every scale has the same length.
                //       Think about a way to work with any scale
                if (dir.magnitude <= carryingObject.transform.localScale.z + 0.5F) {
                    gunState                             = GunStates.Carrying;
                    carryingObject.rigidbody.isKinematic = true;
                    carryingObject.rigidbody.velocity    = Vector3.zero;
                }

                RaycastHit hit;
                if (Physics.Raycast(contact.position, contact.forward, out hit, gunRange, movableObjects)) {
                    SetLaserSopt(Vector3.Distance(hit.point, transform.position));
                }
            } else if (gunState == GunStates.Carrying) {
                carryingObject.transform.position = contact.position + contact.forward * carryingObject.transform.localScale.z * 0.5F;
                carryingObject.transform.rotation = contact.rotation;
                carryingObject.collider.isTrigger = true;

                RaycastHit hit;
                if (Physics.Raycast(contact.position, contact.forward, out hit, gunRange, movableObjects)) {
                    SetLaserSopt(Vector3.Distance(hit.point, transform.position));
                }
            }
        } else if (!pressing) {
            if (carryingObject != null) {
                carryingObject.rigidbody.useGravity  = true;
                carryingObject.rigidbody.isKinematic = false;
                carryingObject                       = null;
                carryingObject.collider.isTrigger    = false;
                gunState                             = GunStates.Free;
                SetLaserSopt(gunRange);
            }
        }

        if (fire && carryingObject != null) {
            fire                                 = false;
            gunState                             = GunStates.Throwing;
            timer                                = 0;
            carryingObject.rigidbody.isKinematic = false;
            carryingObject.rigidbody.useGravity  = true;
            carryingObject.collider.isTrigger    = true;
            carryingObject.rigidbody.AddForce(contact.forward * throwForce);
            carryingObject.collider.isTrigger    = false;
            carryingObject                       = null;
            SetLaserSopt(gunRange);
        }
    }

    private void SetLaserSopt(float dist) {
        laserSpot.z = dist;
        laser.SetPosition(0, laserSpot);
    }
}
