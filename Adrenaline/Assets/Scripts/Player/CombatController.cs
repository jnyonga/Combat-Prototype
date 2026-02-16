using UnityEngine;

public class CombatController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private MouseLook mouseLook;
    [SerializeField] private Animator animator;
    private bool isBlocking = false;

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

        if (animator != null)
        {
            animator.SetBool("IsBlocking", false);
        }

        Debug.Log("Block released");
    }

    public void OnAttackPressed()
    {
        // Can't attack while blocking
        if (isBlocking)
            return;

        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }

        Debug.Log("Attack!");
    }

}
