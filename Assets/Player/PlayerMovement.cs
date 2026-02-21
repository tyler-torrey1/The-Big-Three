using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {
    // Object components
    private SpriteRenderer spriteRenderer;

    private Animator animator;
    private Rigidbody2D _rb;

    // 'Cache' for storing user input
    [SerializeField]
    private Vector2 _moveInput;
    [SerializeField] private float _movementSpeed = 50.0f;
    private void Awake() {
        this.spriteRenderer = this.GetComponent<SpriteRenderer>();
        this.animator = this.GetComponent<Animator>();
        this._rb = this.GetComponent<Rigidbody2D>();
    }

    void Start(){
        this._rb.useFullKinematicContacts = true;
    }

    void Update()
    {
        // get direction of player
        
        // flip sprite based on horizontal movement
        if (this._moveInput.x > 0)
        {
            spriteRenderer.flipX = false; 
        }
        else if (this._moveInput.x < 0)
        {
            spriteRenderer.flipX = true; 
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

        if (this._moveInput.x != 0 || this._moveInput.y != 0)
        {
            animator.SetBool("isMoving", true);
            if (this._moveInput.x != 0)
            {
                animator.SetBool("isSideways", true);
                animator.SetBool("isForward", false);
                animator.SetBool("isBackward", false);
            }
            if (this._moveInput.y < 0)
            {
                animator.SetBool("isSideways", false);
                animator.SetBool("isForward", true);
                animator.SetBool("isBackward", false);
            }
            if (this._moveInput.y > 0)
            {
                animator.SetBool("isSideways", false);
                animator.SetBool("isForward", false);
                animator.SetBool("isBackward", true);
            }


        }
        else
        {
            animator.SetBool("isMoving", false);
        }

    }

    // Referenced by the Player Input to pass move commands
    public void MoveInput(InputAction.CallbackContext context) {
        this._moveInput = context.ReadValue<Vector2>();
    }
}
