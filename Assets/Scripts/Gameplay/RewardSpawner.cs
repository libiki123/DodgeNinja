using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardSpawner : MonoBehaviour
{
    [SerializeField] private Grid grid;

    [Header("Rewards")]
    [SerializeField] private Reward coin;
    [SerializeField] private Reward battery;

    Cell coinCell;

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
        coinCell = grid.GetRandomSpawnableCell();
        coin.transform.position = coinCell.transform.position;
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
        Cell batteryCell = grid.GetRandomSpawnableCell();
        while (batteryCell == coinCell)
        {
            batteryCell = grid.GetRandomSpawnableCell();
        }
        battery.transform.position = batteryCell.transform.position;
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
