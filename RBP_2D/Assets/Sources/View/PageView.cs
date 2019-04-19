using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageView : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private string _openParameter = "Open";

    private bool _isOpen;

    public void Open()
    {
        _isOpen = true;

        UpdateView();
    }

    public void Close()
    {
        _isOpen = false;

        UpdateView();
    }

    private void UpdateView()
    {
        _animator.SetBool(_openParameter, _isOpen);
    }
}
