using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Mathematics;
using TMPro;
using DG.Tweening;

public class MainMenu : MonoBehaviour, IDataPersistence
{
    public static MainMenu instance { get; private set; }

    [SerializeField] private SceneLoader sceneLoader;

    [Header ("Player Resources")]
    [SerializeField] private RectMask2D batteryProgressBar;
    [SerializeField] private TMP_Text batteryProgressText;
    [SerializeField] private TMP_Text coinText;

    [Header("Setting")]
    [SerializeField] private GameObject SettingMenu;

    private int battery;
    public int totalCoin { get; private set; }
    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this);
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        AudioManager.instance.InitializeMusic(FMODEvents.instance.mainMenuBMG);
    }

    public void LoadData(GameData data)
    {
        battery = data.batteryProgress;
        //float percentage = math.remap(0, 50, 360, 5, battery);
        //batteryProgressBar.padding = new Vector4(0, 0, 0, percentage);
        batteryProgressText.text = battery.ToString();
        totalCoin = data.totalCoin;
        coinText.text = totalCoin.ToString();
    }

    public void SaveData(ref GameData data)
    {
        
    }

    public void OnPlayClick()
    {
        AudioManager.instance.musicEventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        StartCoroutine(sceneLoader.EndTransition());
    }

    public void OnSettingClick()
    {
        SettingMenu.SetActive(true);
    }

    public void PlayButtonClickSound()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.buttonClick, Vector3.zero);
        AudioManager.instance.musicEventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
