using UnityEngine;
using UnityEngine.UI;

public class PanelConfirm : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject textUse;
    [SerializeField] private GameObject textRemove;

    [SerializeField] private Image iconItem;

    private void Start()
    {
        panel.SetActive(false);
    }

    public void PanelConfirmUse(ItemData item)
    {
        panel.SetActive(true);
        textUse.SetActive(true);
        textRemove.SetActive(false);
        iconItem.sprite = item.icon;
    }

    public void PanelConfirmRemove(ItemData item)
    {
        panel.SetActive(true);
        textUse.SetActive(false);
        textRemove.SetActive(true);
        iconItem.sprite = item.icon;
    }



}
