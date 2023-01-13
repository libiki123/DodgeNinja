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
    [SerializeField] private Transform checkMarkPos;
    [SerializeField] private GameObject lockBackground;

    [Header("Item Info - Auto Populate")]
    public Shop.ShopItemType type;
    public int price;
    public string id;
    public bool isPurchased = false;

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
            lockBackground.gameObject.SetActive(false);
            this.isPurchased = true;
        }
        else
        {
            priceText.gameObject.SetActive(true);
            lockBackground.gameObject.SetActive(true);
            this.isPurchased = false;
        }
    }

    public void SetUsing(ref RectTransform checkMark)
    {
        checkMark.SetParent(checkMarkPos);
        checkMark.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
    }

    public void OnItemClick()
    {
        Shop.instance.OnShopItemClick(this);
    }

    public void LoadData(GameData data)
    {
        data.itemPurchased.TryGetValue(id, out isPurchased);
        SetPurchased(isPurchased);
    }

    public void SaveData(ref GameData data)
    {
        if (data.itemPurchased.ContainsKey(id))
        {
            data.itemPurchased.Remove(id);
        }
        data.itemPurchased.Add(id, isPurchased);
    }
}
