using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelConfirm : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject textUse;
    [SerializeField] private GameObject textRemove;
    [SerializeField] private GameObject textBuy;
    [SerializeField] private GameObject buttonConfirm;

    [SerializeField] private Image iconItem;

    private void Start()
    {
        panel.SetActive(false);
    }

    public void PanelConfirmUse(ItemData item)
    {
        panel.SetActive(true);
        ResetGameObjects(textUse);
        iconItem.sprite = item.icon;
    }

    public void PanelConfirmRemove(ItemData item)
    {
        panel.SetActive(true);
        ResetGameObjects(textRemove);
        iconItem.sprite = item.icon;
    }

    public void PanelBuyItem(ItemData item)
    {
        panel.SetActive(true);
        ResetGameObjects(textBuy);
        iconItem.sprite = item.icon;
    }


    private void ResetGameObjects(GameObject gameObject1)
    {
        List<GameObject> temp = new()
        {
            textUse,
            textRemove,
            textBuy,
        };

        foreach (GameObject item in temp)
        {
            item.SetActive(false);
        }

        gameObject1.SetActive(true);
    }

    public bool PanelActive()
    {
        return textBuy.activeSelf;
    }

    public void ClosePanel()
    {
        panel.SetActive(false);
    }
}
