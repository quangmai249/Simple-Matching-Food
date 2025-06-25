using Assets.Scrips.Manager;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PanelMainMenu : MonoBehaviour
{
    [SerializeField] Button _btnList;
    [SerializeField] Button _btnStart;
    [SerializeField] Button _btnInformation;

    [SerializeField] GameObject _panelMainMenu;

    private void Awake()
    {
        _btnStart.onClick.AddListener(StartGame);
        _btnList.onClick.AddListener(ListMainMenu);
        _btnInformation.onClick.AddListener(Information);
    }

    private void Start()
    {
        _panelMainMenu.SetActive(false);
    }

    private void Information()
    {
        UIManager.instance.ShowPanel(EnumPanelType.Information);
    }

    private void ListMainMenu()
    {
        _panelMainMenu.SetActive(!_panelMainMenu.activeSelf);

        if (_panelMainMenu.activeSelf)
            _panelMainMenu.GetComponent<Animator>().Play(KeyAnim.KEY_TRANSLATE_PANEL_LIST);
    }

    private void StartGame()
    {
        SceneManager.LoadScene(SceneName.SCENE_GAMEPLAY);
    }
}
