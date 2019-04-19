using UnityEditor;
using UnityEditor.Animations;
using UnityEditor.Callbacks;
using UnityEngine;

public static class StylesheetEditorFunctions
{
    const string APPLY_STYLE_MENU_ITEM = "Stylesheet/Apply";

    [MenuItem(APPLY_STYLE_MENU_ITEM)]
    public static void ApplyStyle()
    {
        var stylers = Resources.FindObjectsOfTypeAll(typeof(AStyler)) as AStyler[];
        var length = stylers.Length;
        for (int i = 0; i < length; i++)
        {
            if (EditorUtility.DisplayCancelableProgressBar("Stylesheet", "Applying style", (float)i / (float)length))
            {
                break;
            }
            else
            {
                try
                {
                    stylers[i].ApplyStyle();

                    if (stylers[i].GetType() == typeof(ButtonStyler))
                    {
                        GenerateOrRegenerateButtonsAnimations(stylers[i] as ButtonStyler);
                    }
                }
                catch (System.Exception e)
                {
                    EditorUtility.ClearProgressBar();
                    Debug.Log(e);
                }
                EditorUtility.SetDirty(stylers[i].GetGameObject());
            }
        }

        EditorUtility.ClearProgressBar();
        Canvas.ForceUpdateCanvases();

        foreach (var buttonStyler in Resources.FindObjectsOfTypeAll(typeof(ButtonStyler)) as ButtonStyler[])
        {
            if (!buttonStyler.GetComponent<Animator>())
            {
                var controller = ButtonAnimationGenerator.GenerateAnimations(buttonStyler.Stylesheet, buttonStyler.IsOutlineButton);
                var animator = buttonStyler.gameObject.AddComponent<Animator>();

                AnimatorController.SetAnimatorController(animator, controller);
            }
            else
            {
                var animator = buttonStyler.gameObject.GetComponent<Animator>();
                var controller = ButtonAnimationGenerator.RegenerateAnimations(animator, buttonStyler.Stylesheet, buttonStyler.IsOutlineButton);
            }
        }
    }

    private static void GenerateOrRegenerateButtonsAnimations(ButtonStyler styler)
    {
        AnimatorController controller;
        var animator = styler.GetComponent<Animator>();
        if (animator)
        {
            controller = ButtonAnimationGenerator.RegenerateAnimations(animator, styler.Stylesheet, styler.IsOutlineButton);
        }
        else
        {
            controller = ButtonAnimationGenerator.GenerateAnimations(styler.Stylesheet, styler.IsOutlineButton);
        }
    }

    [PostProcessScene()]
    public static void OnPostprocessScene()
    {
        var stylers = Editor.FindObjectsOfType<ButtonStyler>();
        for (int i = stylers.Length - 1; i >= 0; i--)
        {
            Editor.DestroyImmediate(stylers[i]);
        }
    } 

}
