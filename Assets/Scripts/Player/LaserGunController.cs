using UnityEngine;
using UnityEngine.Rendering;

public class LaserGunController : MonoBehaviour
{
    [Header("3D Objects")]
    [SerializeField] public GameObject laserGunObject;
    [SerializeField] public GameObject laserProjectile;
    [SerializeField] public Transform laserProjectileStartPosition;

    [Header("Ammunation")]
    [SerializeField] public float currentAmmo;
    [SerializeField] public float maxAmmo = 12;
    [SerializeField] public float reserveAmmo = 0f;

    [Header("Reload Settings")]
    [SerializeField] public float reloadDuration = 0.5f;
    [SerializeField] public AudioClip reloadSound;
    public bool isReloading = false;

    [Header("Gun Damage")]
    [SerializeField] public float damage = 20f;
    [SerializeField] public float range = 200f;
    [SerializeField] public float fireRate = 7f;
    [SerializeField] public float recoilForce = 0.1f;
    [SerializeField] public float recoilDuration = 0.1f;

    [Header("Visual Effects")]
    [SerializeField] public GameObject muzzleFlash;
    [SerializeField] public float muzzleFlashDuration;
    [SerializeField] public Camera playerCamera;

    [Header("Delay Time")]
    [SerializeField] public float nextFireTime = 0.1f;
    [SerializeField] public float lastFireTime = 0f;
    public Vector3 initialWeaponPosition;

    [Header("Sound FX")]
    [SerializeField] public AudioSource audioSource;
    [SerializeField] public AudioClip gunShotSound;

    public GunController gunController;



    void Start()
    {
        currentAmmo = maxAmmo;
        gunController = GameObject.Find("GunController").GetComponent<GunController>();
    }

    void Update()
    {
        ActivingLaserGun();
    }

    public void Shoot()
    {
        if (isReloading || currentAmmo <= 0) return;
        else Debug.Log("AudioSource or gunShootSound not found");

        if (laserProjectile != null && laserProjectileStartPosition != null)
        {
            if (gunShotSound != null && audioSource != null) audioSource.PlayOneShot(gunShotSound);
            else Debug.Log("PlayerCamera not found");

            if (playerCamera != null)
            {
                // if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit hit, range))
                // {
                //     Debug.Log("acertou");
                // }

                Instantiate(laserProjectile, laserProjectileStartPosition.position, laserProjectileStartPosition.rotation);
            }
            else Debug.Log("laserProjectile or laserProjectileStartPosition  not found");
        }
        else Debug.Log("PlayerCamera not found");
    }

    public void ActivingLaserGun()
    {
        if (gunController.isUsingLaserGun == false)
        {
            if (laserGunObject != null) laserGunObject.SetActive(false);
        }

        if (gunController.isUsingLaserGun == true)
        {
            if (laserGunObject != null) laserGunObject.SetActive(true);
        }
    }
}
