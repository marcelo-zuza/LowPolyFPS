using UnityEngine;

public class PistolController : MonoBehaviour
{
    [SerializeField] public GameObject pistolObject;
    [Header("Ammunation")]
    [SerializeField] public float currentAmmo;
    [SerializeField] public float maxAmmo = 12f;
    [SerializeField] public float reserveAmmo = 0f;

    [Header("Reload Settings")]
    [SerializeField] public float reloadDuration = 1.5f;
    [SerializeField] public AudioClip reloadSound;
    private bool isReloading = false;

    [Header("Gun Damage")]
    [SerializeField] public float damage = 5f;
    [SerializeField] public float range = 70f;
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
        ActivingPistol();
    }

    public void Shoot()
    {
        if (isReloading || currentAmmo <= 0) return;

        if (gunShotSound != null && audioSource != null) audioSource.PlayOneShot(gunShotSound);

        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit hit, range))
        {
            Debug.Log("Acetou");
        }
    }

    public void ActivingPistol()
    {
        if (gunController.isUsingPistol == false)
        {
            if (pistolObject != null) pistolObject.SetActive(false);
        }
        if (gunController.isUsingPistol == true)
        {
            if (pistolObject != null) pistolObject.SetActive(true);
        }
    }
}
