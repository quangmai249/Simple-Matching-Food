using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scrips.Manager
{
    public class LevelManager : Singleton<LevelManager>
    {
        [SerializeField] List<DataLevel> lsDataLevel = new List<DataLevel>();

        private int _countLevel;

        private DataLevel _dataLevel;
        private DataLevelSaving _dataLevelSaving;
        private Dictionary<Level, DataLevel> dic = new Dictionary<Level, DataLevel>();
        protected override void Awake()
        {
            base.Awake();
        }
        private void Start()
        {
            SaveManager.Instance.DeleteAllKeyData();

            _dataLevelSaving = SaveManager.Instance.GetDataLevelSaving();
            _countLevel = _dataLevelSaving.levels.Length;
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
        public Level[] GetDataLevelSaving()
        {
            return _dataLevelSaving.levels;
        }
        public List<DataLevel> GetListDataLevel
        {
            get => lsDataLevel;
        }
        public DataLevel DataLevel
        {
            get => _dataLevel;
            set => _dataLevel = value;
        }
    }
}