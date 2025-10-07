using System;
using UnityEngine;

[Serializable]
public class SaveData
{
    // Meta
    public int version = 1;
    public string lastScene;
    public string savedAtIsoUtc;

    // Progreso de juego (ejemplos)
    public int level = 1;
    public int coins = 0;
    public float playtimeSeconds = 0f;

    // Player
    public Vector3 playerPos;
    public int hp = 100;
    public int selectedCharacterIndex = 0;

    // Settings
    public float masterVolume = 1f;
}

/**
 * {
 *  "version": 1,
 *  "lastScene": "Beach",
 *  "savedAtIsoUtc" : "07-10-2025T17:29:00Z"
 *  "level": 1,
 *  "coins": 0,
 *  "playtimeSeconds": 0,
 *  "playerPos" : {
 *      "x": 0,
 *      "y": 0,
 *      "z": 
 *  }
 *  "hp": 100,
 *  "selectedCharaterIndex": 0,
 *  "masterVolume": 1,
 * }
 * 
 */