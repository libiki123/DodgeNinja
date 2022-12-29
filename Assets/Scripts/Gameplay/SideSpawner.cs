using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideSpawner : MonoBehaviour
{
    enum SpawnerLocation { TOP, BOTTOM, LEFT, RIGHT }
    [SerializeField] SpawnerLocation spawnerLocation;
    [SerializeField] private List<Gun> guns = new List<Gun>();

    [SerializeField] private float bulletSpeed = 3.0f;

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

    public void SpawnObstacle(int numOfObstacle)
    {
        List<int> randNums = Utils.GenerateRandomNumbers(numOfObstacle, 0, guns.Count - 1);

        foreach (var num in randNums)
        {
            guns[num].Init(direction, bulletSpeed);
        }

    }

}
