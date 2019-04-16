using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class RectView : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI _labelText;

    private RectTransform _rectTransform;
    private Image _image;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _image = GetComponent<Image>();
    }
    public void Initialize(RectData rectData, Color color, float displayRatio)
    {
        _labelText.text = string.Format("{0}x{1}\nx:{2} y:{3}", rectData.Size.x, rectData.Size.y, rectData.Position.x, rectData.Position.y);

        _image.color = color;

        _rectTransform.anchoredPosition = rectData.Position * displayRatio;
        _rectTransform.sizeDelta = rectData.Size * displayRatio;
    }
}
