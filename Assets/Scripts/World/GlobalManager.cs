using System.Collections.Generic;
using UnityEngine;

/**
 * This is a singleton object (only ever one).
 * It offers global world action
 */

public class GlobalManager : MonoBehaviour {
    static GlobalManager instance;

    public PlayerMovement player;
    public List<Stage> stages;
    Stage currentStage;

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

    public static void ChangeStageTo(Stage nextStage, Direction from)
    {
        instance.ChangeStageToInstance(nextStage, from);
    }


    private void ChangeStageToInstance(Stage nextStage, Direction from)
    {

        // Disable all stages but the next stage
        if (this.currentStage != nextStage)
        {
            foreach (Stage stage in this.stages)
            {
                stage.enabled = false;
            }
            nextStage.enabled = true;
        }

        // Move player to the corresponding entrance


        this.currentStage = nextStage;
    }

}
public enum Direction
{
    North,
    South,
    East,
    West
}
