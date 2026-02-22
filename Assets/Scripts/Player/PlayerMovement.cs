using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {

    // Object components
    public Inventory inventory {  get; private set; }
    public SpriteRenderer spriteRenderer { get; private set; }
    public Animator animator { get; private set; }
    public Rigidbody2D _rb { get; private set; }
    new public BoxCollider2D collider { get; private set; }
    public Bounds colliderBounds => this.collider.bounds;

    // 'Cache' for storing user input
    [SerializeField]
    private Vector2 _moveInput;
    [SerializeField] private float _movementSpeed = 50.0f;
    private void Awake() {
        this.spriteRenderer = this.GetComponent<SpriteRenderer>();
        this.animator = this.GetComponent<Animator>();
        this._rb = this.GetComponent<Rigidbody2D>();
        this.inventory = this.GetComponent<Inventory>();
        this.collider = this.GetComponent<BoxCollider2D>();
    }

    public void SetMoveInput(Vector2 dir) {
        this._moveInput = dir;
    }

    void Update() {
        // get direction of player

        // flip sprite based on horizontal movement
        if (this._moveInput.x > 0) {
            this.spriteRenderer.flipX = false;
        } else if (this._moveInput.x < 0) {
            this.spriteRenderer.flipX = true;
        }
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

        if (this._moveInput.x != 0 || this._moveInput.y != 0) {
            this.animator.SetBool("isMoving", true);
            if (this._moveInput.x != 0) {
                this.animator.SetBool("isSideways", true);
                this.animator.SetBool("isForward", false);
                this.animator.SetBool("isBackward", false);
            }
            if (this._moveInput.y < 0) {
                this.animator.SetBool("isSideways", false);
                this.animator.SetBool("isForward", true);
                this.animator.SetBool("isBackward", false);
            }
            if (this._moveInput.y > 0) {
                this.animator.SetBool("isSideways", false);
                this.animator.SetBool("isForward", false);
                this.animator.SetBool("isBackward", true);
            }


        } else {
            this.animator.SetBool("isMoving", false);
        }

    }

    // Referenced by the Player Input to pass move commands
    public void MoveInput(InputAction.CallbackContext context) {
        this._moveInput = context.ReadValue<Vector2>();
    }

    public void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(this.transform.position, this.transform.lossyScale);
    }
}
