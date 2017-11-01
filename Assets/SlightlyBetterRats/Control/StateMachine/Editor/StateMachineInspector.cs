using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(StateMachineDefinition))]
public class StateMachineInspector : Editor {
    public override void OnInspectorGUI() {
        StateMachineDefinition myTarget = (StateMachineDefinition)target;

        if (GUILayout.Button("Open State Machine Editor")) {
            StateMachineEditor.def = myTarget;
            StateMachineEditor.ShowWindow();
        }

        DrawDefaultInspector();
    }
}