using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class GunController : MonoBehaviour
{
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


    void Start()
    {
        initialPosition = transform.localPosition;
        initialWeaponPosition = transform.localPosition;
    }

    void Update()
    {
        ShakeGun();
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
    public IEnumerator MuzzleFlashEffect(GameObject muzzleFlash, float muzzleFlashDuration)
    {
        if (muzzleFlash == null)
        {
            Debug.Log("muzzleFlash no assigned");
            yield break;
        }

        if (firePoint == null)
        {
            Debug.Log("firePoint not assigned");
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
