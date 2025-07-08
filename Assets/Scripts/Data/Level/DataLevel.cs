using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDataLevel", menuName = "Data/Level")]
public class DataLevel : ScriptableObject
{
    public string levelName;
    public int matchingCount;
    public Vector2 levelSize;
    public float timeLimit;
}
