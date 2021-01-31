using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BankAPI : MonoBehaviour
{

    private UserKong mockUser;
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
        if(APIManager.instance.IsLoggedIn)
        {
            return APIManager.instance.FBBank;
        }
        return new List<BankModel>();
        
    }

    public int BuyFromTheBank(string itemName, string itemPrice, int itemValue)
    {
        mockUser = new UserKong();

        if(mockUser.isLogged)
        {
            if(APIManager.instance.BuyDiamonds(itemPrice, mockUser.id))
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
