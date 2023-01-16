using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;

public class Shop : MonoBehaviour, IDataPersistence
{
    public enum ShopItemType { SKIN, STAGE }
    public static Shop instance { get; private set; }

    [Header("Refs")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject stage;


    [Header("Shop Elements")]
    [SerializeField] private Toggle shopBttn;
    [SerializeField] private Button skinBttn;
    [SerializeField] private Button stageBttn;
    [SerializeField] private RectTransform shopMenu;
    [SerializeField] private RectTransform mainMenu;
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
    private Image skinBttnIMG;
    private Image stageBttnIMG;

    private string currentSkinId = "";
    private string currentStageId = "";
    private int currentTotalCoin;
    private int currentTotalScroll;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            gameObject.SetActive(false);
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        skinBttnRT = skinBttn.GetComponent<RectTransform>();
        stageBttnRT = stageBttn.GetComponent<RectTransform>();
        skinBttnIMG = skinBttn.GetComponent<Image>();
        stageBttnIMG = stageBttn.GetComponent<Image>();

        DataPersistenceManager.instance.RefreshDataPersistenceObjs();
        DataPersistenceManager.instance.LoadGame();
        InitShop();
    }

    private void InitShop()
    {
        for (int i = 0; i < ShopData.skins.Count; i++)
        {
            GameObject g = Instantiate(shopSkinPrefab, shopSkinPrefab.transform.parent);
            g.SetActive(true);
            g.GetComponent<ShopItem>().Init(ShopData.skins[i], ShopItemType.SKIN);
            if (currentSkinId == "" && i == 0)
                g.GetComponent<ShopItem>().SetUsing(ref skinCheckMark);
            else if(ShopData.skins[i].id == currentSkinId)
                g.GetComponent<ShopItem>().SetUsing(ref skinCheckMark);
        }

        for (int i = 0; i < ShopData.stages.Count; i++)
        {
            GameObject g = Instantiate(shopStagePrefab, shopStagePrefab.transform.parent);
            g.SetActive(true);
            g.GetComponent<ShopItem>().Init(ShopData.stages[i], ShopItemType.STAGE);
            if (currentStageId == "" && i == 0)
                g.GetComponent<ShopItem>().SetUsing(ref stageCheckMark);
            else if (ShopData.stages[i].id == currentStageId)
                g.GetComponent<ShopItem>().SetUsing(ref stageCheckMark);
        }

        

        UpdateSkin("player");
        UpdateSkin("stage");
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
    public void OnShopItemClick(ShopItem item)
    {
        if (!item.isPurchased)
        {
            if (item.type == ShopItemType.SKIN)
            {
                if (currentTotalCoin >= item.price)
                {
                    currentTotalCoin -= item.price;
                    item.SetPurchased(true);
                }
                else
                    return;
            }
            else
            {
                if (currentTotalScroll >= item.price)
                {
                    currentTotalScroll -= item.price;
                    item.SetPurchased(true);
                }
                else
                    return;
            }
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

        DataPersistenceManager.instance.SaveGame();
        DataPersistenceManager.instance.LoadGame();
    }

    public void OnShopClick()
    {
        if (shopBttn.isOn)
        {
            DataPersistenceManager.instance.RefreshDataPersistenceObjs();
            DataPersistenceManager.instance.LoadGame();

            shopMenu.DOAnchorPosY(shopMenu.rect.height, 0.2f);
            mainMenu.gameObject.SetActive(false);

            OnSkinClick();
        }
        else
        {
            shopBttn.isOn = false;
            skinBttnRT.DOAnchorPosX(0, 0.15f);
            stageBttnRT.DOAnchorPosX(0, 0.15f);
            shopMenu.DOAnchorPosY(-150, 0.15f);
            mainMenu.gameObject.SetActive(true);
        }
    }

    public void CloseShop()
    {
        shopBttn.isOn = false;
    }

    public void OnSkinClick()
    {
        skinBttn.Select();
        skinScrollView.gameObject.SetActive(true);
        stageScrollView.gameObject.SetActive(false);
        skinBttnIMG.sprite = Resources.Load<Sprite>("Sprites/bttn_skin_selected");
        stageBttnIMG.sprite = Resources.Load<Sprite>("Sprites/bttn_stage_default");
        skinBttnRT.DOAnchorPosX(skinBttnRT.rect.width - 20f, 0.05f);
        stageBttnRT.DOAnchorPosX(stageBttnRT.rect.width, 0.1f);
    }

    public void OnStageClick()
    {
        skinScrollView.gameObject.SetActive(false);
        stageScrollView.gameObject.SetActive(true);
        skinBttnIMG.sprite = Resources.Load<Sprite>("Sprites/bttn_skin_default");
        stageBttnIMG.sprite = Resources.Load<Sprite>("Sprites/bttn_stage_selected");
        skinBttnRT.DOAnchorPosX(skinBttnRT.rect.width, 0.1f);
        stageBttnRT.DOAnchorPosX(stageBttnRT.rect.width - 20f, 0.05f);
    }

    public void LoadData(GameData data)
    {
        currentSkinId = data.currentSkinId;
        currentStageId = data.currentStageId;
        currentTotalCoin = data.totalCoin;
        currentTotalScroll = data.batteryProgress;
    }

    public void SaveData(ref GameData data)
    {
        data.currentSkinId = currentSkinId;
        data.currentStageId = currentStageId;
        data.totalCoin = currentTotalCoin;
        data.batteryProgress = currentTotalScroll;
    }
}
