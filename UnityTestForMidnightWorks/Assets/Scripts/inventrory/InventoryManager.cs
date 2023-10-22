using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryManager : MonoBehaviour
{
    public Transform inventoryPanel;
    public List<InventorySlot> slots = new List<InventorySlot>();
    public float reachDistance = 2f;
    private GameObject activeWeapon;
    private GameObject player;
    void Start()
    {       
        player = GameObject.FindGameObjectWithTag("Player");
        for (int i = 0; i < inventoryPanel.childCount; i++) 
        { 
            if (inventoryPanel.GetChild(i).GetComponent<InventorySlot>() != null)
            {
                slots.Add(inventoryPanel.GetChild(i).GetComponent<InventorySlot>());
            }
        }
    }

    void Update()
    {
        activeWeapon = GameObject.FindGameObjectWithTag("Weapon");
        Collider[] colliders = Physics.OverlapSphere(player.transform.position, reachDistance);
        foreach (Collider collider in colliders)
        {
            if (collider.GetComponent<Item>() != null)
            {
                AddItem(collider.gameObject.GetComponent<Item>().item);
                activeWeapon.transform.GetComponent<GunController>().Pickup();
                Destroy(collider.gameObject);
            }
        }
    }

    private void AddItem(ItemScriptableObject _item)
    {
        foreach (InventorySlot slot in slots)
        {
            if(slot.item == _item)
            {
                slot.amount += _item.amount;
                slot.itemAmount.text = slot.amount.ToString();
                return;
            }
        }

        foreach (InventorySlot slot in slots)
        {
            if (slot.isEmpty == true)
            {
                slot.item = _item;
                slot.amount = _item.amount;
                slot.isEmpty = false;
                slot.SetIcon(_item.icon);
                slot.itemAmount.text = _item.amount.ToString();
                if (slot.item.itemType == ItemType.Weapon)
                {
                    slot.itemAmount.text = slot.item.itemName;
                }
                break;
            }
        }
    }
}
