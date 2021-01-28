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
    private DatabaseStore database;
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
        database = GetComponent<DatabaseStore>();
        ChangeStore(0);
        UIManager.UpdateInventory(GetUsedSkin(), GetUsedArmor());
        UIManager.UpdatePotions(database.potions[0].quantity, database.potions[1].quantity);
        UIManager.UpdateGems(database.diamonds, database.rubys);
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Space)) //FOR TESTING ONLY
        {
            database.diamonds += 100;
            database.rubys += 100;
            UIManager.UpdateGems(database.diamonds, database.rubys);
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            database.DeleteData();
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
                        foreach (var item in database.skins)
                        {
                            GameObject skin = Instantiate(baseItem) as GameObject;
                            skin.transform.SetParent(storeContent, false);
                            Text price = skin.GetComponentInChildren<Text>();
                            price.text = database.CheckItemBought(item.id, database.skins);
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
                        foreach (var item in database.potions)
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
                        foreach (var item in database.smith)
                        {
                            GameObject skin = Instantiate(baseItem) as GameObject;
                            skin.transform.SetParent(storeContent, false);
                            Text price = skin.GetComponentInChildren<Text>();
                            price.text = database.CheckItemBought(item.id, database.smith);
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
                        foreach (var item in database.bank)
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

    private string GetUsedSkin()
    {
        return database.CheckUsedSkin();
    }

    private string GetUsedArmor()
    {
        return database.CheckUsedArmor();
    }


    public void BuyItem(int id, string itemName)
    {
        print("ID: " + id + " Item Name: " + itemName);
        switch(storeOpen)
        {
            case StoreOpen.SKIN:
                if(database.skins[id].haveIt){
                    EquipItem(id);
                }
                else
                {
                    if(database.diamonds - database.skins[id].price >= 0)
                    {
                        database.diamonds -= database.skins[id].price;
                        database.skins[id].haveIt = true;
                        UIManager.UpdateTextButtons("Click to use", skinsList[id]);
                    }else{
                        print("ERROR: Not enough diamonds - " + database.diamonds + " diamonds x " + database.skins[id].price.ToString() + " cost");
                    }
                    
                }
                break;

            case StoreOpen.POTION:
                if(database.potions[id] != null)
                {
                    if(database.diamonds - database.potions[id].price >= 0)
                    {
                        database.diamonds -= database.potions[id].price;
                        database.potions[id].quantity += 1;
                    }else
                    {
                        print("ERROR: Not enough diamonds - " + database.diamonds + " diamonds x " + database.potions[id].price.ToString() + " cost");
                    }
                    
                }
                else
                {
                    Debug.Log("Error while updating potions");
                }
                print(itemName + ": " + database.potions[id].quantity.ToString());
                UIManager.UpdatePotions(database.potions[0].quantity,database.potions[1].quantity);
                break;
            
            case StoreOpen.ARMOR:
                if(database.smith[id].haveIt){
                    EquipItem(id);
                }
                else
                {
                    if(database.diamonds - database.smith[id].price >= 0)
                    {
                        database.diamonds -= database.smith[id].price;
                        database.smith[id].haveIt = true;
                        UIManager.UpdateTextButtons("Click to use", smithList[id]);
                    }else{
                        print("ERROR: Not enough diamonds - " + database.diamonds + " diamonds x " + database.smith[id].price.ToString() + " cost");
                    }
                }
                break;

            default:
                Debug.Log("No store open");
                break;
            }
            UIManager.UpdateGems(database.diamonds, database.rubys);
    }

    public void EquipItem(int id)
    {
        switch(storeOpen)
        {
            case StoreOpen.SKIN:
                foreach(var s in database.skins){
                    if(s.isEquiped){
                        s.isEquiped = false;
                        UIManager.UpdateTextButtons("Click to use", skinsList[s.id]);
                    }
                }
                database.skins[id].isEquiped = true;
                UIManager.UpdateTextButtons("Using", skinsList[id]);
                PlayerPrefs.SetString("UsedSkin", database.skins[id].nameItem); //TODO: fix this
                break;
            case StoreOpen.ARMOR:
                foreach(var s in database.smith){
                    if(s.isEquiped){
                        s.isEquiped = false;
                        UIManager.UpdateTextButtons("Click to use", smithList[s.id]);
                    }
                }
                database.smith[id].isEquiped = true;
                UIManager.UpdateTextButtons("Using", smithList[id]);
                break;
            default:
                Debug.Log("Not Implemented");
                break;

        }
        UIManager.UpdateInventory(database.CheckUsedSkin(), database.CheckUsedArmor());
    }

    public void ReturnToMainMenu()
    {
        //TODO: encapsulate this into overseer maybe?
        database.SaveData();
        SceneManager.LoadScene("MainMenu");

    }
}
