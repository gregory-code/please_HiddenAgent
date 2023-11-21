using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemWidget : MonoBehaviour
{
    [SerializeField] Button myButton;
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI itemName;
    [SerializeField] TextMeshProUGUI price;
    [SerializeField] TextMeshProUGUI description;
    [SerializeField] Image grayout;

    ShopItem item;

    internal void Init(ShopItem item, int credits, ShopUI shop)
    {
        icon.sprite = item.Icon;
        itemName.text = item.Title;
        price.text = "$ " + item.Price;
        description.text = item.Description;
        this.item = item;

        myButton.onClick.AddListener(() => shop.SelectItem(item, gameObject));

        Refresh(credits);
    }

    public void Refresh(int credits)
    {
        grayout.enabled = false;
        if (credits < item.Price)
        {
            grayout.enabled = true;
        }
    }

}
