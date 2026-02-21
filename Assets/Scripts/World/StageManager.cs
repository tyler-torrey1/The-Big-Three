using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    public List<Stage> stages;
    public Stage currentStage;

    [SerializeField] private Door _northDoor;
    [SerializeField] private Door _southDoor;
    [SerializeField] private Door _eastDoor;
    [SerializeField] private Door _westDoor;

    private Dictionary<Direction, Door> _doors;
    private void Start() {
        this._doors = new Dictionary<Direction, Door>();
        if (this._northDoor != null) { this._doors[Direction.North] = this._northDoor; }
        if (this._southDoor != null) { this._doors[Direction.South] = this._southDoor; }
        if (this._eastDoor != null) { this._doors[Direction.East] = this._eastDoor; }
        if (this._westDoor != null) { this._doors[Direction.West] = this._westDoor; }

        foreach (Door door in this._doors.Values) {
            Debug.Log("Setting door: " + door.name);
            door.OnDoorEntered += this.HandleDoorEntered;
        }
    }

    // Cleans up the event listeners
    private void OnDestroy() {
        if (this._doors != null) {
            foreach (Door door in this._doors.Values) {
                door.OnDoorEntered -= this.HandleDoorEntered;
            }
        }
    }

    /**
     * Enter first stage of this scene
     */
    public void EnterScene(Direction fromDirection)
    {
        ChangeStageTo(stages[0], fromDirection);
    }

    private void ChangeStageTo(Stage nextStage, Direction entryDirection)
    {
        Door oppositeDoor = this._doors[entryDirection];
        HandleDoorEntered(oppositeDoor);

        // Disable all stages but the next
        if (this.currentStage != nextStage)
        {
            foreach (Stage stage in stages)
            {
                stage.enabled = false;
            }
            nextStage.enabled = true;
        }

        // Move player to the corresponding entrance


        this.currentStage = nextStage;
    }

    private void HandleDoorEntered(Door enteredDoor) {
        Vector2 spawnPos = this.getEntranceOffset(enteredDoor);

        Vector3 vector3SpawnPos = new(spawnPos.x, spawnPos.y, -5);
        this.player.transform.position = spawnPos;
    }

    private Vector2 getEntranceOffset(Door enteredDoor) {
        //Debug.Log("EnteredDoor Name: " + enteredDoor.name);
        //Debug.Log("EnteredDoor Direction: " + enteredDoor.direction);
        Door oppositeDoor = this._doors[GlobalManager.GetOppositeDirection(enteredDoor.direction)];
        Vector2 entrancePoint = oppositeDoor.entrance;

        SpriteRenderer playerRenderer = this.player.GetComponent<SpriteRenderer>();
        switch (oppositeDoor.direction) {
            case Direction.North:
                // This is hacky. I actually no idea what position on the player object drives this, but I'm manually adjusting where he lands when he lands north, so he's closer to the door.
                // Logically, this should be a subtraction, but I don't know anything anymore at all
                entrancePoint.y += 1f;
                break;

            case Direction.South:
                // This is the dumbest fucking shit
                entrancePoint.y += playerRenderer.size.y / 6f;
                break;

            case Direction.East:
                entrancePoint.x -= playerRenderer.size.x / 8f;
                break;

            case Direction.West:
                entrancePoint.x += playerRenderer.size.x / 8f;
                break;
        }
        return entrancePoint;
    }
    public void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(this.player.transform.localPosition, this.player.transform.localScale);
    }
}