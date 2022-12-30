using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] private List<SideSpawner> sideSpawners = new List<SideSpawner>();
    [SerializeField] private TrapSpawner trapSpawner;
    [SerializeField] private RewardSpawner rewardSpawner;

    [SerializeField] private SpawnerManager_SO waveData;
    [SerializeField] private float gunWaveDelay = 3.0f;
    [SerializeField] private float dropTrapWaveDelay = 4.0f;

    private int maxTrapAmount = 8; //25
    private int totalTrapCount = 0;
    private int spikeTrapCount = 0;
    private int currentWaveGroupIndex = 0;
    void Start()
    {
        UIManager.Instance.PointAdded += SpwanTrapWave;

        rewardSpawner.SpawnCoin();
        StartCoroutine(SpawnWave());
        StartCoroutine(SpawnDropTrap());
    }

    private IEnumerator SpawnWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(gunWaveDelay);
            int numOfNachine = GetNumOfMachine();
            int numOfBullet = GetNumOfBullet(numOfNachine);

            List<int> randomListOfMachineIndex = Utils.GenerateRandomNumbers(numOfNachine, 0, sideSpawners.Count - 1);
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
        if (totalTrapCount == maxTrapAmount) return;

        switch (UIManager.Instance.score)
        {
            case 2:
                trapSpawner.SpawnSpikeTrap();
                totalTrapCount++;
                break;
            case 5:
                trapSpawner.SpawnBlockTrap();
                totalTrapCount++;
                break;
            default:
                if(UIManager.Instance.score % 5 == 0)
                {
                    if(spikeTrapCount >= 3)
                    {
                        trapSpawner.SpawnBlockTrap();
                        spikeTrapCount = 0;
                        totalTrapCount++;
                    }
                    else
                    {
                        trapSpawner.SpawnSpikeTrap();
                        spikeTrapCount++;
                        totalTrapCount++;
                    }
                }
                break;
        }
    }

    private IEnumerator SpawnDropTrap()
    {
        while (true)
        {
            if (UIManager.Instance.score >= 5)
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
        switch (UIManager.Instance.score)
        {
            case < 10: // wave group 1-9
                currentWaveGroupIndex = 0;
                break;
            case < 20:
                currentWaveGroupIndex = 1;
                break;
            case < 30:
                currentWaveGroupIndex = 2;
                break;
            case < 40:
                currentWaveGroupIndex = 3;
                break;
            case < 50:
                currentWaveGroupIndex = 4;
                break;
            case >= 50:
                currentWaveGroupIndex = 5;
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
        UIManager.Instance.PointAdded -= SpwanTrapWave;
    }
}
