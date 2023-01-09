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
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject stage;


    [Header("Shop Elements")]
    [SerializeField] private Toggle shopBttn;
    [SerializeField] private Button skinBttn;
    [SerializeField] private Button stageBttn;
    [SerializeField] private RectTransform shopMenu;
    [SerializeField] private Transform skinScrollView;
    [SerializeField] private Transform stageScrollView;
    [SerializeField] private RectTransform skinCheckMark;
    [SerializeField] private RectTransform stageCheckMark;

    [Header("Shop Data")]
    [SerializeField] private Shop_SO ShopData;
    [SerializeField] private GameObject shopSkinPrefab;
    [SerializeField] private GameObject shopStagePrefab;

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

        DataPersistenceManager.Instance.LoadGame();
        InitShop();
    }

    private void InitShop()
    {
        foreach(ShopItemData skin in ShopData.skins)
        {
            GameObject g = Instantiate(shopSkinPrefab, skinScrollView);
            g.SetActive(true);
            g.GetComponent<ShopItem>().Init(skin, ShopItemType.SKIN);
            if (skin.id == currentSkinId)
                g.GetComponent<ShopItem>().SetUsing(ref skinCheckMark);
        }

        foreach (ShopItemData stage in ShopData.stages)
        {
            GameObject g = Instantiate(shopStagePrefab, stageScrollView);
            g.SetActive(true);
            g.GetComponent<ShopItem>().Init(stage, ShopItemType.STAGE);
            if (stage.id == currentStageId)
                g.GetComponent<ShopItem>().SetUsing(ref stageCheckMark);
        }

        UpdateSkin("player");
        UpdateSkin("stage");
    }

    public void OnShopItemClick(ShopItem item)
    {
        if (!item.isPurchased)
        {
            if (currentTotalCoin >= item.price)
            {
                currentTotalCoin -= item.price;
                item.SetPurchased(true);
            }
            else
                return;
        }

        if (item.type == ShopItemType.SKIN)
        {
            currentSkinId = item.id;
            item.SetUsing(ref skinCheckMark);
            UpdateSkin("player");
        }
        else
        {
            currentStageId = item.id;
            item.SetUsing(ref stageCheckMark);
            UpdateSkin("stage");
        }

        DataPersistenceManager.Instance.SaveGame();
    }

    public void UpdateSkin(string objectName)
    {
        if(objectName == "player")
        {
            SkinnedMeshRenderer SMR = player.GetComponentInChildren<SkinnedMeshRenderer>();

            if (currentSkinId == "")
            {
                SMR.sharedMesh = ShopData.skins[0].mesh;
                SMR.material = ShopData.skins[0].material;
            }

            foreach (var skin in ShopData.skins)
            {
                if (skin.id == currentSkinId)
                {
                    SMR.sharedMesh = skin.mesh;
                    SMR.material = skin.material;
                }
            }
        }
        else if (objectName == "stage")
        {
            MeshFilter SF = stage.GetComponent<MeshFilter>();
            MeshRenderer SR = stage.GetComponent<MeshRenderer>();

            if (currentStageId == "")
            {
                SF.sharedMesh = ShopData.stages[0].mesh;
                SR.material = ShopData.stages[0].material;
            }

            foreach (var skin in ShopData.stages)
            {
                if (skin.id == currentStageId)
                {
                    SF.sharedMesh = skin.mesh;
                    SR.material = skin.material;
                }
            }
        }
        
    }

    //============================================ Button Event ============================================//
    public void OnShopClick()
    {
        if (shopBttn.isOn)
        {
            skinBttnRT.DOAnchorPosX(skinBttnRT.rect.width, 0.2f);
            stageBttnRT.DOAnchorPosX(stageBttnRT.rect.width, 0.2f);
            shopMenu.DOAnchorPosY(shopMenu.rect.height, 0.2f);

            DataPersistenceManager.Instance.RefreshDataPersistenceObjs();
            DataPersistenceManager.Instance.LoadGame();

            skinScrollView.gameObject.SetActive(true);
            stageScrollView.gameObject.SetActive(false);
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
        currentStageId = data.currentStageId;
        currentTotalCoin = data.totalCoin;
    }

    public void SaveData(ref GameData data)
    {
        data.currentSkinId = currentSkinId;
        data.currentStageId = currentStageId;
        data.totalCoin = currentTotalCoin;

    }
}
