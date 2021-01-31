using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable] public class ItemModel
{
    public int id;
    public string nameItem;

    public ItemModel(int id, string name)
    {
        this.id = id;
        this.nameItem = name;
    }
}

[System.Serializable] public class PotionModel: ItemModel
{
    public int quantity;
    public int price;

    public PotionModel(int id, int price, string name, int quantity) : base(id, name)
    {
        this.id = id;
        this.price = price;
        this.nameItem = name;
        this.quantity = quantity;
    }
}

[System.Serializable] public class EquipModel: ItemModel
{
    public bool haveIt;
    public bool isEquiped;
    public int price;

    public EquipModel(int id, int price, string name, bool haveIt, bool isEquiped) : base(id, name)
    {
        this.id = id;
        this.price = price;
        this.nameItem = name;
        this.haveIt = haveIt;
        this.isEquiped = isEquiped;
    }
}

[System.Serializable] public class BankModel: ItemModel
{
    public string price;
    public int value;

    public BankModel(int id, string price, string name, int value) : base(id, name)
    {
        this.id = id;
        this.price = price;
        this.nameItem = name;
        this.value = value;
    }
}

[System.Serializable] public class DatabaseItems
{
    public List<EquipModel> dataSkins;
    public List<PotionModel> dataPotions;
    public List<EquipModel> dataSmith;
    public List<BankModel> dataBank;
}

[System.Serializable] public class DataGems
{
    public int diamonds;
    public int rubys;
}
