using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Shop_SO")]
public class Shop_SO : ScriptableObject
{
    public List<ShopItemData> skins = new List<ShopItemData>();
    public List<ShopItemData> stages = new List<ShopItemData>();
}

[System.Serializable]
public struct ShopItemData
{
    public string id;
    public Sprite icon;
    public int price;
    public Mesh mesh;
    public Material material;
    public GameObject skinEffect;
    public bool isDefault;
}

