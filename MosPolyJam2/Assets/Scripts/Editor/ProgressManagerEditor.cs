using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

[CustomEditor(typeof(ProgressManager))]
public class ProgressManagerEditor : Editor
{
    ProgressManager progressManager;

    public override VisualElement CreateInspectorGUI()
    {
        progressManager = target as ProgressManager;
        return base.CreateInspectorGUI();
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.Space(10);
        if (GUILayout.Button("Initialize Progress Points Array"))
        {
            progressManager.InitializeProgressPointsArray();
        }
    }
}
