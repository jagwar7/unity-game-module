using UnityEngine;

public class RagdollController : MonoBehaviour
{
    [SerializeField] private Animator characterAnimator;
    [SerializeField] private float defaultImpulseMultiplier = 25f;

    private Health health;
    private Rigidbody[] ragdollRigidbodies;
    private Collider[] ragdollColliders;

    private void Awake()
    {
        if (characterAnimator == null)
            characterAnimator = GetComponent<Animator>();

        health = GetComponent<Health>();

        ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();
        ragdollColliders = GetComponentsInChildren<Collider>();

        SetRagdollActive(false);
    }

    private void OnEnable()
    {
        if (health != null)
        {
            health.OnDeathWithForce += HandleDeathWithForce;
        }
    }

    private void OnDisable()
    {
        if (health != null)
        {
            health.OnDeathWithForce -= HandleDeathWithForce;
        }
    }

    public void SetRagdollActive(bool active)
    {
        if (characterAnimator != null)
            characterAnimator.enabled = !active;

        foreach (var rb in ragdollRigidbodies)
        {
            if (rb.gameObject == this.gameObject) continue;
            rb.isKinematic = !active;
        }

        foreach (var col in ragdollColliders)
        {
            if (col.gameObject == this.gameObject) continue;
            col.enabled = true;
        }
    }

    private void HandleDeathWithForce(Vector3 hitDirection, Vector3 hitPoint)
    {
        SetRagdollActive(true);

        if (hitDirection != Vector3.zero)
        {
            Vector3 impulse = hitDirection.normalized * defaultImpulseMultiplier;
            foreach (var rb in ragdollRigidbodies)
            {
                if (rb.gameObject == this.gameObject) continue;
                rb.AddForceAtPosition(impulse, hitPoint, ForceMode.Impulse);
            }
        }
    }
}