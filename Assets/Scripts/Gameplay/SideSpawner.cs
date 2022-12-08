using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideSpawner : MonoBehaviour
{

    enum SpawnerLocation { TOP, BOTTOM, LEFT, RIGHT }
    [SerializeField] SpawnerLocation spawnerLocation;
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private List<Transform> spawnPoints = new List<Transform>();

    [SerializeField] private float speed = 2.0f;

    Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        switch (spawnerLocation)
        {
            case SpawnerLocation.TOP:
                direction = Vector3.back;
                break;
            case SpawnerLocation.BOTTOM:
                direction = Vector3.forward;
                break;
            case SpawnerLocation.LEFT:
                direction = Vector3.right;
                break;
            case SpawnerLocation.RIGHT:
                direction = Vector3.left;
                break;
        }
    }

    public void SpawnObstacle()
    {
        int count = Random.Range(1, 3);
        List<int> randNums = Utils.GenerateRandomNumbers(count, 0, spawnPoints.Count);

       foreach(var num in randNums)
        {
            GameObject a = Instantiate(obstaclePrefab, spawnPoints[num].position, Quaternion.identity);
            a.GetComponent<Obstacle>().Init(direction, speed);
        }
    }

}
