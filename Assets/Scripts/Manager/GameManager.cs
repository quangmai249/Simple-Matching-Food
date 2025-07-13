using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scrips.Manager
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] bool isPaused = false;

        protected override void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            isPaused = false;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        public void RestartGame()
        {
            GameObject tileSpawner = GameObject.FindGameObjectWithTag(TagName.TAG_TILE_SPAWNER);

            if (tileSpawner != null)
                tileSpawner.GetComponent<TileSpawner>().SetDefault();
        }

        public void DisableAllTiles()
        {
            foreach (GameObject item in GameObject.FindGameObjectsWithTag(TagName.TAG_TILE))
            {
                item.GetComponent<Tile>().SetDefault();
                TilePool.Instance.Pool.ReturnToPool(item, TilePool.Instance.gameObject);
            }
        }

        public bool IsPaused
        {
            get => isPaused;
            set => isPaused = value;
        }

        public void PauseGame()
        {
            DOTween.timeScale = 0f;
            Time.timeScale = 0f;
        }

        public void ResumeGame()
        {
            DOTween.timeScale = 1f;
            Time.timeScale = 1f;
        }
    }
}