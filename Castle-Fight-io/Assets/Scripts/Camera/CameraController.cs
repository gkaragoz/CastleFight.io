﻿using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour {

    [Header("Movement")]
    [SerializeField]
    private bool _usePanning = true;
    [SerializeField]
    private KeyCode _panningKey = KeyCode.Mouse2;
    [SerializeField]
    private float _followingSpeed = 5f;
    [SerializeField]
    private float _panningSpeed = 10f;

    private int _panFingerId;
    private Vector3 _lastPanPosition;

    [Header("Map Limits")]
    [SerializeField]
    private bool _limitMap = true;
    [SerializeField]
    private float[] _boundsX = new float[] { -10f, 5f };
    [SerializeField]
    private float[] _boundsZ = new float[] { -18f, -4f };

    [Header("Targeting")]
    [SerializeField]
    private Transform _targetFollow;
    [SerializeField]
    private Vector3 _targetOffset;

    public bool IsLocked { get; set; }

    public bool IsPanning {
        get {
            if (Input.touchCount == 1) {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began || (touch.fingerId == _panFingerId && touch.phase == TouchPhase.Moved)) {
                    return true;
                }
            }
            if (Input.GetKey(_panningKey)) {
                return true;
            }

            return false;
        }
    }

    public bool IsFollowingTarget {
        get {
            return _targetFollow != null;
        }
    }

    private Vector2 MouseInput {
        get { return Input.mousePosition; }
    }

    private Vector2 MouseAxis {
        get { return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")); }
    }

    private void Update() {
        CameraUpdate();
    }

    private void CameraUpdate() {
        // Quit from following target while panning.
        if (IsPanning) {
            ResetTarget();
        }
        
        if (IsLocked) {
            return;
        }

        if (IsFollowingTarget) {
            FollowTarget();
        } else {
            if (Input.touchSupported) {
                TouchMovement();
            } else {
                MouseMovement();
            }
        }

        LimitPosition();
    }

    private void TouchMovement() {
        if (Input.touchCount == 1) {
            // Panning
            // If the touch began, capture its position and its finger ID.
            // Otherwise, if the finger ID of the touch doesn't match, skip it.
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began) {
                _lastPanPosition = touch.position;
                _panFingerId = touch.fingerId;
            } else if (touch.fingerId == _panFingerId && touch.phase == TouchPhase.Moved) {
                PanCamera(touch.position);
            }
        }
    }

    private void PanCamera(Vector3 newPanPosition) {
        // Determine how much to move the camera
        Vector3 offset = Camera.main.ScreenToViewportPoint(_lastPanPosition - newPanPosition);
        Vector3 move = new Vector3(offset.x * _panningSpeed, 0, offset.y * _panningSpeed);

        // Perform the movement
        transform.Translate(move, Space.World);

        // Cache the position
        _lastPanPosition = newPanPosition;
    }

    private void MouseMovement() {
        if (_usePanning && Input.GetKey(_panningKey) && MouseAxis != Vector2.zero) {
            Vector3 desiredMove = new Vector3(-MouseAxis.x, 0, -MouseAxis.y);

            desiredMove *= _panningSpeed;
            desiredMove *= Time.deltaTime;
            desiredMove = Quaternion.Euler(new Vector3(0f, transform.eulerAngles.y, 0f)) * desiredMove;
            desiredMove = transform.InverseTransformDirection(desiredMove);

            transform.Translate(desiredMove, Space.Self);
        }
    }

    private void FollowTarget() {
        Vector3 targetPos = new Vector3(_targetFollow.position.x, transform.position.y, _targetFollow.position.z) + _targetOffset;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * _followingSpeed);
    }

    private void LimitPosition() {
        if (!_limitMap)
            return;

        // Ensure the camera remains within bounds.
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(transform.position.x, _boundsX[0], _boundsX[1]);
        pos.z = Mathf.Clamp(transform.position.z, _boundsZ[0], _boundsZ[1]);
        transform.position = pos;
    }

    public void SetTarget(Transform target) {
        _targetFollow = target;
    }

    public void ResetTarget() {
        _targetFollow = null;
    }

}
