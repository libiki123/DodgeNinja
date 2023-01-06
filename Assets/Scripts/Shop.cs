using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;

public class Shop : MonoBehaviour, IDataPersistence
{
    public enum ShopItemType { SKIN, STAGE }
    public static Shop Instance { get; private set; }

    [Header("Refs")]
    [SerializeField] private Player player;
    [SerializeField] private GameObject stage;

    [Header("Shop Elements")]
    [SerializeField] private Toggle shopBttn;
    [SerializeField] private Button skinBttn;
    [SerializeField] private Button stageBttn;
    [SerializeField] private RectTransform shopMenu;
    [SerializeField] private Transform skinScrollView;
    [SerializeField] private Transform stageScrollView;

    [Header("Shop Data")]
    [SerializeField] private Shop_SO ShopData;
    [SerializeField] private GameObject shopItemPrefab;

    private RectTransform skinBttnRT;
    private RectTransform stageBttnRT;

    private string currentSkinId = "";
    private string currentStageId = "";
    private int currentTotalCoin;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            gameObject.SetActive(false);
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        skinBttnRT = skinBttn.GetComponent<RectTransform>();
        stageBttnRT = stageBttn.GetComponent<RectTransform>();

        InitShop();
        DataPersistenceManager.Instance.RefreshDataPersistenceObjs();
        DataPersistenceManager.Instance.LoadGame();
    }

    private void InitShop()
    {
        foreach(ShopItemData skin in ShopData.skins)
        {
            GameObject g = Instantiate(shopItemPrefab, skinScrollView);
            g.SetActive(true);
            g.GetComponent<ShopItem>().Init(skin, ShopItemType.SKIN);
        }

    }

    public void OnShopItemClick(ShopItem item)
    {
        if (item.isPurchased)
        {
            if (item.type == ShopItemType.SKIN)
                currentSkinId = item.id;
            else
                currentStageId = item.id;
        }
        else if (currentTotalCoin >= item.price)
        {
            currentTotalCoin -= item.price;
            if (item.type == ShopItemType.SKIN)
                currentSkinId = item.id;
            else
                currentStageId = item.id;
            item.SetPurchased(true);
        }
        else
        {
            return;
        }

        DataPersistenceManager.Instance.SaveGame();
        DataPersistenceManager.Instance.LoadGame();
        
    }

    //============================================ Button Event ============================================//
    public void OnShopClick()
    {
        if (shopBttn.isOn)
        {
            skinBttnRT.DOAnchorPosX(skinBttnRT.rect.width, 0.2f);
            stageBttnRT.DOAnchorPosX(stageBttnRT.rect.width, 0.2f);
            shopMenu.DOAnchorPosY(shopMenu.rect.height, 0.2f);
        }
        else
        {
            skinBttnRT.DOAnchorPosX(0, 0.2f);
            stageBttnRT.DOAnchorPosX(0, 0.2f);
            shopMenu.DOAnchorPosY(0, 0.2f);
        }
    }

    public void OnSkinClick()
    {
        skinScrollView.gameObject.SetActive(true);
        stageScrollView.gameObject.SetActive(false);
    }

    public void OnStageClick()
    {
        skinScrollView.gameObject.SetActive(false);
        stageScrollView.gameObject.SetActive(true);
    }

    public void LoadData(GameData data)
    {
        currentSkinId = data.currentSkinId;
        //currentStageId = data.currentStageId;
        currentTotalCoin = data.totalCoin;
    }

    public void SaveData(ref GameData data)
    {
        data.currentSkinId = currentSkinId;
        //data.currentStageId = currentStageId;
        data.totalCoin = currentTotalCoin;
    }

}
