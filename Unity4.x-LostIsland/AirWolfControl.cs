using UnityEngine;

public class AirWolfControl : MonoBehaviour {

	public  bool            pause;
	public  Rigidbody       missile;
	public  Transform       missileMuzzle;
	public  GameObject      missleExplosion;
	public  Transform       leftFireMuzzle;
	public  ParticleSystem  leftFlash;
	public  Transform       rightFireMuzzle;
	public  ParticleSystem  rightFlash;
	public  ParticleEmitter sparks;
	public  LayerMask       targetLayer;
	public  GameObject      airWolfCamera;
	public  bool            canDestroyTheIsland;
	public  Transform       bender;
	private float           rollAngle;
	private bool            left, right;
	private float           yawAngle;
	private bool            forward;
	private bool            alreadyFired;
	private Vector3         initialPos;
	private Quaternion      initialRot;
	private Vector3         velocity;
	private float           timer;
	private bool            isShooting;


	public void Start() {
		rollAngle          = 0;
		yawAngle            = 0;
		pause               = false;
		left                = false;
		right               = false;
		forward             = false;
		alreadyFired        = false;
		canDestroyTheIsland = false;
		initialPos          = transform.position;
		initialRot          = transform.rotation;
	}


	public void Update() {
		if (pause) return;

		velocity = Vector3.zero;

		if (Input.GetKey(KeyCode.A)) {
			if (!left) left = true;

			// Roll the Chopter
			if (rollAngle < GameValues.AIR_WOLF_MAX_ROLL_ANGLE)	{
				rollAngle += Time.deltaTime * GameValues.AIR_WOLF_ROLL_SPEED;
				if (rollAngle > GameValues.AIR_WOLF_MAX_ROLL_ANGLE) rollAngle = GameValues.AIR_WOLF_MAX_ROLL_ANGLE;

				transform.RotateAroundLocal(transform.up, Time.deltaTime);
			}

			Vector3 strafeDir = transform.right - Vector3.Dot(transform.right, Vector3.up) * Vector3.up;
			velocity         += strafeDir.normalized * GameValues.AIR_WOLF_STRAFE_SPEED;
		} else if (Input.GetKey(KeyCode.D)) {
			if (!right) right = true;

			// Roll
			if (rollAngle > -GameValues.AIR_WOLF_MAX_ROLL_ANGLE) {
				rollAngle -= Time.deltaTime * GameValues.AIR_WOLF_ROLL_SPEED;
				if (rollAngle < -GameValues.AIR_WOLF_MAX_ROLL_ANGLE) rollAngle = -GameValues.AIR_WOLF_MAX_ROLL_ANGLE;

				transform.RotateAroundLocal(transform.up, -Time.deltaTime);
			}

			Vector3 strafeDir = Vector3.Dot(transform.right, Vector3.up) * Vector3.up - transform.right;
			velocity += strafeDir.normalized * GameValues.AIR_WOLF_STRAFE_SPEED;
		} else 	if (Input.GetKey(KeyCode.W)) {
			if (!forward) forward = true;

			// Yaw
			if (yawAngle > -GameValues.AIR_WOLF_MAX_YAW_ANGLE) {
				yawAngle -= Time.deltaTime * GameValues.AIR_WOLF_YAW_SPEED;
				if (yawAngle < -GameValues.AIR_WOLF_MAX_YAW_ANGLE) yawAngle = -GameValues.AIR_WOLF_MAX_YAW_ANGLE;

				transform.RotateAroundLocal(transform.right, -Time.deltaTime);
			}

			Vector3 forwardDir = transform.up - Vector3.Dot(transform.up, Vector3.up) * Vector3.up;
			velocity += forwardDir.normalized * GameValues.AIR_WOLF_FORWARD_SPEED;
		} else if (Input.GetKey(KeyCode.Q))	{
			transform.Rotate(Vector3.up, -Time.deltaTime * GameValues.AIR_WOLF_PITCH_SPEED, Space.World);
		} else if (Input.GetKey(KeyCode.E))	{
			transform.Rotate(Vector3.up, Time.deltaTime * GameValues.AIR_WOLF_PITCH_SPEED, Space.World);
		}

		if (Input.GetKey(KeyCode.R)) {
			velocity += Vector3.up * GameValues.AIR_WOLF_UPWARD_SPEED;
		}

		if (Input.GetKey(KeyCode.F)) {
			velocity -= Vector3.up * GameValues.AIR_WOLF_UPWARD_SPEED;
		}

		if (Input.GetKeyDown(KeyCode.Space) && canDestroyTheIsland && !alreadyFired) {
			alreadyFired        = true;
			canDestroyTheIsland = false;

			Rigidbody b          = GameObject.Instantiate(missile, missileMuzzle.position, missile.transform.rotation) as Rigidbody;
			b.transform.rotation = transform.rotation;
			b.AddForce(missileMuzzle.forward * GameValues.AIR_WOLF_MISSLE_LAUNCH_FORCE);

			GameObject.FindWithTag(GameValues.SOUND_CONTROL_TAG).audio.volume = 0.4F;
			GameObject.FindWithTag(GameValues.SOUND_CONTROL_TAG).GetComponent<SoundControl>().PlaySound(GameValues.MISSLE_SOUND_ID);
		}


		if (Input.GetMouseButton(0)) {
			timer += Time.deltaTime;
			if (timer >= GameValues.AIR_WOLF_SHOOT_FREQUENCY) {
				timer = 0;

				if (!isShooting) {
					isShooting = true;
					rightFlash.Play();
					leftFlash.Play();
					leftFireMuzzle.audio.Play();
					rightFireMuzzle.audio.Play();
				}

				if (isShooting) {
					RaycastHit hit;
					if (Physics.Raycast(rightFireMuzzle.position, rightFireMuzzle.forward, out hit, targetLayer)) {
						if (hit.transform.gameObject.tag == GameValues.BLACK_BIRD_TAG) {
							hit.transform.gameObject.GetComponent<BlackBirdControl>().ApplyDamage();
						}

						ParticleEmitter go = GameObject.Instantiate(sparks, hit.point, Quaternion.identity) as ParticleEmitter;
						go.transform.rotation = Quaternion.LookRotation(hit.normal);
					}

					if (Physics.Raycast(leftFireMuzzle.position, leftFireMuzzle.forward, out hit, targetLayer)) {
						if (hit.transform.gameObject.tag == GameValues.BLACK_BIRD_TAG) {
							hit.transform.gameObject.GetComponent<BlackBirdControl>().ApplyDamage();
						}

						ParticleEmitter go = GameObject.Instantiate(sparks, hit.point, Quaternion.identity) as ParticleEmitter;
						go.transform.rotation = Quaternion.LookRotation(hit.normal);
					}
				}
			}
		}

		if (Input.GetMouseButtonUp(0)) {
			isShooting = false;
			rightFlash.Stop();
			leftFlash.Stop();
			leftFireMuzzle.audio.Stop();
			rightFireMuzzle.audio.Stop();
		}


		if (!Input.GetKey(KeyCode.A)) left    = false;
		if (!Input.GetKey(KeyCode.D)) right   = false;
		if (!Input.GetKey(KeyCode.W)) forward = false;

		if (!left && !right && rollAngle != 0) {
			if (rollAngle > 0) {
				rollAngle -= Time.deltaTime * GameValues.AIR_WOLF_ROLL_SPEED;
				if (rollAngle < 0) rollAngle = 0;
				transform.RotateAroundLocal(transform.up, -Time.deltaTime);
			}

			if (rollAngle < 0) {
				rollAngle += Time.deltaTime * GameValues.AIR_WOLF_ROLL_SPEED;
				if (rollAngle > 0) rollAngle = 0;
				transform.RotateAroundLocal(transform.up, Time.deltaTime);
			}
		}

		if (!forward && yawAngle != 0) {
			yawAngle += Time.deltaTime * GameValues.AIR_WOLF_YAW_SPEED;
			if (yawAngle > 0) yawAngle = 0;
			transform.RotateAroundLocal(transform.right, Time.deltaTime);
		}
	}

