using UnityEditor;

[CustomEditor(typeof(DetectPlayer))]
public class DetectPlayerEditor : Editor
{
    // Type of detection
    SerializedProperty detectType;

    // Obstacle layer
    SerializedProperty obstacleLayer;

    // Vision settings
    SerializedProperty visionDistance;
    SerializedProperty visionAngle;

    // Sensor settings
    SerializedProperty sensorRadius;

    // Hearing settings
    SerializedProperty hearingDistance;

    private void OnEnable()
    {
        detectType = serializedObject.FindProperty("detectType");

        obstacleLayer = serializedObject.FindProperty("obstacleLayer");

        visionDistance = serializedObject.FindProperty("visionDistance");
        visionAngle = serializedObject.FindProperty("visionAngle");

        sensorRadius = serializedObject.FindProperty("sensorRadius");

        hearingDistance = serializedObject.FindProperty("hearingDistance");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(detectType);

        if (((DetectType)detectType.intValue & DetectType.Vision) != 0)
        {
            EditorGUILayout.PropertyField(obstacleLayer);

            EditorGUILayout.PropertyField(visionDistance);
            EditorGUILayout.PropertyField(visionAngle);
        }

        if (((DetectType)detectType.intValue & DetectType.Sensor) != 0)
        {
            EditorGUILayout.PropertyField(sensorRadius);
        }

        if (((DetectType)detectType.intValue & DetectType.Hearing) != 0)
        {
            EditorGUILayout.PropertyField(hearingDistance);
        }

        serializedObject.ApplyModifiedProperties();
    }
}