using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    [SerializeField] private float moveForceMagnitude = 5f;

    [SyncVar] private float _moveForceMagnitude;

    private Vector3 _movementVector;
    private Rigidbody _rb;
    private InputManager _inputManager;
    private Transform _cameraPivotTransform;

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
        CameraPivot.OnSpawnPlayer += GetCurrentCameraTransform;
    }

    private void Update()
    {
        if (!isLocalPlayer) return;
        if (_cameraPivotTransform == null) return;
        _movementVector = Quaternion.Euler(0, _cameraPivotTransform.localRotation.eulerAngles.y, 0) * _inputManager.GetMovementVector();
    }

    private void FixedUpdate()
    {
        if (isLocalPlayer)
        {
            CmdMovingPlayer(_movementVector);
            MovingPlayer(_movementVector);
        }
    }

    private void GetCurrentCameraTransform(Transform transform)
    {
        _cameraPivotTransform = transform;
    }

    private void SetInputManager()
    {
        _inputManager = InputManager.Instance;
    }

    [Command]
    private void CmdMovingPlayer(Vector3 vector)
    {
        _rb.AddForce(vector.normalized * _moveForceMagnitude, ForceMode.Force);
    }

    private void MovingPlayer(Vector3 vector)
    {
        _rb.AddForce(vector.normalized * _moveForceMagnitude, ForceMode.Force);
    }

    private void OnDestroy()
    {
        CameraPivot.OnSpawnPlayer -= GetCurrentCameraTransform;
    }
}
