using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType {Weapon, PistolAmmo, AK47Ammo, M249Ammo}
[CreateAssetMenu(fileName = "Item", menuName = "Invertory/Items/New Item")]
public class ItemScriptableObject : ScriptableObject
{
    public ItemType itemType;
    public string itemName;
    public int amount;
    public GameObject itemPrefab;
    public Sprite icon;

}
