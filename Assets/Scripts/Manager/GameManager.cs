using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scrips.Manager
{
    public class GameManager : Singleton<GameManager>
    {
        protected override void Awake()
        {
            base.Awake();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        public void RestartGame(float timeSpawn)
        {
            GameObject tileSpawner = GameObject.FindGameObjectWithTag(TagName.TAG_TILE_SPAWNER);

            if (tileSpawner != null)
                StartCoroutine(tileSpawner.GetComponent<TileSpawner>().SetDefault(timeSpawn));
        }

        public void DisableAllTiles()
        {
            foreach (GameObject item in GameObject.FindGameObjectsWithTag(TagName.TAG_TILE))
            {
                item.GetComponent<Tile>().SetDefault();
                TilePool.Instance.Pool.ReturnToPool(item, TilePool.Instance.gameObject);
            }
        }
    }
}