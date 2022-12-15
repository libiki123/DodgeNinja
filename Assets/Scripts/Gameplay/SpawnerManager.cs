using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] private List<SideSpawner> sideSpawners = new List<SideSpawner>();
    [SerializeField] private RewardSpawner rewardSpawner;

    [SerializeField] private float delay = 3.0f;

    

    void Start()
    {
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        rewardSpawner.SpawnCoin();

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
        }
    }
}
