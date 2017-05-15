using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(FluxMenuStateManager))]
public class FluxMenuStateManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        FluxMenuStateManager Statemanager = (FluxMenuStateManager)target;

        GUI.backgroundColor = Color.gray;
        FluxEditor.BeginGroup();

        FluxEditor.BeginGroup("Flux Menu Manager", new Color(1, 0.77f, 0.05f), 2, 1);
        FluxEditor.EndGroup();
        GUI.color = Color.white;
        GUI.backgroundColor = Color.white;

        FluxEditor.BeginGroup();
        EditorGUILayout.LabelField("Properties", EditorStyles.boldLabel);
        Statemanager.DefaultMenu = EditorGUILayout.ObjectField("Default Menu", Statemanager.DefaultMenu, typeof(FluxMenu), true) as FluxMenu;
        Statemanager.ExitMenu = EditorGUILayout.ObjectField("Exit Menu", Statemanager.ExitMenu, typeof(FluxMenu), true) as FluxMenu;
        DrawSplitter();
        if (FluxEditor.Button("Add Menu", true))
        {
            AddMenu();
        }
        FluxEditor.EndGroup();

        FluxEditor.EndGroup();
    }

    private void DrawSplitter()
    {
        GUILayout.Box("", new GUILayoutOption[] { GUILayout.ExpandWidth(true), GUILayout.Height(1) });
    }

    private void AddMenu()
    {
        FluxMenuStateManager StateManager = (FluxMenuStateManager)target;

        GameObject Obj = StateManager.AddMenuObject();
        
        Selection.objects = new Object[] { Obj };
        EditorGUIUtility.PingObject(Obj);
    }
}
