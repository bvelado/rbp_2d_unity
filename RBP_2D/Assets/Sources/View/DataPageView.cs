using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPageView : MonoBehaviour
{
    [SerializeField] private PageView _pageView;
    [SerializeField] private DataPageBinSizeInput _binSizeInput;
    [SerializeField] private RectTransform _listContainer;
    [SerializeField] private SizeListItemView _sizeListItemViewPrefab;

    private List<SizeListItemView> _listItems;

    public delegate void DataPageValidatedHandler(List<Vector2Int> items, Vector2Int binSize);
    private DataPageValidatedHandler _onValidatedCallback;

    public delegate void DataPageViewOpenHandler(DataPageView view, SettingsData settings);
    public event DataPageViewOpenHandler DataPageViewOpen;

    public void Open(SettingsData settings, DataPageValidatedHandler onValidatedCallback = null)
    {
        if(_listItems == null)
        {
            _listItems = new List<SizeListItemView>();
        }

        _binSizeInput.SetSize(settings.BinSize);

        _onValidatedCallback = onValidatedCallback;

        ClearList();
        GenerateList(settings.Items);
        Display();

        if (DataPageViewOpen != null)
        {
            DataPageViewOpen.Invoke(this, settings);
        }
    }

    private void ClearList()
    {
        // Registered items
        for (int i = _listItems.Count - 1; i >= 0; i--)
        {
            RemoveItem(_listItems[i]);
        }
    }

    public void AddNewItem(Vector2Int size = new Vector2Int())
    {
        var listItem = Instantiate(_sizeListItemViewPrefab, _listContainer);
        listItem.Initialize(size, (item) => RemoveItem(item));

        _listItems.Add(listItem);

        Canvas.ForceUpdateCanvases();
    }

    public void RemoveItem(SizeListItemView item)
    {
        if (_listItems.Contains(item))
        {
            _listItems.Remove(item);
            DestroyImmediate(item.gameObject);
        }

        Canvas.ForceUpdateCanvases();
    }

    public void Validate()
    {
        var items = new List<Vector2Int>();
        foreach(var i in _listItems)
        {
            items.Add(i.Size);
        }

        if(_onValidatedCallback != null)
        {
            _onValidatedCallback.Invoke(items, _binSizeInput.GetSize());
        }

        Hide();
    }

    private void Display()
    {
        _pageView.Open();
    }
    private void Hide()
    {
        _pageView.Close();
    }

    private void GenerateList(List<Vector2Int> rects)
    {

        foreach(var r in rects)
        {
            var listItem = Instantiate(_sizeListItemViewPrefab, _listContainer);
            listItem.Initialize(r, (item) => RemoveItem(item));

            _listItems.Add(listItem);
        }

        Canvas.ForceUpdateCanvases();
    }
}
