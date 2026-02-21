using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StageManager : MonoBehaviour
{
    public List<Stage> stages;
    public int currentStage;

    [SerializeField] private Door _northDoor;
    [SerializeField] private Door _southDoor;
    [SerializeField] private Door _eastDoor;
    [SerializeField] private Door _westDoor;

    Direction[] solution;
    int solutionCount = 3;


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

        solution = new Direction[solutionCount];
        for (int i = 0; i < solutionCount; i++)
        {
            solution[i] = (Direction) Random.Range(0, 4);
        }
        Debug.Log(SceneManager.GetActiveScene().name + ": solution=" + solution);
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
        ChangeStageTo(stages[0], fromDirection, true);
    }

    private void ChangeStageTo(Stage nextStage, Direction entryDirection, bool fromHub)
    {
        Door oppositeDoor = this._doors[entryDirection];
        HandleDoorEntered(oppositeDoor);

        // Disable all stages but the next
        //if (this.currentStage != nextStage)
        {
            foreach (Stage stage in stages)
            {
                stage.enabled = false;
            }
            nextStage.enabled = true;
        }

        // Move player to the corresponding entrance


        //this.currentStage = nextStage;
    }

    private void HandleDoorEntered(Door enteredDoor) {

        if (enteredDoor.direction == solution[currentStage])
        {
            // correct direction
        }
        Vector2 spawnPos = this.getEntranceOffset(enteredDoor);

        Vector3 vector3SpawnPos = new(spawnPos.x, spawnPos.y, -5);
        GlobalManager.player.transform.position = spawnPos;
    }

    private Vector2 getEntranceOffset(Door enteredDoor) {
        //Debug.Log("EnteredDoor Name: " + enteredDoor.name);
        //Debug.Log("EnteredDoor Direction: " + enteredDoor.direction);
        Door oppositeDoor = this._doors[GlobalManager.GetOppositeDirection(enteredDoor.direction)];
        Vector2 entrancePoint = oppositeDoor.entrance;

        SpriteRenderer playerRenderer = GlobalManager.player.GetComponent<SpriteRenderer>();

        BoxCollider2D playerCollider = GlobalManager.player.GetComponent<BoxCollider2D>();

        Vector2 playerCenter = GlobalManager.player.transform.localPosition;
        Bounds footBounds = playerCollider.bounds;
        Debug.Log("Player Center: " + playerCenter);
        Debug.Log("Foot Bounds: " + footBounds);

        float padding = 0.2f;

        switch (oppositeDoor.direction) {
            case Direction.North:
                // We did it :)
                entrancePoint.y += playerCenter.y - footBounds.min.y - padding;
                break;

            case Direction.South:
                // This is the dumbest fucking shit AND WE "SOLVED" IT
                entrancePoint.y += playerCenter.y - footBounds.max.y + padding;
                break;

            case Direction.East:
                entrancePoint.x -= playerCenter.x - footBounds.center.x + padding;
                break;

            case Direction.West:
                entrancePoint.x += playerCenter.x - footBounds.center.x + padding;
                break;
        }
        return entrancePoint;
    }
}