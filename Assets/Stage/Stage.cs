using System;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField] private Door _northDoor;
    [SerializeField] private Door _southDoor;
    [SerializeField] private Door _eastDoor;
    [SerializeField] private Door _westDoor;

    Dictionary<Direction, Door> doors;

    private void Start()
    {
        doors = new Dictionary<Direction, Door>();
        if (_northDoor != null) { doors[Direction.North] = _northDoor; }
        if (_southDoor != null) { doors[Direction.South] = _southDoor; }
        if (_eastDoor != null) { doors[Direction.East] = _eastDoor; }
        if (_westDoor != null) { doors[Direction.West] = _westDoor; }
    }

    public void Enter(PlayerMovement player, Direction from)
    {
        if (!doors.ContainsKey(from))
        {
            Debug.LogError(name + ": '" + from + "' not handled");
        }

        player.transform.position = doors[from].entrance.position;
    }
}

public enum Direction
{
    North,
    South,
    East,
    West
}

[Serializable]
public class Door
{
    public Collider2D exit;
    public Transform entrance;
}