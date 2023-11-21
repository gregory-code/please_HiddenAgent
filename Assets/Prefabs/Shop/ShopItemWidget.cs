using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemWidget : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI itemName;
    [SerializeField] TextMeshProUGUI price;
    [SerializeField] TextMeshProUGUI description;

    internal void Init(ShopItem item)
    {
        icon.sprite = item.Icon;
        itemName.text = item.Title;
        price.text = "$ " + item.Price;
        description.text = item.Description;
    }

}
