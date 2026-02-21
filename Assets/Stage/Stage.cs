using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour {
    [SerializeField] private Door _northDoor;
    [SerializeField] private Door _southDoor;
    [SerializeField] private Door _eastDoor;
    [SerializeField] private Door _westDoor;

    Dictionary<Direction, Door> doors;

    private void Start() {
        this.doors = new Dictionary<Direction, Door>();
        if (this._northDoor != null) { this.doors[Direction.North] = this._northDoor; }
        if (this._southDoor != null) { this.doors[Direction.South] = this._southDoor; }
        if (this._eastDoor != null) { this.doors[Direction.East] = this._eastDoor; }
        if (this._westDoor != null) { this.doors[Direction.West] = this._westDoor; }
    }


    public void Enter(PlayerMovement player, Direction from) {
        if (!this.doors.ContainsKey(from)) {
            Debug.LogError(this.name + ": '" + from + "' not handled");
        }

        player.transform.position = this.doors[from].entrance;
    }
}

public enum Direction {
    North,
    South,
    East,
    West
}
