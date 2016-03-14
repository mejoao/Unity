using UnityEngine;
public class FSMSimple : MonoBehaviour {

	#region FSM States
	public enum      FSMStates { Waypoints, Chasing, Shooting, Firing };
	public FSMStates state = FSMStates.Waypoints;
	#endregion

	#region Generic Variable
	public  GameObject target;
	public  float      speed;
	public  float      rotSpeed;
	private float      timer;
	private Vector3    dir;
	private Rigidbody  rb;
	#endregion

	#region Wapypoints
	public  Transform[] waypoints;
	public  float       distanceToChangeWaypoint;
	private int         currentWaypoint;
	#endregion

	#region Chasing
	public float distanceToStartChasing;
	public float distanceToStopChasing;
	public float distanceToAttack;
	public float distanceToReturnChase;
	public float chanceToFire;
	#endregion

	#region Shooting
	public  Rigidbody bullet;
	public  Transform muzzle;
	public  float     bulletInitialForce;
	public  float     frequency;
	public  int       maxNumberOfShoots;
	private int       numberOfShoots;
	#endregion

	#region Firing
	public GameObject flame;
	public float      flameTime;
	#endregion

	#region Unity Functions
	public void Start() {
		currentWaypoint = 0;
		timer           = 0;
		rb              = GetComponent<Rigidbody>();
		flame.SetActive(false);


	}


	public void FixedUpdate() {
		dir = target.transform.position - transform.position;

		switch (state) {
			case FSMStates.Waypoints: WaypointState(); break;
			case FSMStates.Chasing:   ChaseState();    break;
			case FSMStates.Shooting:  ShootState();    break;
			case FSMStates.Firing:    FireState();     break;

			default: print("BUG: state should never be on default clause"); break;
		}
	}
	#endregion

	#region Wapypoints State
	private void WaypointState() {
		// Check if target is in range to chase
		if (dir.magnitude <= distanceToStartChasing) {
			state = FSMStates.Chasing;
			return;
		}

		// Find the direction to the current waypoint,
		//   rotate and move towards it
		Vector3 wpDir         = waypoints[currentWaypoint].position - transform.position;
		transform.rotation    = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(wpDir), Time.deltaTime * rotSpeed);
		transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
		if (wpDir.magnitude <= distanceToChangeWaypoint) {
			currentWaypoint++;
			if (currentWaypoint >= waypoints.Length)
				currentWaypoint = 0;

		} else
			rb.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
	}
	#endregion

	#region Chasing State
	private void ChaseState() {
		// Check if target is close enough to shoot or fire
		//   or if target is too far way, then return to Waypoints
		if (dir.magnitude > distanceToStopChasing) {
			state = FSMStates.Waypoints;
			return;
		} else if (dir.magnitude <= distanceToAttack) {
			timer = 0;
			rb.velocity = Vector3.zero;
			// Get a random number to choose one of the attacks
			float randomNumber = UnityEngine.Random.Range(0F, 10F);
			if (randomNumber > chanceToFire) {
				state = FSMStates.Firing;
				flame.SetActive(true);
			} else {
				state = FSMStates.Shooting;
				numberOfShoots = 0;
			}
			return;
		}

		transform.rotation    = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotSpeed);
		transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
		rb.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
	}
	#endregion

	#region Shooting State
	private void ShootState() {
		transform.rotation    = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotSpeed);
		transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

		timer += Time.deltaTime;
		if (timer >= frequency) {
			timer = 0;

			Rigidbody b = GameObject.Instantiate(bullet, muzzle.position, muzzle.rotation) as Rigidbody;
			b.AddForce(muzzle.forward * bulletInitialForce);
			//GameObject.FindWithTag("soundcontrol").GetComponent<SoundControl>().PlaySound("shoot");

			numberOfShoots++;
			if (numberOfShoots >= maxNumberOfShoots) {
				if (dir.magnitude < distanceToAttack)
					numberOfShoots = 0;
				else if (dir.magnitude > distanceToAttack && dir.magnitude <= distanceToReturnChase)
					state = FSMStates.Chasing;
				else if (dir.magnitude > distanceToReturnChase)
					state = FSMStates.Waypoints;
			}
		}
	}
	#endregion

	#region Firing State
	private void FireState() {
		transform.rotation    = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotSpeed);
		transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

		timer += Time.deltaTime;
		if (timer >= flameTime) {
			timer = 0;

			if (dir.magnitude > distanceToAttack && dir.magnitude <= distanceToReturnChase) {
				state = FSMStates.Chasing;
				flame.SetActive(false);
			}
			else if (dir.magnitude > distanceToReturnChase) {
				state = FSMStates.Waypoints;
				flame.SetActive(false);
			}
		}
	}
	#endregion
}
