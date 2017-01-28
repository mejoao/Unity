using UnityEngine;
using UnityEngine.UI;

namespace TheMaze {

    public enum HUD_MESSAGES { CAMERA_CAUGHT,
                               PREDATOR_VISION_USE,
                               SAW_CAUGHT,
                               SCREEN_TOUCHED,
                               PUZZLE_SOLVED,
                               BEAM_CAUGHT,
                               HAMMER_SMASHED,
                               BULLET_HIT,
                               DOOR_TRAP_COLLISION,
                               GOTO_THE_SHIP,
                               END_GAME_SUCCESS,
                               END_GAME_FAILED }

    public class HUDMessages : MonoBehaviour {
        private Text messageText;
        private Animator anim;

        public void StopAnimation() {
            anim.enabled = false;
        }

        public void Start() {
            messageText = GetComponent<Text>();
            messageText.enabled = false;
            anim = messageText.gameObject.GetComponent<Animator>();
            anim.enabled = false;
        }

        public void ActivateMessage(HUD_MESSAGES message) {
            switch (message) {
                case HUD_MESSAGES.CAMERA_CAUGHT:
                    ShowMessage(GameConstants.CAMERA_CAUGHT_HUD_MESSAGE);
                break;

                case HUD_MESSAGES.PREDATOR_VISION_USE:
                    ShowMessage(GameConstants.PREDATOR_VISION_USE_HUD_MESSAGE);
                break;

                case HUD_MESSAGES.SAW_CAUGHT:
                    ShowMessage(GameConstants.SAW_COLLISION_HUD_MESSAGE);
                break;

                case HUD_MESSAGES.SCREEN_TOUCHED:
                    ShowMessage(GameConstants.SCREEN_TOUCHED_HUD_MESSAGE);
                break;

                case HUD_MESSAGES.BEAM_CAUGHT:
                    ShowMessage(GameConstants.BEAM_CAUGHT_HUD_MESSAGE);
                break;

                case HUD_MESSAGES.HAMMER_SMASHED:
                    ShowMessage(GameConstants.SECOND_ROOM_HAMMER_SMASHED_HUD_MESSAGE);
                break;

                case HUD_MESSAGES.PUZZLE_SOLVED:
                    ShowMessage(GameConstants.PUZZLE_SOLVED_HUD_MESSAGE);
                break;

                case HUD_MESSAGES.BULLET_HIT:
                    ShowMessage(GameConstants.TURRET_BULLET_HIT_HUD_MESSAGE);
                break;

                case HUD_MESSAGES.DOOR_TRAP_COLLISION:
                    ShowMessage(GameConstants.DOOR_TRAP_COLLISION_HUD_MESSAGE);
                break;

                case HUD_MESSAGES.GOTO_THE_SHIP:
                    ShowMessage(GameConstants.GOTO_THE_SHIP_HUD_MESSAGE);
                break;

                case HUD_MESSAGES.END_GAME_SUCCESS:
                    ShowMessage(GameConstants.FINISH_GAME_WITH_SUCCESS_HUD_MESSAG);
                break;

                case HUD_MESSAGES.END_GAME_FAILED:
                    ShowMessage(GameConstants.FINISH_GAME_WITH_FAILED_HUD_MESSAG);
                break;
            }
        }

        private void ShowMessage(string msg) {
            messageText.text    = msg;
            messageText.enabled = true;
            anim.enabled        = true;
            anim.Play(GameConstants.HUD_MESSAGES_ANIMATION_NAME, -1, 0);
        }
    }
}
