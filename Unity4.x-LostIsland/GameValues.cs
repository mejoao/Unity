using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameValues : MonoBehaviour {

    // Tags
    public static string PLAYER_TAG                    = "Player";
	public static string PLAYER_BULLET_TAG             = "PlayerBullet";
    public static string PORTAL_ENTRY_TAG              = "Portal";
    public static string TURRET_TAG                    = "Turret";
	public static string TURRET_FLAME_TAG              = "TurretFlame";
	public static string TURRET_PHOTON_TORP_TAG        = "Photon";
	public static string TURRET_FIRE_BALL_TAG          = "FireBall";
	public static string TURRET_SHOCKW_BULLET_TAG      = "ShockWaveBullet";
	public static string BLACK_BIRD_LASER_TAG          = "BlackbirdLaser";
	public static string GAME_CONTROL_TAG              = "GameControl";
	public static string HUD_CONTROL_TAG               = "HudControl";
	public static string ELECTRIC_ROOM_TAG             = "ElectricRoom";
	public static string ELECTRIC_ROOM_ENTRY_TAG       = "ElectricRoomEntry";
	public static string ELECTRIC_ROOM_TILE_TAG        = "ElectricTile";
	public static string ELECTRIC_ROOM_TILE_OFF_TAG    = "ElectricTileOff";
	public static string ELECTRIC_ROOM_FLOOR_LASER_TAG = "ElectricRoomFloorLaser";
	public static string ELECTRIC_ROOM_TOP_LASER_TAG   = "ElectricRoomTopLaser";
	public static string ELECTRIC_ROOM_TARGET_TAG      = "ElectricRoomTarget";
	public static string MEMORY_ROOM_TAG               = "MemoryRoom";
	public static string MEMORY_ROOM_ENTRY_TAG         = "MemoryRoomEntry";
	public static string MEMORY_ROOM_TARGET_TAG        = "MemoryRoomTarget";
	public static string TRAPS_ROOM_TAG                = "TrapsRoom";
	public static string TRAPS_ROOM_ENTRY_TAG          = "TrapsRoom";
	public static string TRAPS_ROOM_GUILLOTINE_TAG     = "Guillotine";
	public static string TRAPS_ROOM_SHURIKEN_TAG       = "Shuriken";
	public static string PUZZLE_ROOM_C_LAVA_FLOOR_TAG  = "PuzzleRoomColumnsLava";
	public static string PUZZLE_ROOM_P_LAVA_FLOOR_TAG  = "PuzzleRoomPlatformsLava";
	public static string FIRST_AID_TAG                 = "FirstAid";
	public static string KEY_TAG                       = "Ank";
	public static string SEA_RESET_TAG                 = "SeaReset";
	public static string MUSIC_CONTROL_TAG             = "MusicControl";
	public static string SOUND_CONTROL_TAG             = "SoundControl";
	public static string WEAPON_TAG                    = "Weapon";
	public static string AMMO_TAG                      = "Ammo";
	public static string TURN_OFF_MINI_MAP_TAG         = "ToggleMiniMap";
	public static string AIRPLANE_STUB_TAG             = "AirWolfTrigger";
	public static string AIRPLANE_TAG                  = "Airplane";
	public static string CINIMATIC_CAMERA_TAG          = "CinematicCamera";
	public static string BLACK_BIRD_TAG                = "BlackBird";
	public static string STATIC_OBJECTS_TAG            = "StaticObjects";


    // Times
    public static float SPLASHSCREEN_FADE_SPEED          = 0.1F;
    public static float SPLASHSCREEN_FADE_WAITING_TIME   = 1.3F;
    public static float SPLASHSCREEN_FADE_RATE           = 0.1F;
	public static float SPLASHSCREEN_AUDIO_VOLUME_SPEED  = 0.1F;
    public static float FADE_PLANE_WAITING_TIME          = 1F;
    public static float FADE_PLANE_FADE_SPEED            = 0.05F;
    public static float FADE_PLANE_YIELD_TIME            = 0.05F;
    public static float TEXT_WAITING_TIME                = 1.5F;
	public static float TEXT_FADE_SPEED                  = 0.1F;
	public static float TEXT_YIELD_TIME                  = 0.1F;
	public static float BLACK_BIRD_SHOOT_TIME            = 3;
	public static float TURRET2_BULLET_LIFE_TIME         = 10;
	public static float TURRET3_BULLET_LIFE_TIME         = 10;
	public static float TURRET4_BULLET_LIFE_TIME         = 10;
	public static float MEMORY_ROOM_TARGET_FADE_TIME     = 0.1F;
	public static float MEMORY_ROOM_WAITING_PERIOD_TIME  = 2;
	public static float MEMORY_ROOM_STANDBY_TIME         = 1;
	public static float PLAYER_SHOOT_FREQUENCY           = 0.2F;
	public static float AIR_WOLF_SHOOT_FREQUENCY         = 0.1F;
	public static float AIR_WOLF_TIME_TO_FADE_SCENE      = 1.2F;
	public static float END_GAME_TIME_TO_RELEASE_CREDITS = 0.5F;
	

    // Screen
    public static float  SCREEN_WIDTH  = 1024;
    public static float  SCREEN_HEIGHT = 768;


    // Menu
    public static float  BUTTON_WIDTH             = 100;
    public static float  BUTTON_HEIGHT            = 25;
    public static float  BUTTON_OFFSET_W          = 20;
    public static float  BUTTON_OFFSET_H          = 20;
    public static float  BACK_WIDTH               = 2 * BUTTON_OFFSET_W + BUTTON_WIDTH;
    public static float  BACK_HEIGHT              = 5 * BUTTON_OFFSET_H + 4 * BUTTON_HEIGHT;
    public static float  BACK_POSX                = SCREEN_WIDTH  * 0.5F - BUTTON_WIDTH * 0.5F - BUTTON_OFFSET_W;
    public static float  BACK_POSY                = SCREEN_HEIGHT * 0.5F - 2 * BUTTON_OFFSET_H - 1.5F * BUTTON_HEIGHT;
    public static float  BUTTON_POSX              = BACK_POSX + BUTTON_OFFSET_W;
    public static float  NEW_BUTTON_POSY          = BACK_POSY +     BUTTON_OFFSET_H;
    public static float  CONTINUE_BUTTON_POSY     = BACK_POSY + 2 * BUTTON_OFFSET_H +     BUTTON_HEIGHT;
    public static float  HOWTO_BUTTON_POSY        = BACK_POSY + 3 * BUTTON_OFFSET_H + 2 * BUTTON_HEIGHT;
    public static float  EXIT_BUTTON_POSY         = BACK_POSY + 4 * BUTTON_OFFSET_H + 3 * BUTTON_HEIGHT;
    public static string MENU_LABLE               = "Menu";
    public static string NEW_BUTTON_LABLE         = "New";
    public static string CONTINUE_BUTTON_LABLE    = "Continue";
    public static string HOWTO_BUTTON_LABLE       = "HowToPlay";
    public static string EXIT_BUTTON_LABLE        = "Exit";
	public static float  HOW_TO_PLAY_IMAGE_X      = 10;
	public static float  HOW_TO_PLAY_IMAGE_Y      = 10;
	public static float  HOW_TO_PLAY_IMAGE_WIDTH  = 0.5F;
	public static float  HOW_TO_PLAY_IMAGE_HEIGHT = 0.5F;

    // HUD Messages
    public static string HUD_MESSAGE_START_GAME              = "Find Your Weapon \nInside the Lighthouse";
	public static string HUD_MESSAGE_ELECTRIC_ROOM_MINI_GAME = "Don\'t step on the Energy Field\nShoot the Targets";
	public static string HUD_MESSAGE_MEMORY_ROOM_MINI_GAME   = "Shoot the Targets in time";
	public static string HUD_MESSAGE_PUZZLE_ROOM_MINI_GAME   = "Your Ank is at the End of the Path";
	public static string HUD_MESSAGE_TRAPS_ROOM_MINI_GAME    = "Your Ank is at the End of the Path";
	public static string HUD_MESSAGE_MINI_GAME_VICTORY       = "You Win";
	public static string HUD_MESSAGE_MINI_GAME_LOOSE         = "Try Again";
	public static string HUD_MESSAGE_PORTAL_LOCKED           = "Complete the MiniGame to Exit the Room";
	public static string HUD_MESSAGE_COLLECT_ANKS            = "Find the 4 Portals\nAnd Collect the 4 Anks";
	public static string HUD_MESSAGE_NEW_ANK                 = "You've gotta a new Ank";
	public static string HUD_MESSAGE_PROCEED_TO_SECRET_ROOM  = "You have the 4 Anks\nGo to Helicopter";
	public static string HUD_MESSAGE_PLAYER_WITH_NO_WEAPON   = "You must have the weapon to do this MiniGame";
	public static string HUD_MESSAGE_START_AIR_WOLF_PHASER   = "Destroy the BlackBird Airplane";
	public static string HUD_MESSAGE_DESTROY_THE_TOTEM       = "Use SpaceBar to shoot a Missle\nTowards Bender and Destroy the Island";


	// HUD
	public static float MINI_MAP_WIDTH         = 128;
	public static float MINI_MAP_HEIGHT        = 128;
	public static float MINI_MAP_OFFSET        = 10;
	public static float MINI_MAP_CAMERA_HEIGHT = 15;
	public static int   MAX_NUMBER_OF_KEYS     = 4;
	public static int   KEY_WIDTH              = 32;
	public static int   KEY_HEIGHT             = 32;
	public static int   KEY_START_X            = 10;
	public static int   KEY_OFFSET_X           = 5;
	public static int   KEY_START_Y            = 30;
	public static int   AMMO_START_X           = KEY_START_X;
	public static int   AMMO_START_Y           = KEY_START_Y * 3;
	public static int   AMMO_OFFSET_X          = -10;
	public static int   AMMO_OFFSET_Y          = 10;
	public static int   AMMO_TEXT_OFFSET_X     = -10;
	public static int   AMMO_TEXT_OFFSET_Y     = 2;


	// Credits
	public static float CREDITS_VERTICAL_TIME  = 0.008F;
	public static float CREDITS_VERTICAL_SPACE = 1;
	public static float CREDITS_LETTER_WIDTH   = 30;
	public static float CREDITS_LINE_HEIGHT    = 50;
	public static float CREDITS_OFFSET_ELEMENT = 5;
	public static float CREDITS_OFFSET_GROUP   = 50;


    // Music / Sound
    public static int ISLAND_MUSIC_ID               = 1;
	public static int TELEPORT_SOUND_ID             = 2;
	public static int FIRST_AID_PICKUP_SOUND_ID     = 3;
	public static int KEY_PICKUP_SOUND_ID           = 4;
	public static int ELECTRIC_ROOM_DAMAGE_SOUND_ID = 5;
	public static int MINI_GAME_MUSIC_ID            = 6;
	public static int MENU_SOUND_ID                 = 7;
	public static int WEAPON_PICKUP_SOUND_ID        = 8;
	public static int AMMO_PICKUP_SOUND_ID          = 9;
	public static int MENU_CLICK_SOUND_ID           = 10;
	public static int LASER_COLLISION_SOUND_ID      = 11;
	public static int RESET_SOUND_ID                = 12;
	public static int CHOPTER_MUSIC_ID              = 13;
	public static int MISSLE_SOUND_ID               = 14;
	public static int EXPLOSION_SOUND_ID            = 15;


	// Turrets
	public static float TURRET1_MIN_SQUARE_DISTANCE         = 400;      // 15 m
	public static float TURRET2_MIN_SQUARE_DISTANCE         = 900;      // 30 m
	public static float TURRET3_MIN_SQUARE_DISTANCE         = 900;      // 30 m
	public static float SHOCKWAVE_BULLET_DISTANCE_TO_DAMAGE = 400;      // 20 m 
	public static float TURRET2_BULLET_FREQUENCY1           = 1;
	public static float TURRET2_BULLET_FREQUENCY2           = 4;
	public static float TURRET3_BULLET_FREQUENCY            = 1.5F;
	public static float TURRET2_BULLET_FORCE                = 1000;
	public static float TURRET4_BULLET_FREQUENCY1           = 0.5F;
	public static float TURRET4_BULLET_FREQUENCY2           = 1.5F;
	public static float TURRET4_BULLET_FORCE                = 1000;


	// BlackBird
	public static float  BLACK_BIRD_PATROLL_SPEED = -5;
	public static float  BLACK_BIRD_FREQUENCY1    = 30;
	public static float  BLACK_BIRD_FREQUENCY2    = 50;
	public static float  BLACK_BIRD_HP            = 200;


	// AirWolf
	public static float AIR_WOLF_CAMERA_OFFSET_DISTANCE = 50;
	public static float AIR_WOLF_CAMERA_HEIGHT          = 20;
	public static float AIR_WOLF_MAX_ROLL_ANGLE         = 0.7F;
	public static float AIR_WOLF_ROLL_SPEED             = 4;
	public static float AIR_WOLF_MAX_YAW_ANGLE          = 2;
	public static float AIR_WOLF_YAW_SPEED              = 4;
	public static float AIR_WOLF_PITCH_SPEED            = 80;
	public static float AIR_WOLF_UPWARD_SPEED           = 60F;
	public static float AIR_WOLF_STRAFE_SPEED           = 60F;
	public static float AIR_WOLF_FORWARD_SPEED          = 60F;
	public static float AIR_WOLF_MISSLE_LAUNCH_FORCE    = 2000;



	// Electric Room Targets
	public static float ELECTRIC_ROM_TARGET_OSCILATING_SPEED0     = 0.008F;
	public static float ELECTRIC_ROM_TARGET_OSCILATING_SPEED1     = 0.03F;
	public static float ELECTRIC_ROM_TARGET_OSCILATING_AMPLITUDE0 = 5;
	public static float ELECTRIC_ROM_TARGET_OSCILATING_AMPLITUDE1 = 9;


	// Memory Room
	public static float MEMORY_ROOM_TARGET_FADE_SPEED = 0.1F;


	// Traps Room
	public static float TRAPS_ROOM_SHURIKEN_ROT_SPEED = 1000;


	// Puzzle Room
	public static float PUZZLE_ROOM_COLUMN_ORIGINAL_HEIGHT = 0.3075256F;
	public static float PUZZLE_ROOM_COLUMN_MAX_HEIGHT      = 0.3074799F;
	public static float PUZZLE_ROOM_COLUMN_MIN_HEIGHT      = -9;
	public static float PUZZLE_ROOM_COLUMN_MIN_SPEED       = 2;
	public static float PUZZLE_ROOM_COLUMN_MAX_SPEED       = 5;
	public static float PUZZLE_ROOM_PLATFORM_MIN_SPEED     = 65;
	public static float PUZZLE_ROOM_PLATFORM_MAX_SPEED     = 80;

	// Items
	public static float ITEM_ROTATION_SPEED = 35;


	// Damage 
	//  Obs: The values of the damage to be applied to the Player
	//       varies between 0 and 1 because of the alpha channel
	//       of the image used to depict the damage  
	//       Damage applied by the Player can be any positive value
	public static float FLAME_DAMAGE                   = 0.7F;
	public static float FLAME_DAMAGE_FREQUENCY         = 0.3F;
	public static float PHOTON_DAMAGE                  = 0.6F;
	public static float FIRE_BALL_DAMAGE               = 0.8F;
	public static float SHOCK_WAVE_DAMAGE              = 0.7F;
	public static float ELECTRIC_ROOM_DAMAGE           = 0.7F;
	public static float ELECTRIC_ROOM_DAMAGE_FREQUENCY = 1.5F;
	public static float ELECTRIC_ROOM_LASER_DAMAGE     = 0.6F;
	public static float BLACK_BIRD_LASER_DAMAGE        = 0.5F;
	public static float PLAYER_DAMAGE                  = 10;
	public static int   PLAYER_WEAPON_CLIP_SIZE        = 25;
	public static int   PLAYER_AIR_WOLF_DAMAGE         = 20;


	// Debug-Cheat
	public static float PLAYER_FREEZE_MOVEMENT_TIME = 0.5F;
}
