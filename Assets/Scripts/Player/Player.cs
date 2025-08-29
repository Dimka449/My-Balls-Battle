using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    [SerializeField] private float moveForceMagnitude = 5f;

    [SyncVar] private float _moveForceMagnitude;

    private Rigidbody _rb;
    private InputManager _inputManager;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        if (isLocalPlayer)
        {
            SetInputManager();
        }
        if (isServer)
        {
            _moveForceMagnitude = moveForceMagnitude;
        }
    }

    private void FixedUpdate()
    {
        Vector3 vector = _inputManager.movementVector;
        if (isServer)
        {
            ServerMovingPlayer(vector);
        }
        if (isLocalPlayer)
        {
            MovingPlayer(vector);
        }
    }
    private void SetInputManager()
    {
        _inputManager = InputManager.Instance;
    }

    [Server]
    private void ServerMovingPlayer(Vector3 vector)
    {
        _rb.AddForce(vector.normalized * _moveForceMagnitude, ForceMode.Force);
    }

    private void MovingPlayer(Vector3 vector)
    {
        _rb.AddForce(vector.normalized * _moveForceMagnitude, ForceMode.Force);
    }
}
