using System;
using UnityEngine;

public class Inventory : MonoBehaviour {
    public Action<ItemType> OnItemPickedUp;

    [SerializeField]
    bool[] contains;

    private void Start() {
        this.contains = new bool[3];
    }

    public bool Contains(ItemType item) {
        return this.contains[(int)item];
    }

    public void PickupItem(Item item) {
        this.contains[(int)item.itemType] = true;

        this.OnItemPickedUp?.Invoke(item.itemType);
    }


}
