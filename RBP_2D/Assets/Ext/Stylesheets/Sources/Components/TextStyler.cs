using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextStyler : AStyler
{
    [SerializeField] private TextStylerColor _color;
    [SerializeField] private TextStylerFont _font;
    public enum TextStylerColor
    {
        Dark,
        Light,
        Primary,
        Secondary
    }

    public enum TextStylerFont
    {
        Regular,
        Bold,
        Black
    }

    public override void ApplyStyle()
    {
        var text = GetComponent<TextMeshProUGUI>();

        if (text)
        {
            switch (_color)
            {
                case TextStylerColor.Dark:
                    text.color = stylesheet.DarkColor;
                    break;
                case TextStylerColor.Light:
                    text.color = stylesheet.LightColor;
                    break;
                case TextStylerColor.Primary:
                    text.color = stylesheet.MainColor;
                    break;
                case TextStylerColor.Secondary:
                    text.color = stylesheet.SecondaryColor;
                    break;
            }

            switch (_font)
            {
                case TextStylerFont.Regular:
                    text.font = stylesheet.RegularFont;
                    break;
                case TextStylerFont.Bold:
                    text.font = stylesheet.BoldFont;
                    break;
                case TextStylerFont.Black:
                    text.font = stylesheet.BlackFont;
                    break;
            }
        }
    }
}
