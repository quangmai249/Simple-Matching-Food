﻿using DG.Tweening;
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
            isPaused = false;
        }
        private void Start()
        {
            Application.targetFrameRate = 60;
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
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
        public void DisableAllTiles()
        {
            foreach (GameObject item in GameObject.FindGameObjectsWithTag(TagName.TAG_TILE))
            {
                item.GetComponent<Tile>().SetDefault();
                TilePool.Instance.Pool.ReturnToPool(item, TilePool.Instance.gameObject);
            }
        }
        public void RestartGame(float timeDelay)
        {
            GameObject tileSpawner = GameObject.FindGameObjectWithTag(TagName.TAG_TILE_SPAWNER);

            if (tileSpawner != null)
                StartCoroutine(tileSpawner.GetComponent<TileSpawner>().SetDefault(timeDelay));
        }
        public bool IsPaused
        {
            get => isPaused;
            set => isPaused = value;
        }
    }
}