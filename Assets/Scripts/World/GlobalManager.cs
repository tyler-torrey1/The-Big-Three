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

    public List<string> sceneNames; // e.g. hub, living room, kitchen, bedroom
    public Dictionary<string, StageManager> stageManagers;
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
            int index = SceneUtility.GetBuildIndexByScenePath(sceneName);
            if (index == -1)
            {
                Debug.LogError("Scene '" + sceneName + "' is not a valid scene");
                continue;
            }
            GameObject[] roots = SceneManager.GetSceneByName(sceneName).GetRootGameObjects();
            StageManager stageManager = null;
            foreach (GameObject root in roots)
            {
                    stageManager = root.GetComponent<StageManager>();
                    if (stageManager != null)
                    {
                        break;
                    }
             
                stageManagers[sceneName] = stageManager;
            }
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

        int index = SceneUtility.GetBuildIndexByScenePath(nextScene);
        if (index == -1)
        {
            Debug.LogError("Scene '" + nextScene + "' is not a valid scene");
            return;
        }

        Scene scene = SceneManager.GetSceneByName(nextScene);

        if (this.currentScene != nextScene)
        {
            SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
        }

        stageManagers[nextScene].EnterScene(GlobalManager.GetOppositeDirection(fromDirection));

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
