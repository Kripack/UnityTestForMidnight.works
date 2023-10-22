using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class GunController : MonoBehaviour
{
    public float range = 100f;
    public float damage = 25f;
    public float impactForce = 30f;
    public float fireRate = 15f;
    public float recoilAmount;
    public float recoilHorizontal;
    public float recoilVertical;
    public bool singleShotMode;
    public int magazine;
    public int ammo;

    private float nextTimeToFire = 0f;
    private int magazineFull;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public Animator animator;
    public AudioSource audioSource;
    public AudioClip audioShot;
    public AudioClip audioBulletShells;
    public AudioClip audioReload;
    public GameObject inventory;
    public List<InventorySlot> slots;
    public ItemType requiredAmmoType;
    public Text magazineInfo;
    public Camera firstPersonCamera;
    public Camera thirdPersonCamera;
    public void Awake()
    {
        GetSlots();
    }
    private void Start()
    {
        fpsCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        magazineFull = magazine;
        animator = GetComponent<Animator>();
        inventory = GameObject.FindGameObjectWithTag("Inventory");

    }
    void Update()
    {
        if (firstPersonCamera.enabled == false)
        {
            fpsCam = thirdPersonCamera;
        }
        else if(firstPersonCamera.enabled == true) 
        {
            fpsCam = firstPersonCamera;
        }
        magazineInfo.text = magazine + "/" + magazineFull;
        if (Input.GetKey(KeyCode.Mouse0) && Time.time >= nextTimeToFire && !singleShotMode && magazine != 0 && animator.GetBool("isReload") == false)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            magazine -= 1;
            Shoot();

        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time >= nextTimeToFire && singleShotMode && magazine != 0 && animator.GetBool("isReload") == false)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            magazine -= 1;
            Shoot();
        }

        if ((magazine == 0 || Input.GetKeyDown(KeyCode.R)) && ammo != 0)
        {
            animator.SetBool("isReload", true);
        }
    }

    void Shoot()
    {
        muzzleFlash.Play();
        animator.SetTrigger("isShot");
        audioSource.PlayOneShot(audioBulletShells);
        audioSource.PlayOneShot(audioShot);
        RaycastHit hit;
        Vector3 recoil = Random.insideUnitSphere * recoilAmount;
        recoil.x *= recoilHorizontal;
        recoil.y *= recoilVertical;
        fpsCam.transform.localPosition += recoil;
        Vector3 shotDirection = fpsCam.transform.forward;
        shotDirection += Random.insideUnitSphere * recoilAmount;

        if (Physics.Raycast(fpsCam.transform.position, shotDirection, out hit, range))
        {
            Target target = hit.transform.GetComponent <Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }
    }

    void Reload()
    {
        animator.SetBool("isReload", false);
        foreach (InventorySlot slot in slots)
        {
            if (slot.item != null)
            {
                if (slot.item.itemType == requiredAmmoType)
                {
                    if (slot.amount > 0)
                    {
                        int ammoToAdd = magazineFull - magazine;

                        if (ammoToAdd > 0)
                        {
                            if (slot.amount <= ammoToAdd)
                            {
                                magazine += slot.amount;
                                ammo -= slot.amount;
                                slot.amount = 0;
                            }
                            else
                            {
                                magazine += ammoToAdd;
                                ammo -= ammoToAdd;
                                slot.amount -= ammoToAdd;
                            }
                        }
                        slot.itemAmount.text = slot.amount.ToString();
                        break;
                    }
                }
            }
        }
    }
    void ReloadSound()
    {
        audioSource.PlayOneShot(audioReload);
    }
    public void Pickup()
    {
        foreach (InventorySlot slot in slots)
        {
            if (slot.item != null)
            {
                if (slot.item.itemType == requiredAmmoType)
                {
                    ammo = slot.amount;
                }
            }
        }
    }
    public void GetSlots()
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

    
}
