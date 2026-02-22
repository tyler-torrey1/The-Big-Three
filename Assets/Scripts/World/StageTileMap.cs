using UnityEngine;
using UnityEngine.Tilemaps;

/**
 * A corruption version of a scene.
 */

public class StageTileMap : MonoBehaviour {
    StageManager stageManager;

    Tilemap[] tileMaps;


    private void Awake() {
        this.tileMaps = this.GetComponentsInChildren<Tilemap>(true);
        this.stageManager = this.GetComponentInParent<StageManager>();

    }
    private void Start() {
        this.ChangeTilemap(0);
    }
    private void OnEnable() {
        this.stageManager.OnStageChanged += this.ChangeTilemap;
    }
    private void OnDisable() {
        this.stageManager.OnStageChanged -= this.ChangeTilemap;
    }


    private void ChangeTilemap(int stageIndex) {
        this.tileMaps[0].gameObject.SetActive(stageIndex < 2);
        this.tileMaps[1].gameObject.SetActive(stageIndex >= 2);
    }
}
