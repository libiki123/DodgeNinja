using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardSpawner : MonoBehaviour
{
    [SerializeField] private Reward coin;
    [SerializeField] private Reward battery;

    [SerializeField] private Grid grid;

    private int coinCount = 0;
    private bool isBatteryCollected = true;

    // Start is called before the first frame update
    void Start()
    {
        coin.OnRewardCollected += CoinCollected;
        battery.OnRewardCollected += BatteryCollected;
    }

    public void SpawnCoin()
    {
        int randomIndex = GetRandomCellIndex();

        grid.SetCellType(randomIndex, Grid.CellType.REWARD);
        coin.transform.position = grid.GetCellPosition(randomIndex);
        coin.gameObject.SetActive(true);

        if (isBatteryCollected)
        {
            if (coinCount == 3)
            {
                SpawnBattery();
            }
        }
    }

    private void SpawnBattery()
    {
        Debug.Log("SPAWN BATTERY");
        int randomIndex = GetRandomCellIndex();
        grid.SetCellType(randomIndex, Grid.CellType.REWARD);
        battery.transform.position = grid.GetCellPosition(randomIndex);
        battery.gameObject.SetActive(true);
        isBatteryCollected = false;
        
    }

    private int GetRandomCellIndex()
    {
        int randomIndex = Random.Range(1, grid.GetCellCount());

        while (!grid.IsCellAvailable(randomIndex))
        {
            randomIndex += Random.Range(3, 6);
            if (randomIndex > grid.GetCellCount()) randomIndex = randomIndex - grid.GetCellCount();
        }
        return randomIndex;
    }

    private void CoinCollected()
    {
        ScoreManager.Instance.AddPoint(false);
        coinCount++;
        SpawnCoin();
    }

    private void BatteryCollected()
    {
        ScoreManager.Instance.AddPoint(true);
        isBatteryCollected = true;
        coinCount = 0;
    }

    private void OnDestroy()
    {
        coin.OnRewardCollected -= CoinCollected;
        battery.OnRewardCollected -= BatteryCollected;
    }
}
