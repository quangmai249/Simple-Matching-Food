using Assets.Scrips.Manager;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayScene : MonoBehaviour
{
    [SerializeField] Image imgBG;

    private TileSpawner tileSpawner;

    private void Awake()
    {
        tileSpawner = GameObject.FindGameObjectWithTag(TagName.TAG_TILE_SPAWNER).GetComponent<TileSpawner>();
    }

    private IEnumerator Start()
    {
        yield return null;

        imgBG.DOFade(1f, 1.5f)
            .SetEase(Ease.InOutSine)
            .OnComplete(() =>
            {
                imgBG.transform.DOScale(Vector3.one * 1.75f, 1.5f)
                .SetEase(Ease.InOutSine)
                .OnComplete(() =>
                {
                    imgBG.DOFade(0.1f, 1.5f)
                    .SetEase(Ease.InOutSine);

                    UIManager.instance.ShowPanel(EnumPanelType.HUD);

                    tileSpawner.StartCoroutine(nameof(tileSpawner.SetDefault), 2f);
                });
            });
    }
}
