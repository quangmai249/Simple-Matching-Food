using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scrips.Manager
{
    public class LevelManager : Singleton<LevelManager>
    {
        [SerializeField] List<DataLevel> dataLevel = new List<DataLevel>();
        protected override void Awake()
        {
            base.Awake();
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
        public int GetMaxLevel()
        {
            return dataLevel.Count;
        }
        public DataLevel GetDataLevel(int level)
        {
            if (level < 0 || level > dataLevel.Count)
            {
                Debug.LogError("Level index out of range: " + level);
                return null;
            }
            return dataLevel[level];
        }
    }
}