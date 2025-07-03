using Assets.Scrips.Manager;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeScene : MonoBehaviour
{
    [SerializeField] GameObject txtBG;
    [SerializeField] Image imgBG;
    [SerializeField] SpinBG spinBG;

    private float _valScaleDF;

    private void Start()
    {
        this.SetDefault();
    }

    public void StartGame()
    {
        UIManager.instance.HiddenPanel(EnumPanelType.MainMenu);

        txtBG.transform.DOScale(Vector3.zero, 1.5f)
            .SetEase(Ease.InOutBack);

        imgBG.DOFade(1f, 1.5f)
        .SetEase(Ease.InOutSine)
        .OnComplete(() =>
        {
            imgBG.transform.DOScale(Vector3.one * _valScaleDF, 1.5f)
            .SetEase(Ease.InOutSine)
            .OnComplete(() =>
                {
                    imgBG.DOFade(0f, 1.5f)
                    .SetEase(Ease.InOutSine)
                    .OnComplete(() =>
                    {
                        SceneManager.LoadScene(SceneName.SCENE_GAMEPLAY);
                    });
                });
        });
    }

    private void SetDefault()
    {
        _valScaleDF = spinBG.MaxValScaleDf;
        UIManager.instance.ShowPanel(EnumPanelType.MainMenu);
    }

    private void OnDisable()
    {
        DOTween.Clear(true);
    }

    private void OnDestroy()
    {
    }
}
