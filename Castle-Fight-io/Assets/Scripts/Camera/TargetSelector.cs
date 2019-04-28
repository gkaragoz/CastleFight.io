using UnityEngine;

[RequireComponent(typeof(CameraController))]
public class TargetSelector : MonoBehaviour {

    private CameraController cam;
    private new Camera camera;
    public string targetsTag;

    private void Start() {
        cam = gameObject.GetComponent<CameraController>();
        camera = gameObject.GetComponent<Camera>();
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                if (hit.transform.CompareTag(targetsTag))
                    cam.SetTarget(hit.transform);
                else
                    cam.ResetTarget();
            }
        }
    }

}
