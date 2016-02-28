using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MemoryRoomControl : MonoBehaviour {
	public  GameObject   key;
	public  GameObject[] targets;
	private int          currentTargetIndex;
	private int          numberOfTargetsShown;
	private float        timer;
	private bool         waitingPeriod;
	public  bool         runningMiniGame = false;


	public void Start() {
		currentTargetIndex   = -1;
		numberOfTargetsShown = 0;
		waitingPeriod        = true;
		for (int i = 0; i < targets.Length; i++) {
			Color c = targets[i].renderer.material.color;
			c.a     = 0;
			targets[i].renderer.material.color = c;
			targets[i].collider.enabled = false;
		}
		key.SetActive(false);
	}


	public void SetupRound() {
		int targetIndex;
		do { targetIndex = (int)UnityEngine.Random.Range(0, targets.Length); } while (targetIndex == currentTargetIndex);
		currentTargetIndex   = targetIndex;
		numberOfTargetsShown = 0;
		SetCurrentTargetAlpha(1, true);
	}


	public void Update() {
		if (!runningMiniGame) return;

		timer += Time.deltaTime;
		if (!waitingPeriod) {
			if (timer >= GameValues.MEMORY_ROOM_TARGET_FADE_TIME) {
				timer = 0;
				MakeCurrentTargetMoreTransparent();
			}
		} else {
			if (timer >= GameValues.MEMORY_ROOM_WAITING_PERIOD_TIME) {
				timer         = 0;
				waitingPeriod = false;
				SetupRound();
			}
		}
	}


	public void HitTarget(string target) {
		if (targets[currentTargetIndex].transform.name == target) 
			PlayerHitTarget(true);
	}
	

	private void SetCurrentTargetAlpha(float alpha, bool turnOnCollider) {
		Color c = targets[currentTargetIndex].renderer.material.color;
		c.a     = alpha;
		targets[currentTargetIndex].renderer.material.color = c;
		targets[currentTargetIndex].collider.enabled = turnOnCollider;
	}


	private void MakeCurrentTargetMoreTransparent() {
		Color c = targets[currentTargetIndex].renderer.material.color;
		c.a    -= GameValues.MEMORY_ROOM_TARGET_FADE_SPEED;
		targets[currentTargetIndex].renderer.material.color = c;
		if (c.a <= 0) {
			targets[currentTargetIndex].collider.enabled = false;
			PlayerHitTarget(false);
		}
	}


	private void PlayerHitTarget(bool playerHit) {
		SetCurrentTargetAlpha(0, false);
		numberOfTargetsShown++;
		if (numberOfTargetsShown >= targets.Length) {
			if (playerHit) {
				GameObject.FindWithTag(GameValues.HUD_CONTROL_TAG).GetComponent<HudControl>().SetHudMessage(GameValues.HUD_MESSAGE_MINI_GAME_VICTORY);
				runningMiniGame = false;
				key.SetActive(true);
			} else {
				GameObject.FindWithTag(GameValues.HUD_CONTROL_TAG).GetComponent<HudControl>().SetHudMessage(GameValues.HUD_MESSAGE_MINI_GAME_LOOSE);
				waitingPeriod = true;
			}
		} else {
			NextTarget();
		}
	}


	private void NextTarget() {
		currentTargetIndex++;
		if (currentTargetIndex >= targets.Length) 
			currentTargetIndex = 0;
		SetCurrentTargetAlpha(1, true);
	}


	public IEnumerator WaitToStartMiniGame() {
		yield return new WaitForSeconds(GameValues.MEMORY_ROOM_STANDBY_TIME);
		runningMiniGame = true;
	}
}