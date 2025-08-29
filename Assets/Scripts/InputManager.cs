using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    public Vector3 movementVector;
    private InputActions _inputActions;

    private void Awake()
    {
        Instance = this;
        _inputActions = new InputActions();
        _inputActions.Enable();
    }

    private void Update()
    {
        movementVector = GetMovementVector();
    }

    private Vector3 GetMovementVector()
    {
        Vector3 inputVector = _inputActions.BallFight.Move.ReadValue<Vector3>();
        return inputVector;
    }
}
