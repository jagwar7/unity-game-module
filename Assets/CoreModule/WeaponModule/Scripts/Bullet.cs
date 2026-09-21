using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _damage;
    private float _speed;
    private Vector3 _direction;
    private bool hasHit;
    private Collider bulletCollider;

    private void Awake()
    {
        bulletCollider = GetComponent<Collider>();
    }

    public void Initialize(float damage, float speed, Vector3 direction)
    {
        _damage = damage;
        _speed = speed;
        _direction = direction;
        hasHit = false;
        transform.rotation = Quaternion.Euler(90, 0, 0);
        Destroy(gameObject, 3f);
    }

    private void Update()
    {
        transform.position += _direction * (_speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasHit) return;

        Health targetHealth = other.GetComponentInParent<Health>();
        
        if (targetHealth != null)
        {
            hasHit = true;

            if (bulletCollider != null) 
                bulletCollider.enabled = false;

            targetHealth.TakeDamage(_damage, _direction, transform.position);
        }

        Destroy(gameObject);
    }
}