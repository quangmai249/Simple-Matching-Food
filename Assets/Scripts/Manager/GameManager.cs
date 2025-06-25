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

        public IEnumerator RestartGame(float timeDisableAllTiles, float timeLoadGameplayScene)
        {
            yield return new WaitForSeconds(timeDisableAllTiles);
            this.DisableAllTiles();
            yield return new WaitForSeconds(timeLoadGameplayScene);
            SceneManager.LoadScene(SceneName.SCENE_GAMEPLAY);
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