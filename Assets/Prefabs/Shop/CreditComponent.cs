using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPuchaseListener
{
    bool ItemPurchased(Object newPurchase);
}

public class CreditComponent : MonoBehaviour
{
    [SerializeField] int credits;

    IPuchaseListener[] purchaseListeners;

    private void Awake()
    {
        purchaseListeners = GetComponents<IPuchaseListener>();

    }
    public int Credits { get { return credits; } }

    public delegate void OnCreditChanged(int newCredits);
    public event OnCreditChanged onCreditChanged;

    public bool TryPurchaseItem(ShopItem item)
    {
        if (credits < item.Price) 
            return false;

        credits -= item.Price;
        onCreditChanged?.Invoke(credits);
        foreach(IPuchaseListener listener in purchaseListeners)
        {
            if(listener.ItemPurchased(item.Item))
            {
                break;
            }
        }
        return true;
    }
}
