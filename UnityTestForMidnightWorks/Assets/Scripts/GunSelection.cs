using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSelection : MonoBehaviour
{
    public float gunTrigger = 1f;
    private bool pistolEnabled;
    private bool ak47Enabled;
    private bool m249Enabled;
    public List<InventorySlot> slots;
    public GameObject inventory;
    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Inventory");
        for (int i = 0; i < inventory.transform.childCount; i++)
        {
            if (inventory.transform.GetChild(i).GetComponent<InventorySlot>() != null)
            {
                slots.Add(inventory.transform.GetChild(i).GetComponent<InventorySlot>());
            }
        }
    }
    void Update()
    {
        EnableWeapons();
        Transform hand = GameObject.Find("Hand").transform;
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (gunTrigger >= 4f)
        {
            gunTrigger = 1f;
        }
        if (gunTrigger <= 0f)
        {
            gunTrigger = 3f;
        }
        if (scrollInput > 0)
        {
            gunTrigger += 0.5f;
        }
        if (scrollInput < 0)
        {
            gunTrigger -= 0.5f;
        }

        if (gunTrigger >= 1 && gunTrigger < 2 && pistolEnabled)
        {
            hand.GetChild(2).gameObject.GetComponent<GunController>().ammo = 0;
            hand.GetChild(2).gameObject.SetActive(false);
            hand.GetChild(1).gameObject.SetActive(false);
            if (hand.GetChild(0).gameObject.activeSelf == false)
            {
                hand.GetChild(0).gameObject.SetActive(true);
                hand.GetChild(0).gameObject.GetComponent<GunController>().Pickup();
            }
        }
        if (gunTrigger >= 2 && gunTrigger < 3 && ak47Enabled)
        {
            hand.GetChild(0).gameObject.GetComponent<GunController>().ammo = 0;
            hand.GetChild(0).gameObject.SetActive(false);
            hand.GetChild(2).gameObject.SetActive(false);
            if (hand.GetChild(1).gameObject.activeSelf == false)
            {
                hand.GetChild(1).gameObject.SetActive(true);
                hand.GetChild(1).gameObject.GetComponent<GunController>().Pickup();
            }
        }
        if (gunTrigger >= 3 && gunTrigger < 4 && m249Enabled)
        {
            hand.GetChild(1).gameObject.GetComponent<GunController>().ammo = 0;
            hand.GetChild(1).gameObject.SetActive(false);
            hand.GetChild(0).gameObject.SetActive(false);
            if (hand.GetChild(2).gameObject.activeSelf == false)
            {
                hand.GetChild(2).gameObject.SetActive(true);
                hand.GetChild(2).gameObject.GetComponent<GunController>().Pickup();
            }
        }
    
    }

    void EnableWeapons()
    {
        foreach (InventorySlot slot in slots)
        {
            if (slot.item != null)
            {
                if (slot.item.itemName == "M1911")
                {
                    pistolEnabled = true;
                }
                if (slot.item.itemName == "AK47")
                {
                    ak47Enabled = true;
                }
                if (slot.item.itemName == "M249")
                {
                    m249Enabled = true;
                }
            }
        }
    }


}
