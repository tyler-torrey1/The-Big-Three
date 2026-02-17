using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {
    // Object components
    private Animator _animator;
    private Mover _mover;

    // 'Cache' for storing user input
    public Vector2 moveInput;
    [SerializeField] private float movementSpeed = 3.0f;
    private void Awake() {
        this._animator = this.GetComponent<Animator>();
        this._mover = this.GetComponent<Mover>();
    }

    private void Start() {
        this._mover.Initialize(this.movementSpeed);
    }

    // Update is called once per frame
    void FixedUpdate() {
        this.MovePlayer();
    }

    private void MovePlayer() {
        Debug.Log(this.moveInput);
        Debug.Log(this._mover);
        this._mover.MoveInDirection(this.moveInput);
        // Add animation check here
    }

    // Referenced by the Player Input to pass move commands
    public void MoveInput(InputAction.CallbackContext context) {
        this.moveInput = context.ReadValue<Vector2>();
    }
}
