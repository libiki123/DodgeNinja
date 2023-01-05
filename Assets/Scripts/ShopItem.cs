using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text price;

    private string id; 
    private bool isPurchased = false;

    public void Init(string id, Sprite icon, int price)
    {
        this.id = id;
        this.icon.sprite = icon;
        this.price.text = price.ToString();
    }

    public void OnSkinClick()
    {
        GameManager.Instance.saveData.currentSkinId = id;
        GameManager.Instance.SaveGame();
        Shop.Instance.UpdatePlayerSkin();
    }
}
