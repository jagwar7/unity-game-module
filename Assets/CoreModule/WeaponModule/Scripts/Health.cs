using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    public UnityEvent onDeath;

    private float _currentHealth;
    private bool _isDead;

    private void Awake()
    {
        _currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        if (_isDead) return;

        _currentHealth -= amount;
        Debug.Log("HIT BY PLAYER, HEALTH =  " + _currentHealth);
        if (_currentHealth <= 0f)
        {
            _currentHealth = 0f;
            _isDead = true;
            Debug.Log("EENEMY DIED FUCK!");
            onDeath?.Invoke();
        }
    }
}