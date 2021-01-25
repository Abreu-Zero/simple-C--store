using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class DatabaseStore : MonoBehaviour
{

    public List<ItemModel> bank;
    public List<EquipModel> skins, smith;
    public List<PotionModel> potions;

    void OnEnable()
    {
        if(!LoadData())
        {
            PopulateStore();
        }
    }

    private void PopulateStore()
    {
        EquipModel skin = new EquipModel(0, 0, "SteveRed", true, true); //TODO: implement full serializable items
        skins.Add(skin);
        skin = new EquipModel(1, 0, "SteveAlemanha", false, false);
        skins.Add(skin);
        skin = new EquipModel(2, 0, "SteveBrasil", false, false);
        skins.Add(skin);
        skin = new EquipModel(3, 0, "SteveItalia", false, false);
        skins.Add(skin);
        skin = new EquipModel(4, 0, "SteveUK", false, false);
        skins.Add(skin);
        skin = new EquipModel(5, 0, "SteveUSA", false, false);
        skins.Add(skin);
        PotionModel potion = new PotionModel(0, 0, "PotGreen", 0);
        potions.Add(potion);
        potion = new PotionModel(1, 0, "PotRed", 0);
        potions.Add(potion);
        EquipModel armor = new EquipModel (0, 0, "SteveKlappvisor", false, false);
        smith.Add(armor);
        armor = new EquipModel (1, 0, "StevePigface", false, false);
        smith.Add(armor);
        ItemModel coin = new ItemModel(0, 0, "item (3)");
        bank.Add(coin);
        coin = new ItemModel(1, 0, "chest");
        bank.Add(coin);
    }

    public string CheckItemBought(int id, List<EquipModel> list) 
    {
        if(list[id].haveIt)
        {
            if(list[id].isEquiped)
            {
                return "Using";
            }else
            {
                return "Click to use";
            }
        }else
        {
            return list[id].price.ToString();
        }
    }

    public string CheckUsedSkin()
    {
        foreach(var s in skins)
        {
            if (s.isEquiped){return s.nameItem;}
        }
        return "SteveRed";
    }

    public string CheckUsedArmor()
    {
        foreach(var s in smith)
        {
            if (s.isEquiped){return s.nameItem;}
        }
        return "";
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

    public void DeleteData()
    {
        try
        {
            File.Delete(Application.persistentDataPath + "storeData.data");
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }
}
