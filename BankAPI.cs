using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BankAPI : MonoBehaviour
{

    public static BankAPI instance;

    public void Awake() {
        if(instance == null) {
            instance = this;
        } else if(instance != this) {
            Destroy(gameObject);
        }
    }

    public List<BankModel> CheckBank()
    {
        return APIManager.instance.FBBank;        
    }

    public void BuyFromTheBank(string itemName, int itemValue)
    {
        APIManager.instance.BuyDiamonds(itemName, itemValue);
    }

    public void HandlePurchase(int value)
    {
        DatabaseStore.instance.diamonds += value;
        StoreManager.instance.UpdateUIGems();
    }

}
