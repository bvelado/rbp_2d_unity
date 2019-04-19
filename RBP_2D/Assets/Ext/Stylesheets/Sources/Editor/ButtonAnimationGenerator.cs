using System.Linq;
using TMPro;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public static class ButtonAnimationGenerator
{
    const string NORMAL_PARAMETER = "Normal";
    const string SELECTED_PARAMETER = "Selected";
    const string HIGHLIGHTED_PARAMETER = "Highlighted";
    const string PRESSED_PARAMETER = "Pressed";
    const string DISABLED_PARAMETER = "Disabled";

    public static AnimatorController RegenerateAnimations(Animator animator, StylesheetAsset stylesheet)
    {
        var filePath = AssetDatabase.GetAssetPath(animator.runtimeAnimatorController);
        var controller = AssetDatabase.LoadAssetAtPath<AnimatorController>(filePath);

        while (controller.parameters.Length > 0)
        {
            controller.RemoveParameter(controller.parameters.Length - 1);
        }

        controller.AddParameter(NORMAL_PARAMETER, AnimatorControllerParameterType.Trigger);
        controller.AddParameter(SELECTED_PARAMETER, AnimatorControllerParameterType.Trigger);
        controller.AddParameter(HIGHLIGHTED_PARAMETER, AnimatorControllerParameterType.Trigger);
        controller.AddParameter(PRESSED_PARAMETER, AnimatorControllerParameterType.Trigger);
        controller.AddParameter(DISABLED_PARAMETER, AnimatorControllerParameterType.Trigger);

        var rootStateMachine = controller.layers[0].stateMachine;

        var normalState = rootStateMachine.states.Where(x => x.state.name == NORMAL_PARAMETER).Select(x => x.state).FirstOrDefault();
        if (!normalState)
        {
            normalState = CreateState(NORMAL_PARAMETER, ref rootStateMachine);
        }
        normalState.motion = CreateNormalMotion(stylesheet, filePath);

        var highlightedState = rootStateMachine.states.Where(x => x.state.name == HIGHLIGHTED_PARAMETER).Select(x => x.state).FirstOrDefault();
        if (!highlightedState)
        {
            highlightedState = CreateState(HIGHLIGHTED_PARAMETER, ref rootStateMachine);
        }
        highlightedState.motion = CreateHighlightedMotion(stylesheet, filePath);

        var pressedState = rootStateMachine.states.Where(x => x.state.name == PRESSED_PARAMETER).Select(x => x.state).FirstOrDefault();
        if (!pressedState)
        {
            pressedState = CreateState(PRESSED_PARAMETER, ref rootStateMachine);
        }
        pressedState.motion = CreatePressedMotion(stylesheet, filePath);

        var selectedState = rootStateMachine.states.Where(x => x.state.name == SELECTED_PARAMETER).Select(x => x.state).FirstOrDefault();
        if (!selectedState)
        {
            selectedState = CreateState(SELECTED_PARAMETER, ref rootStateMachine);
        }
        //selectedState.motion = CreateSelectedMotion(stylesheet, filePath);

        var disabledState = rootStateMachine.states.Where(x => x.state.name == DISABLED_PARAMETER).Select(x => x.state).FirstOrDefault();
        if (!disabledState)
        {
            disabledState = CreateState(DISABLED_PARAMETER, ref rootStateMachine);
        }
        disabledState.motion = CreateDisabledMotion(stylesheet, filePath);

        return controller;
    }

    public static AnimatorController GenerateAnimations(StylesheetAsset stylesheet)
    {
        var filePath = EditorUtility.SaveFilePanelInProject("Animator controller", "Ctrl_NewAnimatorController", "controller", "Message");

        if (filePath == "")
        {
            return null;
        }

        var controller = AnimatorController.CreateAnimatorControllerAtPath(filePath);

        controller.AddParameter(NORMAL_PARAMETER, AnimatorControllerParameterType.Trigger);
        controller.AddParameter(SELECTED_PARAMETER, AnimatorControllerParameterType.Trigger);
        controller.AddParameter(HIGHLIGHTED_PARAMETER, AnimatorControllerParameterType.Trigger);
        controller.AddParameter(DISABLED_PARAMETER, AnimatorControllerParameterType.Trigger);
        controller.AddParameter(PRESSED_PARAMETER, AnimatorControllerParameterType.Trigger);

        var rootStateMachine = controller.layers[0].stateMachine;

        var normalState = CreateState(NORMAL_PARAMETER, ref rootStateMachine);
        normalState.motion = CreateNormalMotion(stylesheet, filePath);

        var highlightedState = CreateState(HIGHLIGHTED_PARAMETER, ref rootStateMachine);
        highlightedState.motion = CreateHighlightedMotion(stylesheet, filePath);

        var pressedState = CreateState(PRESSED_PARAMETER, ref rootStateMachine);
        pressedState.motion = CreatePressedMotion(stylesheet, filePath);

        var selectedState = CreateState(SELECTED_PARAMETER, ref rootStateMachine);

        var disabledState = CreateState(DISABLED_PARAMETER, ref rootStateMachine);
        disabledState.motion = CreateDisabledMotion(stylesheet, filePath);

        rootStateMachine.defaultState = normalState;

        var normalTransition = CreateAnyStateTransition(normalState, ref rootStateMachine, 0.05f);
        normalTransition.AddCondition(AnimatorConditionMode.If, 0, NORMAL_PARAMETER);

        var highlightedTransition = CreateAnyStateTransition(highlightedState, ref rootStateMachine, 0.05f); ;
        highlightedTransition.AddCondition(AnimatorConditionMode.If, 0, HIGHLIGHTED_PARAMETER);

        var pressedTransition = CreateAnyStateTransition(pressedState, ref rootStateMachine, 0.05f);
        pressedTransition.AddCondition(AnimatorConditionMode.If, 0, PRESSED_PARAMETER);

        var selectedTransition = CreateAnyStateTransition(selectedState, ref rootStateMachine, 0.05f);
        selectedTransition.AddCondition(AnimatorConditionMode.If, 0, SELECTED_PARAMETER);

        var disabledTransition = CreateAnyStateTransition(disabledState, ref rootStateMachine, 0.25f);
        disabledTransition.AddCondition(AnimatorConditionMode.If, 0, DISABLED_PARAMETER);

        return controller;
    }

    private static AnimatorState CreateState(string name, ref AnimatorStateMachine parent, bool writeDeafultValues = false)
    {
        var state = parent.AddState(name);
        state.writeDefaultValues = writeDeafultValues;
        return state;
    }

    private static AnimatorStateTransition CreateAnyStateTransition(AnimatorState towards, ref AnimatorStateMachine parent, float duration, bool transitionToSelf = false)
    {
        var transition = parent.AddAnyStateTransition(towards);
        transition.duration = duration;
        transition.canTransitionToSelf = transitionToSelf;

        return transition;
    }

    private static AnimationClip CreateNormalMotion(StylesheetAsset stylesheet, string filePath)
    {
        var clip = new AnimationClip();

        AddColorAnimationCurves(ref clip, "", typeof(Image), "m_Color", stylesheet.Buttons.BackgroundDarkColor);
        AddColorAnimationCurves(ref clip, "Txt_Label", typeof(TextMeshProUGUI), "m_fontColor", stylesheet.Buttons.LabelLightColor);
        AddColorAnimationCurves(ref clip, "Img_Icon", typeof(Image), "m_Color", stylesheet.Buttons.IconLightColor);

        AssetDatabase.CreateAsset(clip, filePath.Replace("Ctrl_", "").Replace(".controller", "@default.anim"));

        return clip;
    }

    private static AnimationClip CreateHighlightedMotion(StylesheetAsset stylesheet, string filePath)
    {
        var clip = new AnimationClip();

        AddColorAnimationCurves(ref clip, "", typeof(Image), "m_Color", stylesheet.Buttons.BackgroundDarkColor);
        AddColorAnimationCurves(ref clip, "Txt_Label", typeof(TextMeshProUGUI), "m_fontColor", stylesheet.SecondaryColor);
        AddColorAnimationCurves(ref clip, "Img_Icon", typeof(Image), "m_Color", stylesheet.SecondaryColor);

        AssetDatabase.CreateAsset(clip, filePath.Replace("Ctrl_", "").Replace(".controller", "@highlitghted.anim"));

        return clip;
    }

    private static AnimationClip CreatePressedMotion(StylesheetAsset stylesheet, string filePath)
    {
        var clip = new AnimationClip();

        AddColorAnimationCurves(ref clip, "", typeof(Image), "m_Color", stylesheet.SecondaryColor);
        AddColorAnimationCurves(ref clip, "Txt_Label", typeof(TextMeshProUGUI), "m_fontColor", stylesheet.Buttons.LabelLightColor);
        AddColorAnimationCurves(ref clip, "Img_Icon", typeof(Image), "m_Color", stylesheet.Buttons.LabelLightColor);

        AssetDatabase.CreateAsset(clip, filePath.Replace("Ctrl_", "").Replace(".controller", "@pressed.anim"));

        return clip;
    }

    private static AnimationClip CreateDisabledMotion(StylesheetAsset stylesheet, string filePath)
    {
        var clip = new AnimationClip();

        AddColorAnimationCurves(ref clip, "", typeof(Image), "m_Color", new Color(0.6f, 0.6f, 0.6f, 0.3f));
        AddColorAnimationCurves(ref clip, "Txt_Label", typeof(TextMeshProUGUI), "m_fontColor", new Color(0.6f, 0.6f, 0.6f, 0.2f));
        AddColorAnimationCurves(ref clip, "Img_Icon", typeof(Image), "m_Color", new Color(0.6f, 0.6f, 0.6f, 0.3f));

        AssetDatabase.CreateAsset(clip, filePath.Replace("Ctrl_", "").Replace(".controller", "@disabled.anim"));

        return clip;
    }

    private static void AddColorAnimationCurves(ref AnimationClip clip, string relativePath, System.Type type, string propertyName, Color color)
    {
        var curveR = new AnimationCurve()
        {
            keys = new Keyframe[]
            {
                new Keyframe(0f, color.r)
            }
        };
        var curveG = new AnimationCurve()
        {
            keys = new Keyframe[]
            {
                new Keyframe(0f, color.g)
            }
        };
        var curveB = new AnimationCurve()
        {
            keys = new Keyframe[]
            {
                new Keyframe(0f, color.b)
            }
        };
        var curveA = new AnimationCurve()
        {
            keys = new Keyframe[]
            {
                new Keyframe(0f, color.a)
            }
        };

        clip.SetCurve(relativePath, type, propertyName + ".r", curveR);
        clip.SetCurve(relativePath, type, propertyName + ".g", curveG);
        clip.SetCurve(relativePath, type, propertyName + ".b", curveB);
        clip.SetCurve(relativePath, type, propertyName + ".a", curveA);
    }
}
