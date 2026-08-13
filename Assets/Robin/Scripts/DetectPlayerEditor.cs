using UnityEditor;

[CustomEditor(typeof(DetectPlayer))]
public class DetectPlayerEditor : Editor
{
    // Type of detection
    SerializedProperty detectType;

    // Vision settings
    SerializedProperty visionDistance;
    SerializedProperty visionAngle;

    // Sensor settings
    SerializedProperty sensorRadius;

    private void OnEnable()
    {
        detectType = serializedObject.FindProperty("detectType");

        visionDistance = serializedObject.FindProperty("visionDistance");
        visionAngle = serializedObject.FindProperty("visionAngle");

        sensorRadius = serializedObject.FindProperty("sensorRadius");
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

        if (((DetectType)detectType.intValue & DetectType.Sensor) != 0)
        {
            EditorGUILayout.PropertyField(sensorRadius);
        }

        serializedObject.ApplyModifiedProperties();
    }
}