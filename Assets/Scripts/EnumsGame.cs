using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumsGame {

    public enum BubbleCollors { RED, BLUE, YELLOW };
    public enum GameStates { CREATE_BUBBLE, USE_BUBBLES, MOVING_FORWARD, PAUSE, SHOP, RESULT_LEVEL, RESULT_BOSS, BOSS, INIT_LEVEL };
    public enum PlayerAnimations { CAMI_IDDLE, CAMI_BASIC_SKILL, CREATE_BUBBLE };
    public enum GameSkills { FIRE_BASIC, ELETRIC_BASIC, WATER_BASIC, POISON, EARTHQUAKE, ARKANE, FIRE_PLUS, ELETRIC_PLUS,
    WATER_CURE, WATER_COMBO, COMBO_FIRE, COMBO_ELETRIC, BUBBLE_GEMSTONE, POWER_GEMSTONE, HP_GEMSTONE};
    public enum EnemyType { TANKER_BERSERKER, TANKER_TAUNT, WARRIOR, ROGUE, WIZARD, DRUID }
    public enum GameBullets { ENEMY_BULLET_01 };
    public enum GameplayType { PLAYER_ON_MAP, LEVEL_GAME, FIGHT_GAME, CAVE_GAME, KITCHEN_GAME };

}
