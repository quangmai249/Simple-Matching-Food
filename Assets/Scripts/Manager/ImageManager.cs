using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.UI;

public class ImageManager : Singleton<ImageManager>
{
    [SerializeField] GameObject imagePrefab;

    [SerializeField] List<Sprite> lsBackImg = new List<Sprite>();
    [SerializeField] List<Sprite> lsFrontImg1 = new List<Sprite>();
    [SerializeField] List<Sprite> lsFrontImg2 = new List<Sprite>();

    private Sprite _imgDetailCollection;

    private List<Sprite> lsFrontImg;
    private ObjectPool poolImage;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    private void Start()
    {
        lsFrontImg = lsFrontImg1;
        this.SpawnImageCollections(lsFrontImg1);
    }

    private void SpawnImageCollections(List<Sprite> ls)
    {
        int count = 0;

        poolImage = new ObjectPool(imagePrefab, this.transform, ls.Count);

        foreach (var item in poolImage.Pool)
        {
            item.GetComponent<Image>().sprite = ls[count];
            count++;
        }
    }

    public Sprite ImgDetailCollection
    {
        get => _imgDetailCollection;
        set => _imgDetailCollection = value;
    }

    public ObjectPool PoolImage
    {
        get => poolImage;
    }

    public List<Sprite> LsBackImg
    {
        get => lsBackImg;
    }

    public List<Sprite> LsFrontImg
    {
        get => lsFrontImg;
        set => lsFrontImg = value;
    }

    public List<Sprite> LsFrontImg1
    {
        get => lsFrontImg1;
    }

    public List<Sprite> LsFrontImg2
    {
        get => lsFrontImg2;
    }
}
