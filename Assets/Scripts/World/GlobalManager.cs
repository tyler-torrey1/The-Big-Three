using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

/**
 * This is a singleton object (only ever one).
 * It offers global world action
 */

[DefaultExecutionOrder(-1)]
public class GlobalManager : MonoBehaviour {
    static GlobalManager instance;

    public static PlayerMovement player => instance._player;
    public static Inventory inventory => player.inventory;
    public static SpriteRenderer spriteRenderer => player.spriteRenderer;
    public static TMPro.TextMeshPro finishText => instance._finishText;

    public static event Action OnSceneChanged;

    [SerializeField] private GameObject roomsRoot;
    [SerializeField] private PlayerMovement _player;
    [SerializeField] private TMPro.TextMeshPro _finishText;

    Dictionary<string, StageManager> stageManagers;
    string currentScene;

    public event Action _OnSceneChanged;


    void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Debug.LogError(this.name + ": singleton betrayal!");
            return;
        }
    }

    private void Start() {
        StageManager[] managers = this.roomsRoot.GetComponentsInChildren<StageManager>(true);
        this.stageManagers = new Dictionary<string, StageManager>();
        foreach (StageManager stageManager in managers) {
            this.stageManagers[stageManager.name] = stageManager;
            stageManager.gameObject.SetActive(stageManager.name == "Hub");
        }
    }

    /**
     * Helper for opposing cardinal direction.
     */
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

    public static void ChangeSceneTo(string nextScene, Direction from) {
        instance.ChangeSceneToInstance(nextScene, from);
    }


    private void ChangeSceneToInstance(string nextScene, Direction fromDirection) {
        // exist check
        if (!this.stageManagers.ContainsKey(nextScene)) {
            Debug.LogError("No StageManager named '" + nextScene + "'");
            return;
        }

        OnSceneChanged?.Invoke();

        // Deactivate all but entered scene
        foreach (StageManager manager in this.stageManagers.Values) {
            manager.gameObject.SetActive(manager.name == nextScene);
        }

        if (this.currentScene != nextScene) {
            this.stageManagers[nextScene].EnterScene(fromDirection);
        }

        this.currentScene = nextScene;
    }

    public static IEnumerator FinishCoroutine()
    {
        float duration = 5f;
        float elapsedSeconds = 0f;

        do
        {
            float lifetimeLerp = Mathf.Clamp01(elapsedSeconds / duration);

            // Fade player
            Color color = player.spriteRenderer.color;
            color = new Color(color.r, color.g, color.b, 1f - lifetimeLerp);
            player.spriteRenderer.color = color;

            // Fade in text
            color = finishText.color;
            color = new Color(color.r, color.g, color.b, lifetimeLerp);
            finishText.color = color;

            elapsedSeconds += Time.deltaTime;
            yield return null;
        } while (elapsedSeconds < duration);
    }
}

public enum Direction {
    North,
    South,
    East,
    West
}
