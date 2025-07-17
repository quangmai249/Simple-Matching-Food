using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] List<AudioClip> clipBackgrounds;
    [SerializeField] List<AudioClip> clipSFX;

    [SerializeField] AudioSource audioMusic;
    [SerializeField] AudioSource audioSFX;

    private float _sfx;
    private float _music;
    private DataSetting _dataSetting;
    private Dictionary<EnumAudioClip, AudioClip> dic = new Dictionary<EnumAudioClip, AudioClip>();
    protected override void Awake()
    {
        base.Awake();
        this.Register();
    }
    private void Start()
    {
        _dataSetting = SaveManager.Instance.GetDataSetting();
        this.SetVolume(_dataSetting.sfx, _dataSetting.music);
    }
    private void Update()
    {
        if (!audioMusic.isPlaying)
        {
            audioMusic.clip = clipBackgrounds[UnityEngine.Random.Range(0, clipBackgrounds.Count)];
            audioMusic.Play();
        }
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
    private void Register()
    {
        foreach (AudioClip item in clipSFX)
        {
            if (Enum.TryParse(item.name, out EnumAudioClip enumAudioClip))
                dic[enumAudioClip] = item;
        }
    }
    public void StopSFX()
    {
        audioSFX.Stop();
    }
    public void SetVolume(float sfx, float music)
    {
        audioSFX.volume = sfx;
        audioMusic.volume = music;
    }
    public void PlayAudioClip(EnumAudioClip enumAudioClip)
    {
        if (dic.TryGetValue(enumAudioClip, out AudioClip audioClip))
            audioSFX.PlayOneShot(audioClip);
        else
            Debug.LogWarning($"Audio clip {enumAudioClip} not found in dictionary.");
    }
}
