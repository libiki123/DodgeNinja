using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
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
    }

    private void InitShop()
    {
        foreach(var skin in ShopData.skins)
        {
            GameObject g = Instantiate(shopItemPrefab, skinScrollView);
            g.SetActive(true);
            g.GetComponent<ShopItem>().Init(skin.id, skin.icon, skin.price);
        }
    }

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

    public void UpdatePlayerSkin()
    {
        player.UpdateSkin();
    }
}
