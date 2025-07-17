using System.Collections.Generic;

[System.Serializable]
public class DataSetting
{
    public float sfx;
    public float music;
    public EnumLanguages enumLanguages;
    public float Sfx { get => sfx; set => sfx = value; }
    public float Music { get => music; set => music = value; }
    public EnumLanguages EnumLanguages { get => enumLanguages; set => enumLanguages = value; }
    public DataSetting(float sfx, float music, EnumLanguages enumLanguages)
    {
        this.sfx = sfx;
        this.music = music;
        this.enumLanguages = enumLanguages;
    }
}