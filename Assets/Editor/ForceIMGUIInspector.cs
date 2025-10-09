#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public static class ForceIMGUIInspector
{
    static ForceIMGUIInspector()
    {
        EditorPrefs.SetBool("Inspector.UseUIElements", false);
        Debug.Log("✅ IMGUI Inspector forced (UI Toolkit disabled)");
    }
}
#endif
