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
        if (ownedWeapons.Count < 1) return;

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            currentWeaponIndex = (currentWeaponIndex + 1) % ownedWeapons.Count;
            SelectWeapon(currentWeaponIndex);
        }
    }

    public void SelectWeapon(int index)
    {
        for (int i = 0; i < ownedWeapons.Count; i++)
        {
            ownedWeapons[i].SetActive(i == index);
        }

        switch (index)
        {
            case 0:
                gunController.isUsingPistol = true;
                gunController.isUsingRifle = false;
                break;
            case 1:
                gunController.isUsingPistol = false;
                gunController.isUsingRifle = true;
                break;
            default:
                gunController.isUsingPistol = false;
                gunController.isUsingRifle = false;
                break;
        }
    }

    public void AddWeapon(GameObject newWeapon)
    {
        if (!ownedWeapons.Contains(newWeapon))
        {
            ownedWeapons.Add(newWeapon);
            newWeapon.gameObject.SetActive(false);
        }
    }
}
