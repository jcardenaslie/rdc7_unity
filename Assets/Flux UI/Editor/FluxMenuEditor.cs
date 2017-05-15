using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(FluxMenu))]
public class FluxMenuEditor : Editor
{
    public override void OnInspectorGUI()
    {
        FluxMenu menu = (FluxMenu)target;

        if (menu.StateManager == null)
        {
            menu.StateManager = FindMenuStateManager();
            if (!menu.StateManager)
            {
                return;
            }
        }

        GUI.backgroundColor = Color.gray;
        FluxEditor.BeginGroup();

        FluxEditor.BeginGroup("Flux Menu", new Color(1, 0.77f, 0.05f), 2, 1);
        FluxEditor.EndGroup();
        GUI.color = Color.white;
        GUI.backgroundColor = Color.white;

        if (menu.GetMenuManager().DefaultMenu == menu)
        {
            DrawSplitter();
            GUI.contentColor = new Color(1, 0.77f, 0.05f);
            EditorGUILayout.HelpBox("This is the Default Menu", MessageType.Info);
            GUI.contentColor = Color.white;
        }
        if ((menu.GetMenuManager().ExitMenu == menu))
        {
            DrawSplitter();
            GUI.contentColor = new Color(1, 0.77f, 0.05f);
            EditorGUILayout.HelpBox("This is the Exit Menu", MessageType.Info);
            GUI.contentColor = Color.white;
        }
        DrawSplitter();
        EditorGUILayout.LabelField("Properties", EditorStyles.boldLabel);
        menu.Popup = EditorGUILayout.Toggle("Popup", menu.Popup);
        DrawSplitter();

        EditorGUILayout.BeginHorizontal();
        if (FluxEditor.Button("Make This Default"))
        {
            MakeDefault();
        }

        if (FluxEditor.Button("Make This Exit"))
        {
            MakeExit();
        }
        EditorGUILayout.EndHorizontal();

        FluxEditor.EndGroup();

        EditorUtility.SetDirty(menu);
    }

    private void DrawSplitter()
    {
        GUILayout.Box("", new GUILayoutOption[] { GUILayout.ExpandWidth(true), GUILayout.Height(2) });
    }

    private void MakeDefault()
    {
        FluxMenu menu = (FluxMenu)target;
        menu.MakeDefault();
    }

    private void MakeExit()
    {
        FluxMenu menu = (FluxMenu)target;
        menu.MakeExit();
    }

    private FluxMenuStateManager FindMenuStateManager()
    {
        FluxMenu menu = (FluxMenu)target;

        Transform t = menu.gameObject.transform;
        while (t.parent != null)
        {
            if (t.parent.GetComponent<FluxMenuStateManager>())
            {
                return t.parent.gameObject.GetComponent<FluxMenuStateManager>();
            }
            t = t.parent.transform;
        }
        return null;
    }
}
