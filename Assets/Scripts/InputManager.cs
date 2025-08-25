using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    private InputActions inputActions;

    private void Awake()
    {
        Instance = this;
        inputActions = new InputActions();
        inputActions.Enable();
    }

    public Vector2 GetMovementVector()
    {
        Vector2 inputVector = inputActions.BallFight.Move.ReadValue<Vector2>();
        return inputVector;
    }
}
