using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] private List<SideSpawner> sideSpawners = new List<SideSpawner>();
    [SerializeField] private TrapSpawner trapSpawner;
    [SerializeField] private RewardSpawner rewardSpawner;

    [SerializeField] private float delay = 3.0f;

    private int maxTrapAmount = 25;
    private int totalTrapCount = 0;
    private int spikeTrapCount = 0;
    void Start()
    {
        UIManager.Instance.PointAdded += SpwanTrapWave;

        rewardSpawner.SpawnCoin();
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            //int count = Random.Range(1, sideSpawners.Count);
            //List<int> randNums = Utils.GenerateRandomNumbers(count, 0, sideSpawners.Count - 1);

            //foreach(var num in randNums)
            //{
            //    sideSpawners[num].SpawnObstacle();
            //}

            int randNums = Random.Range(0, 2);
            sideSpawners[randNums].SpawnObstacle();

			if (UIManager.Instance.score > 3)
			{
				//trapSpawner.SpawnDropTrap();
			}

		}
    }

    private void SpwanTrapWave()
    {
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
                    if(spikeTrapCount > 3)
                    {
                        //trapSpawner.SpawnBlockTrap();
                        spikeTrapCount = 0;
                        totalTrapCount++;
                    }
                    else
                    {
                        Debug.Log(spikeTrapCount);
                        trapSpawner.SpawnSpikeTrap();
                        spikeTrapCount++;
                        totalTrapCount++;
                    }
                }
                break;
        }
    }

    private void OnDestroy()
    {
        UIManager.Instance.PointAdded -= SpwanTrapWave;
    }
}