	public void FixedUpdate() {
		if (pause) return;

		rigidbody.velocity = velocity;
	}

	public void OnCollisionEnter(Collision hit) {
		GameObject.FindWithTag(GameValues.MUSIC_CONTROL_TAG).GetComponent<MusicControl>().StopMusic();
		Reset();
	}


	public void TurnCameraOn() {
		airWolfCamera.SetActive(true);
		GameObject.FindWithTag(GameValues.CINIMATIC_CAMERA_TAG).SetActive(false);
		GetComponent<AirWolfControl>().enabled = true;
		GameObject.FindWithTag(GameValues.HUD_CONTROL_TAG).GetComponent<HudControl>().SetHudMessage(GameValues.HUD_MESSAGE_START_AIR_WOLF_PHASER);
	}


	public void PlayFinalScene() {
		rigidbody.isKinematic = true;
		transform.eulerAngles = new Vector3(286, -311, 175);
		animation.Play("AirWolfLeaving");
		GameObject go = GameObject.Instantiate(missleExplosion, bender.position, Quaternion.identity) as GameObject;
		GameObject.Destroy(go, 10F);
		GetComponent<AirWolfControl>().enabled = false;
	}


	public void CreditsStarting() {
		audio.Stop();
	}

	public void Reset() {
		transform.position        = initialPos;
		transform.rotation        = initialRot;
		rigidbody.velocity        = Vector3.zero;
		rigidbody.angularVelocity = Vector3.zero;
		rollAngle                 = 0;
		yawAngle                  = 0;
		pause                     = false;
		left                      = false;
		right                     = false;
		forward                   = false;
		alreadyFired              = false;
		isShooting                = false;
		rightFlash.Stop();
		leftFlash.Stop();
		leftFireMuzzle.audio.Stop();
		rightFireMuzzle.audio.Stop();

		GameObject.FindWithTag(GameValues.MUSIC_CONTROL_TAG).GetComponent<MusicControl>().PlayMusic(GameValues.CHOPTER_MUSIC_ID);
	}
}

