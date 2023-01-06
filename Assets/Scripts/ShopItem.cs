using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ShopItem : MonoBehaviour, IDataPersistence
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text priceText;
    [SerializeField] private GameObject checkMark;
    [SerializeField] private GameObject lockBackground;

    [HideInInspector] public Shop.ShopItemType type;
    [HideInInspector] public int price;
    [HideInInspector] public string id;
    [HideInInspector] public bool isPurchased = false;

    private bool isDefault = false;

    public void Init(ShopItemData itemData, Shop.ShopItemType type)
    {
        id = itemData.id;
        icon.sprite = itemData.icon;
        price = itemData.price;
        priceText.text = price.ToString();
        this.type = type;
        isDefault = itemData.isDefault;
    }

    public void SetPurchased(bool isPurchased)
    {
        if (isPurchased || isDefault)
        {
            priceText.gameObject.SetActive(false);
            checkMark.SetActive(true);
            lockBackground.gameObject.SetActive(false);
            this.isPurchased = true;
        }
        else
        {
            priceText.gameObject.SetActive(true);
            checkMark.SetActive(false);
            lockBackground.gameObject.SetActive(true);
            this.isPurchased = false;
        }
    }

    public void OnSkinClick()
    {
        Shop.Instance.OnShopItemClick(this);
    }

    public void LoadData(GameData data)
    {
        data.skinPurchased.TryGetValue(id, out isPurchased);
        SetPurchased(isPurchased);
    }

    public void SaveData(ref GameData data)
    {
        if (data.skinPurchased.ContainsKey(id))
        {
            data.skinPurchased.Remove(id);
        }
        data.skinPurchased.Add(id, isPurchased);
    }
}
