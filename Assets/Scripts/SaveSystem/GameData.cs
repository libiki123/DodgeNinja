using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int totalCoin;
    public int highScore;
    public int batteryProgress;
    public string currentSkinId;
    public string currentStageId;
    public SerializableDictionary<string, bool> skinPurchased;
    public GameSetting gameSetting;

    public GameData()
    {
        totalCoin = 0;
        highScore = 0;
        batteryProgress = 0;
        currentSkinId = "";
        currentStageId = "";
        skinPurchased = new SerializableDictionary<string, bool>();
        gameSetting = new GameSetting();
    }
}

[System.Serializable]
public class GameSetting
{
    public bool sound;
    public bool sfx;
    public int contronlType;

    public GameSetting()
    {
        sound = true;
        sfx = true;
        contronlType = 0;
    }
}
