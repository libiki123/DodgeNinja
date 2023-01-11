using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    [SerializeField] private Sprite toggle_ON;
    [SerializeField] private Sprite toggle_OFF;
    [SerializeField] private Sprite switch_Right;
    [SerializeField] private Sprite switch_Left;
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

    private void Start()
    {
        sfxToggleIMG = sfxToggle.GetComponent<Image>();
        musicToggleIMG = musicToggle.GetComponent<Image>();
        controlTypeSwitchIMG = controlTypeSwitch.GetComponent<Image>();
    }

    public void OnToggleSFX()
    {
        if (sfxToggle.isOn)
        {
            sfxToggleIMG.sprite = toggle_OFF;
        }
        else
        {
            sfxToggleIMG.sprite = toggle_ON;

        }
    }

    public void OnToggleMusic()
    {
        if (musicToggle.isOn)
        {
            musicToggleIMG.sprite = toggle_OFF;
        }
        else
        {
            musicToggleIMG.sprite = toggle_ON;

        }
    }

    public void OnSwitchingControlType()
    {
        if (controlTypeSwitch.isOn)
        {
            controlTypeSwitchIMG.sprite = switch_Left;
            buttonControlIcon.SetActive(true);
            swipeControlIcon.SetActive(false);
        }
        else
        {
            controlTypeSwitchIMG.sprite = switch_Right;
            buttonControlIcon.SetActive(false);
            swipeControlIcon.SetActive(true);
        }
    }
}
