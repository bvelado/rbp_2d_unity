using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPageView : MonoBehaviour
{
    [SerializeField] private RectTransform _listContainer;
    [SerializeField] private SizeListItemView _sizeListItemViewPrefab;

    private List<SizeListItemView> _listItems;
    private Action<List<Vector2Int>> _onValidatedCallback;

    public void Open(List<Vector2Int> items, Action<List<Vector2Int>> onValidatedCallback = null)
    {
        if(_listItems == null)
        {
            _listItems = new List<SizeListItemView>();
        }

        _onValidatedCallback = onValidatedCallback;

        ClearList();
        GenerateList(items);
        Display();
    }

    private void ClearList()
    {
        // Registered items
        for (int i = _listItems.Count - 1; i >= 0; i--)
        {
            RemoveItem(_listItems[i]);
        }
    }

    public void AddNewItem()
    {
        var listItem = Instantiate(_sizeListItemViewPrefab, _listContainer);
        listItem.Initialize(new Vector2Int(), (item) => RemoveItem(item));

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
            _onValidatedCallback.Invoke(items);
        }

        Hide();
    }

    private void Display()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
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
