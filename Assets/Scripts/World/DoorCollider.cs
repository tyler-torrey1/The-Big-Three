using UnityEngine;

public class DoorCollider : MonoBehaviour {
    [SerializeField]
    BoxCollider2D collider2D = null;

    [SerializeField]
    Rigidbody2D playerBody;

    [SerializeField]
    StageManager manager;
    void Awake() {
        if (this.collider2D == null) {
            this.collider2D = this.GetComponent<BoxCollider2D>();
        }
    }
    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Hit: " + other);
        if (other.tag.Equals("Player")) {
            this.playerBody.MovePosition(Vector2.zero);
        }
    }
}
