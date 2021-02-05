using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BankAPI : MonoBehaviour
{

    public static BankAPI instance;
    private int currentValue;

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
        // if(APIManager.instance.IsLoggedIn)
        // {
        //     return APIManager.instance.FBBank;
        // }
        // return new List<BankModel>();
        
    }

    public int BuyFromTheBank(string itemName, int itemValue)
    {
        APIManager.instance.BuyDiamonds(itemName, itemValue);
        return currentValue;
    }

    public void HandlePurchase(int value)
    {
        currentValue = value;
    }

}
