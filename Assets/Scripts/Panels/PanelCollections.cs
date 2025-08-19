using Assets.Scrips.Manager;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelCollections : MonoBehaviour
{
    [SerializeField] Button btnConfirm;
    [SerializeField] GameObject collections;
    [SerializeField] TextMeshProUGUI txtCount;

    private int _sumCollection, _countUnlocked;
    private GameObject _go;
    private GameObject[] _arr;

    private void Awake()
    {
        this.SetButtons();
    }

    private void Start()
    {
        _sumCollection = ImageManager.Instance.PoolImage.Pool.Count;
        _arr = new GameObject[_sumCollection];

        for (int i = 0; i < _sumCollection; i++)
        {
            _go = ImageManager.Instance.PoolImage.Pool.Dequeue();
            _go.transform.SetParent(collections.transform);
            _arr[i] = _go;
        }

        foreach (GameObject item in _arr)
        {
            this.SetImageCollection(item);
            item.gameObject.SetActive(true);
        }

        txtCount.text = $"{_countUnlocked}/{_sumCollection}";
    }

    private void OnDestroy()
    {
        foreach (GameObject item in _arr)
        {
            if (ImageManager.Instance.gameObject != null)
                ImageManager.Instance.PoolImage.ReturnToPool(item, ImageManager.Instance.gameObject);
        }
    }

    private void SetButtons()
    {
        btnConfirm.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayAudioClip(EnumAudioClip.ClickedButton);
            UIManager.instance.ShowPanel(EnumPanelType.MainMenu);
        });
    }

    private void SetImageCollection(GameObject go)
    {
        if (SaveManager.Instance.GetDataCollection().name.Contains(go.GetComponent<Image>().sprite.name))
        {
            _countUnlocked++;
            go.GetComponent<Collection>().enabled = true;
            go.GetComponent<Image>().color = new Color(1, 1, 1, 1);

            go.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            go.GetComponent<Collection>().enabled = false;
            go.GetComponent<Image>().color = new Color(0, 0, 0, 0.1f);

            go.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
