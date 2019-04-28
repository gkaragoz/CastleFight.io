using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour {

    private Transform m_Transform; //camera tranform
    public bool useFixedUpdate = false; //use FixedUpdate() or Update()

    #region Movement

    public float followingSpeed = 5f; //speed when following a target
    public float panningSpeed = 10f;

    #endregion

    #region MapLimits

    public bool limitMap = true;
    public float limitX = 50f; //x limit of map
    public float limitY = 50f; //z limit of map

    #endregion

    #region Targeting

    public Transform targetFollow; //target to follow
    public Vector3 targetOffset;

    /// <summary>
    /// are we following target
    /// </summary>
    public bool FollowingTarget {
        get {
            return targetFollow != null;
        }
    }

    #endregion

    #region Input

    public bool usePanning = true;
    public KeyCode panningKey = KeyCode.Mouse2;

    private Vector2 MouseInput {
        get { return Input.mousePosition; }
    }

    private Vector2 MouseAxis {
        get { return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")); }
    }

    #endregion

    #region Unity_Methods

    private void Start() {
        m_Transform = transform;
    }

    private void Update() {
        if (!useFixedUpdate)
            CameraUpdate();
    }

    private void FixedUpdate() {
        if (useFixedUpdate)
            CameraUpdate();
    }

    #endregion

    #region RTSCamera_Methods

    /// <summary>
    /// update camera movement and rotation
    /// </summary>
    private void CameraUpdate() {
        // Quit from following target while panning.
        if (usePanning && Input.GetKey(panningKey) && MouseAxis != Vector2.zero) {
            ResetTarget();
        }

        if (FollowingTarget) {
            FollowTarget();
        } else {
            Move();
        }

        LimitPosition();
    }

    /// <summary>
    /// move camera with keyboard or with screen edge
    /// </summary>
    private void Move() {
        if (usePanning && Input.GetKey(panningKey) && MouseAxis != Vector2.zero) {
            Vector3 desiredMove = new Vector3(-MouseAxis.x, 0, -MouseAxis.y);

            desiredMove *= panningSpeed;
            desiredMove *= Time.deltaTime;
            desiredMove = Quaternion.Euler(new Vector3(0f, transform.eulerAngles.y, 0f)) * desiredMove;
            desiredMove = m_Transform.InverseTransformDirection(desiredMove);

            m_Transform.Translate(desiredMove, Space.Self);
        }
    }

    /// <summary>
    /// follow targetif target != null
    /// </summary>
    private void FollowTarget() {
        Vector3 targetPos = new Vector3(targetFollow.position.x, m_Transform.position.y, targetFollow.position.z) + targetOffset;
        m_Transform.position = Vector3.MoveTowards(m_Transform.position, targetPos, Time.deltaTime * followingSpeed);
    }

    /// <summary>
    /// limit camera position
    /// </summary>
    private void LimitPosition() {
        if (!limitMap)
            return;

        m_Transform.position = new Vector3(Mathf.Clamp(m_Transform.position.x, -limitX, limitX),
            m_Transform.position.y,
            Mathf.Clamp(m_Transform.position.z, -limitY, limitY));
    }

    /// <summary>
    /// set the target
    /// </summary>
    /// <param name="target"></param>
    public void SetTarget(Transform target) {
        targetFollow = target;
    }

    /// <summary>
    /// reset the target (target is set to null)
    /// </summary>
    public void ResetTarget() {
        targetFollow = null;
    }

    #endregion
}
