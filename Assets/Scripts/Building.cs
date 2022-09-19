using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for building on the map that hold a Resource inventory and that can be interacted with by Unit.
/// This Base class handle modifying the inventory of resources.
/// </summary>
public abstract class Building : MonoBehaviour
    //, UIMainScene.IUIInfoContent
{
    //need to be serializable for the save system, so maybe added the attribute just when doing the save system
    [System.Serializable]
    public class InventoryEntry
    {
        public string ResourceId;
        public int Count;
    }

    [Tooltip("-1 is infinite")]//还能这样注释？
    public int InventorySpace = 100;
    
    // protected List<InventoryEntry> m_Inventory = new List<InventoryEntry>();
    // public List<InventoryEntry> Inventory => m_Inventory;

    protected int m_CurrentAmount = 0;

    //return 0 if everything fit in the inventory, otherwise return the left over amount
    //如果所有元素都和inventory一样，返回0，否则返回所有左值
    public int AddItem(int amount)//(string resourceId, int amount)//给resourceID的单位添加东西
    {
        //as we use the shortcut -1 = infinite amount, we need to actually set it to max value for computation following
        //如果infinite = -1 ，设置最大数maxinventoryspace为int32。maxvalue
        int maxInventorySpace = InventorySpace == -1 ? Int32.MaxValue : InventorySpace;
        
        if (m_CurrentAmount == maxInventorySpace)
            return amount;
        //
        // int found = m_Inventory.FindIndex(item => item.ResourceId == resourceId);//findindex查个true和faluse？这是遍历了一遍？
        int addedAmount = Mathf.Min(maxInventorySpace - m_CurrentAmount, amount);//为什么addedamount是最大值-currentamount？这里amount是其他inventory传入的值，即把东西从a送给b，所以可能存在b点装满的情况
        
        // //couldn't find an entry for that resource id so we add a new one.
        // //找不到对应id就new一个inventory
        // if (found == -1)//true or false
        // {
        //     m_Inventory.Add(new InventoryEntry()
        //     {
        //         Count = addedAmount,
        //         ResourceId = resourceId
        //     });
        // }
        // else
        // {
        //     m_Inventory[found].Count += addedAmount;
        // }

        m_CurrentAmount += addedAmount;//货物不是运送给base了么？为什么还要加amount？货车本身也会加分的么？我diu，是加分，还要给加分分类是哪个车加的么？麻烦
        return amount - addedAmount;
    }

    //return how much was actually removed, will be 0 if couldn't get any.
    //返回有多少东西被转移。如果不能转移则返回0
    // public int GetItem(string resourceId, int requestAmount)//resourceID被取走货物
    // {
    //     int found = m_Inventory.FindIndex(item => item.ResourceId == resourceId);
    //     
    //     //couldn't find an entry for that resource id so we add a new one.
    //     if (found != -1)
    //     {
    //         int amount = Mathf.Min(requestAmount, m_Inventory[found].Count);
    //         m_Inventory[found].Count -= amount;
    //
    //         if (m_Inventory[found].Count == 0)
    //         {//no more of that resources, so we remove it
    //             //remove不就运算更多了么，用算力换内存么
    //             m_Inventory.RemoveAt(found);
    //         }
    //
    //         m_CurrentAmount -= amount;
    //
    //         return amount;
    //     }
    //
    //     return 0;
    // }

    public virtual string GetName()
    {
        return gameObject.name;
    }

    public virtual string GetData()
    {
        return "";
    }

    // public void GetContent(ref List<InventoryEntry> content)
    // {
    //     content.AddRange(m_Inventory);
    // }
}
