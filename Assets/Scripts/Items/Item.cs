using UnityEngine;

public class Item : MonoBehaviour {
    private BoxCollider2D _coll2D;

    public void Awake() {
        this._coll2D = this.GetComponent<BoxCollider2D>();
    }
    public void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {

        }
    }
}
