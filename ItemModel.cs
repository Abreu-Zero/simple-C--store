﻿//859using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable] public class ItemModel
{
    public int id;
    public int price;
    public string nameItem;

    public ItemModel(int id, int price, string name)
    {
        this.id = id;
        this.price = price;
        this.nameItem = name;
    }
}

[System.Serializable] public class PotionModel: ItemModel
{
    public int quantity;

    public PotionModel(int id, int price, string name, int quantity) : base(id, price, name)
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

    public EquipModel(int id, int price, string name, bool haveIt, bool isEquiped) : base(id, price, name)
    {
        this.id = id;
        this.price = price;
        this.nameItem = name;
        this.haveIt = haveIt;
        this.isEquiped = isEquiped;
    }
}

[System.Serializable] public class DatabaseItems
{
    public List<ItemModel> dataSkins;
    public List<ItemModel> dataPotions;
    public List<ItemModel> dataSmith;
    public List<ItemModel> dataBank;
}

[System.Serializable] public class DataGems
{
    public int diamonds;
    public int rubys;
}