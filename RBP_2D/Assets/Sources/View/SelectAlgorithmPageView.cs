using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectAlgorithmPageView : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PageView _pageView;
    [SerializeField] private RectTransform _listContainer;
    [SerializeField] private ToggleGroup _toggleGroup;

    [Header("Content")]
    [SerializeField] private ListItemView _listItemViewPrefab;

    private Dictionary<MaxRectsBinPack.FreeRectChoiceHeuristic, ListItemView> _items = new Dictionary<MaxRectsBinPack.FreeRectChoiceHeuristic, ListItemView>();
    private Action<MaxRectsBinPack.FreeRectChoiceHeuristic> _onSelectionValidated;

    private void Awake()
    {
        if(_items == null)
        {
            _items = new Dictionary<MaxRectsBinPack.FreeRectChoiceHeuristic, ListItemView>();
        }
    }

    public void Open(MaxRectsBinPack.FreeRectChoiceHeuristic current, Action<MaxRectsBinPack.FreeRectChoiceHeuristic> settingsSelectedCallback)
    {
        _onSelectionValidated = settingsSelectedCallback;

        ClearListItems();
        GenerateListItems(current);
        Display();
    }

    private void Display()
    {
        _pageView.Open();
    }

    private void Hide()
    {
        _pageView.Close();
    }

    private void GenerateListItems(MaxRectsBinPack.FreeRectChoiceHeuristic current = (MaxRectsBinPack.FreeRectChoiceHeuristic)1)
    {
        for (int i = 0; i < (int)MaxRectsBinPack.FreeRectChoiceHeuristic.COUNT; i++)
        {
            var listItem = Instantiate(_listItemViewPrefab, _listContainer);

            listItem.InitializeToggle(i == (int)current, _toggleGroup);
            listItem.InitializeLabel(Enum.GetName(typeof(MaxRectsBinPack.FreeRectChoiceHeuristic), i));

            var selection = (MaxRectsBinPack.FreeRectChoiceHeuristic)i;
            listItem.RegisterValidatedCallback(() => Validate(selection), null);

            _items.Add((MaxRectsBinPack.FreeRectChoiceHeuristic)i,  listItem); 
        }

        Canvas.ForceUpdateCanvases();
    }

    private void ClearListItems()
    {
        for (int i = _items.Count - 1; i >= 0; i--)
        {
            var key = (MaxRectsBinPack.FreeRectChoiceHeuristic)i;
            _items[key].FinalizeToggle();
            DestroyImmediate(_items[key].gameObject);
            _items.Remove(key);
        }
    }

    public void Validate(MaxRectsBinPack.FreeRectChoiceHeuristic selection)
    {
        if(_onSelectionValidated != null)
        {
            _onSelectionValidated.Invoke(selection);
        }
    }

    public void Validate()
    {
        foreach(var t in _items)
        {
            t.Value.Validate();
        }
        Hide();
    }
}
