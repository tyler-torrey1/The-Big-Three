using UnityEngine;

public class Item : MonoBehaviour {
    private BoxCollider2D _coll2D;

    public ItemType itemType;

    private Inventory _inventory;

    private StageManager _stageManager;

    private int _maxStageIndex = 4;

    public void Awake() {
        this._coll2D = this.GetComponent<BoxCollider2D>();
        this._inventory = GlobalManager.player.GetComponent<Inventory>();
        this._stageManager = this.GetComponentInParent<StageManager>();
    }
    public void Start() {
        this._stageManager.OnStageChanged += this.SetCurrentStage;
        this.SetCurrentStage(0);
    }
    public void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            this._inventory.PickupItem(this);
            Destroy(this.gameObject);
            GlobalManager.ChangeSceneTo("Hub", Direction.South);
        }
    }
    public void SetCurrentStage(int newStageIndex) {
        if (newStageIndex == this._maxStageIndex) {
            this.gameObject.SetActive(true);
        } else {
            if (this.gameObject.activeInHierarchy) {
                this.gameObject.SetActive(false);
            }
        }
    }
}



public enum ItemType {
    Key = 0,
    Wallet,
    Phone
}