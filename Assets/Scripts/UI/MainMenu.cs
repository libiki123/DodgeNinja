using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Mathematics;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public static MainMenu Instance { get; private set; }

    [Header ("Player Resources")]
    [SerializeField] private RectMask2D batteryProgressBar;
    [SerializeField] private TMP_Text batteryProgressText;
    [SerializeField] private TMP_Text coinText;

    private int battery;
    public int totalCoin { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        Init();

        GameManager.Instance.DoneLoadScene += Init;
    }

    public void Init()
    {
        battery = GameManager.Instance.saveData.batteryProgress;
        float percentage = math.remap(0, 50, 210, 5, battery);
        batteryProgressBar.padding = new Vector4(0,0,0, percentage);
        batteryProgressText.text = battery.ToString() + " / 50";
        totalCoin = GameManager.Instance.saveData.totalCoin;
        coinText.text = totalCoin.ToString();
    }

    public void LoadGame()
    {
        GameManager.Instance.StartGame();
    }

    private void OnDestroy()
    {
        GameManager.Instance.DoneLoadScene -= Init;
    }
}
