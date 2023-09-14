using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CanEditMultipleObjects]
[CustomEditor(typeof(GridManager))]
public class GridManagerEditor : UnityEditor.Editor
{
    private GridManager _gridManager;

    private SerializedProperty _level;

    private void OnEnable()
    {
        _gridManager = (GridManager)target;
        _level = serializedObject.FindProperty("Level");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        EditorGUI.BeginChangeCheck();

        
        // _gridManager.ClearGrid();
        // _gridManager.GenerateGrid();
        

        EditorGUI.EndChangeCheck();
        serializedObject.ApplyModifiedProperties();
    }
}
#endif
