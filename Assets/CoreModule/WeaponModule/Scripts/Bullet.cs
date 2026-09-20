using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _damage;
    private float _speed;
    private Vector3 _direction;

    public void Initialize(float damage, float speed, Vector3 direction)
    {
        _damage = damage;
        _speed = speed;
        _direction = direction;
        transform.rotation = Quaternion.Euler(90, 0, 0); ///
        Destroy(gameObject, 3f); // DESTROY
    }

    void Update()
    {
        transform.position += _direction * (_speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("COLLIDED WITH ENEMY");
        Health targetHealth = other.GetComponent<Health>();
        
        if(targetHealth != null)
        {
            targetHealth.TakeDamage(_damage);
        }

        // DESTROY BULLET
        Destroy(gameObject);
    }
}