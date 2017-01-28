using UnityEngine;

namespace TheMaze {
    public class GameplayHelper : MonoBehaviour {
        private GameControl gc;
        private PlayerControl pl;

        public void Start() {
            gc = GetComponent<GameControl>();
            pl = GameObject.FindWithTag(GameConstants.PLAYER_TAG).GetComponent<PlayerControl>();
        }
        
        public void Update() {
            if (Input.GetKeyDown(KeyCode.Alpha1)) gc.SolveFirstPuzzle();
            if (Input.GetKeyDown(KeyCode.Alpha2)) gc.SecondRoomPuzzleSolved();
            if (Input.GetKeyDown(KeyCode.Alpha3)) pl.AllowThermalVision();
            if (Input.GetKeyDown(KeyCode.Alpha4)) gc.FinishFourthPuzzle();
            if (Input.GetKeyDown(KeyCode.Alpha5)) gc.SolveFourthPuzzle();
                
        }
    }
}
