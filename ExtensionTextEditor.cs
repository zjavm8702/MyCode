using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(HMExtensionText))]
public class HMExtensionTextEditor : UnityEditor.UI.TextEditor
{
    private SerializedProperty StringKeyProperty = null;
    private SerializedProperty StringCodeProperty = null;
    private GUIStyle Style = new GUIStyle();

    protected override void OnEnable()
    {
        base.OnEnable();

        StringKeyProperty = serializedObject.FindProperty("StringKey");
        StringCodeProperty = serializedObject.FindProperty("StringCode");

        Style.fontSize = 20;
        Style.fontStyle = FontStyle.Bold;
        Style.normal.textColor = Color.blue;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        base.OnInspectorGUI();

        GUI.contentColor = Color.yellow;
        GUI.backgroundColor = Color.blue;

        EditorGUILayout.BeginVertical();
        EditorGUILayout.Space(10);
        EditorGUILayout.PropertyField(StringKeyProperty, new GUIContent("테이블 키", "StringTable Key 값"));
        EditorGUILayout.PropertyField(StringCodeProperty, new GUIContent("테이블 코드", "StringTable Code 값"));

        GUI.contentColor = Color.white;
        GUI.backgroundColor = Color.white;
        EditorGUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }
}
