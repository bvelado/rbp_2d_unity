using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SizeListItemView : MonoBehaviour
{
    [SerializeField] private TMP_InputField _widthInputField;
    [SerializeField] private TMP_InputField _heightInputField;
    [SerializeField] private Button _deleteButton;
    public int Width { get; private set; }
    public int Height { get; private set; }
    public Vector2Int Size { get { return new Vector2Int(Width, Height); } }

    public void Initialize(Vector2Int size, Action<SizeListItemView> onDeleteCallback)
    {
        Width = size.x;
        Height = size.y;

        _widthInputField.text = Width.ToString();
        _heightInputField.text = Height.ToString();

        _widthInputField.onEndEdit.AddListener(OnWidthFieldEdited);
        _heightInputField.onEndEdit.AddListener(OnHeightFieldEdited);

        _deleteButton.onClick.AddListener(() => onDeleteCallback.Invoke(this));
    }

    private void OnWidthFieldEdited(string newWidth)
    {
        Width = int.Parse(newWidth);
    }

    private void OnHeightFieldEdited(string newHeight)
    {
        Height= int.Parse(newHeight);
    }


}
