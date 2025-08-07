using UnityEngine;

public class LaserProjectileController : MonoBehaviour
{
    public float speed = 20f;
    public float damage = 30f;
    public float lifeTime = 5f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
