using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SaveManager : Singleton<SaveManager>
{
    protected override void Awake()
    {
        base.Awake();
    }

    public DataSetting GetDataSetting()
    {
        DataSetting dataSetting = JsonUtility.FromJson<DataSetting>(PlayerPrefs.GetString(KeyData.DATA_SETTING));

        if (dataSetting == null)
            return new DataSetting(.5f, .5f, EnumLanguages.English);

        return dataSetting;
    }

    public void SaveDataSetting(float sfx, float music, string language)
    {
        if (Enum.TryParse(language, out EnumLanguages enumLanguages))
        {
            DataSetting dataSetting = new DataSetting(sfx, music, enumLanguages);
            string data = JsonUtility.ToJson(dataSetting);

            PlayerPrefs.SetString(KeyData.DATA_SETTING, data);
            PlayerPrefs.Save();
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
}
