using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] AudioClip clipClickedButton;
    [SerializeField] AudioClip clipMatch;
    [SerializeField] AudioClip clipNotMatch;

    [SerializeField] AudioSource audioMusic;
    [SerializeField] AudioSource audioSFX;

    private float _sfx;
    private float _music;
    private DataSetting _dataSetting;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        _dataSetting = SaveManager.Instance.GetDataSetting();

        this.SetVolume(_dataSetting.sfx, _dataSetting.music);

        audioMusic.Play();
    }

    public void TileMatched()
    {
        audioSFX.PlayOneShot(clipMatch);
    }

    public void TileNotMatched()
    {
        audioSFX.PlayOneShot(clipNotMatch);
    }

    public void ClickedButton()
    {
        audioSFX.PlayOneShot(clipClickedButton);
    }

    public void SetVolume(float sfx, float music)
    {
        audioSFX.volume = sfx;
        audioMusic.volume = music;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
}
