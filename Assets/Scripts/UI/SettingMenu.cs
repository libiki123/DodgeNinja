using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour, IDataPersistence
{
    [SerializeField] private GameObject buttonControlIcon;
    [SerializeField] private GameObject swipeControlIcon;
    [SerializeField] private GameObject controlTypePciker;

    [Header("Refs")]
    [SerializeField] private Toggle sfxToggle;
    [SerializeField] private Toggle musicToggle;
    [SerializeField] private Toggle controlTypeSwitch;

    private Image sfxToggleIMG;
    private Image musicToggleIMG;
    private Image controlTypeSwitchIMG;

    private void Awake()
    {
        sfxToggleIMG = sfxToggle.GetComponent<Image>();
        musicToggleIMG = musicToggle.GetComponent<Image>();
        controlTypeSwitchIMG = controlTypeSwitch.GetComponent<Image>();
    }

    public void OnToggleSFX()
    {
        if (sfxToggle.isOn)
        {
            sfxToggleIMG.sprite = Resources.Load<Sprite>("Sprites/toggle_setting_off");
            AudioManager.instance.sfxMute = true;
            AudioManager.instance.UpdateAudioVolume();
        }
        else
        {
            sfxToggleIMG.sprite = Resources.Load<Sprite>("Sprites/toggle_setting_on");
            AudioManager.instance.sfxMute = false;
            AudioManager.instance.UpdateAudioVolume();
        }
    }

    public void OnToggleMusic()
    {
        if (musicToggle.isOn)
        {
            musicToggleIMG.sprite = Resources.Load<Sprite>("Sprites/toggle_setting_off");
            AudioManager.instance.musicMute = true;
            AudioManager.instance.UpdateAudioVolume();
        }
        else
        {
            musicToggleIMG.sprite = Resources.Load<Sprite>("Sprites/toggle_setting_on");
            AudioManager.instance.musicMute = false;
            AudioManager.instance.UpdateAudioVolume();
        }
    }

    public void OnSwitchingControlType()
    {
        if (controlTypeSwitch.isOn)
        {
            controlTypeSwitchIMG.sprite = Resources.Load<Sprite>("Sprites/toggle_control_left");
            buttonControlIcon.SetActive(true);
            swipeControlIcon.SetActive(false);
        }
        else
        {
            controlTypeSwitchIMG.sprite = Resources.Load<Sprite>("Sprites/toggle_control_right");
            buttonControlIcon.SetActive(false);
            swipeControlIcon.SetActive(true);
        }
    }

    public void OnCloseSettingClick()
    {
        DataPersistenceManager.instance.SaveGame();
        gameObject.SetActive(false);
    }

    public void LoadData(GameData data)
    {
        switch (data.gameSetting.contronlType)
        {
            case 0:
                controlTypePciker.SetActive(false);
                break;
            case 1:
                controlTypePciker.SetActive(true);
                controlTypeSwitch.isOn = true;
                break;
            case 2:
                controlTypePciker.SetActive(true);
                controlTypeSwitch.isOn = false;
                break;
        }
        
        musicToggle.isOn = data.gameSetting.muteMusic;
        sfxToggle.isOn = data.gameSetting.muteSFX;
    }

    public void SaveData(ref GameData data)
    {
        data.gameSetting.muteMusic = musicToggle.isOn;
        data.gameSetting.muteSFX = sfxToggle.isOn;
        if(controlTypePciker.activeSelf)
            data.gameSetting.contronlType = controlTypeSwitch.isOn? 1 : 2;
    }

    private void OnEnable()
    {
        DataPersistenceManager.instance.RefreshDataPersistenceObjs();
        DataPersistenceManager.instance.LoadGame();
    }

}
