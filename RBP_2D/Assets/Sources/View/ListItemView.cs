using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ListItemView : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Toggle _toggle;
    [SerializeField] private TextMeshProUGUI _labelText;
    [SerializeField] private Image _iconImage;

    //[Header("Settings")]
    //[SerializeField] private bool _validateOnClick;

    private Action _toggledOnCallback;
    private Action _toggleOffCallback;

    private void OnDestroy()
    {
        if(_toggle && _toggle.group)
        {
            _toggle.group.UnregisterToggle(_toggle);
        }
    }

    public void Initialize(string label, Sprite iconSprite)
    {
        InitializeLabel(label);
        InitializeIcon(iconSprite);
    }

    public void InitializeLabel(string label)
    {
        _labelText.text = label;
        _labelText.gameObject.SetActive(true);
    }

    public void InitializeIcon(Sprite iconSprite)
    {
        _iconImage.sprite = iconSprite;
        _iconImage.gameObject.SetActive(true);
    }

    public void InitializeToggle(bool isOn, ToggleGroup toggleGroup)
    {
        _toggle.isOn = isOn;
        _toggle.group = toggleGroup;
        toggleGroup.RegisterToggle(_toggle);
    }

    public void FinalizeToggle()
    {
        if (_toggle.group)
        {
            _toggle.group.UnregisterToggle(_toggle);
            _toggle.group = null;
        }
    }

    public void RegisterValidatedCallback(Action toggledOnCallback = null, Action toggledOffCallback = null)
    {
        _toggledOnCallback += toggledOnCallback;
        _toggleOffCallback += toggledOffCallback;
    }

    public void ClearCallbacks()
    {
        _toggledOnCallback = null;
        _toggleOffCallback = null;
    }

    public void Validate()
    {
        if (_toggle.isOn)
        {
            if(_toggledOnCallback != null)
            {
                _toggledOnCallback.Invoke();
            }
        } else
        {
            if(_toggleOffCallback != null)
            {
                _toggleOffCallback.Invoke();
            }
        }
    }
}
