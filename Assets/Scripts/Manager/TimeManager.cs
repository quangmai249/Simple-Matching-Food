using Assets.Scrips.Manager;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    [SerializeField] float _timeLimit;

    [SerializeField] Slider sliderTimeLimit;
    [SerializeField] TextMeshProUGUI txtTimeLimit;

    private TileSpawner tileSpawner;
    private Gameplay gameplay;
    private void Start()
    {
        tileSpawner = GameObject.FindGameObjectWithTag(TagName.TAG_TILE_SPAWNER).GetComponent<TileSpawner>();
        gameplay = GameObject.FindGameObjectWithTag(TagName.TAG_GAMEPLAY).GetComponent<Gameplay>();
    }
    private void OnEnable()
    {
        GameEvents.OnTimeLimitChange.Register(OnEnableTimeLimit);
    }
    private void OnDestroy()
    {
        GameEvents.OnLevelChange.Clear();
    }
    private void OnEnableTimeLimit(float timeLimit)
    {
        if (timeLimit > 0)
        {
            int minutes = Mathf.FloorToInt(timeLimit / 60f);
            int remainingSeconds = Mathf.FloorToInt(timeLimit % 60f);

            string formattedTime = string.Format("{0:00}:{1:00}", minutes, remainingSeconds);

            txtTimeLimit.text = formattedTime;

            sliderTimeLimit.value = timeLimit;
        }
        else
        {
            txtTimeLimit.text = "0:00";
            sliderTimeLimit.value = 0f;
        }
    }
    public void SetTimeLimit()
    {
        _timeLimit = LevelManager.Instance.DataLevel.timeLimit;
        sliderTimeLimit.maxValue = _timeLimit;

        GameEvents.OnTimeLimitChange.Raise(_timeLimit);
    }
    public IEnumerator CoroutineRunTime(float timeStart)
    {
        yield return new WaitForSeconds(timeStart);

        AudioManager.Instance.PlayAudioClip(EnumAudioClip.Start);

        GameManager.Instance.RestartGame(0);

        gameplay.IsStarted = true;

        while (gameplay.IsStarted && _timeLimit > 0)
        {
            yield return new WaitForSeconds(0.1f);

            _timeLimit -= 0.1f;

            GameEvents.OnTimeLimitChange.Raise(_timeLimit);

            if (_timeLimit <= 0)
            {
                gameplay.IsLoseGame = true;
                gameplay.IsStarted = false;

                AudioManager.Instance.PlayAudioClip(EnumAudioClip.Fail);
                UIManager.instance.ShowPanel(EnumPanelType.LevelLose);
            }
        }
    }
    public float TimeLimit
    {
        get => _timeLimit;
    }
    public float MaxTimeLimit
    {
        get => LevelManager.Instance.DataLevel.timeLimit;
    }
}
