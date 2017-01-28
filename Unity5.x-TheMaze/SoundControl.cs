using UnityEngine;
using System.Collections;

namespace TheMaze {

    public enum GAME_SOUNDS {
                              BUTTOM_PRESS_SOUND, 
                              CAMERA_CAUGHT_SOUND,
                              PREDATOR_LAUGH_SOUND,
                              SAW_CAUGHT_SOUND,
                              PUZZLE_SOLVED_SOUND,
                              SCREEN_TOUCHED_SOUND,
                              BEAM_CAUGHT_SOUND,
                              HAMMER_SMASH_SOUND,
                              TURRET_BULLET_HIT_SOUND,
                              DOOR_TRAP_COLLISION_SOUND,
                              DOOR_OPENING_SOUND }

    public class SoundControl : MonoBehaviour {
        private AudioSource audioSource;

        public AudioClip predatorLaugh;
        public AudioClip reset;
        public AudioClip screenTouched;
        public AudioClip puzzleSolved;
        public AudioClip doorOpening;
        public AudioClip buttomPress;
       // public AudioClip beamCaught;
       // public AudioClip hammerSmashed;
       // public AudioClip cameraCaught;
       // public AudioClip sawCaught;


        public void Start() {
            audioSource = GetComponent<AudioSource>();
        }

        public void PlaySound(GAME_SOUNDS soundId) {
            switch (soundId) {
                case GAME_SOUNDS.BUTTOM_PRESS_SOUND:
                    audioSource.PlayOneShot(buttomPress);
                break;

                case GAME_SOUNDS.PREDATOR_LAUGH_SOUND:
                    audioSource.PlayOneShot(predatorLaugh);
                break;

                case GAME_SOUNDS.PUZZLE_SOLVED_SOUND:
                    audioSource.PlayOneShot(puzzleSolved);
                break;

                case GAME_SOUNDS.SCREEN_TOUCHED_SOUND:
                    audioSource.PlayOneShot(screenTouched);
                break;

                case GAME_SOUNDS.DOOR_OPENING_SOUND:
                    audioSource.PlayOneShot(doorOpening);
                break;

                case GAME_SOUNDS.CAMERA_CAUGHT_SOUND:
                case GAME_SOUNDS.SAW_CAUGHT_SOUND:
                case GAME_SOUNDS.BEAM_CAUGHT_SOUND:
                case GAME_SOUNDS.HAMMER_SMASH_SOUND:
                case GAME_SOUNDS.DOOR_TRAP_COLLISION_SOUND:
                case GAME_SOUNDS.TURRET_BULLET_HIT_SOUND:
                    audioSource.PlayOneShot(reset);
                break;
            }
        }
    }
}
