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
    //[SerializeField] private RectMask2D batteryProgressBar;
    [SerializeField] private NumberCounter scrollText;
    [SerializeField] private NumberCounter coinText;

    [Header("Setting")]
    [SerializeField] private GameObject SettingMenu;

    public int totalScroll;
    public int totalCoin;
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
        totalScroll = data.totalScroll;
        scrollText.GetComponent<TMP_Text>().text = totalScroll.ToString();
        scrollText.SetDirectValue(totalScroll);
        //float percentage = math.remap(0, 50, 360, 5, battery);
        //batteryProgressBar.padding = new Vector4(0, 0, 0, percentage);

        totalCoin = data.totalCoin;
        coinText.GetComponent<TMP_Text>().text = totalCoin.ToString();
        coinText.SetDirectValue(totalCoin);
    }

    public void SaveData(ref GameData data)
    {
        data.totalCoin = totalCoin;
        data.totalScroll = totalScroll;
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

    public void OnRewardCoinClick()
    {
        MyIronSource.instance.LoadRewardAds(RewardCoin);
    }

    private void RewardCoin()
    {
        totalCoin += 20;
        coinText.value = totalCoin;
        DataPersistenceManager.instance.SaveGame();
    }

    public void OnRewardScrollClick()
    {
        MyIronSource.instance.LoadRewardAds(RewardScroll);
    }

    private void RewardScroll()
    {
        totalScroll += 5;
        scrollText.value = totalScroll;
        DataPersistenceManager.instance.SaveGame();
    }

    public void PlayButtonClickSound()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.buttonClick, Vector3.zero);
    }
}
