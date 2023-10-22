using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public ItemScriptableObject item;
    public int amount;
    public bool isEmpty = true;
    public GameObject iconGO;
    public Text itemAmount;
    private Color defaultIconColor;

    private void Start()
    {
        iconGO = transform.GetChild(0).gameObject;
        defaultIconColor = iconGO.GetComponent<Image>().color;
        itemAmount = transform.GetChild(1).GetComponent<Text>();
    }

    private void Update()
    {
        if(amount <= 0)
        {
            itemAmount.text = string.Empty;
            iconGO.GetComponent<Image>().sprite = null;
            item = null;
            isEmpty = true;
            amount = 0;
            iconGO.GetComponent<Image>().color = defaultIconColor;
        }
    }

    public void SetIcon(Sprite icon)
    { 
        iconGO.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        iconGO.GetComponent<Image>().sprite = icon;
    }
}
