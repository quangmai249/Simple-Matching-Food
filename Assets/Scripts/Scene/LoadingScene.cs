using Assets.Scrips.Manager;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    [SerializeField] float timeLoading = 0;

    private bool _isReady = false;
    private bool _isLoadingFailed = false;
    private TilePool tilePool;

    private void Awake()
    {

    }

    private void Start()
    {
        UIManager.instance.ShowPanel(EnumPanelType.Loading);
        SaveManager.Instance.ChangeLanguage();
        this.CheckTilePool();
    }

    private void Update()
    {
        this.CheckLoading();
    }

    private void CheckLoading()
    {
        timeLoading += Time.deltaTime;

        if (!_isLoadingFailed && timeLoading >= 10f)
        {
            _isLoadingFailed = true;
            return;
        }

        if (!_isLoadingFailed && _isReady && timeLoading >= 3.25f)
        {
            SceneManager.LoadScene(SceneName.SCENE_HOME);
            return;
        }
    }

    private void CheckTilePool()
    {
        tilePool = GameObject.FindGameObjectWithTag(TagName.TAG_TILE_POOL).GetComponent<TilePool>();

        if (tilePool == null)
        {
            Debug.LogError("TilePool not found in the scene. Please ensure it is present.");
            return;
        }

        if (!_isReady && tilePool.Pool.PoolSize == tilePool.transform.childCount)
        {
            _isReady = true;
            return;
        }
    }
}
