using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] private List<SideSpawner> sideSpawners = new List<SideSpawner>();
    [SerializeField] private GroundSpawner groundSpawner;

    [SerializeField] private float delay = 3.0f;

    

    void Start()
    {
        StartCoroutine(SpawnWave());
    }

    void Update()
    {
        if (groundSpawner.isCoinCollected)
        {
            groundSpawner.SpawnReward();
        }
    }

    IEnumerator SpawnWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            int count = Random.Range(1, 4);
            List<int> randNums = Utils.GenerateRandomNumbers(count, 0, sideSpawners.Count);
            
            foreach(var num in randNums)
            {
                sideSpawners[num].SpawnObstacle();
            }

        }
    }
}
