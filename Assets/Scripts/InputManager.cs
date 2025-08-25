using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    private Player _player;
    private InputActions _inputActions;

    private void Awake()
    {
        Instance = this;
        _inputActions = new InputActions();
        _inputActions.Enable();
    }

    public void SetPlayer(Player player)
    {
        _player = player;
    }

    private Vector2 GetMovementVector()
    {
        Vector2 inputVector = _inputActions.BallFight.Move.ReadValue<Vector2>();
        return inputVector;
    }

    private void StartMovePlayer()
    {

    }
}
