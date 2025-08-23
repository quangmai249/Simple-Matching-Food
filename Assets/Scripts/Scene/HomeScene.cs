using Assets.Scrips.Manager;
using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeScene : MonoBehaviour
{
    [SerializeField] GameObject txtBG;
    [SerializeField] Image imgBG;
    [SerializeField] SpinBG spinBG;

    private void Awake()
    {
        this.SetDefault();
        this.AnimBegin();
    }

    private void OnDisable()
    {
        DOTween.Clear(true);
    }

    private void Start()
    {
        //Debug.Log("Sum of collection = " + SaveManager.Instance.GetDataCollection().name.Count);
        //Debug.Log("Sum of list collection = " + ImageManager.Instance.LsFrontImg.Count);
    }

    private void SetDefault()
    {
        txtBG.transform.localScale = Vector3.zero;
        imgBG.color = new Color(1, 1, 1, 0);
        imgBG.transform.localScale = Vector3.one * 10;
    }

    private void AnimBegin()
    {
        imgBG.DOFade(1f, 1f)
          .SetEase(Ease.InOutSine)
          .OnComplete(() =>
          {
              imgBG.transform.DOScale(new Vector3(2, 2, 2), 1.5f)
              .SetEase(Ease.InOutSine);

              imgBG.DOFade(0.1f, 1f)
              .SetEase(Ease.InSine);

              txtBG.transform.DOScale(Vector3.one, 1.5f)
                     .SetEase(Ease.OutSine)
                     .OnComplete(() =>
                     {
                         UIManager.instance.ShowPanel(EnumPanelType.MainMenu);
                     });
          });
    }

    private void AnimStartGame()
    {
        txtBG.transform.DOScale(Vector3.zero, 1.5f)
          .SetEase(Ease.InBack);

        imgBG.DOFade(1f, 1.5f)
        .SetEase(Ease.InOutSine)
        .OnComplete(() =>
        {
            imgBG.transform.DOScale(new Vector3(10, 10, 10), 1.5f)
            .SetEase(Ease.InOutSine)
            .OnComplete(() =>
            {
                imgBG.DOFade(0f, .5f)
                .SetEase(Ease.InSine)
                .OnComplete(() =>
                {
                    SceneManager.LoadScene(SceneName.SCENE_LEVEL);
                });
            });
        });
    }

    public void StartGame()
    {
        UIManager.instance.HiddenPanel(EnumPanelType.MainMenu);
        this.AnimStartGame();
    }
}
