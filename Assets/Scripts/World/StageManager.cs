using System;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour {
    public List<StageTileMap> stages;
    StageTileMap currentStage => this.currentStageIndex == -1 ? null : this.stages[this.currentStageIndex];
    public int currentStageIndex;

    [SerializeField] private Door _northDoor;
    [SerializeField] private Door _southDoor;
    [SerializeField] private Door _eastDoor;
    [SerializeField] private Door _westDoor;

    Direction[] solution;
    int solutionCount = 4;

    public event Action<int> OnStageChanged;

    private Dictionary<Direction, Door> _doors;

    private void Awake() {
        this._doors = new Dictionary<Direction, Door>();
        if (this._northDoor != null) { this._doors[Direction.North] = this._northDoor; }
        if (this._southDoor != null) { this._doors[Direction.South] = this._southDoor; }
        if (this._eastDoor != null) { this._doors[Direction.East] = this._eastDoor; }
        if (this._westDoor != null) { this._doors[Direction.West] = this._westDoor; }

        this.RandomizeSolution();
    }
    private void Start() {
        foreach (Door door in this._doors.Values) {
            Debug.Log("Setting door: " + door.name);
            door.OnDoorEntered += this.HandleDoorEntered;
        }
    }

    private void RandomizeSolution() {
        this.solution = new Direction[this.solutionCount];
        for (int i = 0; i < this.solutionCount; i++) {
            this.solution[i] = (Direction)UnityEngine.Random.Range(0, 4);
            Debug.Log(this.name + ": solution[" + i + "]=" + this.solution[i]);
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
    public void EnterScene(Direction fromDirection) {
        this.gameObject.SetActive(true);
        this.currentStageIndex = 0;
        this.ChangeStageTo(0, fromDirection);
    }

    public void ExitScene() {
        this.gameObject.SetActive(false);
    }

    private void ChangeStageTo(int nextStageIndex, Direction startDirection) {
        // Invoke, for the children
        //if (this.currentStageIndex != nextStageIndex) {
        Debug.Log("Invoking for the children!");
        this.OnStageChanged?.Invoke(nextStageIndex);
        //}

        // Move player to the corresponding entrance
        Vector2 spawnPos = this.getEntranceOffset(startDirection);
        GlobalManager.player.transform.position = spawnPos;

    }

    /**
     * Leaving the door via given door.
     */
    private void HandleDoorEntered(Door enteredDoor) {
        Debug.Log("Entered Door '" + enteredDoor.name + "'");
        bool withinSolution = -1 < this.currentStageIndex && this.currentStageIndex < this.solutionCount;

        int oldStageIndex = this.currentStageIndex;
        if (withinSolution && enteredDoor.direction == this.solution[this.currentStageIndex]) {
            // correct direction
            ++this.currentStageIndex;
        } else {
            // reset if wrong
            this.currentStageIndex = 0;
        }
        Debug.Log(this.name + ": stage index " + oldStageIndex + " -> " + this.currentStageIndex + " (" + this.solutionCount + ")");

        this.ChangeStageTo(this.currentStageIndex, GlobalManager.GetOppositeDirection(enteredDoor.direction));
    }

    private Vector2 getEntranceOffset(Direction spawnAt) {

        Door door = this._doors[spawnAt];
        Vector2 entrancePoint = door?.entrance ?? Vector2.zero;

        Vector2 playerCenter = GlobalManager.player.transform.localPosition;
        Bounds footBounds = GlobalManager.player.colliderBounds;
        Debug.Log("Player Center: " + playerCenter);
        Debug.Log("Foot Bounds: " + footBounds);

        float padding = 0.3f;

        switch (door.direction) {
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