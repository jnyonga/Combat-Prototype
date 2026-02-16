using UnityEngine;

public class CombatController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private MouseLook mouseLook;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform punchPoint;
    [SerializeField] private Adrenaline adrenalineSystem;

    [Header("Punch Settings")]
    [SerializeField] private float punchDamage = 10f;
    [SerializeField] private float punchRange = 2f;
    [SerializeField] private float punchCooldown = 0.5f;
    [SerializeField] private LayerMask enemyLayer;

    [Header("Adrenaline Settings")]
    [SerializeField] private float adrenalinePerHit = 5f;
    [SerializeField] private float fightBarPerHit = 10f;

    private bool isBlocking = false;
    private float lastPunchTime = 0f;

    public void OnBlockPressed()
    {
        isBlocking = true;
        mouseLook.SetBlocking(true);

        if (animator != null)
        {
            animator.SetBool("IsBlocking", true);
        }

        Debug.Log("Block started");
    }

    public void OnBlockReleased()
    {
        isBlocking = false;
        mouseLook.SetBlocking(false);
        mouseLook.SetFreeLook(false);

        if (animator != null)
        {
            animator.SetBool("IsBlocking", false);
        }

        Debug.Log("Block released");
    }

    public void OnAttackPressed()
    {
        if (isBlocking)
            return;

        if (Time.time - lastPunchTime < punchCooldown)
            return;

        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }

        PerformPunch();

        lastPunchTime = Time.time;
        Debug.Log("Punch!");
    }
    public void OnFreeLookPressed()
    {
        if (isBlocking)
        {
            mouseLook.SetFreeLook(true);
            Debug.Log("Free look enabled");
        }
    }

    public void OnFreeLookReleased()
    {
        mouseLook.SetFreeLook(false);
        Debug.Log("Free look disabled");
    }

    private void PerformPunch()
    {
        // Raycast forward from punch point to detect enemies
        RaycastHit hit;
        Vector3 punchOrigin = punchPoint != null ? punchPoint.position : transform.position;
        Vector3 punchDirection = punchPoint != null ? punchPoint.forward : transform.forward;

        if (Physics.Raycast(punchOrigin, punchDirection, out hit, punchRange, enemyLayer))
        {
            Debug.Log($"Hit: {hit.collider.name}");

            // Try to damage the enemy
            // You can replace this with your enemy health system
            // Example: hit.collider.GetComponent<EnemyHealth>()?.TakeDamage(punchDamage);
            RewardHit();

            Debug.Log($"Dealt {punchDamage} damage to {hit.collider.name}");
        }
        else
        {
            Debug.Log("Punch missed!");
        }

        // Visual debug - shows punch range in Scene view
        Debug.DrawRay(punchOrigin, punchDirection * punchRange, Color.red, 0.5f);
    }

    private void RewardHit()
    {
        if(adrenalineSystem == null)
        {
            return;
        }

        adrenalineSystem.currentAdrenaline += adrenalinePerHit;
        if(adrenalineSystem.currentAdrenaline > adrenalineSystem.maxAdrenaline)
        {
            adrenalineSystem.currentAdrenaline = adrenalineSystem.maxAdrenaline;
        }

        if(!adrenalineSystem.isFightActive && !adrenalineSystem.isBurnoutActive)
        {
            adrenalineSystem.currentFight += fightBarPerHit;
            if(adrenalineSystem.currentFight > adrenalineSystem.maxFight)
            {
                adrenalineSystem.currentFight = adrenalineSystem.maxFight;
            }
        }

        Debug.Log($"+{adrenalinePerHit} Adrenaline, +{fightBarPerHit} Fight Bar");
    }
}
