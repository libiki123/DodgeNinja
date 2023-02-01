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
    [SerializeField] private GameObject confirmGroup;

    [Header("Shop Data")]
    [SerializeField] private Shop_SO ShopData;
    [SerializeField] private GameObject shopSkinPrefab;
    [SerializeField] private GameObject shopStagePrefab;

    private RectTransform skinBttnRT;
    private RectTransform stageBttnRT;
    private Image skinBttnIMG;
    private Image stageBttnIMG;
    private ShopItem selectedItem;
    private GameObject currentSkinEffect;
    private GameObject currentStageEffect;

    private string currentSkinId = "";
    private string currentStageId = "";

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

                //if (currentSkinEffect != null) Destroy(currentSkinEffect);
                //if (ShopData.skins[0].effect != null) currentSkinEffect = Instantiate(ShopData.skins[0].effect, player.transform.Find("Root_M"));
            }

            foreach (var skin in ShopData.skins)
            {
                if (skin.id == currentSkinId)
                {
                    SMR.sharedMesh = skin.mesh;
                    SMR.material = skin.material;

                    //if (currentSkinEffect != null) Destroy(currentSkinEffect);
                    //if (skin.effect != null) currentSkinEffect = Instantiate(skin.effect, player.transform.Find("Root_M"));
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

                //if (currentStageEffect != null) Destroy(currentStageEffect);
                //if (ShopData.skins[0].effect != null) currentStageEffect = Instantiate(ShopData.stages[0].effect, stage.transform);
            }

            foreach (var stage in ShopData.stages)
            {
                if (stage.id == currentStageId)
                {
                    SF.sharedMesh = stage.mesh;
                    SR.material = stage.material;

                    //if (currentStageEffect != null) Destroy(currentStageEffect);
                    //if (stage.effect != null) currentStageEffect = Instantiate(stage.effect, this.stage.transform);
                }
            }
        }
        
    }

    //============================================ Button Event ============================================//
    public void OnShopItemClick(ShopItem item)
    {
        

        if (selectedItem != null)
        {
            if (selectedItem == item)
                return;
            selectedItem.transform.DOScale(Vector3.one, 0.1f);
        }

        selectedItem = item;

        if (!selectedItem.isPurchased)
        {
            if (selectedItem.type == ShopItemType.SKIN)
            {
                if (MainMenu.instance.totalCoin >= selectedItem.price)
                {
                    selectedItem.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.1f);
                    skinCheckMark.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
                    stageCheckMark.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
                    confirmGroup.SetActive(true);
                    return;
                }
            }
            else
            {
                if (MainMenu.instance.totalScroll >= selectedItem.price)
                {
                    selectedItem.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.1f);
                    skinCheckMark.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
                    stageCheckMark.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
                    confirmGroup.SetActive(true);
                    return;
                }
            }

            selectedItem.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.1f);
            skinCheckMark.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
            stageCheckMark.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
            confirmGroup.SetActive(false);
            return;
        }

        confirmGroup.SetActive(false);
        selectedItem.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.1f);
        skinCheckMark.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        stageCheckMark.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);

        if (selectedItem.type == ShopItemType.SKIN)
        {
            currentSkinId = selectedItem.id;
            selectedItem.SetUsing(ref skinCheckMark);
            UpdateSkin("player");
        }
        else
        {
            currentStageId = selectedItem.id;
            selectedItem.SetUsing(ref stageCheckMark);
            UpdateSkin("stage");
        }

        DataPersistenceManager.instance.SaveGame();
        DataPersistenceManager.instance.LoadGame();
    }

    public void purchaseConfirm()
    {
        if (selectedItem.type == ShopItemType.SKIN)
        {
            if (MainMenu.instance.totalCoin >= selectedItem.price)
            {
                MainMenu.instance.totalCoin -= selectedItem.price;
                selectedItem.SetPurchased(true);
            }
            else
                return;
        }
        else
        {
            if (MainMenu.instance.totalScroll >= selectedItem.price)
            {
                MainMenu.instance.totalScroll -= selectedItem.price;
                selectedItem.SetPurchased(true);
            }
            else
                return;
        }

        confirmGroup.SetActive(false);

        if (selectedItem.type == ShopItemType.SKIN)
        {
            currentSkinId = selectedItem.id;
            selectedItem.SetUsing(ref skinCheckMark);
            UpdateSkin("player");
        }
        else
        {
            currentStageId = selectedItem.id;
            selectedItem.SetUsing(ref stageCheckMark);
            UpdateSkin("stage");
        }

        DataPersistenceManager.instance.SaveGame();
        DataPersistenceManager.instance.LoadGame();
    }

    public void purchaseCancel()
    {
        confirmGroup.SetActive(false);
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
            shopMenu.DOAnchorPosY(-550, 0.15f);
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
        confirmGroup.SetActive(false);
        skinScrollView.gameObject.SetActive(true);
        stageScrollView.gameObject.SetActive(false);
        //skinBttnIMG.sprite = Resources.Load<Sprite>("Sprites/bttn_skin_default");
        //stageBttnIMG.sprite = Resources.Load<Sprite>("Sprites/bttn_stage_selected");
        skinBttn.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.2f);
        stageBttnRT.transform.DOScale(new Vector3(1f, 1f, 1f), 0.1f);
        stageBttnRT.DOAnchorPosX(skinBttnRT.rect.width - 30f, 0.05f);
        skinBttnRT.DOAnchorPosX(stageBttnRT.rect.width, 0.1f);
    }

    public void OnStageClick()
    {
        confirmGroup.SetActive(false);
        skinScrollView.gameObject.SetActive(false);
        stageScrollView.gameObject.SetActive(true);
        //skinBttnIMG.sprite = Resources.Load<Sprite>("Sprites/bttn_skin_selected");
        //stageBttnIMG.sprite = Resources.Load<Sprite>("Sprites/bttn_stage_default");
        stageBttnRT.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.2f);
        skinBttnRT.transform.DOScale(new Vector3(1f, 1f, 1f), 0.1f);
        stageBttnRT.DOAnchorPosX(skinBttnRT.rect.width, 0.1f);
        skinBttnRT.DOAnchorPosX(stageBttnRT.rect.width - 30f, 0.05f);
    }

    public void LoadData(GameData data)
    {
        currentSkinId = data.currentSkinId;
        currentStageId = data.currentStageId;
    }

    public void SaveData(ref GameData data)
    {
        data.currentSkinId = currentSkinId;
        data.currentStageId = currentStageId;
    }
}
