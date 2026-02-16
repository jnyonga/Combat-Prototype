using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] Movement movement;
    InputActionAsset InputActions;

    private InputAction m_moveAction;
    private InputAction m_lookAction;

    private Vector2 m_moveAmt;
    private Vector2 m_lookAmt;

    public float speed = 5;
    public float roateSpeed = 5;

    private void Awake()
    {
        InputActions = new InputActionAsset();

        m_moveAction = InputSystem.actions.FindAction("Move");
        m_moveAction = InputSystem.actions.FindAction("Look");
    }

    private void Update()
    {
        movement.ReceiveInput(m_moveAmt = m_moveAction.ReadValue<Vector2>());
        m_lookAmt = m_lookAction.ReadValue<Vector2>();
    }
    private void OnEnable()
    {
        InputActions.FindActionMap("Player").Enable();
    }
    private void OnDisable()
    {
        InputActions.FindActionMap("Player").Disable();
    }
}
