using Assets.Scrips.Manager;
using UnityEngine;
using UnityEngine.UI;
public class ButtonLevel : MonoBehaviour
{
    [SerializeField] Sprite spriteStar;
    [SerializeField] Sprite[] spriteLock;

    private Level level;
    private void Awake()
    {
    }
    public void SetDefault()
    {
        foreach (Transform child in this.transform)
            child.gameObject.SetActive(Data.isUnlocked);

        if (Data.isUnlocked)
        {
            this.GetComponent<Button>().interactable = true;
            this.GetComponent<Image>().sprite = spriteLock[1];

            this.level = SaveManager.Instance.GetLevelFromDataLevelSaving(Data.levelName);

            if (this.level != null)
                for (int i = 0; i < this.level.starCount; i++)
                    this.transform.GetChild(0).GetChild(i).GetComponent<Image>().sprite = spriteStar;
        }
        else
        {
            this.GetComponent<Button>().interactable = false;
            this.GetComponent<Image>().sprite = spriteLock[0];
        }
    }
    public int StarCount { get => this.level.starCount; }
    public DataLevel Data { get; set; }
}
