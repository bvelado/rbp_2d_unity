using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageStyler : AStyler
{
    [SerializeField] private ImageStylerColor _color;

    public enum ImageStylerColor
    {
        Dark,
        Light,
        Primary,
        Secondary
    }

    public override void ApplyStyle()
    {
        var image = GetComponent<Image>();
        switch (_color)
        {
            case ImageStylerColor.Dark:
                image.color = stylesheet.DarkColor;
                break;
            case ImageStylerColor.Light:
                image.color = stylesheet.LightColor;
                break;
            case ImageStylerColor.Primary:
                image.color = stylesheet.MainColor;
                break;
            case ImageStylerColor.Secondary:
                image.color = stylesheet.SecondaryColor;
                break;
        }
    }
}
