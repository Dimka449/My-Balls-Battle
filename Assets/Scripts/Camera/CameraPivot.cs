using UnityEngine;
using Mirror;
using System;

public class CameraPivot : NetworkBehaviour
{
    [SerializeField] private float verticalRotateSpeed = 2f;
    [SerializeField] private float horizontalRotateSpeed = 3f;
    [SerializeField][Range(0.01f, 1f)] private float rotationSmoothness = 0.95f;
    [SerializeField][Range(0.01f, 1f)] private float smoothnessOfFollowing = 0.9f;
    [SerializeField] private float verticalMinAngle = 0f;
    [SerializeField] private float verticalMaxAngle = 85f;

    private float _currentHorizontalAngle = 0f;
    private float _currentVerticalAngle = 0f;
    private Transform _target;
    private Vector2 _mouseMovementDelta = new Vector2(0f, 0f);
    private InputManager _inputManager;

    public static event Action<Transform> OnSpawnPlayer;

    private void Start()
    {
        SetInputManager();
    }

    private void Update()
    {
        if (NetworkClient.localPlayer != null)
        {
            _target = NetworkClient.localPlayer.transform;
            OnSpawnPlayer?.Invoke(transform);
        }
        else _target = null;
        _mouseMovementDelta = _inputManager.GetMouseMovementDelta();
    }

    private void LateUpdate()
    {
        if (_target == null) return;
        float verticalRotate = _mouseMovementDelta.y * verticalRotateSpeed;
        float horizontalRotate = _mouseMovementDelta.x * horizontalRotateSpeed;
        _currentVerticalAngle -= verticalRotate;
        _currentVerticalAngle = Mathf.Clamp(_currentVerticalAngle, verticalMinAngle, verticalMaxAngle);
        _currentHorizontalAngle += horizontalRotate;
        Quaternion targetRotation = Quaternion.Euler(_currentVerticalAngle, _currentHorizontalAngle, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSmoothness);


        transform.position = Vector3.Lerp(transform.position, _target.position, smoothnessOfFollowing);
    }

    private void SetInputManager()
    {
        _inputManager = InputManager.Instance;
    }
}
