using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class DatabaseStore : MonoBehaviour
{

    public List<BankModel> bank;
    public List<EquipModel> skins, smith;
    public List<PotionModel> potions;
    public int diamonds, rubys;

    void OnEnable()
    {
        //DeleteData();
        if(!LoadData())
        {
            PopulateStore();
        }

        if(!LoadScore())
        {
            diamonds = 0;
            rubys = 0;
        }
    }

    private void PopulateStore()
    {
        EquipModel skin = new EquipModel(0, 0, "SteveRed", true, true);
        skins.Add(skin);
        skin = new EquipModel(1, 200, "SteveAlemanha", false, false);
        skins.Add(skin);
        skin = new EquipModel(2, 300, "SteveBrasil", false, false);
        skins.Add(skin);
        skin = new EquipModel(3, 400, "SteveItalia", false, false);
        skins.Add(skin);
        skin = new EquipModel(4, 500, "SteveUK", false, false);
        skins.Add(skin);
        skin = new EquipModel(5, 600, "SteveUSA", false, false);
        skins.Add(skin);
        PotionModel potion = new PotionModel(0, 25, "PotGreen", 0);
        potions.Add(potion);
        potion = new PotionModel(1, 50, "PotRed", 0);
        potions.Add(potion);
        EquipModel armor = new EquipModel (0, 500, "SteveKlappvisor", false, false);
        smith.Add(armor);
        armor = new EquipModel (1, 500, "StevePigface", false, false);
        smith.Add(armor);
        BankModel coin = new BankModel(0, 0, "item (3)", 500); //TODO: set new sprites for bank items  
        bank.Add(coin);
        coin = new BankModel(1, 0, "chest", 1000);
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

        fStream = File.Create(Application.persistentDataPath + "scoreData.data");
        DataGems data2 = new DataGems();
        data2.diamonds = diamonds;
        data2.rubys = rubys;
        bFormatter.Serialize(fStream, data2);
        fStream.Close();
    }

    private bool LoadData()
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

    private bool LoadScore()
    {
        if(File.Exists(Application.persistentDataPath + "scoreData.data"))
        {
            BinaryFormatter bFormatter = new BinaryFormatter();
            FileStream fStream = File.Open(Application.persistentDataPath + "scoreData.data", FileMode.Open);

            DataGems data = (DataGems) bFormatter.Deserialize(fStream);
            diamonds = data.diamonds;
            rubys = data.rubys;

            fStream.Close();

            return true;
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
