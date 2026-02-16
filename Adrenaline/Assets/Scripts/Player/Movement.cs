using UnityEngine;

public class Movement : MonoBehaviour
{
    Vector2 m_moveAmt;
    public void ReceiveInput(Vector2 moveInput)
    {
        m_moveAmt = moveInput;
    }
}
