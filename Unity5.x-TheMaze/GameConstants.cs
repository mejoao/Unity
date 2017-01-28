namespace TheMaze {
    public class GameConstants {
        // Tags
        public const string PLAYER_TAG                      = "Player";
        public const string MASK_TAG                        = "mask";
        public const string SECURITY_CAMERA_TAG             = "seccamera";
        public const string SAW_TRAP_TAG                    = "saw";
        public const string FIRST_ROOM_SCREEN_TAG           = "frscreen";
        public const string USED_SCREEN_TAG                 = "usedscreen";
        public const string SECOND_ROOM_DOOR_TAG            = "sroomdoor";
        public const string SECOND_ROOM_SCREEN_TAG          = "srscreen";
        public const string SECOND_ROOM_LASER_TAG           = "halllaser";
        public const string SECOND_ROOM_BEAM_TAG            = "beam";
        public const string SECOND_ROOM_HAMMER_TAG          = "hammer";
        public const string BULLET_FACTORY_TAG              = "bulletfactory";
        public const string TURRET_BULLET_TAG               = "turretbullet";
        public const string THIRD_ROOM_FIRST_TRAP_DOOR_TAG  = "firstTD";
        public const string THIRD_ROOM_SECOND_TRAP_DOOR_TAG = "secondTD";
        public const string THIRD_ROOM_THIRD_TRAP_DOOR_TAG  = "thirdTD";
        public const string THIRD_ROOM_HARD_TRAP_DOOR_TAG   = "hardTD";
        public const string THIRD_ROOM_HARD_TRAP_TAG        = "hardtrap";
        public const string FOURTH_ROOM_SCREEN_TAG          = "qrscreen";
        public const string NORMANDY_TAG                    = "normandy";

        // Animation Names
        public const string HUD_MESSAGES_ANIMATION_NAME = "HudMessage";

        // HUD Messages
        public const string PREDATOR_VISION_USE_HUD_MESSAGE        = "Puzzle Solved. \nPress \"E\" Key to Toggle The Predator Vision \nAnd See the Walls of the Final Maze";
        public const string CAMERA_CAUGHT_HUD_MESSAGE              = "Turn off the Cameras First";
        public const string SAW_COLLISION_HUD_MESSAGE              = "Avoid Collision with the Saw";
        public const string SCREEN_TOUCHED_HUD_MESSAGE             = "Touch the other Terminals To Disable the Cameras";
        public const string PUZZLE_SOLVED_HUD_MESSAGE              = "Puzzle Solved. Go to the Next Room";
        public const string BEAM_CAUGHT_HUD_MESSAGE                = "Avoid Collision With the Rotating Beams";
        public const string SECOND_ROOM_HAMMER_SMASHED_HUD_MESSAGE = "You've been smashed!";
        public const string TURRET_BULLET_HIT_HUD_MESSAGE          = "Disable the Turrets first in one of the Rooms";
        public const string DOOR_TRAP_COLLISION_HUD_MESSAGE        = "Not Fast Enough!";
        public const string GOTO_THE_SHIP_HUD_MESSAGE              = "TheMaze Game Finished. \nYou have 10 seconds to reach the Normandy";
        public const string FINISH_GAME_WITH_SUCCESS_HUD_MESSAG    = "You've escaped TheMaze";
        public const string FINISH_GAME_WITH_FAILED_HUD_MESSAG     = "TheMaze has you forever";

        // Times
        public const float CAMERA_LOOKING_PERIOD                  = 5;
        public const float CAMERA_CAUGHT_PERIOD                   = 0.5F;
        public const float PUZZLE_DELTA_TIME                      = 0.1F;
        public const float PUZZLE_SUCCESS_YIELD_TIME              = 1.5F;
        public const float PUZZLE_SUCCESS_RESET_TIME              = 1.5F;
        public const float TURRET_SHOOT_INTERVAL                  = 1F;
        public const float CAPSULE_LASER_ROTATING_SOUND_FREQUENCY = 2;
        public const float ENERGY_BEAM_ROTATING_SOUND_FREQUENCY   = 2;
        public const float END_GAME_COUNT_DOWN                    = 10;
        public const float WAIT_FOR_CREDITS_TIME                  = 3;
        public const float WALL_INVISIBLE_DELTA_TIME              = 0.1F;

        // Room Puzzles
        public const int   FIRST_ROOM_NUMBER_OF_SCREENS = 3;
        public const float SECOND_ROOM_MIN_ROT_SPEED    = 150;
        public const float SECOND_ROOM_MAX_ROT_SPEED    = 350;
        public const float SECOND_ROOM_BEAM_ROT_SPEED   = 200;

        // Physics
        public const float TURRET_BULLET_FORCE = 1000F;

        // Credits
        public const float CREDITS_VERTICAL_TIME  = 0.008F;
        public const float CREDITS_VERTICAL_SPACE = 2;
        public const float CREDITS_LETTER_WIDTH   = 30;
        public const float CREDITS_LINE_HEIGHT    = 50;
        public const float CREDITS_OFFSET_ELEMENT = 5;
        public const float CREDITS_OFFSET_GROUP   = 50;

        // Animation Times
        public const float WALLS_INVISIBLE_SPEED = 0.01F;
    }
}
