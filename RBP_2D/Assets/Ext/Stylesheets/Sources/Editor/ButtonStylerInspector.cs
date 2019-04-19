using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

[CustomEditor(typeof(ButtonStyler))]
public class ButtonStylerInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var buttonStyler = target as ButtonStyler;

        if (buttonStyler)
        {
            if(GUILayout.Button("Apply Style"))
            {
                buttonStyler.ApplyStyle();
            }

            if (!buttonStyler.GetComponent<Animator>())
            {
                if (GUILayout.Button("Generate Animations"))
                {
                    var controller = ButtonAnimationGenerator.GenerateAnimations(buttonStyler.Stylesheet, buttonStyler.IsOutlineButton);
                    var animator = buttonStyler.gameObject.AddComponent<Animator>();

                    AnimatorController.SetAnimatorController(animator, controller);
                }
            }
            else
            {
                if (GUILayout.Button("Regenerate Animations"))
                {
                    var animator = buttonStyler.gameObject.GetComponent<Animator>();
                    var controller = ButtonAnimationGenerator.RegenerateAnimations(animator, buttonStyler.Stylesheet, buttonStyler.IsOutlineButton);

                    AnimatorController.SetAnimatorController(animator, controller);
                }
            }
        }
    }
}
