using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level
{
    public bool isUnlocked;
    public int starCount;
    public string levelName;

    public Level(bool isUnlocked, int starCount, string levelName)
    {
        this.isUnlocked = isUnlocked;
        this.starCount = starCount;
        this.levelName = levelName;
    }

    public string DisplayToString()
    {
        return $"unlocked {isUnlocked}, star {starCount}, name {levelName}";
    }
}
