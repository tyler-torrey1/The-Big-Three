using System.Collections.Generic;
using UnityEngine;
public class StageableRoomObject : MonoBehaviour {
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private List<Sprite> _sprites;

    private StageManager _stageManager;

    public bool IsActive = false;

    [SerializeField]
    private int _maxStages;
    private int _maxStageIndex => this._maxStages - 1;
    private int _currentStageIndex;

    public bool changeStage = false;

    void Update() {
        if (this.changeStage) {
            this.SetCurrentStage(this._currentStageIndex + 1);
            this.changeStage = false;
        }
    }
    void Awake() {
        this._currentStageIndex = this._maxStages;
        this._spriteRenderer = this.GetComponent<SpriteRenderer>();
        this._stageManager = this.GetComponent<StageManager>();

        this._stageManager.OnStageChanged += this.SetCurrentStage;

        this.SetCurrentStage(0);
    }

    public void SetCurrentStage(int newStageIndex) {
        if (newStageIndex > this._maxStageIndex) { Debug.LogError("The new stage is equal to the max stage.."); return; }
        newStageIndex = Mathf.Min(this._maxStageIndex, newStageIndex);

        // Decrements weirdness. Sprites are populated by weirdest -> least weird
        this._spriteRenderer.sprite = this._sprites[newStageIndex];
        this._currentStageIndex = newStageIndex;
    }
}
