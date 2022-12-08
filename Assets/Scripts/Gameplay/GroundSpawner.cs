using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    public Reward coin;

    [SerializeField] private Grid grid;

    public bool isCoinCollected = false;
    // Start is called before the first frame update
    void Start()
    {
        coin.OnRewardCollected += RewardCollected;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnReward()
    {
        int randomIndex = Random.Range(1, grid.GetCellCount());

        if (!grid.IsCellAvailable(randomIndex))
        {
            randomIndex++;
            if(randomIndex > grid.GetCellCount()) randomIndex = randomIndex - grid.GetCellCount();
        }

        grid.SetCellType(randomIndex, Grid.CellType.REWARD);
        coin.transform.position = grid.GetCell(randomIndex).transform.position;
        coin.gameObject.SetActive(true);

        isCoinCollected = false;
    }

    private void RewardCollected()
    {
        isCoinCollected = true;
        ScoreManager.Instance.AddPoint();
    }

    private void OnDestroy()
    {
        
    }
}
