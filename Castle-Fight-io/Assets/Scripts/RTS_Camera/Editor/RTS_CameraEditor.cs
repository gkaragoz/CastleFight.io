﻿using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

namespace RTS_Cam
{
    [CustomEditor(typeof(RTS_Camera))]
    public class RTS_CameraEditor : Editor
    {
        private RTS_Camera camera { get { return target as RTS_Camera; } }

        private TabsBlock tabs;

        private void OnEnable()
        {
            tabs = new TabsBlock(new Dictionary<string, System.Action>() 
            {
                {"Movement", MovementTab},
                {"Height", HeightTab}
            });
            tabs.SetCurrentMethod(camera.lastTab);
        }

        public override void OnInspectorGUI()
        {
            //base.OnInspectorGUI();
            Undo.RecordObject(camera, "RTS_CAmera");
            tabs.Draw();
            if (GUI.changed)
                camera.lastTab = tabs.curMethodIndex;
            EditorUtility.SetDirty(camera);
        }

        private void MovementTab()
        {
            using (new HorizontalBlock())
            {
                GUILayout.Label("Use keyboard input: ", EditorStyles.boldLabel, GUILayout.Width(170f));
                camera.useKeyboardInput = EditorGUILayout.Toggle( camera.useKeyboardInput);
            }
            if(camera.useKeyboardInput)
            {
                camera.horizontalAxis = EditorGUILayout.TextField("Horizontal axis name: ", camera.horizontalAxis);
                camera.verticalAxis = EditorGUILayout.TextField("Vertical axis name: ", camera.verticalAxis);
                camera.keyboardMovementSpeed = EditorGUILayout.FloatField("Movement speed: ", camera.keyboardMovementSpeed);
            }

            using (new HorizontalBlock())
            {
                GUILayout.Label("Screen edge input: ", EditorStyles.boldLabel, GUILayout.Width(170f));
                camera.useScreenEdgeInput = EditorGUILayout.Toggle( camera.useScreenEdgeInput);
            }

            if(camera.useScreenEdgeInput)
            {
                EditorGUILayout.FloatField("Screen edge border size: ", camera.screenEdgeBorder);
                camera.screenEdgeMovementSpeed = EditorGUILayout.FloatField("Screen edge movement speed: ", camera.screenEdgeMovementSpeed);
            }

            using (new HorizontalBlock())
            {
                GUILayout.Label("Panning with mouse: ", EditorStyles.boldLabel, GUILayout.Width(170f));
                camera.usePanning = EditorGUILayout.Toggle(camera.usePanning);
            }
            if(camera.usePanning)
            {
                camera.panningKey = (KeyCode)EditorGUILayout.EnumPopup("Panning when holding: ", camera.panningKey);
                camera.panningSpeed = EditorGUILayout.FloatField("Panning speed: ", camera.panningSpeed);
            }

            using (new HorizontalBlock())
            {
                GUILayout.Label("Limit movement: ", EditorStyles.boldLabel, GUILayout.Width(170f));
                camera.limitMap = EditorGUILayout.Toggle(camera.limitMap);
            }
            if (camera.limitMap)
            {
                camera.limitX = EditorGUILayout.FloatField("Limit X: ", camera.limitX);
                camera.limitY = EditorGUILayout.FloatField("Limit Y: ", camera.limitY);
            }

            GUILayout.Label("Follow target", EditorStyles.boldLabel);
            camera.targetFollow = EditorGUILayout.ObjectField("Target to follow: ", camera.targetFollow, typeof(Transform)) as Transform;
            camera.targetOffset = EditorGUILayout.Vector3Field("Target offset: ", camera.targetOffset);
            camera.followingSpeed = EditorGUILayout.FloatField("Following speed: ", camera.followingSpeed);
        }

        private void HeightTab()
        {
            using (new HorizontalBlock())
            {
                GUILayout.Label("Keyboard zooming: ", EditorStyles.boldLabel, GUILayout.Width(170f));
                camera.useKeyboardZooming = EditorGUILayout.Toggle(camera.useKeyboardZooming);
            }
            if(camera.useKeyboardZooming)
            {
                camera.zoomInKey = (KeyCode)EditorGUILayout.EnumPopup("Zoom In: ", camera.zoomInKey);
                camera.zoomOutKey = (KeyCode)EditorGUILayout.EnumPopup("Zoom Out: ", camera.zoomOutKey);
                camera.keyboardZoomingSensitivity = EditorGUILayout.FloatField("Keyboard sensitivity: ", camera.keyboardZoomingSensitivity);
            }

            using (new HorizontalBlock())
            {
                GUILayout.Label("Scrollwheel zooming: ", EditorStyles.boldLabel, GUILayout.Width(170f));
                camera.useScrollwheelZooming = EditorGUILayout.Toggle(camera.useScrollwheelZooming);
            }
            if (camera.useScrollwheelZooming) {
                camera.scrollWheelZoomingSensitivity = EditorGUILayout.FloatField("Scrollwheel sensitivity: ", camera.scrollWheelZoomingSensitivity);
                camera.heightDampening = EditorGUILayout.FloatField("Height dampening: ", camera.heightDampening);
            }

            if (camera.useScrollwheelZooming || camera.useKeyboardZooming)
            {
                using (new HorizontalBlock())
                {
                    camera.maxHeight = EditorGUILayout.FloatField("Max height: ", camera.maxHeight);
                    camera.minHeight = EditorGUILayout.FloatField("Min height: ", camera.minHeight);
                }
            }  
        }
    }
}