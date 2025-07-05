using Assets.Scrips.Manager;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameplayScene : MonoBehaviour
{
    [SerializeField] Image imgBG;
    [SerializeField] Image loading;

    private TileSpawner tileSpawner;

    private void Awake()
    {
        SaveManager.Instance.ChangeLanguage();
    }

    private IEnumerator Start()
    {
        tileSpawner = GameObject.FindGameObjectWithTag(TagName.TAG_TILE_SPAWNER).GetComponent<TileSpawner>();

        UIManager.instance.ShowPanel(EnumPanelType.Loading);

        yield return new WaitForSeconds(2f);

        UIManager.instance.ShowPanel(EnumPanelType.HUD);

        imgBG.DOFade(1f, 1f)
             .SetEase(Ease.InOutSine)
             .OnComplete(() =>
             {
                 imgBG.transform.DOScale(new Vector3(2, 2, 2), 1.5f)
                 .SetEase(Ease.InOutSine);

                 imgBG.DOFade(0.1f, 1f)
                 .SetEase(Ease.InSine);
             });

        tileSpawner.StartCoroutine(nameof(tileSpawner.SetDefault), 2f);
    }
}
