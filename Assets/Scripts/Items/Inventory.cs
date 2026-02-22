using System;
using UnityEngine;

public class Inventory : MonoBehaviour {
    public Action<ItemType> OnItemPickedUp;

    bool[] contains;

    private void Start()
    {
        contains = new bool[3];
    }

    public bool Contains(ItemType item)
    {
        return contains[(int) item];
    }

    public bool PickupItem(Item item)
    {
        contains[(int)item.itemType] = true;

        this.OnItemPickedUp?.Invoke(item.itemType);
        return true;
    }


}
