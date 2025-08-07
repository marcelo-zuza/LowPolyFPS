using System.Collections;
using UnityEngine;


public class RifleController : MonoBehaviour
{
    [Header("Ammunation")]
    [SerializeField] public GameObject rifleObject;
    [SerializeField] public float currentAmmo;
    [SerializeField] public float maxAmmo = 32f;
    [SerializeField] public float reserveAmmo = 0f;

    [Header("Realod Settings")]
    [SerializeField] public float reloadDuration = 1.5f;
    [SerializeField] public AudioClip reloadSound;
    public bool isReloading = false;

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
        activingRifle();
        if (Input.GetKeyDown(KeyCode.R)) StartCoroutine(Reload());
    }

    public void Shoot()
    {
        if (isReloading || currentAmmo <= 0) return;


        if (gunShotSound != null && audioSource != null) audioSource.PlayOneShot(gunShotSound);
        else Debug.Log("gunShotSound or audioSource not assigned");

        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit hit, range))
        {
            Debug.Log("Acertou");
        }
        currentAmmo--;
    }

    public IEnumerator Reload()
    {
        if (reserveAmmo > 0 && currentAmmo < maxAmmo)
        {
            isReloading = true;
            Vector3 startPosition = transform.localPosition;
            Vector3 loweredPosition = startPosition + new Vector3(0, -0.2f, 0);

            float timer = 0f;
            if (reloadSound != null && audioSource != null) audioSource.PlayOneShot(reloadSound);
            else Debug.Log("AudioSource or RealoadSound not found");

            while (timer < reloadDuration)
            {
                transform.localPosition = Vector3.Lerp(startPosition, loweredPosition, timer / reloadDuration);
                timer += Time.deltaTime;
                yield return null;
            }

            if (reserveAmmo < maxAmmo)
            {
                currentAmmo = reserveAmmo;
                reserveAmmo = 0;
            }
            else
            {
                reserveAmmo -= (maxAmmo - currentAmmo);
                currentAmmo += (maxAmmo - currentAmmo);

            }
            transform.localPosition = startPosition;
            isReloading = false;
        }


    }

    public void activingRifle()
    {
        if (gunController.isUsingRifle == false)
        {
            if (rifleObject != null) rifleObject.SetActive(false);

        }
        if (gunController.isUsingRifle == true)
        {
            if (rifleObject != null) rifleObject.gameObject.SetActive(true);
        }
    }




}
