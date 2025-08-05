using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    [Header("Weapon Switcher Manager")]
    [SerializeField] public List<GameObject> ownedWeapons = new List<GameObject>();
    public int currentWeaponIndex = 0;
    public GunController gunController;
    void Start()
    {
        gunController = GameObject.Find("GunController").GetComponent<GunController>();
    }

    void Update()
    {

    }

    void SelectWeapon(int index)
    {
        for (int i = 0; i < ownedWeapons.Count; i++)
        {
            ownedWeapons[i].SetActive(i == index);
            if (i == 0)
            {
                gunController.isUsingPistol = true;
                gunController.isUsingRifle = false;
            }
            else if (i == 1)
            {
                gunController.isUsingPistol = false;
                gunController.isUsingRifle = true;     
            }
        }
    }
}
