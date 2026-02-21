using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Tilemaps;

/**
 * A corruption version of a scene.
 */

public class StageTileMap : MonoBehaviour
{
    StageManager stageManager;

    Tilemap[] tileMaps;


    private void Awake()
    {
        tileMaps = GetComponentsInChildren<Tilemap>();

        stageManager = GetComponentInParent<StageManager>();

        stageManager.OnStageChanged += ChangeTilemap;
    }

    private void ChangeTilemap(int tilemapIndex)
    {
        for (int i = 0; i < tileMaps.Length; i++)
        {
            Tilemap tilemap = tileMaps[i];
            tilemap.gameObject.SetActive(tilemapIndex == i);
        }
    }
}
