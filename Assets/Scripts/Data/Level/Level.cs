using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level
{
    public int starCount;
    public string levelName;

    public Level(int starCount, string levelName)
    {
        this.starCount = starCount;
        this.levelName = levelName;
    }
}
