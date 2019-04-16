using TMPro;
using UnityEngine;

public class BinView : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RectTransform _rectContainer;
    [SerializeField] private TextMeshProUGUI _labelText;

    [Header("Content")]
    [SerializeField] private RectView _rectViewPrefab;

    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void Initialize(BinData binData)
    {
        _labelText.text = string.Format("{0} (Occ. {1:0.##}%)", binData.Label, binData.Occupancy * 100f);

        var randomColorHue = Random.Range(0f, 1f);

        var ratio = _rectTransform.parent.GetComponent<RectTransform>().rect.width / binData.Size.x;

        _rectContainer.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, binData.Size.y * ratio);

        foreach (var rectData in binData.Rects)
        {
            var color = Random.ColorHSV(randomColorHue - 0.04f, randomColorHue + 0.04f, 1f, 1f, 0.2f, 0.4f);
            var rectView = Instantiate(_rectViewPrefab, _rectContainer);
            rectView.Initialize(rectData, color, ratio);
        }
    }
}
