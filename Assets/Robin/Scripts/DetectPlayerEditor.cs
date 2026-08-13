using UnityEditor;

[CustomEditor(typeof(DetectPlayer))]
public class DetectPlayerEditor : Editor
{
    // Type of detection
    SerializedProperty detectType;

    // Vision settings
    SerializedProperty visionDistance;
    SerializedProperty visionAngle;

    // Hearing settings
    SerializedProperty hearingRadius;

    private void OnEnable()
    {
        detectType = serializedObject.FindProperty("detectType");

        visionDistance = serializedObject.FindProperty("visionDistance");
        visionAngle = serializedObject.FindProperty("visionAngle");

        hearingRadius = serializedObject.FindProperty("hearingRadius");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(detectType);

        if (((DetectType)detectType.intValue & DetectType.Vision) != 0)
        {
            EditorGUILayout.PropertyField(visionDistance);
            EditorGUILayout.PropertyField(visionAngle);
        }

        if (((DetectType)detectType.intValue & DetectType.Sound) != 0)
        {
            EditorGUILayout.PropertyField(hearingRadius);
        }

        serializedObject.ApplyModifiedProperties();
    }
}