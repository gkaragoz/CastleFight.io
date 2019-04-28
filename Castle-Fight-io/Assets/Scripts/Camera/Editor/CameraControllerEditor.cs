using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CameraController))]
[AddComponentMenu("CameraController")]
public class CameraControllerEditor : Editor {

    private CameraController cameraController { get { return target as CameraController; } }

    public override void OnInspectorGUI() {
        //base.OnInspectorGUI();
        Undo.RecordObject(cameraController, "RTS_CAmera");
        MovementTab();
        EditorUtility.SetDirty(cameraController);
    }

    private void MovementTab() {
        using (new HorizontalBlock()) {
            GUILayout.Label("Panning with mouse: ", EditorStyles.boldLabel, GUILayout.Width(170f));
            cameraController.usePanning = EditorGUILayout.Toggle(cameraController.usePanning);
        }
        if (cameraController.usePanning) {
            cameraController.panningKey = (KeyCode)EditorGUILayout.EnumPopup("Panning when holding: ", cameraController.panningKey);
            cameraController.panningSpeed = EditorGUILayout.FloatField("Panning speed: ", cameraController.panningSpeed);
        }

        using (new HorizontalBlock()) {
            GUILayout.Label("Limit movement: ", EditorStyles.boldLabel, GUILayout.Width(170f));
            cameraController.limitMap = EditorGUILayout.Toggle(cameraController.limitMap);
        }
        if (cameraController.limitMap) {
            cameraController.limitX = EditorGUILayout.FloatField("Limit X: ", cameraController.limitX);
            cameraController.limitY = EditorGUILayout.FloatField("Limit Y: ", cameraController.limitY);
        }

        GUILayout.Label("Follow target", EditorStyles.boldLabel);
        cameraController.targetFollow = EditorGUILayout.ObjectField("Target to follow: ", cameraController.targetFollow, typeof(Transform)) as Transform;
        cameraController.targetOffset = EditorGUILayout.Vector3Field("Target offset: ", cameraController.targetOffset);
        cameraController.followingSpeed = EditorGUILayout.FloatField("Following speed: ", cameraController.followingSpeed);
    }

}