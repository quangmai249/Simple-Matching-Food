using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scrips.Manager
{
    public class LevelManager : Singleton<LevelManager>
    {
        [SerializeField] GameObject buttonLevel;
        [SerializeField] List<DataLevel> lsDataLevel = new List<DataLevel>();

        private int _currentLevel;

        private ObjectPool _pool;
        private DataLevel _dataLevel;

        private HashSet<string> _hashSetLevelUnlocked;
        private Dictionary<GameObject, DataLevel> dic = new Dictionary<GameObject, DataLevel>();

        protected override void Awake()
        {
            base.Awake();

            _pool = new ObjectPool(buttonLevel, this.transform, lsDataLevel.Count);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        private void Start()
        {
            _hashSetLevelUnlocked = new HashSet<string>(GetListLevel.Select(n => n.levelName));
            this.SetButtonDefault();
        }

        private void SetButtonDefault()
        {
            int count = 0;

            foreach (var item in _pool.Pool)
            {
                item.name = lsDataLevel[count].levelName;
                item.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text
                    = lsDataLevel[count].levelName.Replace("Level ", "");

                dic[item] = lsDataLevel[count];

                count++;
            }
        }

        public Dictionary<GameObject, DataLevel> DicDataLevel
        {
            get => dic;
        }

        public HashSet<string> HashSetLevelUnLocked
        {
            get => _hashSetLevelUnlocked;
        }

        public List<Level> GetListLevel
        {
            get => SaveManager.Instance.GetDataLevelSaving().levels;
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

        public ObjectPool Pool
        {
            get => _pool;
        }

        public int CurrentLevel
        {
            get; set;
        }
    }
}