using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData {

    //Classe para guardar dados do player como skills, exp, level, souls, emblemas, ...
    //Essa classe terá a parte de guardar os dados gerais e os dados da partida atual.

    public static PlayerData instance;

    //Data of player
    [SerializeField] private string playerName;
    public string _playerName { get { return playerName; } set { playerName = value; } }

    [SerializeField] private int totalExp;
    public int _totalExp { get { return totalExp; } }

    //Souls que foram entregues nas catacumbas
    [SerializeField] private int totalSouls; //Quantidade total de souls para comprar magias
    public int _totalSouls { get { return totalSouls; } }

    [SerializeField] private int[] levelsSituation; //0 or 1
    public int[] _levelsSituation { get { return levelsSituation; } }

    /*[0]: Fire, [1]: Eletriv, [2]: Water, [3]: Poison, [4]: Earthquake, [5]: Arkane, [6]: FirePlus, [7]: EletricPlus,
     * [8]: WaterCure, [9]: WaterCombo, [10]: ComboFire, [11]: ComboEletric, [12]: BubbleGemstone, [13]: PowerGemstone,
     * [14]: HPGemstone, [15]: ,[16]: ,[17]: */
    [SerializeField] private int[] skillsSituation; //0 if haven´t skill. 10 to max level of skill.
    public int[] _skillsSituation { get { return skillsSituation; } }

    //Base to calcule of diferent skills
    private int fireBase = 6;
    public int _fireBase { get { return fireBase; } }

    private int eletricBase = 1;
    public int _eletricBase { get { return eletricBase; } }

    private int waterBase = 1;
    public int _waterBase { get { return waterBase; } }

    private Vector3 posPlayerOnMap;
    public Vector3 _posPlayerOnMap { get { return posPlayerOnMap; } set { posPlayerOnMap = value; } }

    private float expMultiplier = 1;
    
    public PlayerData() {

        if (instance == null) {

            instance = this;

            skillsSituation = new int[15];
            for (int i = 0; i < skillsSituation.Length; i++)
                skillsSituation[i] = 0;

            skillsSituation[0] = 1;
            skillsSituation[1] = 1;
            skillsSituation[2] = 1;
            //skillsSituation[3] = 1;
            //skillsSituation[4] = 1;
            //skillsSituation[5] = 1;

            levelsSituation = new int[6];
            for (int i = 0; i < levelsSituation.Length; i++)
                levelsSituation[i] = 0;

        }

    }

    public void addSoul() {
        totalSouls++;
    }

    public int getSkillLevel(EnumsGame.GameSkills skill) {
        int retorno = 0;

        if (skill.Equals(EnumsGame.GameSkills.FIRE_BASIC))
            retorno = skillsSituation[0];

        else if (skill.Equals(EnumsGame.GameSkills.ELETRIC_BASIC))
            retorno = skillsSituation[1];

        else if (skill.Equals(EnumsGame.GameSkills.WATER_BASIC))
            retorno = skillsSituation[2];

        else if (skill.Equals(EnumsGame.GameSkills.POISON))
            retorno = skillsSituation[3];

        else if (skill.Equals(EnumsGame.GameSkills.EARTHQUAKE))
            retorno = skillsSituation[4];

        else if (skill.Equals(EnumsGame.GameSkills.ARKANE))
            retorno = skillsSituation[5];

        else if (skill.Equals(EnumsGame.GameSkills.FIRE_PLUS))
            retorno = skillsSituation[6];

        else if (skill.Equals(EnumsGame.GameSkills.ELETRIC_PLUS))
            retorno = skillsSituation[7];

        else if (skill.Equals(EnumsGame.GameSkills.WATER_CURE))
            retorno = skillsSituation[8];

        else if (skill.Equals(EnumsGame.GameSkills.WATER_COMBO))
            retorno = skillsSituation[9];

        else if (skill.Equals(EnumsGame.GameSkills.COMBO_FIRE))
            retorno = skillsSituation[10];

        else if (skill.Equals(EnumsGame.GameSkills.COMBO_ELETRIC))
            retorno = skillsSituation[11];

        else if (skill.Equals(EnumsGame.GameSkills.BUBBLE_GEMSTONE))
            retorno = skillsSituation[12];

        else if (skill.Equals(EnumsGame.GameSkills.POWER_GEMSTONE))
            retorno = skillsSituation[13];

        else if (skill.Equals(EnumsGame.GameSkills.HP_GEMSTONE))
            retorno = skillsSituation[14];

        return retorno;
    }

    public float waterCreaterand() {
        return 94;
    }
    
    public void addExp(int exp) {
        totalExp += (int)(exp * expMultiplier);
    }

    public bool deliverASoul() {

        if(totalSouls > 0) {
            totalSouls -= 1;
            addExp(1);
            return true;
        }

        return false;

    }
    
    /*Fazer um cálculo para o level*/
    public int getPlayerLevel() {

        if (totalExp >= 6000)
            return 20;
        else if (totalExp >= 4900)
            return 19;
        else if (totalExp >= 4200)
            return 18;
        else if (totalExp >= 3700)
            return 17;
        else if (totalExp >= 3300)
            return 16;
        else if (totalExp >= 2900)
            return 15;
        else if (totalExp >= 2600)
            return 14;
        else if (totalExp >= 2300)
            return 13;
        else if (totalExp >= 2000)
            return 12;
        else if (totalExp >= 1750)
            return 11;
        else if (totalExp >= 1500)
            return 10;
        else if (totalExp >= 1250)
            return 09;
        else if (totalExp >= 1000)
            return 08;
        else if (totalExp >= 800)
            return 07;
        else if (totalExp >= 650)
            return 06;
        else if (totalExp >= 500)
            return 05;
        else if (totalExp >= 350)
            return 04;
        else if (totalExp >= 220)
            return 03;
        else if (totalExp >= 100)
            return 02;

        return 1;
    }

    public float getTimeToCreateBubble() {
        float retorno = 6;
        retorno -= (getPlayerLevel() - 1.0f) * 3.0f / 19.0f;

        Debug.Log("Time to create Bubble = " + retorno);

        return retorno;
    }

    public int quantBolhas() {
        int level = getPlayerLevel();
        if (level >= 16)
            return 5;
        else if (level >= 10)
            return 4;
        else if (level >= 6)
            return 3;
        return 2;
    }

    public int waterBubbleFrequency() {
        int level = getPlayerLevel();
        if (level >= 14)
            return 4;
        else if (level >= 9)
            return 6;
        else if (level >= 5)
            return 8;
        return 10;
    }

    public int maxWaterBubblesOnScene() {
        int level = getPlayerLevel();
        if (level >= 12)
            return 2;
        return 1;
    }

    public int hpMax() {
        return 60 + getPlayerLevel() * 12;
    }

}
