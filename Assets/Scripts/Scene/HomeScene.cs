using Assets.Scrips.Manager;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeScene : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI txtTimeStartGame;

    private float _time;

    private void Awake()
    {
    }

    private IEnumerator Start()
    {
        UIManager.instance.ShowPanel(EnumPanelType.MainMenu);

        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneName.SCENE_GAMEPLAY);
    }

    private void Update()
    {
        _time += Time.deltaTime;

        txtTimeStartGame.text = _time.ToString();
    }
}
