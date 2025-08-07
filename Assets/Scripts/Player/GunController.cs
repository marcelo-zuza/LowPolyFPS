using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GunController : MonoBehaviour
{
    [Header("Current Gun Configurantion")]
    [SerializeField] public bool isUsingRifle = false;
    [SerializeField] public bool isUsingPistol = false;
    [SerializeField] public bool isUsingLaserGun = false;

    [Header("Realod Settings")]
    [SerializeField] public float reloadDuration = 1.5f;
    [SerializeField] public AudioClip reloadSound;
    private bool isReloading = false;

    [Header("Gun Shake Configuration")]
    [SerializeField] public float swayAmountX = 0.05f;
    [SerializeField] public float swayAmountY = 0.05f;
    [SerializeField] public float swaySpeed = 5f;
    [SerializeField] public float returnSmoothness = 10f;
    [SerializeField] public Transform playerTransform;

    [Header("Gun configuration")]
    [SerializeField] public Transform firePoint;
    public Vector3 initialPosition;
    public Vector3 initialWeaponPosition;
    public float timerX;
    public float timerY;

    [Header("Gun Manager")]
    public RifleController rifleController;
    public PistolController pistolController;
    public LaserGunController laserGunController;
    public WeaponSwitcher weaponSwitcher;


    void Start()
    {
        rifleController = GameObject.Find("Rifle").GetComponent<RifleController>();
        pistolController = GameObject.Find("Pistol").GetComponent<PistolController>();
        laserGunController = GameObject.Find("LaserGun").GetComponent<LaserGunController>();
        weaponSwitcher = GameObject.Find("WeaponSwitcher").GetComponent<WeaponSwitcher>();

        initialPosition = transform.localPosition;
        initialWeaponPosition = transform.localPosition;
    }

    void Update()
    {
        ShakeGun();
        // Pistol Shoot
        if (pistolController != null)
        {
            if (isUsingPistol && !isReloading)
            {
                if (Input.GetButtonDown("Fire1") && pistolController.currentAmmo > 0 && Time.time - pistolController.lastFireTime >= 1f / pistolController.fireRate)
                {
                    pistolController.Shoot();
                    pistolController.lastFireTime = Time.time;
                    StartCoroutine(MuzzleFlashEffect(pistolController.muzzleFlash, pistolController.muzzleFlashDuration));
                    StartCoroutine(RecoilEffect(pistolController.recoilDuration, pistolController.recoilDuration));

                }
            }
        }
        else Debug.Log("PistolController not found");

        // Rifle Shoot
        if (rifleController != null)
        {
            if (isUsingRifle && !rifleController.isReloading)
            {
                if (Input.GetButton("Fire1") && rifleController.currentAmmo > 0 && Time.time - rifleController.lastFireTime >= 1f / rifleController.fireRate)
                {
                    rifleController.Shoot();
                    rifleController.lastFireTime = Time.time;
                    StartCoroutine(MuzzleFlashEffect(rifleController.muzzleFlash, rifleController.muzzleFlashDuration));
                    StartCoroutine(RecoilEffect(rifleController.recoilDuration, rifleController.recoilForce));
                }
            }
        }
        else Debug.Log("RifleController not found");

        // Laser Gun Shoot
        if (laserGunController != null)
        {
            if (isUsingLaserGun && !laserGunController.isReloading)
            {
                if (Input.GetButtonDown("Fire1") && laserGunController.currentAmmo > 0 && Time.time - laserGunController.lastFireTime >= 1f / laserGunController.fireRate)
                {
                    laserGunController.Shoot();
                    laserGunController.lastFireTime = Time.time;
                    StartCoroutine(MuzzleFlashEffect(laserGunController.muzzleFlash, laserGunController.muzzleFlashDuration));
                    StartCoroutine(RecoilEffect(laserGunController.recoilDuration, laserGunController.recoilForce));
                }
            }
        }

    }

    void ShakeGun()
    {
        bool isMoving = Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0;

        if (isMoving)
        {
            timerX += Time.deltaTime * swaySpeed;
            timerY += Time.deltaTime * swaySpeed;

            float swayOffsetX = Mathf.Sin(timerX) * swayAmountX;
            float swayOffsetY = Mathf.Cos(timerY) * swayAmountY;

            Vector3 targetPosition = initialPosition + new Vector3(swayOffsetX, swayOffsetY, 0);
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * returnSmoothness);
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, initialPosition, Time.deltaTime * returnSmoothness);
            timerX = 0;
            timerY = 0;
        }
    }
    
    IEnumerator MuzzleFlashEffect(GameObject muzzleFlash, float muzzleFlashDuration)
    {
        if (muzzleFlash == null)
        {
            Debug.Log("muzzleFlash no assigned");
            yield break;
        }

        muzzleFlash.gameObject.SetActive(true);
        yield return new WaitForSeconds(muzzleFlashDuration);
        muzzleFlash.gameObject.SetActive(false);
    }

    public IEnumerator RecoilEffect(float recoilDuration, float recoilForce)
    {
        float timer = 0f;
        Vector3 recoilPosition = initialWeaponPosition - transform.forward * recoilForce;

        while (timer < recoilDuration)
        {
            transform.localPosition = Vector3.Lerp(initialWeaponPosition, recoilPosition, timer / recoilDuration);
            timer += Time.deltaTime;
            yield return null;
        }

        timer = 0f;
        Vector3 currentPosition = transform.localPosition;
        while (timer < recoilDuration)
        {
            transform.localPosition = Vector3.Lerp(currentPosition, initialWeaponPosition, timer / recoilDuration);
            timer += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = initialWeaponPosition;
    }
}
