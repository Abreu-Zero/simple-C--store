using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BankAPI : MonoBehaviour
{

    private bool isLogged = false;

    public int BuyFromTheBank(string itemName, int itemPrice, int itemValue)
    {
        if(isLogged)
        {
            if(APIManager.user.coins - itemPrice >= 0)
            {
                APIManager.RemoveCoins(itemPrice);
                return itemValue;
            }
        }

        return 0;
    }
}
