using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardSpawner : MonoBehaviour
{
    [SerializeField] private Grid grid;

    [Header("Rewards")]
    [SerializeField] private Reward coin;
    [SerializeField] private Reward battery;

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
        int randomIndex = grid.GetRandomSpawnableCellIndex();

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
        int randomIndex = grid.GetRandomSpawnableCellIndex();
        grid.SetCellType(randomIndex, Grid.CellType.REWARD);
        battery.transform.position = grid.GetCellPosition(randomIndex);
        battery.gameObject.SetActive(true);
        isBatteryCollected = false;
        
    }

    private void CoinCollected()
    {
        UIManager.Instance.AddPoint();
        coinCount++;
        SpawnCoin();
    }

    private void BatteryCollected()
    {
        UIManager.Instance.AddBattery();
        isBatteryCollected = true;
        coinCount = 0;
    }

    private void OnDestroy()
    {
        coin.OnRewardCollected -= CoinCollected;
        battery.OnRewardCollected -= BatteryCollected;
    }
}
