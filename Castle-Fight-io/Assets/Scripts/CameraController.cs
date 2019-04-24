using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    private static readonly float _panSpeed = 20f;
    private static readonly float _zoomSpeedTouch = 0.1f;
    private static readonly float _zoomSpeedMouse = 0.5f;

    private static readonly float[] _boundsX = new float[] { -10f, 5f };
    private static readonly float[] _boundsZ = new float[] { -18f, -4f };
    private static readonly float[] _zoomBounds = new float[] { 10f, 85f };

    private Camera _cam;

    private Vector3 _lastPanPosition;
    private int _panFingerId; // Touch mode only

    private bool _wasZoomingLastFrame; // Touch mode only
    private Vector2[] _lastZoomPositions; // Touch mode only

    private void Awake() {
        _cam = GetComponent<Camera>();
    }

    private void Update() {
        if (Input.touchSupported && Application.platform != RuntimePlatform.WebGLPlayer) {
            HandleTouch();
        } else {
            HandleMouse();
        }
    }

    private void HandleTouch() {
        switch (Input.touchCount) {

            case 1: // Panning
                _wasZoomingLastFrame = false;

                // If the touch began, capture its position and its finger ID.
                // Otherwise, if the finger ID of the touch doesn't match, skip it.
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began) {
                    _lastPanPosition = touch.position;
                    _panFingerId = touch.fingerId;
                } else if (touch.fingerId == _panFingerId && touch.phase == TouchPhase.Moved) {
                    PanCamera(touch.position);
                }
                break;

            case 2: // Zooming
                Vector2[] newPositions = new Vector2[] { Input.GetTouch(0).position, Input.GetTouch(1).position };
                if (!_wasZoomingLastFrame) {
                    _lastZoomPositions = newPositions;
                    _wasZoomingLastFrame = true;
                } else {
                    // Zoom based on the distance between the new positions compared to the 
                    // distance between the previous positions.
                    float newDistance = Vector2.Distance(newPositions[0], newPositions[1]);
                    float oldDistance = Vector2.Distance(_lastZoomPositions[0], _lastZoomPositions[1]);
                    float offset = newDistance - oldDistance;

                    ZoomCamera(offset, _zoomSpeedTouch);

                    _lastZoomPositions = newPositions;
                }
                break;

            default:
                _wasZoomingLastFrame = false;
                break;
        }
    }

    private void HandleMouse() {
        // On mouse down, capture it's position.
        // Otherwise, if the mouse is still down, pan the camera.
        if (Input.GetMouseButtonDown(0)) {
            _lastPanPosition = Input.mousePosition;
        } else if (Input.GetMouseButton(0)) {
            PanCamera(Input.mousePosition);
        }

        // Check for scrolling to zoom the camera
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        ZoomCamera(scroll, _zoomSpeedMouse);
    }

    private void PanCamera(Vector3 newPanPosition) {
        // Determine how much to move the camera
        Vector3 offset = _cam.ScreenToViewportPoint(_lastPanPosition - newPanPosition);
        Vector3 move = new Vector3(offset.x * _panSpeed, 0, offset.y * _panSpeed);

        // Perform the movement
        transform.Translate(move, Space.World);

        // Ensure the camera remains within bounds.
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(transform.position.x, _boundsX[0], _boundsX[1]);
        pos.z = Mathf.Clamp(transform.position.z, _boundsZ[0], _boundsZ[1]);
        transform.position = pos;

        // Cache the position
        _lastPanPosition = newPanPosition;
    }

    private void ZoomCamera(float offset, float speed) {
        if (offset == 0) {
            return;
        }

        _cam.fieldOfView = Mathf.Clamp(_cam.fieldOfView - (offset * speed), _zoomBounds[0], _zoomBounds[1]);
    }

}