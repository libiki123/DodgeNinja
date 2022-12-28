using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpawnManager_SO")]
public class SpawnerManager_SO : ScriptableObject
{
    public WaveGroupInfo[] waveGroupInfo = new WaveGroupInfo[6];
}

[System.Serializable]
public struct WaveGroupInfo
{
    public string name;
    public WaveDelayPercentage waveDelay;
    public NumberOfMachinePercentage numOfMerchine;
}

[System.Serializable]
public struct WaveDelayPercentage
{
    public float _1sChance;
    public float _1_5sChance;
    public float _2sChance;
    public float _2_5sChance;
    public float _3sChance;
}

[System.Serializable]
public struct NumberOfMachinePercentage
{
    [Header("1 Machine")]
    public float _1MachineChance;
    public NumberOfBulletPercentage _1M_NumOfBullet;
    
    [Space]
    [Header("2 Machine")]
    public float _2MachineChance;
    public NumberOfBulletPercentage _2M_NumOfBullet;

    [Space]
    [Header("3 Machine")]
    public float _3MachineChance;
    public NumberOfBulletPercentage _3M_NumOfBullet;

    [Space]
    [Header("4 Machine")]
    public float _4MachineChance;
    public NumberOfBulletPercentage _4M_NumOfBullet;

}

[System.Serializable]
public struct NumberOfBulletPercentage
{
    public float _1_BulletChance;
    public float _2_BulletChance;
    public float _3_BulletChance;
}

