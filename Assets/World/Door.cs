using System;
using UnityEngine;
public class Door : MonoBehaviour {
    public BoxCollider2D exit;
    public Vector2 entrance;

    private float enterPadding;
    // Orientation of the door- dictates placement of the spawn point.
    public Direction direction;

    private ExitTrigger _exitTrigger;
    public event Action<Door> OnDoorEntered;

    private void Awake() {
        if (this._exitTrigger == null) {
            ExitTrigger exitTrigger = this.GetComponentInChildren<ExitTrigger>();
            if (exitTrigger == null) {
                Debug.LogError("Door " + this.gameObject.name + " is missing an ExitTrigger!");
            }
            this._exitTrigger = exitTrigger;
        }
        this._exitTrigger.OnPlayerEntered += this.HandleExitTriggered;
    }

    private void OnDestroy() {
        if (this._exitTrigger != null) {
            this._exitTrigger.OnPlayerEntered -= this.HandleExitTriggered;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        this.PlaceEntrance();
    }
    // An "Entrance" is basically a spawn point, with an offset from the door collider. Larger offset for vertical colliders.
    private void PlaceEntrance() {
        Bounds bounds = this.exit.bounds;
        Vector2 entrancePos = bounds.center;
        switch (this.direction) {
            case Direction.North:
                this.enterPadding = .1f; // Spawns slightly offset from the exit
                entrancePos.y = bounds.min.y - this.enterPadding;
                break;
            case Direction.East:
                this.enterPadding = 0.335f; // half the width of player box collider X
                entrancePos.x = bounds.min.x - this.enterPadding;
                break;
            case Direction.West:
                this.enterPadding = 0.335f; // half the width of player box collider X
                entrancePos.x = bounds.max.x + this.enterPadding;
                break;
            case Direction.South:
                this.enterPadding = .1f;
                entrancePos.y = bounds.max.y + this.enterPadding;
                break;
        }
        this.entrance = entrancePos;
    }

    private void HandleExitTriggered() {
        //Debug.Log("HandleExitTriggered");
        OnDoorEntered?.Invoke(this);
    }

    public void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(this.exit.bounds.center, this.exit.bounds.size);
        Gizmos.color = new Color(1f, 0.5f, 0f, 1f); // orange-ish
        Gizmos.DrawWireCube(this.entrance, new Vector3(0.1f, 0.1f, 0f));
    }
}
