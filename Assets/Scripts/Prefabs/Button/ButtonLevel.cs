using UnityEngine;
using UnityEngine.UI;
public class ButtonLevel : MonoBehaviour
{
    [SerializeField] Sprite[] spriteLock;

    private void Awake()
    {
    }
    public void SetDefault()
    {
        if (Data.isUnlocked)
        {
            this.GetComponent<Button>().interactable = true;
            this.GetComponent<Image>().sprite = spriteLock[1];
        }
        else
        {
            this.GetComponent<Button>().interactable = false;
            this.GetComponent<Image>().sprite = spriteLock[0];

            foreach (Transform child in this.transform)
                child.gameObject.SetActive(false);
        }
    }
    public DataLevel Data { get; set; }
}
