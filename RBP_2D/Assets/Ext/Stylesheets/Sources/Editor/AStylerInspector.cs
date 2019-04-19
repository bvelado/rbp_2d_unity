using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AStyler), true)]
[CanEditMultipleObjects]
public class AStylerInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if(GUILayout.Button("Apply style"))
        {
            foreach(var styler in targets)
            {
                (styler as AStyler).ApplyStyle();
            }
        }
    }
}
