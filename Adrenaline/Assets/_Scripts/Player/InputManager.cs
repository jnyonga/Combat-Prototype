using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] Movement movement;
    [SerializeField] MouseLook mouseLook;
    [SerializeField] CombatController combatController;
    [SerializeField] Adrenaline adrenaline;
    [SerializeField] InputActionAsset InputActions;

    private InputAction m_moveAction;
    private InputAction m_lookAction;
    private InputAction m_blockAction;
    private InputAction m_attackAction;
    private InputAction m_freeLookAction;
    private InputAction m_activateFightAction;
    private InputAction m_activateFlightAction;

    private Vector2 m_moveAmt;
    private Vector2 m_lookAmt;

    private void Awake()
    {
        m_moveAction = InputSystem.actions.FindAction("Move");
        m_lookAction = InputSystem.actions.FindAction("Look");
        m_blockAction = InputSystem.actions.FindAction("Block");
        m_attackAction = InputSystem.actions.FindAction("Attack");
        m_freeLookAction = InputSystem.actions.FindAction("Attack");
        m_activateFightAction = InputSystem.actions.FindAction("ActivateFight");
        m_activateFlightAction = InputSystem.actions.FindAction("ActivateFlight");
    }

    private void Update()
    {
        m_moveAmt = m_moveAction.ReadValue<Vector2>();
        m_lookAmt = m_lookAction.ReadValue<Vector2>();

        movement.ReceiveInput(m_moveAmt);
        mouseLook.ReceiveInput(m_lookAmt);
    }
    private void OnEnable()
    {
        InputActions.FindActionMap("Player").Enable();

        m_blockAction.performed += OnBlockPressed;
        m_blockAction.canceled += OnBlockReleased;
        m_attackAction.performed += OnAttackPressed;
        m_attackAction.canceled += OnAttackReleased;
        m_activateFightAction.performed += OnActivateFightPressed;
        m_activateFlightAction.performed += OnActivateFlightPressed;

    }
    private void OnDisable()
    {
        InputActions.FindActionMap("Player").Disable();

        m_blockAction.performed -= OnBlockPressed;
        m_blockAction.canceled -= OnBlockReleased;
        m_attackAction.performed -= OnAttackPressed;
        m_attackAction.canceled -= OnAttackReleased;
        m_activateFightAction.performed -= OnActivateFightPressed;
        m_activateFlightAction.performed -= OnActivateFlightPressed;
    }
    private void OnBlockPressed(InputAction.CallbackContext context)
    {
        combatController.OnBlockPressed();
    }

    private void OnBlockReleased(InputAction.CallbackContext context)
    {
        combatController.OnBlockReleased();
    }

    private void OnAttackPressed(InputAction.CallbackContext context)
    {
        combatController.OnFreeLookPressed();

        combatController.OnAttackPressed();
    }
    private void OnAttackReleased(InputAction.CallbackContext context)
    {
        combatController.OnFreeLookReleased();
    }
    private void OnActivateFightPressed(InputAction.CallbackContext context)
    {
        adrenaline.OnActivateFightPressed();
    }

    private void OnActivateFlightPressed(InputAction.CallbackContext context)
    {
        adrenaline.OnActivateFlightPressed();
    }
}
