using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

/**
 * This is a singleton object (only ever one).
 * It offers global world action
 */

public class StageManager : MonoBehaviour
{
    static StageManager instance;

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
            Debug.LogError(name + ": singleton betrayal!");
            return;
        }
    }

    public static void ChangeStageTo(Stage nextStage, Direction from) => instance.ChangeStageToInstance(nextStage, from);

    /**
     * Enter 
     */
    private void ChangeStageToInstance(Stage nextStage, Direction from)
    {
        if (currentStage != nextStage)
        {
            foreach (Stage stage in stages)
            {
                stage.enabled = false;
            }
            nextStage.enabled = true;
        }

        currentStage = nextStage;
    }
}