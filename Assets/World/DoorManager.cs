using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour {
    [SerializeField]
    private GameObject player;

    [SerializeField] private Door _northDoor;
    [SerializeField] private Door _southDoor;
    [SerializeField] private Door _eastDoor;
    [SerializeField] private Door _westDoor;

    private Dictionary<Direction, Door> _doors;
    private Dictionary<Direction, Direction> _oppositeDir;

    private void Start() {
        this._doors = new Dictionary<Direction, Door>();
        if (this._northDoor != null) { this._doors[Direction.North] = this._northDoor; }
        if (this._southDoor != null) { this._doors[Direction.South] = this._southDoor; }
        if (this._eastDoor != null) { this._doors[Direction.East] = this._eastDoor; }
        if (this._westDoor != null) { this._doors[Direction.West] = this._westDoor; }

        this._oppositeDir = new Dictionary<Direction, Direction> {
            [Direction.North] = Direction.South,
            [Direction.South] = Direction.North,
            [Direction.East] = Direction.West,
            [Direction.West] = Direction.East
        };

        foreach (Door door in this._doors.Values) {
            Debug.Log("Setting door: " + door.name);
            door.OnDoorEntered += this.HandleDoorEntered;
        }
    }

    private void OnDestroy() {
        if (this._doors != null) {
            foreach (Door door in this._doors.Values) {
                door.OnDoorEntered -= this.HandleDoorEntered;
            }
        }
    }

    private void HandleDoorEntered(Door door) {
        Transform playerTransform = this.player.GetComponent<Transform>();

        Door oppositeDoor = this._doors[this._oppositeDir[door.direction]];
        Vector2 spawnPos = oppositeDoor.entrance;

        // Does this really need to be this complicated? Whatever, I want to go to bed
        switch (oppositeDoor.direction) {
            case Direction.North:
                spawnPos += Vector2.down * (playerTransform.lossyScale / 2);
                break;

            case Direction.South:
                // This is the dumbest fucking shit
                spawnPos += (Vector2.up * (playerTransform.lossyScale / 2)) + (Vector2.up * .75f);
                break;

            case Direction.East:
                spawnPos += Vector2.left * (playerTransform.lossyScale / 2);
                break;

            case Direction.West:
                spawnPos += Vector2.right * (playerTransform.lossyScale / 2);
                break;
        }
        // Fucking fuck
        Vector3 vector3SpawnPos = new(spawnPos.x, spawnPos.y, -5);
        this.player.transform.position = spawnPos;
    }
    public void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(this.player.transform.localPosition, this.player.transform.localScale);
    }
}