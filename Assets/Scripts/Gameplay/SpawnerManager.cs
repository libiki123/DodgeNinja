using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public static SpawnerManager instance;
    [SerializeField] private List<SideSpawner> sideSpawners = new List<SideSpawner>();
    [SerializeField] private TrapSpawner trapSpawner;
    [SerializeField] private RewardSpawner rewardSpawner;

    [SerializeField] private SpawnerManager_SO waveData;
    [SerializeField] private float gunWaveDelay = 2.0f;
    [SerializeField] private float dropTrapWaveDelay = 4.0f;

    private int currentWaveGroupIndex = 0;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this);
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        UIManager.instance.PointAdded += SpwanTrapWave;

        rewardSpawner.SpawnCoin();

        StartCoroutine(SpawnWave());
        StartCoroutine(SpawnDropTrap());
    }

    public void StopSpawningWave()
    {
        StopAllCoroutines();
    }

    private IEnumerator SpawnWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(gunWaveDelay);
            int numOfNachine = GetNumOfMachine();
            int numOfBullet = GetNumOfBullet(numOfNachine);

            List<int> randomListOfMachineIndex = Utils.GenerateRandomNumbers(numOfNachine, 0, sideSpawners.Count);
            foreach (var index in randomListOfMachineIndex)
            {
                sideSpawners[index].SpawnObstacle(numOfBullet);
            }
        
            UpdateWaveGroupDelay();
        }
    }

    private void SpwanTrapWave()
    {
        UpdateWaveGroupIndex();

        switch (UIManager.instance.score)
        {
            case 5:
                trapSpawner.SpawnSpikeTrap();
                break;
            case 10:
                trapSpawner.SpawnSpikeTrap();
                break;
            case 20:
                trapSpawner.SpawnSpikeTrap();
                break;
            case 25:
                trapSpawner.SpawnSpikeTrap();
                break;
            case 30:
                trapSpawner.SpawnBlockTrap();
                break;
            case 35:
                trapSpawner.SpawnBlockTrap();
                break;
            case 40:
                trapSpawner.SpawnSpikeTrap();
                break;
            case 45:
                trapSpawner.SpawnSpikeTrap();
                break;
            case 50:
                trapSpawner.SpawnBlockTrap();
                break;
        }
    }

    private IEnumerator SpawnDropTrap()
    {

        while (true)
        {
            if (UIManager.instance.score >= 15)
            {
                yield return new WaitForSeconds(dropTrapWaveDelay);

                trapSpawner.SpawnDropTrap();
            }
            else
                yield return null;  
        }
    }

    private void UpdateWaveGroupIndex()
    {
        switch (UIManager.instance.score)
        {
            case < 5: // wave group 1-5
                currentWaveGroupIndex = 0;
                break;
            case < 10:
                currentWaveGroupIndex = 1;
                break;
            case < 15:
                currentWaveGroupIndex = 2;
                break;
            case < 20:
                currentWaveGroupIndex = 3;
                break;
            case < 25:
                currentWaveGroupIndex = 4;
                break;
            case < 30:
                currentWaveGroupIndex = 5;
                break;
            case < 35:
                currentWaveGroupIndex = 6;
                break;
            case < 40:
                currentWaveGroupIndex = 7;
                break;
            case < 45:
                currentWaveGroupIndex = 8;
                break;
            case < 50:
                currentWaveGroupIndex = 9;
                break;
            case >= 50:
                currentWaveGroupIndex = 10;
                break;
        }
    }

    private void UpdateWaveGroupDelay()
    {
        WaveGroupInfo waveGroupInfo = waveData.waveGroupInfo[currentWaveGroupIndex];
        int chance = GetChanceIndex(waveGroupInfo.waveDelay._1sChance, waveGroupInfo.waveDelay._1_5sChance, waveGroupInfo.waveDelay._2sChance, waveGroupInfo.waveDelay._2_5sChance, waveGroupInfo.waveDelay._3sChance);

        switch (chance) 
        {
            case 1:
                gunWaveDelay = 3f;
                break;
            case 2:
                gunWaveDelay = 3.5f;
                break;
            case 3:
                gunWaveDelay = 4f;
                break;
            case 4:
                gunWaveDelay = 4.5f;
                break;
            case 5:
                gunWaveDelay = 5f;
                break;
        }
    }

    private int GetNumOfMachine()
    {
        NumberOfMachinePercentage waveGroupInfo = waveData.waveGroupInfo[currentWaveGroupIndex].numOfMerchine;
        int numOfMachine = GetChanceIndex(waveGroupInfo._1MachineChance, waveGroupInfo._2MachineChance, waveGroupInfo._3MachineChance, waveGroupInfo._4MachineChance);

        return numOfMachine;
    }

    private int GetNumOfBullet(int numOfCachine)
    {
        NumberOfMachinePercentage waveGroupInfo = waveData.waveGroupInfo[currentWaveGroupIndex].numOfMerchine;
        NumberOfBulletPercentage bulletPercentage = new NumberOfBulletPercentage();
        switch (numOfCachine)
        {
            case 1:
                bulletPercentage = waveGroupInfo._1M_NumOfBullet;
                break;
            case 2:
                bulletPercentage = waveGroupInfo._2M_NumOfBullet;
                break;
            case 3:
                bulletPercentage = waveGroupInfo._3M_NumOfBullet;
                break;
            case 4:
                bulletPercentage = waveGroupInfo._4M_NumOfBullet;
                break;
        }

        int numOfBullet = GetChanceIndex(bulletPercentage._1_BulletChance, bulletPercentage._2_BulletChance, bulletPercentage._3_BulletChance);

        return numOfBullet;
    }

    private int GetChanceIndex(float percent1, float percent2 = 0, float percent3 = 0, float percent4 = 0, float percent5 = 0)
    {
        if (percent1 + percent2 + percent3 + percent4 + percent5 == 0f) return 0;

        int chanceIndex = 0;

        if(Random.Range(0f, 100.0f) <= percent1 && percent1 != 0)
        {
            chanceIndex = 1;
        }

        if (Random.Range(0f, 100.0f) <= percent2 && percent2 != 0)
        {
            chanceIndex = 2;
        }

        if (Random.Range(0f, 100.0f) <= percent3 && percent3 != 0)
        {
            chanceIndex = 3;
        }

        if (Random.Range(0f, 100.0f) <= percent4 && percent4 != 0)
        {
            chanceIndex = 4;
        }

        if (Random.Range(0f, 100.0f) <= percent5 && percent5 != 0)
        {
            chanceIndex = 5;
        }

        if (chanceIndex == 0) return GetChanceIndex(percent1, percent2, percent3, percent4, percent5);

        return chanceIndex;
    }

    private void OnDestroy()
    {
        UIManager.instance.PointAdded -= SpwanTrapWave;
    }
}
