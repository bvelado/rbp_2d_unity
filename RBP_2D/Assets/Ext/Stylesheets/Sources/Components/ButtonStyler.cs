using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonStyler : AStyler
{
    public override void ApplyStyle()
    {
        if (!stylesheet)
        {
            Debug.LogWarning("Couldn't apply style to " + name + ". No stylesheet referenced", gameObject);
            return;
        }

        var button = GetComponent<Button>();

        button.image.color = stylesheet.Buttons.BackgroundDarkColor;

        var texts = button.GetComponentsInChildren<TextMeshProUGUI>(true);
        foreach (var text in texts)
        {
            text.color = stylesheet.Buttons.LabelLightColor;
            text.font = stylesheet.BoldFont;
        }

        var images = button.GetComponentsInChildren<Image>(true);
        // filters the image effects
        // and this button image
        images = images
            .Where(x => !x.CompareTag("UI_Effect"))
            .Where(x => x != button.image)
            .ToArray();
        foreach (var image in images)
        {
            image.color = stylesheet.Buttons.IconLightColor;
        }
    }
}
