using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR

using UnityEditor;

[CustomEditor(typeof(BoxScript)), CanEditMultipleObjects]
public class BoxCustom : Editor
{
    BoxScript selected;
    private void OnEnable()
    {
        if (AssetDatabase.Contains(target))
        {
            selected = null;
        }
        else
        {
            // target은 Object형이므로 Enemy로 형변환
            selected = (BoxScript)target;
        }
    }

    public override void OnInspectorGUI()
    {
        if (selected == null)
            return;

        EditorGUILayout.LabelField("▼ ▼ ▼ ▼ Select Box Type ▼ ▼ ▼ ▼");

        selected.boxType = (BoxScript.BoxType)EditorGUILayout.EnumPopup("Box Type", selected.boxType);

        switch (selected.boxType)
        {
            case BoxScript.BoxType.none:
                break;
            case BoxScript.BoxType.DestroyBox:
                EditorGUILayout.LabelField("Must need Falling_block_TIme.cs");
                break;
            case BoxScript.BoxType.MoveBox:
                selected.EndPos = EditorGUILayout.Vector3Field("EndPosition", selected.EndPos);
                selected.moveSpeed = EditorGUILayout.FloatField("MoveSpeed", selected.moveSpeed);

                break;
            case BoxScript.BoxType.JumpingBox:
                selected.jumpForce = EditorGUILayout.FloatField("JumpForce", selected.jumpForce);
                break;
            case BoxScript.BoxType.TeleportBox:
                selected.tpTarget = EditorGUILayout.ObjectField("Tp target", selected.tpTarget, typeof(Transform), true) as Transform;
                break;
            case BoxScript.BoxType.ButtonMoveBox:
                selected.EndPos = EditorGUILayout.Vector3Field("EndPosition", selected.EndPos);
                selected.moveSpeed = EditorGUILayout.FloatField("MoveSpeed", selected.moveSpeed);
                selected.myButton = EditorGUILayout.ObjectField("My Button", selected.myButton, typeof(buttonBox), true) as buttonBox;
                selected.myKey = EditorGUILayout.ObjectField("My Key", selected.myKey, typeof(Pickup), true) as Pickup;
                break;
            case BoxScript.BoxType.ButtonDestroyBox:
                selected.myButton = EditorGUILayout.ObjectField("My Button", selected.myButton, typeof(buttonBox), true) as buttonBox;
                selected.myKey = EditorGUILayout.ObjectField("My Key", selected.myKey, typeof(Pickup), true) as Pickup;
                break;
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(selected);
        }
    }
}
#endif