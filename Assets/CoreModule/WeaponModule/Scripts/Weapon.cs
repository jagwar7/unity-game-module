using UnityEngine;
using UnityEngine.InputSystem;



public class Weapon : MonoBehaviour
{
    [Header("WEAPON CONFIGURATION")]
    public WeaponConfig config;
    public Transform firePoint;

    private float _nextFireTime;


    void Update(){
        
        if(Input.GetKeyDown(KeyCode.Space)){
            Vector3 target = firePoint.position + firePoint.forward * 20f;
            Fire(target);
        }
    }


    public void Fire(Vector3 targetPosition)
    {
        // IF WEAPON HAS NO CONFIG ---> NO ACTION
        if (config == null || Time.time < _nextFireTime) return;

        _nextFireTime = Time.time + config.fireRate;

        // GET TARGET DIRECTION
        Vector3 direction = (targetPosition - firePoint.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(direction);

        GameObject bulletObj = Instantiate(config.bulletPrefab, firePoint.position, rotation);
        
        if (bulletObj.TryGetComponent(out Bullet bullet))
        {
            bullet.Initialize(config.damage, config.bulletSpeed, direction);

            Quaternion flippedRotation = firePoint.rotation * Quaternion.Euler(0f, 180f, 0f);
            ParticleSystem muzzleFlash = Instantiate(config.fireParticle, firePoint.position, flippedRotation);
            Debug.Log("PARTICLE EFFECT NAME: " + muzzleFlash.gameObject.name);
            muzzleFlash.Play();
            Destroy(muzzleFlash.gameObject, muzzleFlash.main.duration + muzzleFlash.main.startLifetime.constantMax);
        }
    }
}