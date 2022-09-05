using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    #region Singlton:Shop

    public static Shop Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion

    public List<SO_BodyPart> ShopItemsList;

    [SerializeField]
    private GameObject ItemTemplate;
    [SerializeField]
    private Transform ShopScrollView;
    [SerializeField]
    private GameObject ShopPanel;

    private Button buyButton;
    private GameObject g;

    [SerializeField]
    private GameObject OutfitChangeGameObject;

    private void Start()
    {
        int len = ShopItemsList.Count;

        for (int i = 0; i < len; i++)
        {
            g = Instantiate(ItemTemplate, ShopScrollView);
            g.transform.GetChild(0).GetComponent<Image>().sprite = ShopItemsList[i].icon;
            g.transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>().text = ShopItemsList[i].bodyPartPrice.ToString();
            buyButton = g.transform.GetChild(2).GetComponent<Button>();
            buyButton.GetComponentInChildren<TMP_Text>().text = "BUY";

            if (ShopItemsList[i].ownBodyPart)
            {
                ChangeToSellButton(i);
            }
            else
            {
                ChangeToBuyButton(i);
            }
        }
    }

    void OnShopItemButtonClicked(int itemIndex)
    {
        if (Game.Instance.HasEnoughCoins(ShopItemsList[itemIndex].bodyPartPrice))
        {
            Game.Instance.UseCoins(ShopItemsList[itemIndex].bodyPartPrice);

            //Purchase Item
            ShopItemsList[itemIndex].ownBodyPart = true;

            //Change the button to Sell
            buyButton = ShopScrollView.GetChild(itemIndex).GetChild(2).GetComponent<Button>();
            ChangeToSellButton(itemIndex);

            //Change UI text: coins
            Game.Instance.UpdateAllCoinsUIText();

            OutfitChangeGameObject.SetActive(true);
            FindObjectOfType<BodyPartsSelector>().AddItem(ShopItemsList[itemIndex]);
            OutfitChangeGameObject.SetActive(false);
        }
        else
        {
            Debug.Log("You don't have enough coins!!");
        }
    }
    void OnShopItemButtonClickedToSell(int itemIndex)
    {
        Game.Instance.AddCoins(ShopItemsList[itemIndex].bodyPartPrice);

        //Sold Item
        ShopItemsList[itemIndex].ownBodyPart = false;
        
        //Change the button to Buy
        buyButton = ShopScrollView.GetChild(itemIndex).GetChild(2).GetComponent<Button>();
        ChangeToBuyButton(itemIndex);

        //Change UI text: coins
        Game.Instance.UpdateAllCoinsUIText();

        OutfitChangeGameObject.SetActive(true);
        FindObjectOfType<BodyPartsSelector>().RemoveItem(ShopItemsList[itemIndex]);
        OutfitChangeGameObject.SetActive(false);
    }

    void ChangeToSellButton(int itemIndex)
    {
        buyButton.transform.GetChild(0).GetComponent<TMP_Text>().text = "SELL";
        buyButton.transform.GetComponent<Image>().color = Color.red;

        buyButton.onClick.RemoveAllListeners();
        buyButton.AddEventListener(itemIndex, OnShopItemButtonClickedToSell);
    }
    void ChangeToBuyButton(int itemIndex)
    {
        buyButton.transform.GetChild(0).GetComponent<TMP_Text>().text = "BUY";
        buyButton.transform.GetComponent<Image>().color = Color.green;

        buyButton.onClick.RemoveAllListeners();
        buyButton.AddEventListener(itemIndex, OnShopItemButtonClicked);
    }

    /*------------------------------- Open & Close Shop -------------------------------*/
    public void OpenShop()
    {
        ShopPanel.SetActive(true);
    }
    public void CloseShop()
    {
        ShopPanel?.SetActive(false);
    }
}
