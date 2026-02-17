using UnityEngine;

public class Mover : MonoBehaviour {
    [SerializeField] private Rigidbody2D _rb;
    private float _movementSpeed;

    private void Awake() {
        if (!this._rb) {
            this._rb = this.GetComponent<Rigidbody2D>();
        }
    }
    public void Initialize(float speed) {
        this._movementSpeed = speed;
    }
    public void MoveInDirection(Vector2 direction) {
        this._rb.linearVelocity = direction.normalized * this._movementSpeed;
        return;
    }

    public void TeleportTo(Vector2 position) {
        this._rb.position = position;
        this._rb.linearVelocity = Vector2.zero;
    }
    public void MoveToLocation(Vector2 location) {
        return;
    }
    public void Stop() {
        return;
    }
}
