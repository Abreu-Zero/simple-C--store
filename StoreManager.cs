using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StoreManager : MonoBehaviour
{
    public static StoreManager instance;
    public List<ItemModel> skins, potions, smith, bank;
    public Transform storeContent;
    public GameObject baseItem;
    private StoreOpen storeOpen;
    private UIStoreManager UIManager;
    private BankAPI bankAPI;
    private bool IsLoggedIn = false;
    private List<GameObject> skinsList, potionsList, smithList, bankList;
    private List<List<GameObject>> odin;
    
    enum StoreOpen
    {
        DEFAULT,
        SKIN,
        POTION,
        ARMOR,
        BANK,
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        storeContent = GameObject.Find("PanelContent").GetComponent<Transform>();

        skinsList = new List<GameObject>();
        potionsList = new List<GameObject>();
        smithList = new List<GameObject>();
        bankList = new List<GameObject>();
        odin = new List<List<GameObject>>();

        odin.Add(skinsList);
        odin.Add(potionsList);
        odin.Add(smithList);
        odin.Add(bankList);

        UIManager = GetComponent<UIStoreManager>();
        bankAPI = GetComponent<BankAPI>();
        DatabaseStore.instance.CheckBank();

        IsLoggedIn = APIManager.instance.IsLoggedIn;

        ChangeStore(0);
        UIManager.UpdateBankButton(IsLoggedIn);
        UIManager.UpdateInventory(GetUsedSkin(), GetUsedArmor());
        UIManager.UpdatePotions(DatabaseStore.instance.potions[0].quantity, DatabaseStore.instance.potions[1].quantity);
        UIManager.UpdateGems(DatabaseStore.instance.diamonds, DatabaseStore.instance.rubys);
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Space)) //FOR TESTING ONLY
        {
            DatabaseStore.instance.diamonds += 100;
            DatabaseStore.instance.rubys += 100;
            UIManager.UpdateGems(DatabaseStore.instance.diamonds, DatabaseStore.instance.rubys);
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            DatabaseStore.instance.DeleteData();
        }
    }

    public void ChangeStore(int storeID)
    {
        if (CheckStore(storeID))
        {
            foreach(var l in odin)
            {
                foreach(var s in l)
                {
                    s.SetActive(false);
                }
                
            }
        }
        
        switch (storeID)
        {
            case 0:
                if(storeOpen != StoreOpen.SKIN){
                    if( skinsList.Count == 0)
                    {
                        foreach (var item in DatabaseStore.instance.skins)
                        {
                            GameObject skin = Instantiate(baseItem) as GameObject;
                            skin.transform.SetParent(storeContent, false);
                            Text price = skin.GetComponentInChildren<Text>();
                            price.text = DatabaseStore.instance.CheckItemBought(item.id, DatabaseStore.instance.skins);
                            skin.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("Sprites/Steve/Skins/"+ item.nameItem);
                            skin.GetComponentInChildren<Button>().onClick.AddListener(() => BuyItem(item.id, item.nameItem));
                            skinsList.Add(skin);
                        }
                    }else
                    {
                        foreach(GameObject s in skinsList)
                        {
                            s.SetActive(true);
                        }
                    }
                    
                    storeOpen = StoreOpen.SKIN;
                }
                
                break;
            case 1:
                if(storeOpen != StoreOpen.POTION){
                    if(potionsList.Count == 0)
                    {
                        foreach (var item in DatabaseStore.instance.potions)
                        {
                            GameObject skin = Instantiate(baseItem) as GameObject;
                            skin.transform.SetParent(storeContent, false);
                            Text price = skin.GetComponentInChildren<Text>();
                            price.text = item.price.ToString();
                            skin.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("Sprites/Objects/"+ item.nameItem);
                            skin.GetComponentInChildren<Button>().onClick.AddListener(() => BuyItem(item.id, item.nameItem));
                            potionsList.Add(skin);
                        }
                    }else
                    {
                        foreach(GameObject p in potionsList)
                        {
                            p.SetActive(true);
                        }
                    }
                    
                    storeOpen = StoreOpen.POTION;
                }
                break;
            case 2:
                if(storeOpen != StoreOpen.ARMOR){
                    if(smithList.Count == 0)
                    {
                        foreach (var item in DatabaseStore.instance.smith)
                        {
                            GameObject skin = Instantiate(baseItem) as GameObject;
                            skin.transform.SetParent(storeContent, false);
                            Text price = skin.GetComponentInChildren<Text>();
                            price.text = DatabaseStore.instance.CheckItemBought(item.id, DatabaseStore.instance.smith);
                            skin.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("Sprites/Steve/Equips/"+ item.nameItem);
                            skin.GetComponentInChildren<Button>().onClick.AddListener(() => BuyItem(item.id, item.nameItem));
                            smithList.Add(skin);                       
                        }
                    }else
                    {
                        foreach(GameObject a in smithList)
                        {
                            a.SetActive(true);
                        }
                    }
                    
                    storeOpen = StoreOpen.ARMOR;
                }
                break;
            case 3:
                if(storeOpen != StoreOpen.BANK){
                    if(bankList.Count == 0)
                    {
                        foreach (var item in DatabaseStore.instance.bank)
                        {
                            GameObject skin = Instantiate(baseItem) as GameObject;
                            skin.transform.SetParent(storeContent, false);
                            Text price = skin.GetComponentInChildren<Text>();
                            price.text = item.price.ToString();
                            skin.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("Sprites/Objects/"+ item.nameItem);
                            skin.GetComponentInChildren<Button>().onClick.AddListener(() => BuyItem(item.id, item.nameItem));
                            bankList.Add(skin);

                        
                        }
                    }else
                    {
                        foreach(GameObject b in bankList)
                        {
                            b.SetActive(true);
                        }
                    }
                    
                    storeOpen = StoreOpen.BANK;
                }
                break;
            default:
                print("Error 404");
                break;
        }
    }

    private bool CheckStore(int id)
    {
        switch (storeOpen)
        {
            case StoreOpen.SKIN:
                return id != 0;
            case StoreOpen.POTION:
                return id != 1;
            case StoreOpen.ARMOR:
                return id != 2;
            case StoreOpen.BANK:
                return id != 3;
            default:
                return false;
        }
    }

    public List<BankModel> CheckBank()
    {
        return bankAPI.CheckBank();
    }

    private string GetUsedSkin()
    {
        return DatabaseStore.instance.CheckUsedSkin();
    }

    private string GetUsedArmor()
    {
        return DatabaseStore.instance.CheckUsedArmor();
    }


    public void BuyItem(int id, string itemName)
    {
        Debug.Log("ID: " + id + " Item Name: " + itemName);
        switch(storeOpen)
        {
            case StoreOpen.SKIN:
                if(DatabaseStore.instance.skins[id].haveIt){
                    EquipItem(id);
                }
                else
                {
                    if(DatabaseStore.instance.diamonds - DatabaseStore.instance.skins[id].price >= 0)
                    {
                        DatabaseStore.instance.diamonds -= DatabaseStore.instance.skins[id].price;
                        DatabaseStore.instance.skins[id].haveIt = true;
                        UIManager.UpdateTextButtons("Click to use", skinsList[id]);
                    }else{
                        Debug.Log("ERROR: Not enough diamonds - " + DatabaseStore.instance.diamonds + " diamonds x " + DatabaseStore.instance.skins[id].price + " cost");
                    }
                    
                }
                break;

            case StoreOpen.POTION:
                if(DatabaseStore.instance.potions[id] != null)
                {
                    if(DatabaseStore.instance.diamonds - DatabaseStore.instance.potions[id].price >= 0)
                    {
                        DatabaseStore.instance.diamonds -= DatabaseStore.instance.potions[id].price;
                        DatabaseStore.instance.potions[id].quantity += 1;
                    }else
                    {
                        Debug.Log("ERROR: Not enough diamonds - " + DatabaseStore.instance.diamonds + " diamonds x " + DatabaseStore.instance.potions[id].price + " cost");
                    }
                    
                }
                else
                {
                    Debug.Log("Error while updating potions");
                }
                print(itemName + ": " + DatabaseStore.instance.potions[id].quantity.ToString());
                UIManager.UpdatePotions(DatabaseStore.instance.potions[0].quantity,DatabaseStore.instance.potions[1].quantity);
                break;
            
            case StoreOpen.ARMOR:
                if(DatabaseStore.instance.smith[id].haveIt){
                    EquipItem(id);
                }
                else
                {
                    if(DatabaseStore.instance.rubys - DatabaseStore.instance.smith[id].price >= 0)
                    {
                        DatabaseStore.instance.rubys -= DatabaseStore.instance.smith[id].price;
                        DatabaseStore.instance.smith[id].haveIt = true;
                        UIManager.UpdateTextButtons("Click to use", smithList[id]);
                    }else{
                        Debug.Log("ERROR: Not enough rubys - " + DatabaseStore.instance.rubys + " rubys x " + DatabaseStore.instance.smith[id].price + " cost");
                    }
                }
                break;

            case StoreOpen.BANK:
                if(DatabaseStore.instance.bank[id] != null)
                {
                    DatabaseStore.instance.diamonds += bankAPI.BuyFromTheBank(DatabaseStore.instance.bank[id].nameItem, DatabaseStore.instance.bank[id].value);
                }
                break;

            default:
                Debug.Log("No store open");
                break;
            }
            UIManager.UpdateGems(DatabaseStore.instance.diamonds, DatabaseStore.instance.rubys);
    }

    public void EquipItem(int id)
    {
        switch(storeOpen)
        {
            case StoreOpen.SKIN:
                foreach(var s in DatabaseStore.instance.skins){
                    if(s.isEquiped){
                        s.isEquiped = false;
                        UIManager.UpdateTextButtons("Click to use", skinsList[s.id]);
                    }
                }
                DatabaseStore.instance.skins[id].isEquiped = true;
                UIManager.UpdateTextButtons("Using", skinsList[id]);
                PlayerPrefs.SetString("UsedSkin", DatabaseStore.instance.skins[id].nameItem); //TODO: fix this
                break;
            case StoreOpen.ARMOR:
                foreach(var s in DatabaseStore.instance.smith){
                    if(s.isEquiped){
                        s.isEquiped = false;
                        UIManager.UpdateTextButtons("Click to use", smithList[s.id]);
                    }
                }
                DatabaseStore.instance.smith[id].isEquiped = true;
                UIManager.UpdateTextButtons("Using", smithList[id]);
                break;
            default:
                Debug.Log("Not Implemented");
                break;

        }
        UIManager.UpdateInventory(DatabaseStore.instance.CheckUsedSkin(), DatabaseStore.instance.CheckUsedArmor());
    }

    public void ReturnToMainMenu()
    {
        //TODO: encapsulate this into overseer maybe?
        DatabaseStore.instance.SaveData();
        SceneManager.LoadScene("MainMenu");

    }
}
