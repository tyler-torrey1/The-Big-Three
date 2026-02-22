using System;
using UnityEngine;

public class Inventory : MonoBehaviour {
    public Action<ItemType> OnItemPickedUp;

    [SerializeField]
    bool hasKeys = false;

    [SerializeField]
    bool hasWallet = false;

    [SerializeField]
    bool hasPhone = false;

    public bool PickupItem(Item item) {
        switch (item.itemType) {
            case ItemType.Key:
                this.hasKeys = true;
                break;
            case ItemType.Wallet:
                this.hasWallet = true;
                break;
            case ItemType.Phone:
                this.hasPhone = true;
                break;
        }
        this.OnItemPickedUp?.Invoke(item.itemType);
        return true;
    }


}
