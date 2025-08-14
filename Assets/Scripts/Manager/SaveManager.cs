using Assets.Scrips.Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class SaveManager : Singleton<SaveManager>
{
    private int _countLevel;
    private string _jsonDataLevel;
    private List<DataLevel> lsDatalevel;

    private DataSetting dataSetting;
    private DataCollection dataCollection;
    private DataLevelSaving dataLevelSaving;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        StartCoroutine(nameof(SetLocalizationSettings));
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    private IEnumerator SetLocalizationSettings()
    {
        yield return LocalizationSettings.InitializationOperation;
    }

    private DataSetting DataSettingDefault()
    {
        return new DataSetting(.5f, .5f, EnumLanguages.English);
    }

    private DataLevelSaving DataLevelSavingDefault()
    {
        lsDatalevel = LevelManager.Instance.GetListDataLevel;

        _countLevel = lsDatalevel.Count;

        if (_countLevel == 0)
        {
            Debug.LogError("List Level empty!");
            return null;
        }

        dataLevelSaving = new DataLevelSaving();
        dataLevelSaving.levels = new List<Level>();

        PlayerPrefs.SetString(KeyData.DATA_LEVEL, JsonUtility.ToJson(dataLevelSaving, true));
        _jsonDataLevel = PlayerPrefs.GetString(KeyData.DATA_LEVEL);

        dataLevelSaving = JsonUtility.FromJson<DataLevelSaving>(_jsonDataLevel);

        dataLevelSaving.levels.Add(new Level(0, "Level 1"));

        this.SaveAllDataLevel(dataLevelSaving);

        return dataLevelSaving;
    }

    public void DeleteKeyData(string key)
    {
        PlayerPrefs.DeleteKey(key);
        PlayerPrefs.Save();
    }

    public void DeleteAllKeyData()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }

    public void ChangeLanguage()
    {
        if (this.GetDataSetting() != null)
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[(int)this.GetDataSetting().enumLanguages];
            return;
        }

        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[0];
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

    public void SaveDataLevel(Level level)
    {
        foreach (var item in dataLevelSaving.levels)
        {
            if (item.levelName == level.levelName)
            {
                dataLevelSaving.levels.Remove(item);
                break;
            }
        }

        dataLevelSaving.levels.Add(level);
        this.SaveAllDataLevel(dataLevelSaving);
    }

    public void SaveAllDataLevel(DataLevelSaving dataLevelSaving)
    {
        PlayerPrefs.SetString(KeyData.DATA_LEVEL, JsonUtility.ToJson(dataLevelSaving, true));
        PlayerPrefs.Save();
    }

    public void SaveDataCollection(string name)
    {
        if (this.GetDataCollection().name.Contains(name))
            return;

        this.GetDataCollection().name.Add(name);

        PlayerPrefs.SetString(KeyData.DATA_COLLECTION, JsonUtility.ToJson(dataCollection, true));
        PlayerPrefs.Save();

        GameManager.Instance.IsCollectNewTile = true;
    }

    public DataCollection GetDataCollection()
    {
        dataCollection = JsonUtility.FromJson<DataCollection>(PlayerPrefs.GetString(KeyData.DATA_COLLECTION));
        if (dataCollection == null)
        {
            dataCollection = new DataCollection();
            dataCollection.name = new List<string>();
        }
        return dataCollection;
    }

    public DataSetting GetDataSetting()
    {
        dataSetting = JsonUtility.FromJson<DataSetting>(PlayerPrefs.GetString(KeyData.DATA_SETTING));

        dataSetting ??= this.DataSettingDefault();

        return dataSetting;
    }

    public DataLevelSaving GetDataLevelSaving()
    {
        _jsonDataLevel = PlayerPrefs.GetString(KeyData.DATA_LEVEL);

        if (_jsonDataLevel == string.Empty)
        {
            Debug.Log("DataSavingLevel was empty!");
            dataLevelSaving = this.DataLevelSavingDefault();

            this.GetDataLevelSaving();

            return dataLevelSaving;
        }

        dataLevelSaving = JsonUtility.FromJson<DataLevelSaving>(PlayerPrefs.GetString(KeyData.DATA_LEVEL));

        return dataLevelSaving;
    }

    public Level GetLevelFromDataLevelSaving(string levelName)
    {
        if (LevelManager.Instance.HashSetLevelUnLocked.Contains(levelName))
        {
            foreach (var item in this.GetDataLevelSaving().levels)
                if (item.levelName == levelName)
                    return item;
        }
        return null;
    }
}
