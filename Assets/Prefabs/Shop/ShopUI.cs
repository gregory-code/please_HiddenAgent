using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    [SerializeField] Shop shop;
    [SerializeField] ShopItemWidget itemWidgetPrefab;
    [SerializeField] Transform shopList;

    ShopItem selectedItem;
    GameObject selectedGameObject;

    [SerializeField] CreditComponent playerCredit;

    List<ShopItemWidget> shopItemWidgets = new List<ShopItemWidget>();

    private void Awake()
    {
        foreach(ShopItem item in shop.GetItems())
        {
            ShopItemWidget newWidget = Instantiate(itemWidgetPrefab, shopList);
            newWidget.Init(item, playerCredit.GetCredits(), this);
        }

        playerCredit.onCreditChanged += RefreshItems;
    }

    private void RefreshItems(int newCredit)
    {
        foreach(ShopItemWidget item in shopItemWidgets)
        {
            item.Refresh(newCredit);
        }
    }

    public void SelectItem(ShopItem item, GameObject itemGameObject)
    {
        selectedItem = item;
        selectedGameObject = itemGameObject;
    }

    public void BuyItem()
    {
        shop.TryPurchase(selectedItem, playerCredit);
        Destroy(selectedGameObject);
    }
}
