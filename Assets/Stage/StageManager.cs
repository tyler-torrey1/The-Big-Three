using System.Collections.Generic;
using UnityEngine;

/**
 * This is a singleton object (only ever one).
 * It offers global world action
 */

public class StageManager : MonoBehaviour {
    static StageManager instance;

    public List<Stage> stages;
    Stage currentStage;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Debug.LogError(this.name + ": singleton betrayal!");
            return;
        }
    }
    void Start() {
        List<Direction> LIVING_ROOM_CODE = new() {
            Direction.North,
            Direction.South,
            Direction.West
        };
    }
    public static void ChangeStageTo(Stage nextStage, Direction from) {
        instance.ChangeStageToInstance(nextStage, from);
    }

    ///**
    // * Enter 
    // */
    private void ChangeStageToInstance(Stage nextStage, Direction from) {
        if (this.currentStage != nextStage) {
            foreach (Stage stage in this.stages) {
                stage.enabled = false;
            }
            nextStage.enabled = true;
        }

        this.currentStage = nextStage;
    }
}