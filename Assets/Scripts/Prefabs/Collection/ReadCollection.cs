using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ReadCollection : MonoBehaviour
{
    [SerializeField] TextAsset textAsset;

    private string[] _arr;
    private Dictionary<string, List<string>> _dic = new Dictionary<string, List<string>>();

    private void Start()
    {
        _dic = this.GetContent();
        _arr = _dic[EnumLanguages.English.ToString()].ToArray();
    }

    private Dictionary<string, List<string>> GetContent()
    {
        Dictionary<string, List<string>> dicContent = new Dictionary<string, List<string>>();

        string[] lines = textAsset.text.Split('\n');
        string[] keys = lines[0].Split(',');

        for (int i = 0; i < keys.Length; i++)
            if (keys[i] != string.Empty)
                dicContent[keys[i].Trim()] = new List<string>();

        for (int i = 1; i < lines.Length; i++)
        {
            int count = 0;
            string[] values = lines[i].Split(',');

            foreach (string item in values)
            {
                if (item != string.Empty)
                {
                    dicContent[keys[count].Trim()].Add(item.Trim());
                    count++;
                }
            }
        }

        return dicContent;
    }

    public int GetIndex(string name)
    {
        int index = -1;
        foreach (string item in _arr)
        {
            if (item.Trim().ToLower().Equals(name.Trim().ToLower()))
            {
                index = Array.IndexOf(_arr, item);
                break;
            }
        }

        return index;
    }

    public string GetNameCollection(int index)
    {
        if (index < 0)
            return string.Empty;

        string language = SaveManager.Instance.GetDataSetting().enumLanguages.ToString();

        if (_dic[language].Count < index)
            return string.Empty;

        if (_dic[language][index] != null)
            return _dic[language][index];

        return string.Empty;
    }

    public string GetDescriptionCollection(int index)
    {
        if (index < 0)
            return string.Empty;

        string language = "Detail" + SaveManager.Instance.GetDataSetting().enumLanguages.ToString();

        if (_dic[language].Count < index)
            return string.Empty;

        if (_dic[language][index] != null)
            return _dic[language][index];

        return string.Empty;
    }
}
