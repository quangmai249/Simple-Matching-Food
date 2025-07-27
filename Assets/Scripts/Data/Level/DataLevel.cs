using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDataLevel", menuName = "Data/Level")]
public class DataLevel : ScriptableObject
{
    public bool isUnlocked;
    public int matchingCount;
    public float timeLimit;
    public string levelName;
    public Vector2 levelSize;
}
