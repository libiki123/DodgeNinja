using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Grid))]
public class RewardSpawner : MonoBehaviour
{
    [SerializeField] private Grid grid;

    [Header("Rewards")]
    [SerializeField] private Reward coin;
    [SerializeField] private Reward battery;

    private Cell coinCell;
    private Cell batteryCell;

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

        if(batteryCell != null)
        {
            coinCell = grid.GetRandomSpawnableCell(false, batteryCell);
        }

        coin.transform.position = new Vector3(coinCell.transform.position.x, 0.48f, coinCell.transform.position.z);
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
        batteryCell = grid.GetRandomSpawnableCell(false, coinCell);

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
        batteryCell = null;
    }

    private void OnDestroy()
    {
        coin.OnRewardCollected -= CoinCollected;
        battery.OnRewardCollected -= BatteryCollected;
    }
}
