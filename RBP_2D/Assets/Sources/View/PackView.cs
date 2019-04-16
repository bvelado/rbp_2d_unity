using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PackView : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RectTransform _binContainer;
    [SerializeField] private TextMeshProUGUI _algorithmText;
    [SerializeField] private TextMeshProUGUI _numberOfPacksText;
    [SerializeField] private TextMeshProUGUI _averageOccupancyText;

    [Header("Content")]
    [SerializeField] private BinView _binViewPrefab;

    public void DisplayPack(PackData packData)
    {
        ClearBinsViews();

        _algorithmText.text = string.Format("Algorithm : {0}", packData.Algorithm); // Enum.GetName(typeof(MaxRectsBinPack.FreeRectChoiceHeuristic), packData.Algorithm));
        _numberOfPacksText.text = string.Format("Total packs : {0}", packData.Bins.Count());
        _averageOccupancyText.text = string.Format("Average occupancy : {0:0.##}%", packData.Bins.Sum(x => x.Occupancy) / packData.Bins.Count() * 100f);

        var bins = new Dictionary<BinView, BinData>();
        foreach(var binData in packData.Bins)
        {
            bins.Add(Instantiate(_binViewPrefab, _binContainer), binData);
        }

        Canvas.ForceUpdateCanvases();

        foreach(var kvp in bins)
        {
            kvp.Key.Initialize(kvp.Value);
        }

        Canvas.ForceUpdateCanvases();
    }

    private void InitializeBin(ref BinView binView, BinData binData)
    {
        binView.Initialize(binData);
    }

    private void ClearBinsViews()
    {
        for (int i = _binContainer.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(_binContainer.GetChild(i).gameObject);
        }
    }
}
