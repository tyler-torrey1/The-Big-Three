using UnityEngine;

public class SteppingStone : MonoBehaviour {
    public ItemType item;
    private Collider2D steppingStoneBlocker;


    public StageManager stageManager;
    public SpriteRenderer spriteRenderer;

    public Sprite activeSprite;
    public Sprite inactiveSprite;
    private void Awake() {
        GlobalManager.OnSceneChanged += this.RefreshActiveness;

        this.steppingStoneBlocker = this.GetComponent<Collider2D>();
        this.spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    private void RefreshActiveness() {
        bool isWalkable = GlobalManager.inventory.Contains(this.item);

        this.steppingStoneBlocker.isTrigger = isWalkable;
        this.spriteRenderer.sprite = isWalkable ? this.activeSprite : this.inactiveSprite;
    }
}
