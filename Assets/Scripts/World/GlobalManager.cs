using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * This is a singleton object (only ever one).
 * It offers global world action
 */

public class GlobalManager : MonoBehaviour {
    static GlobalManager instance;

    public PlayerMovement player;
    public List<Scene> scenes; // e.g. living room, kitchen, bedroom
    public Dictionary<Scene, StageManager> stageManagers;
    Scene currentStage;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Debug.LogError(this.name + ": singleton betrayal!");
            return;
        }
    }

    private void Start() {
        // get corresponding StageManager per unity scene
        this.stageManagers = new Dictionary<Scene, StageManager>();
        foreach (Scene scene in this.scenes) {
            GameObject[] roots = scene.GetRootGameObjects();
            StageManager stageManager = null;
            foreach (GameObject root in roots) {
                {
                    stageManager = root.GetComponent<StageManager>();
                    if (stageManager != null) {
                        break;
                    }
                }
                this.stageManagers[scene] = stageManager;
            }
        }
    }

    public static Direction GetOppositeDirection(Direction direction) {
        switch (direction) {
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

    public static void ChangeStageTo(StageManager nextStage, Direction from) {
        instance.ChangeStageToInstance(nextStage, from);
    }


    private void ChangeStageToInstance(StageManager nextStage, Direction from) {

        //// Disable all scenes but the next
        //if (this.currentStage != nextStage)
        //{
        //    foreach (StageManager stage in this.scenes)
        //    {
        //        stage.enabled = false;
        //    }
        //    nextStage.enabled = true;
        //}

        //// Move player to the corresponding entrance


        //this.currentStage = nextStage;
    }

}
public enum Direction {
    North,
    South,
    East,
    West
}
