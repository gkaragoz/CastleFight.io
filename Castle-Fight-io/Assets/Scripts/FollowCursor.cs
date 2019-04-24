using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCursor : MonoBehaviour {

    [SerializeField]
    private LayerMask _groundMask;//select the layer of your ground here, or just choose Default.

    private void Update() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, _groundMask)) {
            transform.position = new Vector3(hit.point.x, 0f, hit.point.z);
        }
    }

}
