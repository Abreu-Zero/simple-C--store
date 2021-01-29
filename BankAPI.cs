using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BankAPI : MonoBehaviour
{

    private UserKong mockUser;

    public int BuyFromTheBank(string itemName, int itemPrice, int itemValue)
    {
        mockUser = new UserKong();

        if(mockUser.isLogged)
        {
            if(APIManager.instance.RemoveCoins(itemPrice, mockUser.id))
            {
                Debug.Log("Purchase successful, adding " + itemValue + "diamonds to account");
                return itemValue;
            }else
            {
                Debug.Log("Purchase failed, user did not have the required amount");
            }
        }

        Debug.Log("Purchase Failed, could not retrieve user data");
        return 0;
    }

}
