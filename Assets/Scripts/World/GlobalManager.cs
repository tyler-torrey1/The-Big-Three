using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * This is a singleton object (only ever one).
 * It offers global world action
 */

public class GlobalManager : MonoBehaviour {
    static GlobalManager instance;
    public static PlayerMovement player => instance._player;

    private PlayerMovement _player;
    public GameObject hubWorld;

    public List<string> sceneNames; // e.g. hub, living room, kitchen, bedroom
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
        // get corresponding StageManager per unity scene
        stageManagers = new Dictionary<string, StageManager>();

        foreach (string sceneName in sceneNames)
        {
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }
    }

    public static void RegisterManager(string sceneName, StageManager manager) => instance.RegisterManagerInstance(sceneName, manager);

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
        hubWorld.SetActive(nextScene == "Hub Room");

        int index = SceneUtility.GetBuildIndexByScenePath(nextScene);
        if (index == -1)
        {
            Debug.LogError("Scene '" + nextScene + "' is not a valid scene");
            return;
        }

        if (this.currentScene != nextScene)
        {
            Scene scene = SceneManager.GetSceneByName(nextScene);
            SceneManager.SetActiveScene(scene);

            stageManagers[nextScene].EnterScene(fromDirection);
        }

        this.currentScene = nextScene;
    }
    private void RegisterManagerInstance(string sceneName, StageManager manager)
    {
        stageManagers[sceneName] = manager;
    }
}

public enum Direction
{
    North,
    South,
    East,
    West
}
