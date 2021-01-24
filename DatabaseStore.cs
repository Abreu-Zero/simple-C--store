using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class DatabaseStore : MonoBehaviour
{

    public List<ItemModel> skins, potions, smith, bank;

    void OnEnable()
    {
        if(!LoadData())
        {
            PopulateStore();
        }
    }

    private void PopulateStore()
    {
        ItemModel skin = new ItemModel(0, 0, "SteveRed"); //TODO: implement full serializable items
        skins.Add(skin);
        skin = new ItemModel(1, 0, "SteveAlemanha");
        skins.Add(skin);
        skin = new ItemModel(2, 0, "SteveBrasil");
        skins.Add(skin);
        skin = new ItemModel(3, 0, "SteveItalia");
        skins.Add(skin);
        skin = new ItemModel(4, 0, "SteveUK");
        skins.Add(skin);
        skin = new ItemModel(5, 0, "SteveUSA");
        skins.Add(skin);
        ItemModel potion = new ItemModel(6, 0, "PotGreen");
        potions.Add(potion);
        potion = new ItemModel(7, 0, "PotRed");
        potions.Add(potion);
        ItemModel armor = new ItemModel (8, 0, "SteveKlappvisor");
        smith.Add(armor);
        armor = new ItemModel (9, 0, "StevePigface");
        smith.Add(armor);
        ItemModel coin = new ItemModel(10, 0, "item (3)");
        bank.Add(coin);
        coin = new ItemModel(11, 0, "chest");
        bank.Add(coin);
    }

    public string CheckItemBought(string itemName, int price) 
    {
        if(PlayerPrefs.HasKey(itemName))
        {
            if(CkeckItemEquiped(itemName))
            {
                return "Using";
            }else
            {
                return "Click to use";
            }
        }else
        {
            return price.ToString();
        }
    }

    private bool CkeckItemEquiped(string itemName)
    {
        return PlayerPrefs.GetInt(itemName) == 1;
    }

    public void SaveData()
    {
        BinaryFormatter bFormatter = new BinaryFormatter();
        FileStream fStream = File.Create(Application.persistentDataPath + "storeData.data");

        DatabaseItems data = new DatabaseItems();
        data.dataSkins = skins;
        data.dataPotions = potions;
        data.dataSmith = smith;
        data.dataBank = bank;

        bFormatter.Serialize(fStream, data);
        fStream.Close();
    }

    public bool LoadData()
    {
        if(File.Exists(Application.persistentDataPath + "storeData.data"))
        {
            BinaryFormatter bFormatter = new BinaryFormatter();
            FileStream fStream = File.Open(Application.persistentDataPath + "storeData.data", FileMode.Open);

            DatabaseItems data = (DatabaseItems) bFormatter.Deserialize(fStream);
            skins = data.dataSkins;
            potions = data.dataPotions;
            smith = data.dataSmith;
            bank = data.dataBank;

            fStream.Close();

            if(skins != null && bank != null){return true;}
        }

        return false;
    }
}
