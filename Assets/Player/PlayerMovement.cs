using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {
    // Object components
    private Animator _animator;
    private Rigidbody2D _rb;

    // 'Cache' for storing user input
    [SerializeField]
    private Vector2 _moveInput;
    [SerializeField] private float _movementSpeed = 50.0f;
    private void Awake() {
        this._animator = this.GetComponent<Animator>();
        this._rb = this.GetComponent<Rigidbody2D>();
    }

    void Start(){
        this._rb.useFullKinematicContacts = true;
    }
    // Update is called once per frame
    void FixedUpdate() {
        this.MovePlayer();
    }

    public void MoveInDirection(Vector2 direction) {
        Vector2 calculatedPosDiff = this._movementSpeed * direction.normalized * Time.fixedDeltaTime;
        this._rb.linearVelocity = calculatedPosDiff;
    }

    private void MovePlayer() {
        // Move Player
        this.MoveInDirection(this._moveInput);

        // Play Animation?
    }

    // Referenced by the Player Input to pass move commands
    public void MoveInput(InputAction.CallbackContext context) {
        this._moveInput = context.ReadValue<Vector2>();
    }
}
