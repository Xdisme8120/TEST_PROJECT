using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ItemType
{
    Consumables,//消耗品
    Matirial,//材料
    Equip//装备
}
public class Item
{
    //物品ID
    public int ID;
    //物品名称
    public string name;
    // 物品描述
    public string description;
}
