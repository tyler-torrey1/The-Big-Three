using System.Collections.Generic;
using UnityEngine;

/**
 * This is a singleton object (only ever one).
 * It offers global world action
 */

public class GlobalManager : MonoBehaviour
{
    static GlobalManager instance;

    public static PlayerMovement player => instance._player;

    [SerializeField] private GameObject roomsRoot;
    [SerializeField] private PlayerMovement _player;

    Dictionary<string, StageManager> stageManagers;
    string currentScene;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.LogError(this.name + ": singleton betrayal!");
            return;
        }
    }

    private void Start()
    {
        StageManager[] managers = roomsRoot.GetComponentsInChildren<StageManager>(true);
        stageManagers = new Dictionary<string, StageManager>();
        foreach (StageManager stageManager in managers)
        {
            stageManagers[stageManager.name] = stageManager;
            stageManager.gameObject.SetActive(stageManager.name == "Hub");
        }
    }

    /**
     * Helper for opposing cardinal direction.
     */
    public static Direction GetOppositeDirection(Direction direction)
    {
        switch(direction)
        {
            case Direction.North:
                return Direction.South;
            case Direction.South:
                return Direction.North;
            case Direction.East:
                return Direction.West;
            case Direction.West:
                return Direction.East;
            default:
                Debug.LogError(direction + " has no opposite");
                return direction;
        }
    }

    public static void ChangeSceneTo(string nextScene, Direction from)
    {
        instance.ChangeSceneToInstance(nextScene, from);
    }


    private void ChangeSceneToInstance(string nextScene, Direction fromDirection)
    {
        // exist check
        if (!stageManagers.ContainsKey(nextScene))
        {
            Debug.LogError("No StageManager named '" + nextScene + "'");
            return;
        }

        // Deactivate all but entered scene
        foreach (StageManager manager in stageManagers.Values)
        {
            manager.gameObject.SetActive(manager.name == nextScene);
        }

        if (this.currentScene != nextScene)
        {
            stageManagers[nextScene].EnterScene(fromDirection);
        }

        this.currentScene = nextScene;
    }
}

public enum Direction
{
    North,
    South,
    East,
    West
}
