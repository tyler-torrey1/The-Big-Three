using UnityEngine;
using UnityEngine.InputSystem;
public class FinishLine : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag.Equals("Player")) {
            PlayerMovement player = GlobalManager.player;
            PlayerInput playerInput = player.GetComponent<PlayerInput>();
            playerInput.enabled = false;
            player.SetMoveInput(Vector2.down);
            Debug.Log("Going down");
        }
    }
}
