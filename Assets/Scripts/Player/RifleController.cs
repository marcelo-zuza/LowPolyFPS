using System.Collections;
using UnityEngine;


public class RifleController : MonoBehaviour
{
    [Header("Ammunation")]
    [SerializeField] public bool usingRifle = true;
    [SerializeField] public float currentAmmo;
    [SerializeField] public float maxAmmo = 32f;
    [SerializeField] public float reserveAmmo = 0f;

    [Header("Realod Settings")]
    [SerializeField] public float reloadDuration = 1.5f;
    [SerializeField] public AudioClip reloadSound;
    private bool isReloading = false;

    [Header("Gun Damage")]
    [SerializeField] public float damage = 10f;
    [SerializeField] public float range = 100f;
    [SerializeField] public float fireRate = 15f;
    [SerializeField] public float recoilForce = 0.1f;
    [SerializeField] public float recoilDuration = 0.1f;

    [Header("Visual Effects")]
    [SerializeField] public GameObject muzzleFlash;
    [SerializeField] public float muzzleFlashDuration = 0.1f;
    [SerializeField] public Camera playerCamera;

    [Header("Delay Time")]
    [SerializeField] public float nextFireTime = 0f;
    public Vector3 initialWeaponPosition;

    [Header("Sound FX")]
    [SerializeField] public AudioClip gunShotSound;
    [SerializeField] public AudioSource audioSource;

    public GunController gunController;


    void Start()
    {
        currentAmmo = maxAmmo;
        gunController = GameObject.Find("GunController").GetComponent<GunController>();
    }

    void Update()
    {
        if (usingRifle && currentAmmo > 0)
        {
            if (Input.GetButton("Fire1") && Time.time > nextFireTime)
            {
                nextFireTime = Time.time + 1f / fireRate;
                Shoot();
            }
        }
    }

    void Shoot()
    {
        if (isReloading || currentAmmo <= 0) return;
        StartCoroutine(gunController.MuzzleFlashEffect(muzzleFlash, muzzleFlashDuration));
        StartCoroutine(gunController.RecoilEffect(recoilDuration, recoilForce));

        if (gunShotSound != null && audioSource != null) audioSource.PlayOneShot(gunShotSound);
        else Debug.Log("gunShotSound or audioSource not assigned");

        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit hit, range))
        {
            Debug.Log("Acertou");
            //EnemyController enemy = hit.transform.GetComponent<EnemyController>();
            // if (enemy != null)
            // {
            //     enemy.TakeDamage(damage);;
            // }
            currentAmmo--;

        }
    }




}
