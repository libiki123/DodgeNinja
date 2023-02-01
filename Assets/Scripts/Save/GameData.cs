using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int totalCoin;
    public int highScore;
    public int totalScroll;
    public string currentSkinId;
    public string currentStageId;
    public SerializableDictionary<string, bool> itemPurchased;
    public GameSetting gameSetting;

    public GameData()
    {
        totalCoin = 0;
        highScore = 0;
        totalScroll = 0;
        currentSkinId = "";
        currentStageId = "";
        itemPurchased = new SerializableDictionary<string, bool>();
        gameSetting = new GameSetting();
    }
}

[System.Serializable]
public class GameSetting
{
    public bool muteMusic;
    public bool muteSFX;
    public int contronlType;

    public GameSetting()
    {
        muteMusic = false;
        muteSFX = false;
        contronlType = 0;
    }
}
