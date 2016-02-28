using UnityEngine;
using System.Collections;

public class ReceiveDamage : MonoBehaviour {

	public enum TypeOfTarget {
		MemoryTarget,
		ElectricTarget,
		None
	}

	public TypeOfTarget typeOfTarget;
	public float        hp;
	public GameObject   particles;

	public void ApplyDamage(float damage) {
		hp -= damage;
		if (hp <= 0) {
			if (typeOfTarget == TypeOfTarget.MemoryTarget) {
				GameObject.Instantiate(particles, transform.position, transform.rotation);
				GameObject.FindWithTag(GameValues.MEMORY_ROOM_TAG).GetComponent<MemoryRoomControl>().HitTarget(gameObject.name);
				return;
			} else if (typeOfTarget == TypeOfTarget.ElectricTarget) {
				GameObject.FindWithTag(GameValues.ELECTRIC_ROOM_TAG).GetComponent<ElectricRoomControl>().ActivatePath(gameObject.name);
				return;
			}

			// Different Action based on the this scripit is attached to
			GameObject.Destroy(gameObject);
		}
	}
}
