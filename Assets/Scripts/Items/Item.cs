using UnityEngine;

public class Item : MonoBehaviour {
    private BoxCollider2D _coll2D;

    public ItemType itemType;

    private Inventory _inventory;

    public void Awake() {
        this._coll2D = this.GetComponent<BoxCollider2D>();
        this._inventory = GlobalManager.player.GetComponent<Inventory>();
        Debug.Log("Inventory initiated as:" + this._inventory);
    }
    public void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            bool itemPickedUp = this._inventory.PickupItem(this);
            if (itemPickedUp) {
                Destroy(this);
            }
        }
    }
}

public enum ItemType {
    Key,
    Wallet,
    Phone
}