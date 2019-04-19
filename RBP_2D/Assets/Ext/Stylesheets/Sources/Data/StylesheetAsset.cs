using TMPro;
using UnityEngine;

[CreateAssetMenu]
public class StylesheetAsset : ScriptableObject
{
    public Color MainColor = Color.white;
    public Color SecondaryColor = Color.white;
    public Color DarkColor = Color.white;
    public Color LightColor = Color.white;

    public ButtonStylesheet Buttons;
    public SpecialElementsStylesheet Special;

    public TMP_FontAsset RegularFont;
    public TMP_FontAsset BoldFont;
    public TMP_FontAsset BlackFont;
}

[System.Serializable]
public class ButtonStylesheet
{
    public Color BackgroundDarkColor = Color.white;
    public Color BackgroundLightColor = Color.white;
    public Color LabelDarkColor = Color.white;
    public Color LabelLightColor = Color.white;
    public Color IconDarkColor = Color.white;
    public Color IconLightColor = Color.white;
}

[System.Serializable]
public class SpecialElementsStylesheet
{
    public Color RectColor;
    public Color BinBackgroundColor;
    public Color BinLabelColor;
    public Color RectLabelColor;
}