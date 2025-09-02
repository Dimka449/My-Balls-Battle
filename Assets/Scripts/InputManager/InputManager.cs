using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    private InputActions _inputActions;

    private void Awake()
    {
        Instance = this;
        _inputActions = new InputActions();
        _inputActions.Enable();
    }

    public Vector3 GetMovementVector()
    {
        Vector3 inputVector = _inputActions.BallFight.Move.ReadValue<Vector3>();
        return inputVector;
    }

    public float GetDeltaScroll()
    {
        float delta = _inputActions.BallFight.CameraDistance.ReadValue<float>();
        return delta;
    }

    public Vector2 GetMouseMovementDelta()
    {
        Vector2 delta = _inputActions.BallFight.PivotRotation.ReadValue<Vector2>();
        return delta;
    }
}
