using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BinView : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RectTransform _rectContainer;
    [SerializeField] private TextMeshProUGUI _labelText;
    [SerializeField] private Image _backgroundImage;

    [Header("Content")]
    [SerializeField] private RectView _rectViewPrefab;

    [Header("View")]
    [SerializeField] private StylesheetAsset _stylesheet;

    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void Initialize(BinData binData)
    {
        _labelText.text = string.Format("{0} (Occ. {1:0.##}%)", binData.Label, binData.Occupancy * 100f);
        _labelText.color = _stylesheet.Special.BinLabelColor;

        _backgroundImage.color = _stylesheet.Special.BinBackgroundColor; 

        float h, s, v;
        Color.RGBToHSV(_stylesheet.Special.RectColor, out h, out s, out v);

        var ratio = _rectTransform.rect.width / binData.Size.x;

        _rectContainer.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, binData.Size.y * ratio);

        foreach (var rectData in binData.Rects)
        {
            var color = Random.ColorHSV(h-0.04f, h+ 0.04f, s-0.04f, s+0.04f, v-0.04f, v+0.04f);
            var rectView = Instantiate(_rectViewPrefab, _rectContainer);
            rectView.Initialize(rectData, color, ratio);
        }
    }
}
