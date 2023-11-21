using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    [SerializeField] Shop shop;
    [SerializeField] ShopItemWidget itemWidgetPrefab;
    [SerializeField] Transform shopList;

    private void Awake()
    {
        foreach(ShopItem item in shop.GetItems())
        {
            ShopItemWidget newWidget = Instantiate(itemWidgetPrefab, shopList);
            newWidget.Init(item);
        }
    }
}
